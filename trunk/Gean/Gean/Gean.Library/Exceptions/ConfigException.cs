using System;

namespace Gean.Exceptions
{
	/// <summary>
	/// 配置异常
	/// </summary>
	/// <remarks>
	/// 在配置里面，能发现的异常都会包装成此类的实例
	/// </remarks>
	[Serializable]
	public class ConfigException : GeanException
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public ConfigException() : base() {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常消息</param>
		/// <param name="innerException">内部异常</param>
		public ConfigException(string message, Exception innerException)
			: base(message, innerException) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常消息</param>
		public ConfigException(string message)
			: base(message) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="errorNo">异常编号</param>
        public ConfigException(string message, int errorNo)
            : base(message)
        {
            this.ErrorNumber = errorNo;
		}

		/// <summary>
		/// 构造函数
		/// </summary>
        /// <param name="message">异常消息</param>
        /// <param name="innerException">内部异常</param>
        /// <param name="errorNo">异常编号</param>
        public ConfigException(string message, Exception innerException, int errorNo)
            : base(message, innerException)
        {
            this.ErrorNumber = errorNo;
        }

        /// <summary>
        /// 异常编号
        /// </summary>
        public int ErrorNumber { get; protected set; }
	}
}
