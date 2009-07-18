using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanBishop : Chessman
    {
        internal ChessmanBishop(Enums.ChessmanSide side, Enums.ChessboardGridSide gridSide)
            : base(Enums.ChessmanType.Bishop, side)
        {
            this.Squares.Add(Chessman.GetOpenningsSquare(side, gridSide, 3, 6));
        }

        internal ChessmanBishop(Enums.ChessmanSide side, Square square)
            : base(Enums.ChessmanType.Bishop, side)
        {
            this.Squares.Add(square);
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
