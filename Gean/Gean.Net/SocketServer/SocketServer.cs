using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Gean.Net.CSUST.Net;

namespace Gean.Net
{
    /// <summary>
    /// 一个Socket会话服务器。
    /// (是对EMTASS(一个开源的可扩展多线程异步Socket服务器框架)<see cref="TSocketServerBase"/>的易用性封装。(单件模式))
    /// </summary>
    public sealed class SocketServer
    {
        /// <summary>
        /// 将被使用的静态实例。
        /// </summary>
        static readonly SocketServer _instance = new SocketServer();
        /// <summary>
        /// 静态构造方法。
        /// </summary>
        static SocketServer() { }
        /// <summary>
        /// <see cref="SocketServer"/>的单件实例
        /// </summary>
        /// <value><see cref="SocketServer"/>的单件实例</value>
        public static SocketServer Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 本类型所包含的一个私有EMTASS服务，对一些常用方法与事件进行封装，一些不常用的私有化。组合。
        /// </summary>
        private TSocketServerBase<SessionImplement, DataStore> _socketServer;

        /// <summary>
        /// 私有构造函数
        /// </summary>
        private SocketServer() 
        {
            _socketServer = new TSocketServerBase<SessionImplement, DataStore>();
            this.AttachServerEvent();
        }

        /// <summary>
        /// 启动Socket服务。
        /// </summary>
        public void Start()
        {
            string connStr = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source = DemoAccessDatabase.mdb;";

            //if (ck_UseDatabase.Checked)
            //{
            //    m_socketServer = new TSocketServerBase<TTestSession, TTestAccessDatabase>(1024, 32 * 1024, 64 * 1024, connStr);
            //}
            //else
            //{
            //    m_socketServer = new TSocketServerBase<TTestSession, TTestAccessDatabase>();
            //}

            _socketServer.MaxDatagramSize = 1024 * int.Parse("22");

            _socketServer.Start();
        }
        /// <summary>
        /// 停止Socket服务（并释放资源）。
        /// </summary>
        public void Stop()
        {
            if (_socketServer != null)
            {
                _socketServer.Stop();
                _socketServer.Dispose();
            }

        }
        /// <summary>
        /// 暂停Socket服务。
        /// </summary>
        public void Pause()
        {
            if (_socketServer != null)
            {
                _socketServer.PauseListen();
            }
        }
        /// <summary>
        /// 重新启动Socket服务。
        /// </summary>
        public void Resume()
        {
            if (_socketServer != null)
            {
                _socketServer.ResumeListen();
            }
        }

        /// <summary>
        /// 关闭指定ID的会话
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        public void CloseSession(int sessionId)
        {
            _socketServer.CloseSession(sessionId);
        }
        /// <summary>
        /// 关闭所有的会话
        /// </summary>
        public void CloseAllSessions()
        {
            _socketServer.CloseAllSessions();
        }

        /// <summary>
        /// 发送数据给指定的会话
        /// </summary>
        /// <param name="sessionId">会话ID</param>
        /// <param name="datagramText">要发送的数据</param>
        public void SendToSession(int sessionId, string datagramText)
        {
            _socketServer.SendToSession(sessionId, datagramText);
        }
        /// <summary>
        /// 发送数据给[所有]的会话
        /// </summary>
        /// <param name="datagramText">要发送的数据</param>
        public void SendToAllSessions(string datagramText)
        {
            _socketServer.SendToAllSessions(datagramText);
        }

        #region 事件处理

        /// <summary>
        /// 附加与调度常用的事件。
        /// </summary>
        private void AttachServerEvent()
        {
            _socketServer.ServerStarted         += this.SocketServer_Started;
            _socketServer.ServerClosed          += this.SocketServer_Stoped;
            _socketServer.ServerListenPaused    += this.SocketServer_Paused;
            _socketServer.ServerListenResumed   += this.SocketServer_Resumed;
            _socketServer.ServerException       += this.SocketServer_Exception;

            _socketServer.SessionRejected       += this.SocketServer_SessionRejected;
            _socketServer.SessionConnected      += this.SocketServer_SessionConnected;
            _socketServer.SessionDisconnected   += this.SocketServer_SessionDisconnected;
            _socketServer.SessionReceiveException += this.SocketServer_SessionReceiveException;
            _socketServer.SessionSendException  += this.SocketServer_SessionSendException;

            _socketServer.DatagramDelimiterError += this.SocketServer_DatagramDelimiterError;
            _socketServer.DatagramOversizeError += this.SocketServer_DatagramOversizeError;
            _socketServer.DatagramAccepted      += this.SocketServer_DatagramReceived;
            _socketServer.DatagramError         += this.SocketServer_DatagramrError;
            _socketServer.DatagramHandled       += this.SocketServer_DatagramHandled;

            _socketServer.DatabaseOpenException += this.SocketServer_DatabaseOpenException;
            _socketServer.DatabaseCloseException += this.SocketServer_DatabaseCloseException;
            _socketServer.DatabaseException     += this.SocketServer_DatabaseException;

            _socketServer.ShowDebugMessage      += this.SocketServer_ShowDebugMessage;
        }

