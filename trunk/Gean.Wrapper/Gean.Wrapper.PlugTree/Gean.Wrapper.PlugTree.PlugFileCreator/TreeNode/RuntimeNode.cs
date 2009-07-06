using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class RuntimeNode : TreeNode
    {
        public RuntimeNode(XmlNode node)
            : base(node.LocalName)
        {
            TreeNode treenode = new TreeNode();

            foreach (XmlNode xmlnode in node.ChildNodes)
            {
                if (xmlnode.NodeType == XmlNodeType.Element)
                {

                }
            }

        }
    }
}
