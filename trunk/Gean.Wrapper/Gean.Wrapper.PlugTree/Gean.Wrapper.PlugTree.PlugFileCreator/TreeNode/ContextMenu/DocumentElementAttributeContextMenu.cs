using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class DocumentElementAttributeContextMenu : ElementContextMenu
    {
        protected override void MenuItemBuilder()
        {
            //
        }

        protected override void viewMenu_Click(object sender, EventArgs e)
        {
            string key = CoreService.MainForm.InformationTree.SelectedNode.Text;
            string value = CoreService.MainForm.InformationTree.SelectedNode.Nodes[0].Text;
            AttributeDialog dialog = new AttributeDialog(key, key, value);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XmlAttribute attr = CoreService.PlugDocument.DocumentElement.Attributes[key];
                Debug.Assert(attr != null, "XmlAttribute(" + key + ") is null!");
                attr.InnerText = dialog.Value;
                CoreService.MainForm.InformationTree.SelectedNode.Nodes[0].Text = dialog.Value;
            }
        }
    }
}
