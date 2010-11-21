using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Messages.Interfaces
{
    /// <summary>
    /// 描述一个协议数据组装器的接口
    /// </summary>
    public interface IProtocolCombine
    {
        /// <summary>
        /// 将指定的对象根据规则进行组装成协议数据
        /// </summary>
        /// <param name="message">The args.</param>
        /// <returns></returns>
        string Combin(IMessage message);
    }
}
