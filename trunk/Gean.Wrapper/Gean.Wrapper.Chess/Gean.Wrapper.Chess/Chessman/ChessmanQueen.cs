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
            ChessGrid point = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    point = new ChessGrid(4, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    point = new ChessGrid(4, 8);
                    break;
            }
            this.ChessGrids.Add(point);
        }

        internal ChessmanQueen(Enums.ChessmanSide side, ChessGrid point)
            : base(Enums.ChessmanType.Queen, side)
        {
            this.ChessGrids.Add(point);
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
