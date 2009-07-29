using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanBishop : Chessman
    {
        public ChessmanBishop(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
            : base(Enums.ChessmanType.Bishop, side)
        {
            ChessGrid rid = Chessman.GetOpenningsGrid(side, gridSide, 3, 6);
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
        }

        public ChessmanBishop(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Bishop, side)
        {
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
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
