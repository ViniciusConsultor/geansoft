using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanPawn : Chessman
    {
        public ChessmanPawn(Enums.ChessmanSide side, int column)
            : base(Enums.ChessmanType.Pawn, side)
        {
            ChessGrid rid = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    {
                        rid = new ChessGrid(column, 2);
                        break;
                    }
                case Enums.ChessmanSide.Black:
                    {
                        rid = new ChessGrid(column, 7);
                        break;
                    }
            }
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
        }

        public ChessmanPawn(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Pawn, side)
        {
            ChessStep step = new ChessStep(Enums.Action.Opennings, this.ChessmanType, ChessGrid.Empty, rid);
            this.ChessSteps.Push(step);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "P";
        }
    }
}
