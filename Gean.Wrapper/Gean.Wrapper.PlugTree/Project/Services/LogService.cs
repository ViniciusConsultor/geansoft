using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree.Exceptions;


namespace Gean.Wrapper.PlugTree.Service
{
    public static class LogService
    {
        public static TextLog Log { get; set; }

        public static void Info(params object[] message)
        {
            Log.Write(LogLevel.Info, message);
        }

        public static void Warn(params object[] message)
        {
            Log.Write(LogLevel.Warning, message);
        }

        public static void Error(params object[] message)
        {
            Log.Write(LogLevel.Error, message);
        }

        public static void Debug(params object[] message)
        {
            Log.Write(LogLevel.Debug, message);
        }
    }
}
