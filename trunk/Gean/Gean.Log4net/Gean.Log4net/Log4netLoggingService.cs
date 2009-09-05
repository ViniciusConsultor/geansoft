using System;
using System.IO;
using log4net;
using log4net.Config;

namespace Gean.Log4net
{
    public sealed class Log4netLoggingService : ILoggingService
    {
        ILog log;

        private static Log4netLoggingService singleLog4net = null;
        public static Log4netLoggingService Initialize()
        {
            if (singleLog4net == null)
            {
                singleLog4net = new Log4netLoggingService();
            }
            return singleLog4net;
        }

        internal Log4netLoggingService()
        {
            //XmlConfigurator.ConfigureAndWatch(new FileInfo(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile));
            log = LogManager.GetLogger(typeof(Log4netLoggingService));
        }

        public void Debug(object message)
        {
            log.Debug(message);
        }

        public void DebugFormatted(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }

        public void Info(object message)
        {
            log.Info(message);
        }

        public void InfoFormatted(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }

        public void Warn(object message)
        {
            log.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public void WarnFormatted(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }

        public void Error(object message)
        {
            log.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        public void ErrorFormatted(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }

        public void Fatal(object message)
        {
            log.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }

        public void FatalFormatted(string format, params object[] args)
        {
            log.FatalFormat(format, args);
        }

        public bool IsDebugEnabled
        {
            get { return log.IsDebugEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return log.IsInfoEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return log.IsWarnEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return log.IsErrorEnabled; }
        }

        public bool IsFatalEnabled
        {
            get { return log.IsFatalEnabled; }
        }
    }
}
