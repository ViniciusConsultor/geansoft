using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanQueen : Chessman
    {
        internal ChessmanQueen(Enums.ChessmanSide side)
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
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
        }

        internal ChessmanQueen(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.Queen, side)
        {
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
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
