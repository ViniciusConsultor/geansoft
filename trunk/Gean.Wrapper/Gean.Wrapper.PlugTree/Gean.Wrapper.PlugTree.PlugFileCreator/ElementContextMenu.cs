using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    abstract class ElementContextMenu : ContextMenuStrip
    {

        public ElementContextMenu()
        {
            this.MenuItemBuilder();
            this.Items.Add(new ToolStripSeparator());
            this.Items.Add(viewMenu);
            viewMenu.Click += new EventHandler(viewMenu_Click);
        }


        ToolStripMenuItem viewMenu = new ToolStripMenuItem("View(&V)");
        abstract protected void viewMenu_Click(object sender, EventArgs e);
        abstract protected void MenuItemBuilder();

    }
}
