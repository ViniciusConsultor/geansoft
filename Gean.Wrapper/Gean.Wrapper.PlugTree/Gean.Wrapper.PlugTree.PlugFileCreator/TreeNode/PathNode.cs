using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class PathNode : TreeNode
    {
        public PathNode(XmlNode xmlnode)
            : base(xmlnode.LocalName)
        {
            AttributeCtMenu menuitem = new AttributeCtMenu(xmlnode as XmlElement);
            this.ContextMenuStrip = menuitem;
        }

    }
}
