using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.Common
{
    /// <summary>
    /// 从消息中解析命令字的接口
    /// </summary>
    public interface ICommandParser
    {
        string Parse(string message);
    }
}
