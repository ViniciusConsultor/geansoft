using System;

namespace Gean.Config
{
	/// <summary>
	/// �����쳣
	/// </summary>
	/// <remarks>
	/// ���������棬�ܷ��ֵ��쳣�����װ�ɴ����ʵ��
	/// </remarks>
	[Serializable]
	public class ConfigException : BaseException
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public ConfigException() : base() {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="message">�쳣��Ϣ</param>
		/// <param name="innerException">�ڲ��쳣</param>
		public ConfigException(string message, Exception innerException)
			: base(message, innerException) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="message">�쳣��Ϣ</param>
		public ConfigException(string message)
			: base(message) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="errorNo">�쳣���</param>
		/// <param name="message">�쳣��Ϣ</param>
		public ConfigException(int errorNo, string message)
			: base(errorNo, message) {
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="errorNo">�쳣���</param>
		/// <param name="message">�쳣��Ϣ</param>
		/// <param name="innerException">�ڲ��쳣</param>
		public ConfigException(int errorNo, string message, Exception innerException)
			: base(errorNo, message, innerException) {
		}
	}
}
