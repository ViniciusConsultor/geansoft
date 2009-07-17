using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public abstract class ChessmanEventArgs : EventArgs
    {
        public Chessman Chessman { get; set; }
        public ChessmanEventArgs(Chessman chessman)
        {
            this.Chessman = chessman;
        }
    }

}
