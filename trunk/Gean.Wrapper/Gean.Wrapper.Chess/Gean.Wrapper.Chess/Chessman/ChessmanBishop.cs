using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanBishop : ChessmanBase
    {
        internal ChessmanBishop(Enums.ChessmanSide side, Enums.ChessSquareSide gridSide)
            : base(Enums.ChessmanType.Bishop, side)
        {
            this.Squares.Add(ChessmanBase.GetOpenningsSquare(side, gridSide, 3, 6));
        }

        internal ChessmanBishop(Enums.ChessmanSide side, ChessSquare square)
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
