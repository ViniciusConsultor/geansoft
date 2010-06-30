using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Gean.SimpleLogger
{
    public class SimpleLoggerWriter : ISimpleLoggerWriter
    {
        protected virtual FileInfo LogFile { get; set; }
        protected virtual StreamWriter Stream { get; set; }

        /// <summary>
        /// 构造函数。文本文件日志记录类。
        /// </summary>
        /// <param name="logfiles">日志记录的文件</param>
        private SimpleLoggerWriter(string logfile)
        {
            if (!File.Exists(logfile))//如果Log文件存在，将不在保留
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(logfile)));
                Stream = FileCreator(logfile);
            }
            else
            {
                try
                {
                    File.SetAttributes(logfile, FileAttributes.Normal);
                    File.Delete(logfile);
                }
                catch//文件虽然存在，但文件操作发生异常，一般可能是被锁定
                {
                    logfile += ".log";
                }
                Stream = SimpleLoggerWriter.FileCreator(logfile);
            }
            this.LogFile = new FileInfo(logfile);
        }

        /// <summary>
        /// 写一条日志信息
        /// </summary>
        /// <param name="logLevel">当前信息的日志等级</param>
        /// <param name="message">信息主体，可是多个对象(当未是String时，将会调用object的Tostring()获取字符串)</param>
        public void Write(SimpleLoggerLevel logLevel, params object[] message)
        {
            lock (Stream)
            {
                StringBuilder sb = new StringBuilder();
                //加入时间信息
                sb.Append(DateTime.Now.ToString("yyMMdd HH:mm:ss"))
                  .Append(" ")
                  .Append(DateTime.Now.Millisecond.ToString().PadLeft(3, '0'))
                  .Append(",\t")
                  .Append(logLevel.ToString())
                  .Append(",   \t");
                //使用者附加的Log信息
                foreach (object item in message)
                {
                    if (item is Exception)
                    {
                        if (item != null)
                        {
                            sb.Append(((Exception)item).Message).Append(" | ");
                        }
                    }
                    else if (item == null)
                    {
                        continue;
                    }
                    else
                    {
                        sb.Append(item.ToString()).Append(" | ");
                    }
                }
                //写入文件
                string log = sb.ToString(0, sb.Length - 2);
                Stream.WriteLine(log);
                Stream.Flush();
                OnLogWrited(new LogWritedEventArgs(log));
            }//lock (_StreamWriterDic)
        }

        static SimpleLoggerWriter _logWriter = null;
        /// <summary>
        /// 文本文件日志记录类的初始化，主要初始化日志文件。
        /// </summary>
        /// <param name="logTaget">log文件的完全路径名</param>
        public static SimpleLoggerWriter InitializeComponent(string logfiles)
        {
            if (_logWriter == null)
            {
                _logWriter = new SimpleLoggerWriter(logfiles);
            }
            return _logWriter;
        }

        /// <summary>
        /// 创建一个日志文件，第一行将注明日志创建日期
        /// </summary>
        /// <param name="ufile">文件全名</param>
        /// <returns></returns>
        private static StreamWriter FileCreator(string file)
        {
            StreamWriter sw;
            string begin = string.Format("### {0} {1} ###\r\n==========\t\t=====\t\t==========\r\n", DateTime.Now, DateTime.Now.Millisecond);
            File.AppendAllText(file, begin, Encoding.UTF8);
            sw = File.AppendText(file);
            return sw;
        }

        /// <summary>
        /// 备份日志文件
        /// </summary>
        public void BakupLogFile()
        {
            string bakFile = this.LogFile.FullName + DateTime.Now.ToString("-yy-MM-dd HH-mm-ss") + ".bak.log";
            this.Stream.Flush();
            this.LogFile.CopyTo(bakFile);
        }

        /// <summary>
        /// 关闭日志文件的读写流(使用后，全局日志相关方法将全部失效)
        /// </summary>
        /// <param name="isBakup">是否备份</param>
        public void Close(bool isBakup)
        {
            lock (Stream)
            {
                Stream.Flush();
                Stream.Close();
                Stream.Dispose();
            }
            if (isBakup)
            {
                BakupLogFile();
            }
        }

        /// <summary>
        /// 当日志写入后发生的事件
        /// </summary>
        public event LogWritedEventHandler LogWritedEvent;
        private void OnLogWrited(LogWritedEventArgs e)
        {
            if (LogWritedEvent != null)
                LogWritedEvent(this, e);
        }
        public delegate void LogWritedEventHandler(Object sender, LogWritedEventArgs e);
        public class LogWritedEventArgs : EventArgs
        {
            public String LogString { get; private set; }
            public LogWritedEventArgs(String logString)
            {
                this.LogString = logString;
            }
        }

    }
}
