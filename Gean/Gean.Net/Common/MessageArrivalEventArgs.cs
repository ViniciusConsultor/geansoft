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
        private static string _ClientId = string.Empty;
        static MessageArrivalEventArgs()
        {
            _ClientId = UtilityHardware.GetCpuID();
        }

        /// <summary>
        /// Gets or sets 命令字.
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; private set; }
        /// <summary>
        /// Gets the 请求者类型.
        /// </summary>
        /// <value>The type of the client.</value>
        public int ClientType { get { return SocketOption.ME.ClientType; } }
        /// <summary>
        /// Gets the 请求者编号.
        /// </summary>
        /// <value>The client id.</value>
        public string ClientId { get { return _ClientId; } }
        /// <summary>
        /// Gets the 时间戳.
        /// </summary>
        /// <value>The time ticks.</value>
        public string TimeTicks { get { return UtilityDateTime.CurrTicks().ToString(); } }
        /// <summary>
        /// Gets or sets 交易ID（由交易的发起者负责维护）
        /// </summary>
        /// <value>The talk id.</value>
        public string TalkId { get { return DataIDGenerator.ME.Generate(); } }
        /// <summary>
        /// Gets or sets 请求数据.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; set; }

        public MessageSource Source { get; private set; }

        public MessageArrivalEventArgs(MessageSource msgSource, string command, string processResult)
        {
            this.Source = msgSource;
            this.Command = command;
            this.Data = processResult;
        }

        public override string ToString()
        {
            switch (this.Source)
            {
                case MessageSource.AsyncSending:
                case MessageSource.SynchroSending:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(this.Command).Append('|');
                        sb.Append(ClientType).Append('|');
                        sb.Append(ClientId).Append('|');
                        sb.Append(TimeTicks).Append('|');
                        sb.Append(TalkId).Append('|');
                        sb.Append(Data).Append('|');
                        sb.Append('@');
                        return sb.ToString();
                    }
                case MessageSource.AsyncReceiving:
                case MessageSource.SynchroReceiving:
                default:
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(this.Command).Append('|');
                        sb.Append(Data).Append('|');
                        sb.Append('@');
                        return sb.ToString();
                    }
            }
        }
    }
}
