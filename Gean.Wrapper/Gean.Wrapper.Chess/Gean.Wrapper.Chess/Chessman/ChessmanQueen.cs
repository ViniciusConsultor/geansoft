using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanQueen : Chessman
    {
        public ChessmanQueen(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.Queen, side)
        {
            ChessGrid rid = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    rid = new ChessGrid(4, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    rid = new ChessGrid(4, 8);
                    break;
            }
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
        }

        public ChessmanQueen(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Queen, side)
        {
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "Q";
        }
    }
}
