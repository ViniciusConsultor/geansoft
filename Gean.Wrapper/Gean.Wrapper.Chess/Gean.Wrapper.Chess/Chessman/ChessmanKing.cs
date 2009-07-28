using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKing : Chessman
    {
        internal ChessmanKing(Enums.ChessmanSide side)
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
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
        }

        internal ChessmanKing(Enums.ChessmanSide side, ChessGrid rid)
            : base(Enums.ChessmanType.King, side)
        {
            this.ChessGrids.Push(new Enums.ActionGridPair(rid, Enums.Action.Opennings));
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
