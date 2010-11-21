using System;
using System.Collections.Generic;
using System.Text;
using Gean.Net.Messages;
using Gean.Net.KeepSocket;

namespace Gean.Net.MessagePools
{
    /// <summary>
    /// 用于长连接的协议异步消息数据池。包括：1.待发送的消息队列; 2.接收到的消息队列
    /// </summary>
    public abstract class MessagePool
    {
        public string ID { get { return UtilityGuid.Get(); } }

        public override bool Equals(object obj)
        {
            if (null == obj || !(obj is MessagePool))
                return false;
            return this.ID.Equals(((MessagePool)obj).ID);
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
        protected MessageQueue _SendingQueue = new MessageQueue();
        /// <summary>
        /// 发送队列线程锁
        /// </summary>
        protected object _SendLock = new object();

        /// <summary>
        /// 【接收到的消息队列】
        /// </summary>
        protected MessageQueue _ReceivingQueue = new MessageQueue();
        /// <summary>
        /// 接收队列线程锁
        /// </summary>
        protected object _ReceiveLock = new object();

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
        /// 返回本消息池中的消息来源是否是异步的
        /// </summary>
        /// <value><c>true</c> if this instance is async; otherwise, <c>false</c>.</value>
        public abstract bool IsAsync { get; }

        /// <summary>
        /// 移除并返回位于消息池中【待发送的消息队列】开始处的对象。
        /// </summary>
        public abstract string DequeueSending();
        /// <summary>
        /// 移除并返回位于消息池中【接收到的消息队列】开始处的对象。
        /// </summary>
        public abstract string DequeueReceiving();
        /// <summary>
        /// 将消息添加到位于消息池中【待发送的消息队列】的结尾处。
        /// </summary>
        public abstract void EnqueueSending(string message);
        /// <summary>
        /// 将消息添加到位于消息池中【接收到的消息队列】的结尾处。
        /// </summary>
        public abstract void EnqueueReceiving(string message);

        #region 事件
        /// <summary>
        /// 新发送消息到达时的事件
        /// </summary>
        public event SendingMessageArrivalEventHandler SendingMessageArrivalEvent;
        protected virtual void OnSendingMessageArrival(MessageWrapper e)
        {
            if (SendingMessageArrivalEvent != null)
                SendingMessageArrivalEvent(this, e);
        }
        public delegate void SendingMessageArrivalEventHandler(object sender, MessageWrapper e);

        /// <summary>
        /// 新回复消息到达时的事件
        /// </summary>
        public event ReceivingMessageArrivalEventHandler ReceivingMessageArrivalEvent;
        protected virtual void OnReceivingMessageArrival(MessageWrapper e)
        {
            if (ReceivingMessageArrivalEvent != null)
                ReceivingMessageArrivalEvent(this, e);
        }
        public delegate void ReceivingMessageArrivalEventHandler(object sender, MessageWrapper e);
        #endregion
    }
}
