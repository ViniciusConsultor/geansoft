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
        public KeepConnectionProtocolPool()
        {
            this.ID = UtilityGuid.Get();
        }

        public string ID { get; private set; }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is KeepConnectionProtocolPool))
                return false;
            return this.ID.Equals(((KeepConnectionProtocolPool)obj).ID);
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + ((this.ID == null) ? 0 : this.ID.GetHashCode());
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
        public string DequeueSendingMessage()
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
        public string DequeueReceivingMessage()
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
        public void EnqueueSendingMessage(string message)
        {
            message = message.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                lock (_SendLock)
                    _SendingQueue.Enqueue(message);
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
            }
        }

    }
}
