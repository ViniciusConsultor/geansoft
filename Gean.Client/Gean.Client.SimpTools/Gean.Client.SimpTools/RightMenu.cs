using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gean.Client.SimpTools
{
    public partial class RightMenu : ContextMenuStrip
    {
        public RightMenu()
        {
            SimpToolsServices.SimpApplication[] sas = SimpToolsServices.ReadApplicationFromDirectory();
            foreach (var item in sas)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem();
                menu.Text = item.Text;
                menu.ToolTipText = item.ToolTip;
                menu.Name = item.Name;
                menu.Click += new EventHandler(menu_Click);
                this.Items.Add(menu);
            }
        }

        void menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            SimpToolsServices.StartApplication(menuItem.Name);
        }
    }
}
