using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Net.Sockets;
using NLog;
using Gean.Net.ProtocolWrapper;

namespace Gean.Net.KeepSocket
{
    /// <summary>
    /// 长连接Socket服务
    /// </summary>
    public class KeepSocket
    {
        private static Logger _Logger = LogManager.GetCurrentClassLogger();

        #region 属性与变量定义

        #region 标记

        /// <summary>
        /// 是否需要发送线程监控
        /// </summary>
        public bool _NeedSendMonitor = true;
        /// <summary>
        /// 关闭本连接的心跳监控
        /// </summary>
        public void CloseSendMonitor()
        {
            _NeedSendMonitor = false;
        }

        /// <summary>
        /// 是否需要心跳监控
        /// </summary>
        private bool _NeedHeartMonitor = true;
        /// <summary>
        /// 关闭本连接的心跳监控
        /// </summary>
        public void CloseHeartMonitor()
        {
            _NeedHeartMonitor = false;
        }

        #endregion

        /// <summary>
        /// 本应用程序连接器Id
        /// </summary>
        public string _ClientId = string.Empty;

        /// <summary>
        /// 供多线程加锁服务
        /// </summary>
        private object _Lock = new object();

        /// <summary>
        /// 核心的Socket客户端对象
        /// </summary>
        private TcpClient _SocketClient;

        /// <summary>
        /// 接收缓冲区
        /// </summary>
        private byte[] _ReceiveBuffer = new byte[2048 * 1024];

        private Queue<string> _CommandQueue;

        public string QServerIP
        {
            get { return _QServerIP; }
        }
        private string _QServerIP = "127.0.0.1";

        public string QServerPort
        {
            get { return _QServerPort; }
        }
        private string _QServerPort = "8000";

        private Queue<string> _ResultQueue;

        #endregion

        #region 单件实例

        /// <summary>
        /// 构造函数
        /// </summary>
        private KeepSocket()
        {
            //this.InitializeComponent();
        }

        /// <summary>
        /// 初始化本类型。在初始化之前会启动本类型的单建模式。
        /// </summary>
        /// <returns></returns>
        public static KeepSocket InitializeComponent()
        {
            KeepSocket me = KeepSocket.ME;

            me._ClientId = UtilityHardware.GetCpuID();
            me._ResultQueue = new Queue<string>();
            me._CommandQueue = new Queue<string>();
            me.InitThread();

            return me;
        }

        /// <summary>
        /// 启动核心的三个线程
        /// </summary>
        protected void InitThread()
        {
            Thread HeartBeatWorkThread = new Thread(new ThreadStart(HeartBeatThread));
            HeartBeatWorkThread.IsBackground = true;
            HeartBeatWorkThread.Start();
            _Logger.Debug("Socket心跳线程启动");

            Thread SendWorkThread = new Thread(new ThreadStart(SendThread));
            SendWorkThread.IsBackground = true;
            SendWorkThread.Start();
            _Logger.Debug("发送线程启动");

            Thread ReceiveWorkThread = new Thread(new ThreadStart(ReceiveThread));
            ReceiveWorkThread.IsBackground = true;
            ReceiveWorkThread.Start();
            _Logger.Debug("接收线程启动");
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static KeepSocket ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton()
            {
                Instance = new KeepSocket();
            }

            internal static readonly KeepSocket Instance = null;
        }

        ~KeepSocket()
        {
            try
            {
                this.Close();
            }
            catch (Exception e)
            {
                _Logger.Error("关闭SocketConnection实例时异常。异常信息：" + e.Message);
            }
        }

        #endregion

        #region 发送线程

        private void SendThread()
        {
            while (_NeedSendMonitor)
            {
                SendCheck();
                Thread.Sleep(100);
            }
        }

