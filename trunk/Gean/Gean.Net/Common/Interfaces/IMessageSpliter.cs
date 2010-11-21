using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Messages.Interfaces
{
    /// <summary>
    /// 消息分割器
    /// </summary>
    public interface IMessageSpliter
    {
        /// <summary>
        /// 从消息中分离命令字.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        string SplitAtCommand(string message);
        /// <summary>
        /// 分离整个消息为各个域的数组
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        string[] Split(string message);
        /// <summary>
        /// 分离消息，并返回指定位置的值
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        string SplitAt(string message, int index);
    }
}
