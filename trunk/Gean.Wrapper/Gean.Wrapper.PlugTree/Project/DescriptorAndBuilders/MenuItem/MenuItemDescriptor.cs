using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// Represents a menu item. These objects are created by the MenuItemDoozer and
    /// then converted into GUI-toolkit-specific objects by the MenuService.
    /// </summary>
    public sealed class MenuItemDescriptor
    {
        public readonly object Caller;
        public readonly PlugAtom PlugAtom;
        public readonly IList SubItems;

        public MenuItemDescriptor(object caller, PlugAtom plugAtom, IList subItems)
        {
            this.Caller = caller;
            this.PlugAtom = plugAtom;
            this.SubItems = subItems;
        }
    }
}
