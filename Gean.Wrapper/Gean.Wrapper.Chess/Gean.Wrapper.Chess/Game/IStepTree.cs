using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public interface IStepTree
    {
        object Parent { get; set; }
        ChessSequence Items { get; set; }
        bool HasChildren { get; }
    }
}
