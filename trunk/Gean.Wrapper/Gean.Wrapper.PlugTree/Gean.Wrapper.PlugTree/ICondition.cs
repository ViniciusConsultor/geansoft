using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    public interface ICondition
    {
        string Name { get; }
        object Owner { get; }
        bool SetByCondition(PlugPath path);
    }
}
