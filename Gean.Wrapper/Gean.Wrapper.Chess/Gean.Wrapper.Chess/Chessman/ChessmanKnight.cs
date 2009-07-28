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
            this.ChessGrids.Push(Chessman.GetOpenningspoint(side, gridSide, 2, 7));
        }

        internal ChessmanKnight(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Knight, side)
        {
            this.ChessGrids.Push(rid);
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
