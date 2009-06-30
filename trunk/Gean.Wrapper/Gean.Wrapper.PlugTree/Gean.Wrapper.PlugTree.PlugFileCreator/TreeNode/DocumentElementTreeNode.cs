using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class DocumentElementTreeNode : TreeNode
    {
        public DocumentElementTreeNode(XmlElement element)
            : base(element.LocalName)
        {
            foreach (XmlAttribute attr in element.Attributes)
            {
                TreeNode nameNode = new TreeNode();
                nameNode.Text = attr.LocalName;

                TreeNode valueNode = new TreeNode();
                valueNode.Text = attr.Value;
                nameNode.Nodes.Add(valueNode);
                this.Nodes.Add(nameNode);
            }
            this.Expand();
            DocumentElementContextMenu cm = new DocumentElementContextMenu(element);
            this.ContextMenuStrip = cm;
        }
    }
}
