using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanBishop : Chessman
    {
        internal ChessmanBishop(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
            : base(Enums.ChessmanType.Bishop, side)
        {
            this.ChessGrids.Add(Chessman.GetOpenningspoint(side, gridSide, 3, 6));
        }

        internal ChessmanBishop(Enums.ChessmanSide side, ChessGrid point)
            : base(Enums.ChessmanType.Bishop, side)
        {
            this.ChessGrids.Add(point);
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
