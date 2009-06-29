using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    class DocumentElementContextMenu : ContextMenuStrip
    {
        ToolStripMenuItem addMenu = new ToolStripMenuItem("增加");
        ToolStripMenuItem deleteMenu = new ToolStripMenuItem("删除");
        ToolStripMenuItem editMenu = new ToolStripMenuItem("修改");

        public XmlElement Element { get; private set; }
        public DocumentElementContextMenu(XmlElement element)
        {
            this.Element = element;
            this.Items.Add(addMenu);
            this.Items.Add(deleteMenu);
            this.Items.Add(editMenu);

            addMenu.Click += new EventHandler(addMenu_Click);
            deleteMenu.Click += new EventHandler(deleteMenu_Click);
            editMenu.Click += new EventHandler(editMenu_Click);
        }

        void editMenu_Click(object sender, EventArgs e)
        {
            
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
