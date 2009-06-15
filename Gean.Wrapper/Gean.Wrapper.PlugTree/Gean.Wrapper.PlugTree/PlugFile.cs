using System;
using System.Collections.Generic;
using System.Text;
using Gean.Xml;
using System.Xml;
using System.Collections.Specialized;
using System.IO;

namespace Gean.Wrapper.PlugTree
{
    public class PlugFile : AbstractXmlDocument
    {
        public PlugFile(string filePath)
            : base(filePath)
        {
            this.GetRunner();
        }

        private void GetRunner()
        {
            XmlElement element = ((XmlElement)this.DocumentElement.SelectSingleNode("MainDescription"));
            string assName = element.SelectSingleNode("Assembly").InnerText;
            XmlNodeList nodelist = element.SelectSingleNode("Class").ChildNodes;
            List<string> strlist = new List<string>();
            foreach (XmlNode node in nodelist)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                strlist.Add(node.InnerText);
            }
            string path = UtilityFile.GetDirectoryByFilepath(this.FilePath);
            this._Runner = new Runner(Path.Combine(path, assName), strlist.ToArray());
        }

        private Runner _Runner;
        public Runner Runner
        {
            get { return this._Runner; }
        }
    }
}
