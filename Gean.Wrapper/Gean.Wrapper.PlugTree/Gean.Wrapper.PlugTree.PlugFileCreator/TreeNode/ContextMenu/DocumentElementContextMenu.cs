using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Diagnostics;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class DocumentElementContextMenu : ElementContextMenu
    {
        protected override void viewMenu_Click(object sender, EventArgs e)
        {
        }

        protected override void MenuItemBuilder()
        {
            this.viewMenu.Enabled = false;
        }
    }
}
