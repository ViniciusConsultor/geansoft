using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class ProducerNode : TreeNode
    {
        public ProducerNode(XmlNode xmlnode)
            : base(xmlnode.LocalName)
        {

        }
    }
}
