using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanPawn : Chessman
    {
        internal ChessmanPawn(Enums.ChessmanSide side, int column)
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
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
        }

        internal ChessmanPawn(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Pawn, side)
        {
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
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
