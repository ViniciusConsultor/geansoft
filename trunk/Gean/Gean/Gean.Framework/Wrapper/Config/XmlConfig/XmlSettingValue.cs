using System.Web;
using System.Xml;

namespace Gean.Config.XmlConfig
{
    /// <summary>
    /// 使用XML实现<see cref="SettingValue"/>
    /// </summary>
    public class XmlSettingValue : SettingValue
    {
        #region constructor

        /// <summary>
        /// 使用name/value的形式初始化
        /// </summary>
        /// <param name="name">配置值名</param>
        /// <param name="value">配置值</param>
        /// <param name="readonly">是否只读</param>
        public XmlSettingValue(string name, string @value, bool @readonly) : base(name, @value, @readonly) { }

        /// <summary>
        /// 使用XmlNode初始化
        /// </summary>
        /// <param name="xmlNode">XmlNode</param>
        /// <param name="readonly">是否只读</param>
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
        /// 创建配置值实例
        /// </summary>
        /// <param name="name">配置值名</param>
        /// <param name="value">配置值</param>
        /// <param name="readonly">是否只读</param>
        /// <returns>SettingValue</returns>
        protected override SettingValue CreateSettingValue(string name, string value, bool @readonly)
        {
            return new XmlSettingValue(name, value, @readonly);
        }

        /// <summary>
        /// 转换成字符串格式
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            return string.Format("{0}=\"{1}\"", this.Name, HttpUtility.HtmlAttributeEncode(this.Value));
        }
    }
}