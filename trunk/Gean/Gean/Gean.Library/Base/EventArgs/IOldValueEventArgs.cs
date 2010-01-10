using System;
using System.Collections.Generic;

using System.Text;

namespace Gean
{
    public interface IOldValueEventArgs<T>
    {
        T OldValue { get; }
    }
}
