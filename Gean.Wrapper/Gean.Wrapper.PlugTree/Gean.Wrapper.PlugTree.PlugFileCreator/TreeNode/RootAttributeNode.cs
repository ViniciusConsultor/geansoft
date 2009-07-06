using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class RootAttributeNode : TreeNode
    {
        public RootAttributeNode(XmlNode xmlnode)
            : base(xmlnode.LocalName + " | " + xmlnode.Value)
        {
            AttributeCtMenu menu = new AttributeCtMenu(xmlnode as XmlElement);
            this.ContextMenuStrip = menu;
        }
    }
}
