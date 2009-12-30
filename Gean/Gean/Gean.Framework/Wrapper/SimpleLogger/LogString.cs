using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.SimpleLogger
{
    /// <summary>
    /// 组装一些较易阅读的日志字符串
    /// </summary>
    public class LogString
    {
        private static readonly string Arrow = " -->> ";

        /// <summary>
        /// 正常...
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static string Normal(string str)
        {
            StringBuilder sb = new StringBuilder();
            return sb.Append("(正常)").Append(Arrow).Append(str).ToString();
        }
        /// <summary>
        /// 正常启动某...
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static string NormalStart(string str)
        {
            StringBuilder sb = new StringBuilder();
            return sb.Append("(正常启动)").Append(Arrow).Append(str).ToString();
        }
        /// <summary>
        /// 任务已完成...
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public static string TaskIsDone(string str)
        {
            StringBuilder sb = new StringBuilder();
            return sb.Append("(任务已完成)").Append(Arrow).Append(str).ToString();
        }
    }
}
