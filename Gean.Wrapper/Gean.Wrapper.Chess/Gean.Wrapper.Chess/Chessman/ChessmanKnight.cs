using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKnight : Chessman
    {
        internal ChessmanKnight(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
            : base(Enums.ChessmanType.Knight, side)
        {
            this.ChessGrids.Add(Chessman.GetOpenningspoint(side, gridSide, 2, 7));
        }

        internal ChessmanKnight(Enums.ChessmanSide side, ChessGrid point)
            : base(Enums.ChessmanType.Knight, side)
        {
            this.ChessGrids.Add(point);
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
