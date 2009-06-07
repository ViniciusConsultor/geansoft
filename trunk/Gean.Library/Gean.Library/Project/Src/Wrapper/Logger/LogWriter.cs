using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Gean
{
    public abstract class LogWriter : ILogWriter
    {

        protected List<string> _ObjectNameList = new List<string>();
        public EventIndexedDictionary<string, object> TargetObjectDictionary { get; set; }

        public void Write(params object[] messages)
        {
            this.Write(LogLevel.Info, _ObjectNameList[0], messages);
        }

        public void Write(LogLevel logLevel, params object[] messages)
        {
            this.Write(logLevel, _ObjectNameList[0], messages);
        }

        public void Write(string objName, params object[] messages)
        {
            this.Write(LogLevel.Info, objName, messages);
        }

        public abstract void Write(LogLevel logLevel, string objName, params object[] messages);

    }
}
