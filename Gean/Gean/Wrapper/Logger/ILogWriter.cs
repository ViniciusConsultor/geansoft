using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public interface ILogWriter
    {
        /// <summary>
        /// 写入Log信息
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        void Write(LogLevel logLevel, params object[] message);
    }
}
