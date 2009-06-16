using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gean.Client
{
    public class ToolStripMenuItemService
    {
        public static ToolStripMenuItem Parse(Definers definers)
        {
            ToolStripMenuItem menustrip = new ToolStripMenuItem();
            foreach (KeyValuePair<string, object> item in definers)
            {
                switch (item.Key.ToLowerInvariant())
                {
                    case "type":
                        break;
                    case "class":
                        break;
                    case "shortcut":
                        break;
                    case "text":
                        break;
                    default:
                        break;
                }
            }
            return menustrip;
        }
    }
}
