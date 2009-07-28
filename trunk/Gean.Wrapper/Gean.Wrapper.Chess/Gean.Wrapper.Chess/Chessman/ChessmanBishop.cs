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
            ChessStep step = new ChessStep(Enums.Action.Opennings, this, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
        }

        internal ChessmanBishop(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Bishop, side)
        {
            ChessStep step = new ChessStep(Enums.Action.Opennings, this, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
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
