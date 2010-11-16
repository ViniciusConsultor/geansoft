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
        private static Logger logger = LogManager.GetCurrentClassLogger();

        #region 属性与变量定义

        #region 标记

        /// <summary>
        /// 是否需要发送线程监控
        /// </summary>
        private bool _NeedSendMonitor = true;
        /// <summary>
        /// 关闭本连接的发送线程监控
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
        private string _ClientId = string.Empty;

        /// <summary>
        /// 供多线程加锁服务
        /// </summary>
        private object _Lock = new object();

        /// <summary>
        /// 核心的Socket客户端对象
        /// </summary>
        private TcpClient _SocketClient;

        /// <summary>
        /// 协议消息池
        /// </summary>
        public KeepConnectionProtocolPool MessagePool { get { return _MessagePool; } }
        private KeepConnectionProtocolPool _MessagePool = null;

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

        #endregion

        #region 单件实例

        /// <summary>
        /// 构造函数
        /// </summary>
        private KeepSocket()
        {
        }

        /// <summary>
        /// 初始化本类型。在初始化之前会启动本类型的单建模式。
        /// </summary>
        /// <returns></returns>
        public static KeepSocket InitializeComponent()
        {
            KeepSocket me = KeepSocket.ME;

            me._ClientId = UtilityHardware.GetCpuID();
            me._MessagePool = new KeepConnectionProtocolPool();
            me.InitThread();
            logger.Trace("消息池编号:" + me.MessagePool.ID);

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
            logger.Debug("Socket心跳线程启动");

            Thread SendWorkThread = new Thread(new ThreadStart(SendThread));
            SendWorkThread.IsBackground = true;
            SendWorkThread.Start();
            logger.Debug("发送线程启动");

            Thread ReceiveWorkThread = new Thread(new ThreadStart(ReceiveThread));
            ReceiveWorkThread.IsBackground = true;
            ReceiveWorkThread.Start();
            logger.Debug("接收线程启动");
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
                logger.Error("关闭SocketConnection实例时异常。异常信息：" + e.Message);
            }
        }

        /// <summary>
        /// 返回当前长连接的连接状态
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected
        {
            get { return !(_SocketClient == null || _SocketClient.Connected == false); }
        }

        #endregion

        #region 发送线程

        private void SendThread()
        {
            while (_NeedSendMonitor)
            {
                string command = "";
                try
                {
                    command = _MessagePool.DequeueSendingMessage();
                    if (!string.IsNullOrEmpty(command))
                        this.SendDatagram(command);
                }
                catch (Exception e)
                {
                    logger.Warn(string.Format("KeepSocket发送报文失败。报文:{0}。异常信息:{1}", command, e.Message), e);
                }
                Thread.Sleep(60);
            }
        }

        /// <summary>
        /// 发送数据的具体执行函数
        /// </summary>
        /// <param name="datagramText">The datagram text.</param>
        private void SendDatagram(string datagramText)
        {
            if (!IsConnected)
                this.Connect();
            byte[] datagram = Encoding.Default.GetBytes(datagramText);
            try
            {
                _SocketClient.Client.Send(datagram);
                logger.Trace(string.Format("发送消息:{0}", datagramText));
            }
            catch (Exception e)
            {
                logger.Error("Socket发送异常。发送内容:" + datagramText + ",异常信息:" + e.Message);
                this.SafeClose();
            }
        }

        #endregion

        #region 接收线程

        private byte[] _ReceiveByteArray;
        private SplitBytes _SplitByte;
        /// <summary>
        /// 正在接收
        /// </summary>
        private bool _OnReceive = false;

        /// <summary>
        /// 接收线程
        /// </summary>
        protected void ReceiveThread()
        {
            while (_NeedSendMonitor)
            {
                if (!IsConnected)
                    Thread.Sleep(1000);
                else
                {
                    if (_OnReceive)
                        Thread.Sleep(500);
                    else
                        this.ReceiveDatagram();
                }
            }
        }

        private void ReceiveDatagram()
        {
            _SplitByte = new SplitBytes();
            _ReceiveByteArray = new byte[2 * 1024];
            try
            {
                if (!_OnReceive)
                {
                    _OnReceive = true;
                    if (true)
                    {
                        AsyncCallback receiveCallback = new AsyncCallback(this.AsyncGetMessage);
                        _SocketClient.GetStream().BeginRead(_ReceiveByteArray, 0, 1024, receiveCallback, null);
                    }
                    else
                    {
                        this.SynchroGetMessage();
                    }
                }
            }
            catch (Exception e)
            {
                logger.Warn("ReceiveDatagram时发生异常.异常信息:" + e.Message, e);
                _OnReceive = false;
            }
        }

        /// <summary>
        /// 【异步】从Socket的输入流中获取消息
        /// </summary>
        /// <param name="ar">The ar.</param>
        private void AsyncGetMessage(IAsyncResult ar)
        {
            int numberOfBytesRead;
            bool isFinish = true;

            try
            {
                lock (_SocketClient.GetStream())
                {
                    numberOfBytesRead = _SocketClient.GetStream().EndRead(ar);
                    if (numberOfBytesRead < 1)
                    {
                        this.Close();
                        _OnReceive = false;
                        return;
                    }
                }
                _SplitByte.AddBytes(_ReceiveByteArray, numberOfBytesRead);
            }
            catch (Exception e)
            {
                logger.Warn("AsyncGetMessage时发生异常(0).异常信息:" + e.Message, e);
            }

            try
            {
                if (_SocketClient.GetStream().DataAvailable)
                {
                    isFinish = false;
                    _SocketClient.GetStream().BeginRead(_ReceiveByteArray, 0, _ReceiveByteArray.Length, new AsyncCallback(AsyncGetMessage), _SocketClient.GetStream());
                }
            }
            catch (Exception e)
            {
                logger.Warn("AsyncGetMessage时发生异常(1).异常信息:" + e.Message, e);
            }

            try
            {
                if (isFinish)
                {
                    string replyMesage = Encoding.Default.GetString(_SplitByte.ReceiveAllByte, 0, _SplitByte.ReceiveAllByte.Length);
                    _MessagePool.EnqueueReceivingMessage(replyMesage);
                    logger.Trace(string.Format("收到消息:{0}", replyMesage));
                }
            }
            catch (Exception e)
            {
                logger.Warn("AsyncGetMessage时发生异常(2).异常信息:" + e.Message, e);
                this.SafeClose();
                this.Connect();
            }
            try
            {
                _SplitByte.Dispose();
                _OnReceive = false;
            }
            catch (Exception e)
            {
                logger.Warn("AsyncGetMessage时发生异常(3).异常信息:" + e.Message, e);
                this.SafeClose();
                this.Connect();
            }
        }

        /// <summary>
        /// 【同步】从Socket的输入流中获取消息
        /// </summary>
        private void SynchroGetMessage()
        {
            _OnReceive = true;
            lock (_SocketClient.GetStream())
            {
                int size = _SocketClient.Client.Receive(_ReceiveByteArray);
                if (size < 1)
                {
                    this.Close();
                    _OnReceive = false;
                    return;
                }
                else
                {
                    _SplitByte.AddBytes(_ReceiveByteArray, size);
                    string replyMesage = Encoding.Default.GetString(_SplitByte.ReceiveAllByte, 0, _SplitByte.ReceiveAllByte.Length);
                    _MessagePool.EnqueueReceivingMessage(replyMesage);
                    logger.Trace(string.Format("收到消息:{0}", replyMesage));
                    _OnReceive = false;
                }
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
                        logger.Info(string.Format("Socket连接成功。ServerIP:{0}, ServerPort:{1}", QServerIP, QServerPort));
                    else
                        logger.Warn(string.Format("Socket连接失败。ServerIP:{0}, ServerPort:{1}", QServerIP, QServerPort));
                }
                catch (Exception e)
                {
                    logger.Error(string.Format("Socket连接导常。ServerIP:{0},ServerPort:{1}异常信息:{2}", QServerIP, QServerPort, e.Message));
                }
            }
        }

        /// <summary>
        /// 关闭连接。销毁连接的实例。如需安全关闭，并打算销毁连接的实例，请调用SafeClose()。
        /// </summary>
        public void Close()
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
                    logger.Debug("Socket关闭连接成功");
                }
                catch (Exception e)
                {
                    logger.Debug("Socket关闭连接导常。异常信息:" + e.Message);
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
                    logger.Debug("Socket关闭连接导常。异常信息:" + e.Message);
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
            if (_MessagePool.SendingQueueCount == 0)
                //将连接状态校验命令字加入发送队列
                _MessagePool.EnqueueSendingMessage(Protocols.VerifyConnectionStatus(_ClientId));
        }

        #endregion
    }
}
