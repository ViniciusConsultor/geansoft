using System;
using System.Collections.Generic;
using System.Text;
using Gean.Net.Common;
using Gean.Net.KeepSocket;

namespace Gean.Net.Common
{
    /// <summary>
    /// 用于长连接的协议消息数据池。包括：1.待发送的消息队列; 2.接收到的消息队列
    /// </summary>
    public class AsyncMessagePool
    {
        public AsyncMessagePool()
        {
            this.ID = UtilityGuid.Get();
        }

        public string ID { get; private set; }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is AsyncMessagePool))
                return false;
            return this.ID.Equals(((AsyncMessagePool)obj).ID);
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int nullCode = -71;
            int result = 1;
            result = prime * result + ((this.ID == null) ? nullCode : this.ID.GetHashCode());
            return result;
        }

        /// <summary>
        /// 【待发送的消息队列】
        /// </summary>
        private MessageQueue _SendingQueue = new MessageQueue();
        /// <summary>
        /// 发送队列线程锁
        /// </summary>
        private object _SendLock = new object();

        /// <summary>
        /// 【接收到的消息队列】
        /// </summary>
        private MessageQueue _ReceivingQueue = new MessageQueue();
        /// <summary>
        /// 接收队列线程锁
        /// </summary>
        private object _ReceiveLock = new object();

        /// <summary>
        /// 返回发送队列的消息数
        /// </summary>
        /// <value>The sending queue count.</value>
        public int SendingQueueCount
        {
            get { return _SendingQueue.Count; }
        }

        /// <summary>
        /// 返回接收到的消息数
        /// </summary>
        /// <value>The receiving queue count.</value>
        public int ReceivingQueueCount
        {
            get { return _ReceivingQueue.Count; }
        }       

        /// <summary>
        /// 移除并返回位于消息池中【待发送的消息队列】开始处的对象。
        /// </summary>
        public string DequeueSending()
        {
            string message = string.Empty;
            if (_SendingQueue.Count > 0)
            {
                lock (_SendLock)
                    return message = _SendingQueue.Dequeue();
            }
            return message;
        }

        /// <summary>
        /// 移除并返回位于消息池中【接收到的消息队列】开始处的对象。
        /// </summary>
        public string DequeueReceiving()
        {
            string message = string.Empty;
            if (_ReceivingQueue.Count > 0)
            {
                lock (_ReceiveLock)
                    return _ReceivingQueue.Dequeue();
            }
            return message;
        }

        /// <summary>
        /// 将消息添加到位于消息池中【待发送的消息队列】的结尾处。
        /// </summary>
        public void EnqueueSending(string message)
        {
            message = message.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                lock (_SendLock)
                {
                    _SendingQueue.Enqueue(message);
                }
                string command = KeepSocketOption.ME.CommandParser.Parse(message);
                OnReceivingMessageArrival(new MessageArrivalEventArgs(MessageSource.AsyncSending, command, message));
            }
        }

        /// <summary>
        /// 将消息添加到位于消息池中【接收到的消息队列】的结尾处。
        /// </summary>
        public void EnqueueReceiving(string message)
        {
            message = message.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                lock (_ReceiveLock)
                {
                    _ReceivingQueue.Enqueue(message);
                }
                string command = KeepSocketOption.ME.CommandParser.Parse(message);
                OnReceivingMessageArrival(new MessageArrivalEventArgs(MessageSource.AsyncReceiving, command, message));
            }
        }

        /// <summary>
        /// 新发送消息到达时的事件
        /// </summary>
        public event SendingMessageArrivalEventHandler SendingMessageArrivalEvent;
        protected virtual void OnSendingMessageArrival(MessageArrivalEventArgs e)
        {
            if (SendingMessageArrivalEvent != null)
                SendingMessageArrivalEvent(this, e);
        }
        public delegate void SendingMessageArrivalEventHandler(object sender, MessageArrivalEventArgs e);

        /// <summary>
        /// 新回复消息到达时的事件
        /// </summary>
        public event ReceivingMessageArrivalEventHandler ReceivingMessageArrivalEvent;
        protected virtual void OnReceivingMessageArrival(MessageArrivalEventArgs e)
        {
            if (ReceivingMessageArrivalEvent != null)
                ReceivingMessageArrivalEvent(this, e);
        }
        public delegate void ReceivingMessageArrivalEventHandler(object sender, MessageArrivalEventArgs e);

    }
}
