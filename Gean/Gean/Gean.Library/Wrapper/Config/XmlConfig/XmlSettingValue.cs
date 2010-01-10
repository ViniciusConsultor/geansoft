using System.Web;
using System.Xml;

namespace Gean.Config.XmlConfig
{
    /// <summary>
    /// ʹ��XMLʵ��<see cref="SettingValue"/>
    /// </summary>
    public class XmlSettingValue : SettingValue
    {
        #region constructor

        /// <summary>
        /// ʹ��name/value����ʽ��ʼ��
        /// </summary>
        /// <param name="name">����ֵ��</param>
        /// <param name="value">����ֵ</param>
        /// <param name="readonly">�Ƿ�ֻ��</param>
        public XmlSettingValue(string name, string @value, bool @readonly) : base(name, @value, @readonly) { }

        /// <summary>
        /// ʹ��XmlNode��ʼ��
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="readonly">�Ƿ�ֻ��</param>
        public XmlSettingValue(XmlNode xmlNode, bool @readonly)
            : base(null, null, @readonly)
        {
            this.name = xmlNode.Name;
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Text)
                {
                    this.value = node.Value;
                    break;
                }
            }
        }

        #endregion

        /// <summary>
        /// ��������ֵʵ��
        /// </summary>
        /// <param name="name">����ֵ��</param>
        /// <param name="value">����ֵ</param>
        /// <param name="readonly">�Ƿ�ֻ��</param>
        /// <returns>SettingValue</returns>
        protected override SettingValue CreateSettingValue(string name, string value, bool @readonly)
        {
            return new XmlSettingValue(name, value, @readonly);
        }

        /// <summary>
        /// ת�����ַ�����ʽ
        /// </summary>
        /// <returns>�ַ���</returns>
        public override string ToString()
        {
            return string.Format("{0}=\"{1}\"", this.Name, HttpUtility.HtmlAttributeEncode(this.Value));
        }
    }
}