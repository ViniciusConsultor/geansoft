using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Gean
{
    public class LogWriter : ILogWriter
    {
        protected virtual FileInfo LogFile { get; set; }
        protected virtual StreamWriter Stream { get; set; }

        /// <summary>
        /// 构造函数。文本文件日志记录类。
        /// </summary>
        /// <param name="logfiles">日志记录的文件</param>
        private LogWriter(string logfile)
        {
            StreamWriter sw;
            if (!File.Exists(logfile))//如果Log文件存在，将不在保留
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(logfile)));
                sw = FileCreator(logfile);
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
                sw = LogWriter.FileCreator(logfile);
            }
            this.LogFile = new FileInfo(logfile);
        }

        /// <summary>
        /// 写一条日志信息
        /// </summary>
        /// <param name="logLevel">当前信息的日志等级</param>
        /// <param name="message">信息主体，可是多个对象(当未是String时，将会调用object的Tostring()获取字符串)</param>
        public void Write(LogLevel logLevel, params object[] message)
        {
            lock (Stream)
            {
                StringBuilder sb = new StringBuilder();
                //加入时间信息
                sb.Append(DateTime.Now.ToString("MM-dd HH:mm:ss"))
                  .Append(" ")
                  .Append(DateTime.Now.Millisecond.ToString())
                  .Append("\t")
                  .Append("< ")
                  .Append(logLevel.ToString())
                  .Append(" >")
                  .Append("\t");
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
                    else
                    {
                        sb.Append(item.ToString()).Append(" | ");
                    }
                }
                //写入文件
                Stream.WriteLine(sb.ToString(0, sb.ToString().Length - 2));
                Stream.Flush();
            }//lock (_StreamWriterDic)
        }

        static LogWriter _logWriter = null;
        /// <summary>
        /// 文本文件日志记录类的初始化，主要初始化日志文件。
        /// </summary>
        /// <param name="logTaget">log文件的完全路径名</param>
        public static LogWriter InitializeComponent(string logfiles)
        {
            if (_logWriter == null)
            {
                _logWriter = new LogWriter(logfiles);
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
            File.AppendAllText(file, "### " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond.ToString() + " ###\r\n", Encoding.UTF8);
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
    }
}
