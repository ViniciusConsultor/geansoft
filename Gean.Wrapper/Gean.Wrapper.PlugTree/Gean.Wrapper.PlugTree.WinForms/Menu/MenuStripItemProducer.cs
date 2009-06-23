using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.WinForms
{
    public class MenuStripItemProducer : IProducer 
    {
        public string Name
        {
            get { return this._Name; }
        }
        private string _Name;

        public object CreateObject(Plug plug, object caller)
        {
            this._Name = (string)plug.Definers["Name"];
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = (string)plug.Definers["Text"];
            menuItem.Click += new EventHandler(_MenuItem_Click);
            return menuItem;
        }

        void _MenuItem_Click(object sender, EventArgs e)
        {
            IRun run = PlugTree.Runners[this.Name];
            run.Run();
        }
    }
}
