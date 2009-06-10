using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    public class ToolStripItemBuilder : IBuilder
    {
        #region IBuilder 成员

        public bool HandleConditions
        {
            get { return true; }
        }

        public object BuildItem(object caller, PlugAtom plugAtom, System.Collections.ArrayList subItems)
        {
            return new ToolStripItemDescriptor(caller, plugAtom, subItems);
        }

        #endregion
    }
}
