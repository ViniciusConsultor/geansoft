using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKnight : ChessmanBase
    {
        internal ChessmanKnight(Enums.ChessmanSide side, Enums.ChessSquareSide gridSide)
            : base(Enums.ChessmanType.Knight, side)
        {
            this.Squares.Add(ChessmanBase.GetOpenningsSquare(side, gridSide, 2, 7));
        }

        internal ChessmanKnight(Enums.ChessmanSide side, ChessSquare square)
            : base(Enums.ChessmanType.Knight, side)
        {
            this.Squares.Add(square);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "N";
        }
    }
}
