using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.SimpleLogger
{
    public interface ISimpleLoggerWriter
    {
        /// <summary>
        /// 写入Log信息
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="message">The message.</param>
        void Write(SimpleLoggerLevel logLevel, params object[] message);
    }
}
