using System;
using System.Collections.Generic;

using System.Text;

namespace Gean
{
    public interface IValueEventArgs<T>
    {
        T Value { get; }
    }
}
