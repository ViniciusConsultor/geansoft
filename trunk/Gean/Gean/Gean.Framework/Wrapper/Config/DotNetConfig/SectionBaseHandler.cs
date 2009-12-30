using System.Configuration;
using System.Reflection;
using System.Xml;

namespace Gean.Config.DotNetConfig
{
	/// <summary>
	/// ���ýڻ��������࣬�̳��� <see cref="ConfigurationSection"/>
	/// </summary>
	/// <typeparam name="T">������</typeparam>
	/// <remarks>
	/// ע���� <see cref="BaseConfigurationElement"/> ������
	/// </remarks>
	public abstract class SectionBaseHandler<T> : ConfigurationSection where T : SectionBaseHandler<T>
	{
		/// <summary>
		/// ��ȡ��ǰ���ý�ʵ��
		/// </summary>
		public static T Current {
			get { return GroupHandler.GetSection<T>(false); }
		}

		/// <summary>
		/// ��ȡһ��ֵ����ֵָʾ�����л��������Ƿ�����δ֪����
		/// </summary>
		/// <param name="name">�޷�ʶ������Ե�����</param>
		/// <param name="value">�޷�ʶ������Ե�ֵ</param>
		/// <returns>��������л�����������δ֪���ԣ���Ϊ<c>true</c></returns>
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value) {
			return this.OnDeserializeUnrecognizedFlag;
		}

		/// <summary>
		/// ��ȡһ��ֵ����ֵָʾ�����л��������Ƿ�����δ֪Ԫ��
		/// </summary>
		/// <param name="elementName">δ֪����Ԫ�ص�����</param>
		/// <param name="reader">���ڷ����л��� <seealso cref="XmlReader"/> ����</param>
		/// <returns>��������л�����������δ֪Ԫ�أ���Ϊ true</returns>
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader) {
			return this.OnDeserializeUnrecognizedFlag;
		}

		/// <summary>
		/// �Ƿ�����δ֪�����Ի�Ԫ��
		/// </summary>
		/// <remarks>
		///		<para>���������Ҫ����δ��������ԣ��������д������</para>
		/// </remarks>
		protected virtual bool OnDeserializeUnrecognizedFlag {
			get { return false; }
		}

		/// <summary>
		/// �����ýڶ�Ӧ��Xml
		/// </summary>
		public virtual string OuterXml {
			get { return this.outerXml; }
		}

		private string outerXml;

		/// <summary>
		/// ��ȡ�����ļ��е� XML
		/// </summary>
		/// <param name="reader">�������ļ��н��ж�ȡ������ <seealso cref="XmlReader"/></param>
		/// <param name="serializeCollectionKey">Ϊ <c>true</c>����ֻ���л����ϵļ����ԣ�����Ϊ <c>false</c></param>
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey) {
			FieldInfo field = reader.GetType().GetField("_rawXml", FieldMemberInfo.FieldBindingFlags);
			this.outerXml = (string)field.GetValue(reader);
			base.DeserializeElement(reader, serializeCollectionKey);
		}
	}
}