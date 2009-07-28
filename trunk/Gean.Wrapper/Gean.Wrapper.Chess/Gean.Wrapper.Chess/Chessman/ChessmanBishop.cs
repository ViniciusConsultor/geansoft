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
            ChessGrid rid = Chessman.GetOpenningsGrid(side, gridSide, 3, 6);
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
        }

        internal ChessmanBishop(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Bishop, side)
        {
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
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
