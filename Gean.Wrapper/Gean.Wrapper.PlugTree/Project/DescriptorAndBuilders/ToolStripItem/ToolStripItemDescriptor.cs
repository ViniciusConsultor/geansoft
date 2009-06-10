using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.PlugTree
{
    public sealed class ToolStripItemDescriptor
    {	
        public readonly object Caller;
        public readonly PlugAtom PlugAtom;
		public readonly IList SubItems;

        public ToolStripItemDescriptor(object caller, PlugAtom atom, IList subItems)
		{
			this.Caller = caller;
			this.PlugAtom = atom;
			this.SubItems = subItems;
		}

    }
}
