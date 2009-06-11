using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public interface IOwnerState
    {
        Enum InternalState { get; }
    }
}
