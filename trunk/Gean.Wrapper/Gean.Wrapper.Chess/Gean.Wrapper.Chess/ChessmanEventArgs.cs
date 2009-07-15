using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public abstract class ChessmanEventArgs : EventArgs
    {
        public ChessmanBase Chessman { get; set; }
        public ChessmanEventArgs(ChessmanBase chessman)
        {
            this.Chessman = chessman;
        }
    }

}
