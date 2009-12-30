using System.Reflection;

namespace Gean
{
	/// <summary>
	/// ��<see cref="ColumnAttribute"/>�İ�װ
	/// </summary>
	public interface IFieldMemberInfo
	{
		/// <summary>
		/// �ֶ��Ƿ�ɶ�
		/// </summary>
		bool CanRead { get; }

		/// <summary>
		/// �ֶ��Ƿ��д
		/// </summary>
		bool CanWrite { get; }

		/// <summary>
		/// �ֶ���
		/// </summary>
		string Name { get; }

		/// <summary>
		/// �ֶμ��صĳ�Ա��Ϣ<see cref="MemberInfo"/>
		/// </summary>
		MemberInfo MemberInfo { get; }

		/// <summary>
		/// �ֶα���ļ�����Ϣ<see cref="ColumnAttribute"/>
		/// </summary>
		ColumnAttribute Column { get; }

		/// <summary>
		/// ��ȡ�ֶε�ֵ
		/// </summary>
		/// <param name="obj">�������ֶε�ʵ��</param>
		/// <returns>�ֶε�ֵ</returns>
		object GetValue(object obj);

		/// <summary>
		/// �����ֶε�ֵ
		/// </summary>
		/// <param name="obj">�������ֶε�ʵ��</param>
		/// <param name="value">�ֶε�ֵ</param>
		void SetValue(object obj, object value);
	}
}
