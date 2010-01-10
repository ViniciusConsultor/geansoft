using System;

namespace Gean.Exceptions
{
	/// <summary>
	/// �����쳣
	/// </summary>
	/// <remarks>
	/// ���������棬�ܷ��ֵ��쳣�����װ�ɴ����ʵ��
	/// </remarks>
	[Serializable]
	public class ConfigException : GeanException
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
        /// <param name="message">�쳣��Ϣ</param>
        /// <param name="errorNo">�쳣���</param>
        public ConfigException(string message, int errorNo)
            : base(message)
        {
            this.ErrorNumber = errorNo;
		}

		/// <summary>
		/// ���캯��
		/// </summary>
        /// <param name="message">�쳣��Ϣ</param>
        /// <param name="innerException">�ڲ��쳣</param>
        /// <param name="errorNo">�쳣���</param>
        public ConfigException(string message, Exception innerException, int errorNo)
            : base(message, innerException)
        {
            this.ErrorNumber = errorNo;
        }

        /// <summary>
        /// �쳣���
        /// </summary>
        public int ErrorNumber { get; protected set; }
	}
}
