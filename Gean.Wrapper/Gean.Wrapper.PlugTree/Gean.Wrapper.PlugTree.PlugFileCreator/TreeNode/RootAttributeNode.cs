using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class RootAttributeNode : TreeNode
    {
        public RootAttributeNode(XmlNode node)
            : base(node.LocalName + " | " + node.Value)
        {
            RootAttributeCtMenu menu = new RootAttributeCtMenu();
            this.ContextMenuStrip = menu;
        }
    }
}
