using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Common
{
    /// <summary>
    /// 描述一个协议数据解析器的接口
    /// </summary>
    public interface IProtocolParser
    {
        /// <summary>
        /// 将协议数据解析成相应的对象
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        object Parse(object message);
    }
}
