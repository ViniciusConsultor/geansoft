using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.IO;
using System.Xml;

namespace Gean.Net.AloneSocket
{
    public class MessageService
    {
        #region Fields

        //probe time
        private static long _checktime;

        //hosts and ports
        private static List<Host> _hosts = new List<Host>();

        //active host and failures
        private static Host _activeHost;
        private static bool _hasFailures = false;

        //file to save unsent messages to
        private static string _filepath;

        //log path
        private static string _logpath;

        private BackgroundWorker _backgroundWorker;

        private static Queue<string> _messages = new Queue<string>();
        private static MessageService _instance = new MessageService();
        
        #endregion

        #region Events

        public event EventHandler<MessageEventArgs> MessageSent;

        #endregion

        #region Singleton

        private MessageService()
        {
            //try
            //{
                //load configuration
                ParseConfig();

                //check failed messages on start
                FileInfo fi = new FileInfo(_filepath);
                if (fi.Exists)
                    _hasFailures = true;

                _activeHost = FindHost();
                //set timer for periodic connectivity check
                TimerCallback tcb = new TimerCallback(TimerElapsed);
                Timer timer = new Timer(tcb, null, _checktime, _checktime);
            //}
            //catch
            //{
                //intentionally left blank to not have the Windows Unhandled Exception box
            //}
        }

        #endregion

        #region Public Methods

        public void Send(string message)
        {
            if (!_hasFailures)
            {
                _messages.Enqueue(message);

                if (_backgroundWorker == null)
                {
                    CreateWorker();
                }
            }
            else
            {
                //save to file
                File.AppendAllText(_filepath, message + Environment.NewLine, Encoding.UTF8);
                Log(new MessageEventArgs(null, 0, MessageStatus.Failure, "Communications error"));
            }
        }

        public static MessageService Instance
        {
            get { return _instance; }
        }

        #endregion

        #region Private Methods

        private void CreateWorker()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += new DoWorkEventHandler(OnDoWork);

            _backgroundWorker.RunWorkerAsync();
        }

        void OnDoWork(object sender, DoWorkEventArgs e)
        {
            while (_messages.Count > 0)
            {
                TcpClient client = new TcpClient();
                try
                {
                    client.Connect(IPAddress.Parse(_activeHost.IP), _activeHost.Port);

                    string message = _messages.Dequeue();

                    byte[] buffer = Encoding.Unicode.GetBytes(message);

                    client.Client.Send(buffer);

                    Log(new MessageEventArgs(message, _messages.Count, MessageStatus.Success, null));
                }
                catch (SocketException ex)
                {
                    _hasFailures = true;
                    string[] messages = _messages.ToArray();
                    Log(new MessageEventArgs(null, _messages.Count, MessageStatus.Failure, ex.Message));

                    _messages.Clear();
                    //save array to file
                    StringBuilder allMessages = new StringBuilder();
                    foreach (string message in messages)
                    {
                        allMessages.Append(message + Environment.NewLine);
                    }
                    File.AppendAllText(_filepath, allMessages.ToString(), Encoding.UTF8);
                    //failover
                    _activeHost = FindHost();
                }
                finally
                {
                    if (client.Connected)
                    {
                        client.Client.Shutdown(SocketShutdown.Both);
                        client.Client.Disconnect(false);
                    }
                }
            }
            _backgroundWorker = null;
        }

        //timer callback
        private void TimerElapsed(object info)
        {
            _activeHost = FindHost();
        }
        private Host FindHost()
        {
            Log(new MessageEventArgs(null, 0, MessageStatus.Connecting, null));
            TcpClient client = new TcpClient();
            Ping pingSender = new Ping();
            for (int i = 0; i < _hosts.Count; i++)
            {
                PingReply reply = pingSender.Send(_hosts[i].IP);
                if (reply.Status == IPStatus.Success)
                {
                    Log(new MessageEventArgs(string.Format("Ping to host {0} successful", _hosts[i].IP), 0, MessageStatus.PingSuccess, null));

                    try
                    {
                        client.Connect(IPAddress.Parse(_hosts[i].IP), _hosts[i].Port);
                        if (client.Connected)
                        {
                            Log(new MessageEventArgs(string.Format("Connection to host {0}:{1} sucessful", _hosts[i].IP, _hosts[i].Port), 0, MessageStatus.Connected, null));
                            
                            client.Client.Shutdown(SocketShutdown.Both);
                            client.Client.Disconnect(false);
                            //reset blocking messages to be sent
                            if (_hasFailures)
                            {
                                _hasFailures = false;
                                SendSavedMessages();
                            }
                            return _hosts[i];
                        }
                    }
                    catch
                    {
                        Log(new MessageEventArgs(string.Format("Connection to host {0}:{1} failed", _hosts[i].IP, _hosts[i].Port), 0, MessageStatus.ConnectionFailed, null));
                    }
                }
                else
                {
                    Log(new MessageEventArgs(string.Format("Ping to host {0} failed", _hosts[i].IP), 0, MessageStatus.PingFailed, null));
                }
            }
            _hasFailures = true;
            return null;
        }

