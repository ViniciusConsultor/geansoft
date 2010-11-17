using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Common
{
    /// <summary>
    /// 描述一个协议数据组装器的接口
    /// </summary>
    public interface IProtocolCombin
    {
        /// <summary>
        /// 将指定的对象根据规则进行组装成协议数据
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        string Combin(params object[] args);
    }
}