        private void SendCheck()
        {
            try
            {
                string CmdToSend;
                lock (_CommandQueue)
                {
                    if (_CommandQueue.Count > 0)
                    {
                        CmdToSend = _CommandQueue.Dequeue();

                        if (CmdToSend.Length > 0)
                        {
                            SendDatagram(CmdToSend);
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public bool IsConnected()
        {
            if (_SocketClient == null || _SocketClient.Connected == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SendDatagram(string msg)
        {
            if (_SocketClient == null || _SocketClient.Connected == false)
            {
                this.Connect();
            }
            this.SendOneDatagram(msg);
        }

        private void SendOneDatagram(string datagramText)
        {
            byte[] datagram = Encoding.Default.GetBytes(datagramText);
            try
            {
                _SocketClient.Client.Send(datagram);
            }
            catch (Exception e)
            {
                _Logger.Error("Socket发送异常。发送内容:" + datagramText + ",异常信息:" + e.Message);
                this.SafeClose();
            }
        }

        #endregion

        #region 接收线程

        private byte[] recByte;
        private SplitBytes sb;

        private bool OnReceive = false;

        /// <summary>
        /// 接收线程
        /// </summary>
        protected void ReceiveThread()
        {
            sb = new SplitBytes();
            recByte = new byte[1024];

            while (_NeedSendMonitor)
            {
                if (_SocketClient == null || _SocketClient.Connected == false)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    if (OnReceive)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        CheckReceive();
                    }
                }
            }
        }

        private void CheckReceive()
        {
            try
            {
                AsyncCallback GetMsgCallback = new AsyncCallback(GetMsg);
                OnReceive = true;
                _SocketClient.GetStream().BeginRead(recByte, 0, 1024, GetMsgCallback, null);
            }
            catch
            {
                OnReceive = false;
            }
        }

        private void GetMsg(IAsyncResult ar)
        {
            try
            {
                int numberOfBytesRead;
                bool isFinish = true;

                lock (_SocketClient.GetStream())
                {
                    numberOfBytesRead = _SocketClient.GetStream().EndRead(ar);

                    if (numberOfBytesRead < 1)
                    {
                        Close();
                        OnReceive = false;
                        return;
                    }
                }

                sb.AddBytes(recByte, numberOfBytesRead);
                recByte = new byte[1024];

                if (_SocketClient.GetStream().DataAvailable)
                {
                    isFinish = false;
                    _SocketClient.GetStream().BeginRead(recByte, 0, recByte.Length, new AsyncCallback(GetMsg), _SocketClient.GetStream());
                }

                if (isFinish)
                {
                    string replyMesage = Encoding.Default.GetString(sb.ReceiveAllByte, 0, sb.ReceiveAllByte.Length);
                    ProcessProtocolMessage(replyMesage);

                    sb.Dispose();
                    OnReceive = false;
                }
            }
            catch
            {
                sb.Dispose();
                OnReceive = false;
                this.SafeClose();
            }
        }
        
        #endregion

        #region 通讯（连接与关闭）

        /// <summary>
        /// 通讯连接
        /// </summary>
        private void Connect()
        {
            lock (_Lock)
            {
                try
                {
                    _SocketClient = new TcpClient(QServerIP, int.Parse(QServerPort));
                    //一次接收的延时
                    _SocketClient.ReceiveTimeout = 2 * 1000;

                    if (_SocketClient.Connected)
                    {
                        _Logger.Debug("Socket连接成功.");
                    }
                    else
                    {
                        _Logger.Debug("Socket连接失败.");
                    }
                }
                catch (Exception e)
                {
                    _Logger.Debug("Socket连接导常。异常信息:" + e.Message);
                }
            }
        }

        /// <summary>
        /// 关闭连接。销毁连接的实例。如需安全关闭，并打算销毁连接的实例，请调用SafeClose()。
        /// </summary>
        private void Close()
        {
            lock (_Lock)
            {
                if (_SocketClient == null)
                {
                    return;
                }
                try
                {
                    _SocketClient.Close();
                    _Logger.Debug("Socket关闭连接成功");
                }
                catch (Exception e)
                {
                    _Logger.Debug("Socket关闭连接导常。异常信息:" + e.Message);
                }
                finally
                {
                    _SocketClient = null;
                }
            }
        }

        /// <summary>
        /// 安全关闭连接。先终止接收与发送，再关闭连接，但不销毁连接的实例。
        /// </summary>
        private void SafeClose()
        {
            lock (_Lock)
            {
                if (_SocketClient == null)
                {
                    return;
                }
                try
                {
                    _SocketClient.Client.Shutdown(SocketShutdown.Both);
                    _SocketClient.Client.Close();
                }
                catch (Exception e)
                {
                    _Logger.Debug("Socket关闭连接导常。异常信息:" + e.Message);
                }
            }
        }

        #endregion

        #region 心跳线程

        private void HeartBeatThread()
        {
            while (_NeedHeartMonitor)
            {
                HeartBeat();
                //每分钟心跳一次，检查连接状态
                Thread.Sleep(60 * 1000);
            }
        }

        public void HeartBeat()
        {
            lock (_CommandQueue)
            {
                if (_CommandQueue.Count == 0)
                {
                    //将连接状态校验命令字加入发送队列
                    _CommandQueue.Enqueue(Protocols.VerifyConnectionStatus(_ClientId));
                }
            }
        }

        #endregion

        protected void ProcessProtocolMessage(string msg)
        {
            _Logger.Debug("收到消息：" + msg);
            string protocolMessage = msg;
            string localMsg = protocolMessage;
            string[] msgArray = localMsg.Split('@');
            if (msgArray.Length > 1)
            {
                for (int i = 0; i < msgArray.Length - 1; i++)
                {
                    lock (_ResultQueue)
                    {
                        _ResultQueue.Enqueue(msgArray[i] + "@");
                    }
                }
            }
            protocolMessage = msgArray[msgArray.Length - 1];
        }

        public void AddCmd(string cmd)
        {
            lock (_CommandQueue)
            {
                _Logger.Debug(cmd);
                _CommandQueue.Enqueue(cmd);
            }
        }

        public string GetCmd()
        {
            string result = string.Empty;
            lock (_ResultQueue)
            {
                if (_ResultQueue.Count > 0)
                {
                    result = _ResultQueue.Dequeue();
                }
            }
            return result;
        }

    }
}
