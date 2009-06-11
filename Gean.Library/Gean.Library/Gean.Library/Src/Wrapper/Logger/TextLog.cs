using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Gean
{
    /// <summary>
    /// 文本文件日志记录类；
    /// 在应用中止前请使用该类的Close方法释放此类的资源调用（可选择是否备份）；
    /// 已考虑多线程安全。
    /// </summary>
    public class TextLog : LogWriter
    {

        /// <summary>
        /// 构造函数。文本文件日志记录类。
        /// </summary>
        /// <param name="logfiles">日志记录的文件</param>
        protected TextLog(params string[] logfiles)
        {
            this.TargetObjectDictionary = new EventIndexedDictionary<string, object>();

            foreach (string file in logfiles)
            {
                this._ObjectNameList.Add(file);
                StreamWriter sw;
                string ufile = file;
                if (!File.Exists(ufile))//如果Log文件存在，将不在保留
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(ufile)));
                    sw = FileCreator(ufile);
                }
                else
                {
                    try
                    {
                        File.SetAttributes(ufile, FileAttributes.Normal);
                        File.Delete(ufile);
                    }
                    catch//文件虽然存在，但文件操作发生异常，一般可能是被锁定
                    {
                        ufile += ".log";
                    }
                    sw = FileCreator(ufile);
                }
                this.TargetObjectDictionary.Add(ufile, sw);
            }
        }

        static ILogWriter _ILogWriter = null;
        /// <summary>
        /// 文本文件日志记录类的初始化，主要初始化日志文件。
        /// </summary>
        /// <param name="logTaget">log文件的完全路径名</param>
        static public ILogWriter InitializeComponent(params string[] logTaget)
        {
            if (_ILogWriter == null)
            {
                _ILogWriter = new TextLog(logTaget);
            }
            return _ILogWriter;
        }

        /// <summary>
        /// 写一条日志信息
        /// </summary>
        /// <param name="logLevel">当前信息的日志等级</param>
        /// <param name="file">日志将要写入的文件</param>
        /// <param name="message">信息主体，可是多个对象(当未是String时，将会调用object的Tostring()获取字符串)</param>
        public override void Write(LogLevel logLevel, string file, params object[] message)
        {
            Debug.Assert(this._ObjectNameList.Contains(file), file + " isn't Exist!");
            lock (this.TargetObjectDictionary)
            {
                StreamWriter sw = this.TargetObjectDictionary[file] as StreamWriter;
                lock (sw)
                {
                    StringBuilder sb = new StringBuilder();
                    //加入时间信息
                    sb.Append(DateTime.Now.ToString("MM-dd HH:mm:ss"))
                        .Append(" ")
                        .Append(DateTime.Now.Millisecond.ToString())
                        .Append("\t");
                    //加入Log等级信息
                    sb.Append("< ").Append(logLevel.ToString()).Append(" >").Append("\t");
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
                    sw.WriteLine(sb.ToString(0, sb.ToString().Length - 2));
                    sw.Flush();
                }//lock (sw)
            }//lock (_StreamWriterDic)
        }

        /// <summary>
        /// 创建一个日志文件，第一行将注明日志创建日期
        /// </summary>
        /// <param name="ufile">文件全名</param>
        /// <returns></returns>
        private static StreamWriter FileCreator(string ufile)
        {
            StreamWriter sw;
            File.AppendAllText(ufile, "### " + DateTime.Now.ToString() + " " + DateTime.Now.Millisecond.ToString() + " ###\r\n", Encoding.UTF8);
            sw = File.AppendText(ufile);
            return sw;
        }

        /// <summary>
        /// 备份日志文件
        /// </summary>
        public void BakupLogFile()
        {
            foreach (var item in this.TargetObjectDictionary)
            {
                string bakFile = item.Key + DateTime.Now.ToString("-yy-MM-dd HH-mm-ss") + ".bak.log";
                File.Copy(item.Key, bakFile);
            }
        }

        /// <summary>
        /// 关闭日志文件的读写流
        /// </summary>
        /// <param name="isBakup">是否备份</param>
        public void Close(bool isBakup)
        {
            if (isBakup)
            {
                BakupLogFile();
            }
            foreach (var item in this.TargetObjectDictionary)
            {
                lock (item.Value)
                {
                    StreamWriter sw = item.Value as StreamWriter;
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

    }//class
}
