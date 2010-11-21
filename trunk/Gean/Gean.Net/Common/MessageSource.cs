using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Common
{
    /// <summary>
    /// 消息来源
    /// </summary>
    public enum MessageSource
    {
        /// <summary>
        /// 异步发送
        /// </summary>
        AsyncSending,
        /// <summary>
        /// 异步接收
        /// </summary>
        AsyncReceiving,
        /// <summary>
        /// 同步发送
        /// </summary>
        SynchroSending,
        /// <summary>
        /// 同步接收
        /// </summary>
        SynchroReceiving
    }
}
