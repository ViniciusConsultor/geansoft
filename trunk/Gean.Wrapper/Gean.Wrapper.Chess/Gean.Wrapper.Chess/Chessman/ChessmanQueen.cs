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
            ChessPoint point = ChessPoint.Empty;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    point = new ChessPoint(4, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    point = new ChessPoint(4, 8);
                    break;
            }
            this.ChessPoints.Push(point);
        }

        public ChessmanQueen(Enums.ChessmanSide side, ChessPoint point)
            : base(Enums.ChessmanType.Queen, side)
        {
            this.ChessPoints.Push(point);
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
