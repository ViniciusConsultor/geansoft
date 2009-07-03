using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class RootAttributeCtMenu : BaseContextMenu
    {
        protected override void MenuItemBuilder()
        {
            ToolStripMenuItem deleteMenu = new ToolStripMenuItem("Delete(&D)");
            this.Items.Add(deleteMenu);
            deleteMenu.Click += new EventHandler(deleteMenu_Click);

            ToolStripMenuItem viewMenu = new ToolStripMenuItem("View(&V)");
            this.Items.Add(viewMenu);
            viewMenu.Click += new EventHandler(viewMenu_Click);
        }

        void deleteMenu_Click(object sender, EventArgs e)
        {
            int i = CoreService.MainForm.InformationTree.Nodes[0].Nodes.IndexOf(
                                CoreService.MainForm.InformationTree.SelectedNode);
            string[] pair = CoreService.MainForm.InformationTree.SelectedNode.Text.Split('|');
            string key = pair[0].Trim();
            string value = pair[1].Trim();

            CoreService.MainForm.InformationTree.Nodes[0].Nodes.RemoveAt(i);
            CoreService.PlugDocument.DocumentElement.Attributes.RemoveNamedItem(key);
        }

        protected virtual void viewMenu_Click(object sender, EventArgs e)
        {
            string[] pair = CoreService.MainForm.InformationTree.SelectedNode.Text.Split('|');
            string key = pair[0].Trim();
            string value = pair[1].Trim();
            AttributeDialog dialog = new AttributeDialog(key, key, value);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XmlAttribute attr = CoreService.PlugDocument.DocumentElement.Attributes[key];
                Debug.Assert(attr != null, "XmlAttribute(" + key + ") is null!");
                attr.InnerText = dialog.Value;
                CoreService.MainForm.InformationTree.SelectedNode.Text = key + " | " + dialog.Value;
            }
        }
    }
}
