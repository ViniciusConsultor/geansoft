using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    class ChessmanQueen : ChessmanBase
    {
        public ChessmanQueen(ChessboardGrid grid, Enums.ChessmanSide side)
            : base(grid, Enums.ChessmanType.Queen)
        {
            this.ChessmanSide = side;
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "Q";
        }
    }
}
