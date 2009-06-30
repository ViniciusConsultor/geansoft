using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class DocumentElementContextMenu : ContextMenuStrip
    {
        ToolStripMenuItem addMenu = new ToolStripMenuItem("增加");
        ToolStripMenuItem deleteMenu = new ToolStripMenuItem("删除");
        ToolStripMenuItem editMenu = new ToolStripMenuItem("修改");

        public DocumentElementContextMenu()
        {
            this.Items.Add(addMenu);
            this.Items.Add(deleteMenu);
            this.Items.Add(editMenu);

            addMenu.Click += new EventHandler(addMenu_Click);
            deleteMenu.Click += new EventHandler(deleteMenu_Click);
            editMenu.Click += new EventHandler(editMenu_Click);
        }

        void editMenu_Click(object sender, EventArgs e)
        {
            string key = CoreService.MainForm.InformationTree.SelectedNode.Text;
            string value = CoreService.MainForm.InformationTree.SelectedNode.Nodes[0].Text;
            AttributeDialog dialog = new AttributeDialog(key, key, value);
            dialog.StartPosition = FormStartPosition.CenterParent;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                XmlAttribute attr = CoreService.PlugDocument.DocumentElement.Attributes[key];
                Debug.Assert(attr != null, "XmlAttribute(" + key + ") is null!");
                attr.InnerText = dialog.Value;
                CoreService.MainForm.InformationTree.SelectedNode.Nodes[0].Text = dialog.Value;
            }
        }

        void deleteMenu_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void addMenu_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
