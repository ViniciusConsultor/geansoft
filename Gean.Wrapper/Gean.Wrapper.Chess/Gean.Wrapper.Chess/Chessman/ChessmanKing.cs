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
            ChessPoint point = ChessPoint.Empty;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    point = new ChessPoint(5, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    point = new ChessPoint(5, 8);
                    break;
            }
            this.ChessPoints.Push(point);
        }

        public ChessmanKing(Enums.ChessmanSide side, ChessPoint point)
            : base(Enums.ChessmanType.King, side)
        {
            this.ChessPoints.Push(point);
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
