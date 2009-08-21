using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    abstract class BaseContextMenu : ContextMenuStrip
    {

        public BaseContextMenu()
        {
            this.MenuItemBuilder();
        }

        abstract protected void MenuItemBuilder();
    }
}
