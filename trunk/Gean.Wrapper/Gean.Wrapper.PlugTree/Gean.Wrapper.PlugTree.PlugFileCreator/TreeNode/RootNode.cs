using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class RootNode : TreeNode
    {
        public RootNode(XmlElement element)
            : base(element.LocalName)
        {
            RootCtMenu menu = new RootCtMenu();
            foreach (XmlAttribute attr in element.Attributes)
            {
                RootAttributeNode node = new RootAttributeNode(attr);
                this.Nodes.Add(node);
            }
            this.ContextMenuStrip = menu;
            this.Expand();
        }
    }
}
