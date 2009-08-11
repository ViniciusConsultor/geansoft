using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public interface ILogWriter
    {
        /// <summary>
        /// 获取与设置Log信息所需置入的目标（可能是一个Xml文件，可能是一个Text文本文件，也可能是一个数据库），
        /// Key是目标的键值（如文件名），Value是目标
        /// </summary>
        /// <value>Log置入的目标.</value>
        Dictionary<string, object> TargetObjectDictionary { get; set; }

        /// <summary>
        /// 写入Log信息
        /// </summary>
        /// <param name="logLevel">The log level.</param>
        /// <param name="objName">Name of the obj.</param>
        /// <param name="message">The message.</param>
        void Write(LogLevel logLevel, string objName, params object[] message);
    }
}
