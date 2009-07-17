using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    class ChessmanBishop : Chessman
    {
        public ChessmanBishop(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.Bishop, side)
        {
            this.ChessmanSide = side;
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "B";
        }
    }
}
