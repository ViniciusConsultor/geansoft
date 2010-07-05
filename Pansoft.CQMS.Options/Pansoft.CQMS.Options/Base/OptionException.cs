using System;
using Gean.Exceptions;

namespace Pansoft.CQMS.Options
{
	/// <summary>
	/// 选项异常
	/// </summary>
	/// <remarks>
	/// 在选项里面，能发现的异常都会包装成此类的实例
	/// </remarks>
	[Serializable]
	public class OptionException : GeanException
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		public OptionException() : base() {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常消息</param>
		/// <param name="innerException">内部异常</param>
		public OptionException(string message, Exception innerException)
			: base(message, innerException) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="message">异常消息</param>
		public OptionException(string message)
			: base(message) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="errorNo">异常编号</param>
		/// <param name="message">异常消息</param>
		public OptionException(int errorNo, string message)
			: base(errorNo, message) {
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="errorNo">异常编号</param>
		/// <param name="message">异常消息</param>
		/// <param name="innerException">内部异常</param>
		public OptionException(int errorNo, string message, Exception innerException)
			: base(errorNo, message, innerException) {
		}

	}
}
