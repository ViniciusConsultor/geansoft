using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class PlugNode : TreeNode
    {
        public PlugNode(XmlNode xmlnode)
            : base(xmlnode.LocalName)
        {
            this.Element = xmlnode as XmlElement;
            PlugCtMenu menuitem = new PlugCtMenu(xmlnode as XmlElement);
            this.ContextMenuStrip = menuitem;
            this.LoadPath();
        }

        protected XmlElement Element { get; set; }

        private void LoadPath()
        {
            foreach (XmlNode node in this.Element.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                if (node.LocalName != "Path")
                {
                    continue;
                }
                XmlElement ele = (XmlElement)node;
                PathNode pathnode = new PathNode(node);
                this.Nodes.Add(pathnode);
            }
        }

    }
}
