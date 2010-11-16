using System;
using System.Collections.Generic;
using System.Text;
using Gean.Net.Common;

namespace Gean.Net.ProtocolWrapper
{
    /// <summary>
    /// 用于长连接的协议消息数据池。包括：1.待发送的消息队列; 2.接收到的消息队列
    /// </summary>
    public class KeepConnectionProtocolPool
    {
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
        public string DequeueSendingMessage()
        {
            string message = string.Empty;
            if (_SendingQueue.Count > 0)
            {
                lock (_SendLock)
                    message = _SendingQueue.Dequeue();
                OnSendingRemoved(new ProtocolMessageEventArgs(message));
            }
            return message;
        }

        /// <summary>
        /// 移除并返回位于消息池中【接收到的消息队列】开始处的对象。
        /// </summary>
        public string DequeueReceivingMessage()
        {
            string message = string.Empty;
            if (_ReceivingQueue.Count > 0)
            {
                lock (_ReceiveLock)
                    _ReceivingQueue.Dequeue();
                OnReceivingRemoved(new ProtocolMessageEventArgs(message));
            }
            return message;
        }

        /// <summary>
        /// 将消息添加到位于消息池中【待发送的消息队列】的结尾处。
        /// </summary>
        public void EnqueueSendingMessage(string message)
        {
            message = message.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                lock (_SendLock)
                    _SendingQueue.Enqueue(message);
                OnSendingAdded(new ProtocolMessageEventArgs(message));
            }
        }

        /// <summary>
        /// 将消息添加到位于消息池中【接收到的消息队列】的结尾处。
        /// </summary>
        public void EnqueueReceivingMessage(string message)
        {
            message = message.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                lock (_ReceiveLock)
                    _ReceivingQueue.Enqueue(message);
                OnReceivingAdded(new ProtocolMessageEventArgs(message));
            }
        }


        /// <summary>
        /// 【待发送的消息队列】新增消息后发生的事件
        /// </summary>
        public event SendingAddedEventHandler SendingAddedEvent;
        protected virtual void OnSendingAdded(ProtocolMessageEventArgs e)
        {
            if (SendingAddedEvent != null)
                SendingAddedEvent(this, e);
        }
        public delegate void SendingAddedEventHandler(Object sender, ProtocolMessageEventArgs e);

        /// <summary>
        /// 【待发送的消息队列】移除消息后发生的事件
        /// </summary>
        public event SendingRemovedEventHandler SendingRemovedEvent;
        protected virtual void OnSendingRemoved(ProtocolMessageEventArgs e)
        {
            if (SendingAddedEvent != null)
                SendingAddedEvent(this, e);
        }
        public delegate void SendingRemovedEventHandler(Object sender, ProtocolMessageEventArgs e);

        /// <summary>
        /// 【接收到的消息队列】新增消息后发生的事件
        /// </summary>
        public event ReceivingAddedEventHandler ReceivingAddedEvent;
        protected virtual void OnReceivingAdded(ProtocolMessageEventArgs e)
        {
            if (SendingAddedEvent != null)
                SendingAddedEvent(this, e);
        }
        public delegate void ReceivingAddedEventHandler(Object sender, ProtocolMessageEventArgs e);

        /// <summary>
        /// 【接收到的消息队列】新增消息后发生的事件
        /// </summary>
        public event ReceivingRemovedEventHandler ReceivingRemovedEvent;
        protected virtual void OnReceivingRemoved(ProtocolMessageEventArgs e)
        {
            if (SendingAddedEvent != null)
                SendingAddedEvent(this, e);
        }
        public delegate void ReceivingRemovedEventHandler(Object sender, ProtocolMessageEventArgs e);

        /// <summary>
        /// 包含Socket协议消息事件数据的类
        /// </summary>
        public class ProtocolMessageEventArgs : EventArgs
        {
            public string Message { get; private set; }
            public ProtocolMessageEventArgs(string message)
            {
                this.Message = message;
            }
        }

    }
}
