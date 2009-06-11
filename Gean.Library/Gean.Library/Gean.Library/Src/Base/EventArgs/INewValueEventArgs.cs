using System;
using System.Collections.Generic;

using System.Text;

namespace Gean
{
    public interface INewValueEventArgs<T>
    {
        T NewValue { get; set; }
    }
}
