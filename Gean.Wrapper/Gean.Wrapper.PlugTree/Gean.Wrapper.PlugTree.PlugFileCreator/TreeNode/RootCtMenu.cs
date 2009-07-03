using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class RootCtMenu : BaseContextMenu
    {
        protected override void MenuItemBuilder()
        {
            ToolStripMenuItem item = new ToolStripMenuItem("&ReSetting");
            this.Items.Add(item);
            item.Click += new EventHandler(item_Click);
        }

        void item_Click(object sender, EventArgs e)
        {
            PlugInformationDialog dialog = new PlugInformationDialog(CoreService.PlugDocument.DocumentElement);
            dialog.ShowDialog(this);
        }
    }
}
