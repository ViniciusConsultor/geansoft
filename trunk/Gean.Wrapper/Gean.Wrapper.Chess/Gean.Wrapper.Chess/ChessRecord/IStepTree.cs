using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace Gean.Wrapper.Chess
{
    public interface IStepTree
    {
        object Parent { get; set; }
        bool HasChildren { get; }
        ChessSequence Items { get; set; }
        
    }
}
