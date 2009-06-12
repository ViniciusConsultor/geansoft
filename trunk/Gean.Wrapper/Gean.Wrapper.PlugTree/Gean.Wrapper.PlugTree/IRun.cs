using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    public interface IRun
    {
        object Owner { get; set; }
        void Run();
        event EventHandler OwnerChanged;
    }
}
