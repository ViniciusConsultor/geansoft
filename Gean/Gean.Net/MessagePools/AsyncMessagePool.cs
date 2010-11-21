using System;
using System.Collections.Generic;
using System.Text;
using Gean.Net.KeepSocket;
using Gean.Net.Common;

namespace Gean.Net.MessagePools
{
    public class AsyncMessagePool : MessagePool
    {
        /// <summary>
        /// 返回本消息池中的消息来源是否是异步的
        /// </summary>
        /// <value><c>true</c> if this instance is async; otherwise, <c>false</c>.</value>
        public override bool IsAsync
        {
            get
            {
                return SocketOption.ME.IsAsync;
            }
        }

        /// <summary>
        /// 移除并返回位于消息池中【待发送的消息队列】开始处的对象。
        /// </summary>
        public override string DequeueSending()
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
        public override string DequeueReceiving()
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
        public override void EnqueueSending(string message)
        {
            message = message.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                lock (_SendLock)
                {
                    _SendingQueue.Enqueue(message);
                }
                string command = SocketOption.ME.CommandParser.Parse(message);
                OnReceivingMessageArrival(new MessageArrivalEventArgs(MessageSource.AsyncSending, command, message));
            }
        }

        /// <summary>
        /// 将消息添加到位于消息池中【接收到的消息队列】的结尾处。
        /// </summary>
        public override void EnqueueReceiving(string message)
        {
            message = message.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                lock (_ReceiveLock)
                {
                    _ReceivingQueue.Enqueue(message);
                }
                string command = SocketOption.ME.CommandParser.Parse(message);
                OnReceivingMessageArrival(new MessageArrivalEventArgs(MessageSource.AsyncReceiving, command, message));
            }
        }

    }
}
