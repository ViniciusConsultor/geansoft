using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Gean.Net.Messages;
using NLog;
using Gean.Net.MessagePools;

namespace Gean.Net.KeepSocket
{
    /// <summary>
    /// 长连接Socket服务
    /// </summary>
    public class KeepSocketClient
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static Dictionary<string, KeepSocketClient> _ClientMap = new Dictionary<string, KeepSocketClient>();
        public static Dictionary<string, KeepSocketClient> ClientMap
        {
            get { return _ClientMap; }
        }

        #region 属性与变量定义

        #region 标记

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

        public string ID { get { return UtilityGuid.Get(); } }

        public override int GetHashCode()
        {
            int prime = 31;
            int nullCode = -71;
            int result = 1;
            result = prime * result + ((this.ID == null) ? nullCode : this.ID.GetHashCode());
            return result;
        }
        public override bool Equals(object obj)
        {
            return this.ID.Equals(((KeepSocketClient)obj).ID);
        }

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
        public MessagePool MessagePool { get { return _MessagePool; } }
        private MessagePool _MessagePool = null;

        #endregion

        #region 单件实例

        /// <summary>
        /// 构造函数
        /// </summary>
        public KeepSocketClient(string id)
        {
            if (_ClientMap.ContainsKey(id))
            {
                logger.Fatal("长连接服务 ID 已存在，程序将不可使用，请联系管理员重新配置程序。");
                return;
            }
            _ClientId = id;
            _MessagePool = new AsyncMessagePool();
            _ClientMap.Add(id, this);
        }

        /// <summary>
        /// 初始化本类型。在初始化之前会启动本类型的单建模式。
        /// </summary>
        /// <returns></returns>
        public KeepSocketClient InitializeComponent()
        {
            this.InitThread();
            logger.Info("长连接ID:" + this.ID);
            logger.Info("长连接客户端编号:" + this._ClientId);
            logger.Info("消息池编号:" + this.MessagePool.ID);
            return this;
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

        ~KeepSocketClient()
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
            while (true)
            {
                string command = "";
                try
                {
                    command = _MessagePool.DequeueSending();
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
        private bool _WhileReceiving = false;

        /// <summary>
        /// 接收线程
        /// </summary>
        protected void ReceiveThread()
        {
            while (true)
            {
                if (!IsConnected)
                {
                    Thread.Sleep(1000);//休息1秒再进行尝试，由心跳去控制再次连接
                }
                else
                {
                    if (_WhileReceiving)//正在接收时
                        Thread.Sleep(200);
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
                if (!_WhileReceiving)
                {
                    _WhileReceiving = true;
                    if (KeepSocketOption.ME.IsAsync)//选择接收方式
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
                _WhileReceiving = false;
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
                        _WhileReceiving = false;
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
                    string endChar = KeepSocketOption.ME.EndChar;
                    string replyMesage = Encoding.Default.GetString(_SplitByte.ReceiveAllByte, 0, _SplitByte.ReceiveAllByte.Length);
                    string[] msgs = null;
                    if (endChar.Length > 1)
                    {
                        msgs = replyMesage.Split(new string[] { endChar }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    else if (endChar.Length == 1)
                    {
                        msgs = replyMesage.Split(new char[] { endChar[0] }, StringSplitOptions.RemoveEmptyEntries);
                    }
                    for (int i = 0; i < msgs.Length; i++)
                    {
                        _MessagePool.EnqueueReceiving(msgs[i]);
                        logger.Trace(string.Format("收到消息:{0}", msgs[i]));
                    }
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
                _WhileReceiving = false;
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
            _WhileReceiving = true;
            lock (_SocketClient.GetStream())
            {
                int size = _SocketClient.Client.Receive(_ReceiveByteArray);
                if (size < 1)
                {
                    this.Close();
                    _WhileReceiving = false;
                    return;
                }
                else
                {
                    _SplitByte.AddBytes(_ReceiveByteArray, size);
                    string replyMesage = Encoding.Default.GetString(_SplitByte.ReceiveAllByte, 0, _SplitByte.ReceiveAllByte.Length);
                    _MessagePool.EnqueueReceiving(replyMesage);
                    logger.Trace(string.Format("收到消息:{0}", replyMesage));
                    _WhileReceiving = false;
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
                    _SocketClient = new TcpClient(KeepSocketOption.ME.IPAddress, KeepSocketOption.ME.Port);
                    _SocketClient.ReceiveTimeout = 2 * 1000;//一次接收的延时

                    if (_SocketClient.Connected)
                    {
                        logger.Info(string.Format("Socket连接成功。ServerIP:{0}, ServerPort:{1}", KeepSocketOption.ME.IPAddress, KeepSocketOption.ME.Port));
                        OnKeepSocketStatusChanged(new KeepSocketStatusChangedEventArgs(ConnectionStatus.Normal));
                    }
                    else
                    {
                        OnKeepSocketStatusChanged(new KeepSocketStatusChangedEventArgs(ConnectionStatus.Break));
                        logger.Warn(string.Format("Socket连接失败。ServerIP:{0}, ServerPort:{1}", KeepSocketOption.ME.IPAddress, KeepSocketOption.ME.Port));
                    }
                }
                catch (Exception e)
                {
                    OnKeepSocketStatusChanged(new KeepSocketStatusChangedEventArgs(ConnectionStatus.Break));
                    logger.Error(string.Format("Socket连接异常。ServerIP:{0},ServerPort:{1}异常信息:{2}", KeepSocketOption.ME.IPAddress, KeepSocketOption.ME.Port, e.Message));
                }
            }
        }

        /// <summary>
        /// 关闭连接。销毁连接的实例。
        /// 如需安全关闭，并且不打算销毁连接的实例时，请调用<see cref="SafeClose"/>
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
                    OnKeepSocketStatusChanged(new KeepSocketStatusChangedEventArgs(ConnectionStatus.Break));
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
                    OnKeepSocketStatusChanged(new KeepSocketStatusChangedEventArgs(ConnectionStatus.Break));
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
                Thread.Sleep(KeepSocketOption.ME.HeartRange * 1000);
            }
        }

        public void HeartBeat()
        {
            //将连接状态校验命令字加入发送队列
            if (_MessagePool.SendingQueueCount == 0)
            {
                string verifyConn = string.Format(KeepSocketOption.ME.VerifyConnCommandString, 11, _ClientId);
                _MessagePool.EnqueueSending(verifyConn);
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// Socket长连接断开时发生的事件
        /// </summary>
        public event KeepSocketStatusChangedEventHandler KeepSocketStatusChangedEvent;
        protected virtual void OnKeepSocketStatusChanged(KeepSocketStatusChangedEventArgs e)
        {
            if (KeepSocketStatusChangedEvent != null)
                KeepSocketStatusChangedEvent(this, e);
        }
        public delegate void KeepSocketStatusChangedEventHandler(Object sender, KeepSocketStatusChangedEventArgs e);

        /// <summary>
        /// Socket长连接断开时发生的事件包含数据的类
        /// </summary>
        public class KeepSocketStatusChangedEventArgs : EventArgs
        {
            public ConnectionStatus Status { get; private set; }
            public KeepSocketStatusChangedEventArgs(ConnectionStatus status)
            {
                this.Status = status;
            }
        }

        #endregion
    }
}
