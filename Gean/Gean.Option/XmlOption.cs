using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Gean.Options;
using NLog;
using Gean.Options.Common;

namespace Gean.Options
{
    /// <summary>
    /// 内容保存在XML中的Option的抽象类
    /// </summary>
    public abstract class XmlOption : Option<XmlElement>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Gets or sets 选项数据所在的XML文件中的Element节点。
        /// </summary>
        /// <value>The element.</value>
        protected XmlElement Element { get; private set; }

        /// <summary>
        /// 初始化选项数据
        /// </summary>
        /// <param name="source">The source.</param>
        protected override void Initializes(XmlElement source)
        {
            this.Element = source;
            base.Initializes(source);
        }

        /// <summary>
        /// 保存设定的选项值
        /// </summary>
        /// <returns></returns>
        public override bool Save()
        {
            try
            {
                this.GetChangedDatagram();
                string klass = this.Element.GetAttribute("class");
                OptionXmlFile file = OptionService.ME.XmlFileMap[klass];
                file.Save();
                logger.Info(string.Format("选项数据所在的XmlElement保存成功。{0}", file.FilePath));
                return true;
            }
            catch (Exception e)
            {
                logger.Error(string.Format("选项数据所在的XmlElement保存时发生异常。数据内容:{0}。\r\n异常信息:{1}", Element.OuterXml, e.Message), e);
                return false;
            }

        }

    }
}
