using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Gean.Wrapper.PlugTree
{
#if DEBUG
    /// <summary>
    /// 否决的！禁止正式代码中使用。该类在Release状态时不存在。
    /// </summary>
    public static class DemoService
    {
        /// <summary>
        /// 从给定的一个XML文件中扫描所有的PlugPath
        /// </summary>
        /// <param name="doc">一个Plug的XML文件</param>
        public static PlugPath ScanPlugPath(XmlDocument doc)
        {
            PlugPath plugpath = new PlugPath("Application");

            XmlNodeList nodelist = doc.DocumentElement.SelectNodes("Path");
            foreach (XmlNode node in nodelist)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement element = (XmlElement)node;
                PlugPath.Install(element, plugpath);
            }
            return plugpath;
        }

    }
#endif
}
