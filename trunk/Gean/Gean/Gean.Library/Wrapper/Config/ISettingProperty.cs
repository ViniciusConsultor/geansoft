using System;

namespace Gean.Config
{
	/// <summary>
	/// ���ý����Խӿ�
	/// </summary>
	/// <remarks>
	///	���������XML�ڱ�ʾһ�����ýڣ�
	///		<code>
	///			&lt;app my="myProperty"&gt;myValue&lt;/app&gt;
	///		</code>
	///	��ʱ��<c>Name="app"</c>��Value��ֵΪ"myValue"��Property��ֵΪ"myProperty"
	/// </remarks>
	public interface ISettingProperty : ICloneable
	{
		/// <summary>
		/// ��ǰ���ý������Ƿ�ֻ��
		/// </summary>
		bool ReadOnly { get; }

		/// <summary>
		/// ���ýڵ����Ը���
		/// </summary>
		int Count { get; }

		/// <summary>
		/// ��ȡ����ֵ(����������)
		/// </summary>
		/// <param name="propertyName">������</param>
		ISettingValue this[string propertyName] { get; }

		/// <summary>
		/// ��ȡ����ֵ(������������)
		/// </summary>
		/// <param name="propertyIndex">��������</param>
		ISettingValue this[int propertyIndex] { get; }

		/// <summary>
		/// ���Ի�ȡĳ����ֵ
		/// </summary>
		/// <param name="propertyName">������</param>
		/// <returns>����ֵ</returns>
		string TryGetPropertyValue(string propertyName);

		/// <summary>
		/// ���Ի�ȡĳ����ֵ��ת����ָ������
		/// </summary>
		/// <typeparam name="T">ת����ָ��������</typeparam>
		/// <param name="propertyName">������</param>
		/// <returns>ָ�����͵�ʵ��</returns>
		T TryGetPropertyValue<T>(string propertyName);

		/// <summary>
		/// ���Ի�ȡĳ����ֵ��ת����ָ������
		/// </summary>
		/// <typeparam name="T">ת����ָ��������</typeparam>
		/// <param name="propertyName">������</param>
		/// <param name="defaultValue">ȱʡֵ</param>
		/// <returns>ָ�����͵�ʵ��</returns>
		T TryGetPropertyValue<T>(string propertyName, T defaultValue);

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <param name="readonly">�Ƿ�ֻ��</param>
		/// <param name="deep">�Ƿ���ȸ���</param>
		/// <returns>ISettingProperty</returns>
		ISettingProperty Clone(bool @readonly, bool deep);
	}
}