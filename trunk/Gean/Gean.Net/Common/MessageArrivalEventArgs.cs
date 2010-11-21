using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Common
{
    /// <summary>
    /// 当新（发送、回复）消息到达时的事件发生时包含事件数据的类
    /// </summary>
    public class MessageArrivalEventArgs : EventArgs
    {
        public string Data { get; set; }
        public int ClientType { get; set; }
        public string Timestamp { get; set; }
        public string DataId { get; set; }
        public string ClientId { get; set; }
        /// <summary>
        /// Gets or sets 命令字.
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; private set; }
        public MessageArrivalEventArgs(MessageSource msgSource, string command, string processResult)
        {
            this.Command = command;
            this.Data = processResult;
        }
    }
}
