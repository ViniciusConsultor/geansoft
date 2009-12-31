using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Gean.Exceptions
{
    /// <summary>
    /// Gean.Framework的的基础异常类，所有的异常从本类派生
    /// </summary>
    [global::System.Serializable]
    public class GeanException : ApplicationException
    {
        public GeanException() { }
        public GeanException(string message) : base(message) { }
        public GeanException(string message, Exception inner) : base(message, inner) { }

        protected GeanException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// 查找原始的异常
        /// </summary>
        /// <param name="e">异常</param>
        /// <returns>原始的异常</returns>
        public static Exception FindSourceException(Exception e)
        {
            Exception e1 = e;
            while (e1 != null)
            {
                e = e1;
                e1 = e1.InnerException;
            }
            return e;
        }

        /// <summary>
        /// 从异常树种查找指定类型的异常
        /// </summary>
        /// <param name="e">异常</param>
        /// <param name="expectedExceptionType">期待的异常类型</param>
        /// <returns>所要求的异常，如果找不到，返回null</returns>
        public static Exception FindSourceException(Exception e, Type expectedExceptionType)
        {
            while (e != null)
            {
                if (e.GetType() == expectedExceptionType)
                {
                    return e;
                }
                e = e.InnerException;
            }
            return null;
        }

    }
}
