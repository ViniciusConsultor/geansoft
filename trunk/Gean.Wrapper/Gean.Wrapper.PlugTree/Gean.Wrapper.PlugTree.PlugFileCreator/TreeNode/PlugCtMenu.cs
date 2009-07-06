using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class PlugCtMenu : AttributeCtMenu
    {
        public PlugCtMenu(XmlElement ele)
            : base(ele)
        {

        }

        protected override void MenuItemBuilder()
        {
            ToolStripMenuItem addItem = new ToolStripMenuItem("&Add");
            this.Items.Add(addItem);
            addItem.Click += new EventHandler(addItem_Click);
            base.MenuItemBuilder();
        }

        void addItem_Click(object sender, EventArgs e)
        {
            PathDialog dialog = new PathDialog();
            dialog.StartPosition = FormStartPosition.CenterScreen;
            dialog.ShowDialog(this);
        }
    }
}
