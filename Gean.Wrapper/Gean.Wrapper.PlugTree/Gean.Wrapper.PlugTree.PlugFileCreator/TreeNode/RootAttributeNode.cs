using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class RootAttributeNode : TreeNode
    {
        public RootAttributeNode(XmlAttribute attr)
            : base(attr.LocalName + " | " + attr.Value)
        {
            RootAttributeCtMenu menu = new RootAttributeCtMenu();
            this.ContextMenuStrip = menu;
        }
    }
}
