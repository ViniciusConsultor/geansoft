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
            ChessGrid point = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    point = new ChessGrid(5, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    point = new ChessGrid(5, 8);
                    break;
            }
            this.ChessGrids.Add(point);
        }

        internal ChessmanKing(Enums.ChessmanSide side, ChessGrid point)
            : base(Enums.ChessmanType.King, side)
        {
            this.ChessGrids.Add(point);
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
