using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public interface IRangeEventArgs : IIndexEventArgs
    {
        int Count { get; }
    }
}
