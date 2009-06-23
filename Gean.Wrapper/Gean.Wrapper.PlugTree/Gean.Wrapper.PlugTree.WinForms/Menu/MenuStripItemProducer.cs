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
            get { return this._name; }
        }
        private string _name;

        public object CreateObject(Plug plug, object caller)
        {
            _name = (string)plug.Definers["Name"];
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Name = _name;
            menuItem.Image = ((System.Drawing.Image)(ResourcesService.GetObject(_name)));
            menuItem.ShortcutKeys = (Keys)plug.Definers["ShortcutKeys"]; ;
            menuItem.ShowShortcutKeys = Boolean.Parse((string)plug.Definers["ShowShortcutKeys"]);
            menuItem.Text = (string)plug.Definers["Text"]; ;
            menuItem.ToolTipText = (string)plug.Definers["ToolTipText"]; ;

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
