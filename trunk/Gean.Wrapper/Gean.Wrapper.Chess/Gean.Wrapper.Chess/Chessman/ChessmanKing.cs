using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKing : Chessman
    {
        public ChessmanKing(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.King, side)
        {
            ChessGrid rid = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    rid = new ChessGrid(5, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    rid = new ChessGrid(5, 8);
                    break;
            }
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
        }

        public ChessmanKing(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.King, side)
        {
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "K";
        }
    }
}
