using System;

namespace Gean.Config
{
	/// <summary>
	/// ����ֵ�ӿ�
	/// </summary>
	/// <remarks>
	///	���������XML�ڱ�ʾһ�����ýڣ�
	///		<code>
	///			&lt;app my="myProperty"&gt;myValue&lt;/app&gt;
	///		</code>
	///	��ʱ��<c>Name="app"</c>��Value��ֵΪ"myValue"��Property��ֵΪ"myProperty"
	/// </remarks>
	public interface ISettingValue : ICloneable, IConverting
	{
		/// <summary>
		/// ��ǰ����ֵ�Ƿ�ֻ��
		/// </summary>
		bool ReadOnly { get; }

		/// <summary>
		/// ����ֵ��
		/// </summary>
		string Name { get; }

		/// <summary>
		/// ����ֵ
		/// </summary>
		string Value { get; }

		/// <summary>
		///  ��¡����ֵ
		/// </summary>
		/// <param name="readonly">�Ƿ�ֻ��</param>
		/// <returns>ISettingValue</returns>
		ISettingValue Clone(bool @readonly);
	}
}