        private void SocketServer_Started(object sender, EventArgs e)
        {
            this.AddInfo("Server started at: " + DateTime.Now.ToString());           
        }

        private void SocketServer_Stoped(object sender, EventArgs e)
        {
            this.AddInfo("Server stoped at: " + DateTime.Now.ToString());
        }

        private void SocketServer_Paused(object sender, EventArgs e)
        {
            this.AddInfo("Server paused at: " + DateTime.Now.ToString());
        }

        private void SocketServer_Resumed(object sender, EventArgs e)
        {
            this.AddInfo("Server resumed at: " + DateTime.Now.ToString());
        }

        private void SocketServer_Exception(object sender, TExceptionEventArgs e)
        {
            //this.tb_ServerExceptionCount.Text = _socketServer.ServerExceptionCount.ToString();
            this.AddInfo("Server exception: " + e.ExceptionMessage);
        }

        private void SocketServer_SessionRejected(object sender, EventArgs e)
        {
            this.AddInfo("Session connect rejected");
        }

        private void SocketServer_SessionTimeout(object sender, TSessionEventArgs e)
        {
            this.AddInfo("Session timeout: ip " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_SessionConnected(object sender, TSessionEventArgs e)
        {
            //this.tb_SessionCount.Text = _socketServer.SessionCount.ToString();
            this.AddInfo("Session connected: ip " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_SessionDisconnected(object sender, TSessionEventArgs e)
        {
            //this.tb_SessionCount.Text = _socketServer.SessionCount.ToString();
            this.AddInfo("Session disconnected: ip " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_SessionReceiveException(object sender, TSessionEventArgs e)
        {
            //this.tb_SessionCount.Text = _socketServer.SessionCount.ToString();
            //this.tb_ClientExceptionCount.Text = _socketServer.SessionExceptionCount.ToString();
            this.AddInfo("Session receive exception: ip " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_SessionSendException(object sender, TSessionEventArgs e)
        {
            //this.tb_SessionCount.Text = _socketServer.SessionCount.ToString();
            //this.tb_ClientExceptionCount.Text = _socketServer.SessionExceptionCount.ToString();
            this.AddInfo("Session send exception: ip " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_SocketReceiveException(object sender, TSessionExceptionEventArgs e)
        {
            //this.tb_SessionCount.Text = _socketServer.SessionCount.ToString();
            //this.tb_ClientExceptionCount.Text = _socketServer.SessionExceptionCount.ToString();
            this.AddInfo("client socket receive exception: ip: " + e.SessionBaseInfo.IP + " exception message: " + e.ExceptionMessage);
        }

        private void SocketServer_SocketSendException(object sender, TSessionExceptionEventArgs e)
        {
            //this.tb_SessionCount.Text = _socketServer.SessionCount.ToString();
            //this.tb_ClientExceptionCount.Text = _socketServer.SessionExceptionCount.ToString();
            this.AddInfo("client socket send exception: ip: " + e.SessionBaseInfo.IP + " exception message: " + e.ExceptionMessage);
        }

        private void SocketServer_DatagramDelimiterError(object sender, TSessionEventArgs e)
        {
            //this.tb_DatagramCount.Text = _socketServer.ReceivedDatagramCount.ToString();
            //this.tb_DatagramQueueCount.Text = _socketServer.DatagramQueueLength.ToString();
            //this.tb_ErrorDatagramCount.Text = _socketServer.ErrorDatagramCount.ToString();

            this.AddInfo("datagram delimiter error. ip: " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_DatagramOversizeError(object sender, TSessionEventArgs e)
        {
            //this.tb_DatagramCount.Text = _socketServer.ReceivedDatagramCount.ToString();
            //this.tb_DatagramQueueCount.Text = _socketServer.DatagramQueueLength.ToString();
            //this.tb_ErrorDatagramCount.Text = _socketServer.ErrorDatagramCount.ToString();

            this.AddInfo("datagram oversize error. ip: " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_DatagramReceived(object sender, TSessionEventArgs e)
        {
            //this.tb_DatagramCount.Text = _socketServer.ReceivedDatagramCount.ToString();
            //this.tb_DatagramQueueCount.Text = _socketServer.DatagramQueueLength.ToString();
            this.AddInfo("datagram received. ip: " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_DatagramrError(object sender, TSessionEventArgs e)
        {
            //this.tb_DatagramCount.Text = _socketServer.ReceivedDatagramCount.ToString();
            //this.tb_DatagramQueueCount.Text = _socketServer.DatagramQueueLength.ToString();
            //this.tb_ErrorDatagramCount.Text = _socketServer.ErrorDatagramCount.ToString();

            this.AddInfo("datagram error. ip: " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_DatagramHandled(object sender, TSessionEventArgs e)
        {
            //this.tb_DatagramCount.Text = _socketServer.ReceivedDatagramCount.ToString();
            //this.tb_DatagramQueueCount.Text = _socketServer.DatagramQueueLength.ToString();
            this.AddInfo("datagram handled. ip: " + e.SessionBaseInfo.IP);
        }

        private void SocketServer_DatabaseOpenException(object sender, TExceptionEventArgs e)
        {
            //this.tb_DatabaseExceptionCount.Text = _socketServer.DatabaseExceptionCount.ToString();
            this.AddInfo("open database exception: " + e.ExceptionMessage);
        }

        private void SocketServer_DatabaseCloseException(object sender, TExceptionEventArgs e)
        {
            //this.tb_DatabaseExceptionCount.Text = _socketServer.DatabaseExceptionCount.ToString();
            this.AddInfo("close database exception: " + e.ExceptionMessage);
        }

        private void SocketServer_DatabaseException(object sender, TExceptionEventArgs e)
        {
            //this.tb_DatabaseExceptionCount.Text = _socketServer.DatabaseExceptionCount.ToString();
            this.AddInfo("operate database exception: " + e.ExceptionMessage);
        }

        private void SocketServer_ShowDebugMessage(object sender, TExceptionEventArgs e)
        {
            this.AddInfo("debug message: " + e.ExceptionMessage);
        }

        private void AddInfo(string message)
        {
            //if (lb_ServerInfo.Items.Count > 1000)
            //{
            //    lb_ServerInfo.Items.Clear();
            //}

            //lb_ServerInfo.Items.Add(message);
            //lb_ServerInfo.SelectedIndex = lb_ServerInfo.Items.Count - 1;
            //lb_ServerInfo.Focus();
        }

        #endregion

        internal class ServerInfo
        {
            public ServerInfo()
            {

            }
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="maxSessionCount">最多会话数量.</param>
            /// <param name="receiveBufferSize">接收缓存大小.</param>
            /// <param name="sendBufferSize">发送区缓存大小.</param>
            public ServerInfo(uint maxSessionCount, uint receiveBufferSize, uint sendBufferSize)
            {
                _MaxSessionCount = maxSessionCount;
                _ReceiveBufferSize = receiveBufferSize;
                _SendBufferSize = sendBufferSize;
            }

            /// <summary>
            /// 最多会话数量，默认1024
            /// </summary>
            public uint MaxSessionCount
            {
                get { return _MaxSessionCount; }
                set { _MaxSessionCount = value; }
            }
            private uint _MaxSessionCount = 1024;

            /// <summary>
            /// 接收缓存大小，默认16K
            /// </summary>
            public uint ReceiveBufferSize
            {
                get { return _ReceiveBufferSize; }
                set { _ReceiveBufferSize = value; }
            }
            private uint _ReceiveBufferSize = 16 * 1024;  // 16 K

            /// <summary>
            /// 发送区缓存大小，默认16K
            /// </summary>
            public uint SendBufferSize
            {
                get { return _SendBufferSize; }
                set { _SendBufferSize = value; }
            }
            private uint _SendBufferSize = 16 * 1024;
            
            /// <summary>
            /// 接收端口号，默认3130
            /// </summary>
            public uint ServertPort
            {
                get { return _ServertPort; }
                set { _ServertPort = value; }
            }
            private uint _ServertPort = 3130;

            /// <summary>
            /// 最大会话数据大小，默认1M
            /// </summary>
            public uint MaxDatagramSize
            {
                get { return _MaxDatagramSize; }
                set { _MaxDatagramSize = value; }
            }
            private uint _MaxDatagramSize = 1024 * 1024;  // 1M

            /// <summary>
            /// 最大会话时间，默认2分钟
            /// </summary>
            public uint MaxSessionTimeout
            {
                get { return _MaxSessionTimeout; }
                set { _MaxSessionTimeout = value; }
            }
            private uint _MaxSessionTimeout = 120;   // 2 minutes

            /// <summary>
            /// 最大监听队列长度，默认16
            /// </summary>
            public uint MaxListenQueueLength
            {
                get { return _MaxListenQueueLength; }
                set { _MaxListenQueueLength = value; }
            }
            private uint _MaxListenQueueLength = 16;

            /// <summary>
            /// 最大相同ID地址数量，默认64
            /// </summary>
            public uint MaxSameIPCount
            {
                get { return _MaxSameIPCount; }
                set { _MaxSameIPCount = value; }
            }
            private uint _MaxSameIPCount = 64;

            /// <summary>
            /// 侦听论询时间间隔(ms)，默认25
            /// </summary>
            public uint AcceptListenTimeInterval
            {
                get { return _AcceptListenTimeInterval; }
                set { _AcceptListenTimeInterval = value; }
            }
            private uint _AcceptListenTimeInterval = 25;

            /// <summary>
            /// 清理Timer的时间间隔(ms)，默认100
            /// </summary>
            public uint CheckSessionTableTimeInterval
            {
                get { return _CheckSessionTableTimeInterval; }
                set { _CheckSessionTableTimeInterval = value; }
            }
            private uint _CheckSessionTableTimeInterval = 100;

            /// <summary>
            /// 检查数据包队列时间休息间隔(ms)，默认100
            /// </summary>
            public uint CheckDatagramQueueTimeInterval
            {
                get { return _CheckDatagramQueueTimeInterval; }
                set { _CheckDatagramQueueTimeInterval = value; }
            }
            private uint _CheckDatagramQueueTimeInterval = 100;
        }
    }
}
