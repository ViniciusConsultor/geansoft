using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public delegate void EventHandler<TEventArgs, TSender>(TSender sender, TEventArgs e) where TEventArgs : EventArgs;
}
