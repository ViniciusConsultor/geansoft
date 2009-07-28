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
            ChessGrid rid = Chessman.GetOpenningsGrid(side, gridSide, 2, 7);
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
        }

        internal ChessmanKnight(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Knight, side)
        {
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
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