        void SendSavedMessages()
        {
            //get messages from file and attempt to send
            UTF8Encoding myEnc = new UTF8Encoding();
            FileStream fs = new FileStream(_filepath, FileMode.Open, FileAccess.ReadWrite);
            byte[] theBytes = new byte[fs.Length];
            fs.Read(theBytes, 0, theBytes.Length);
            fs.Flush();
            fs.Close();
            FileInfo fi = new FileInfo(_filepath);
            fi.Delete();

            //copy file contents, omit UTF-8 Byte Offset Marker
            string fileString = myEnc.GetString(theBytes).Substring(1);
            string[] messages = fileString.Split(Environment.NewLine.ToCharArray());
            //fill the queue
            for (int i = 0; i < messages.Length; i++)
            {
                if(!string.IsNullOrEmpty(messages[i]))
                    _messages.Enqueue(messages[i]);
            }

            //start the worker
            if (_backgroundWorker == null)
            {
                CreateWorker();
            }
        }

        private void Log(MessageEventArgs e)
        {
            string message;
            LogCategory category;
            switch (e.Status)
            {
                case MessageStatus.Success:
                    message = string.Format("Send of message \"{0}\" completed - remaining messages: {1}",
                    e.Message, e.QueuedMessageCount);
                    category = LogCategory.Message;
                    break;
                case MessageStatus.Failure:
                    message = string.Format("Send failed" + Environment.NewLine +
                    "Reason: {0}", e.Error);
                    category = LogCategory.Message;
                    break;
                case MessageStatus.Connecting:
                    message = "Trying to connect";
                    category = LogCategory.Connection;
                    break;
                default:
                    message = e.Message;
                    category = LogCategory.Connection;
                    break;
            }
            LogMessage msg = new LogMessage(DateTime.Now, message, category);
            string formattedMessage = msg.FormattedMessage;
            File.AppendAllText(_logpath, formattedMessage, Encoding.UTF8);
            //forward to eventlistener
            if (MessageSent != null && category == LogCategory.Message)
            {
                MessageSent(this, e);
            }
        }

        private void ParseConfig()
        {
            //load configuration file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("configuration.xml");

            //parse elements
            XmlElement root = xmlDoc.DocumentElement;
            foreach (XmlElement element in root.ChildNodes)
            {
                if (element.Name == "hosts")
                {
                    foreach (XmlElement subElement in element.ChildNodes)
                    {
                        if (subElement.Name == "host")
                        {
                            if (subElement.ChildNodes.Count == 2)
                            {
                                XmlNode IP = subElement.ChildNodes[0];
                                XmlNode port = subElement.ChildNodes[1];
                                Host myHost = new Host(IP.InnerText, int.Parse(port.InnerText));
                                _hosts.Add(myHost);
                            }
                        }
                    }
                }
                if (element.Name == "backupFile")
                {
                    _filepath = element.InnerText;
                }
                if (element.Name == "logPath")
                {
                    _logpath = ((element.InnerText.EndsWith(@"\") ? element.InnerText.Substring(0, element.InnerText.Length - 1) : element.InnerText) + @"\" + DateTime.Today.ToString("yyyyMMdd") + ".log");
                }
                if (element.Name == "interval")
                {
                    _checktime = long.Parse(element.InnerText);
                }
            }
            //log application startup
            string message = string.Format("{0}{1} - Application started{0}", Environment.NewLine, DateTime.Now.ToString("HH:mm:ss.ffffff"));
            File.AppendAllText(_logpath, message, Encoding.UTF8);
        }

        #endregion

    }
}
