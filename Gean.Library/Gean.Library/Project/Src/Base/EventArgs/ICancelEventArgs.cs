using System;
using System.Collections.Generic;

using System.Text;

namespace Gean
{
    public interface ICancelEventArgs
    {
        bool IsCanceled { get; }
        void Cancel();
    }
}
