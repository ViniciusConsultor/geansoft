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
            ChessPosition point = ChessPosition.Empty;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    point = new ChessPosition(4, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    point = new ChessPosition(4, 8);
                    break;
            }
            this.ChessPoints.Push(point);
        }

        public ChessmanQueen(Enums.ChessmanSide side, ChessPosition point)
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
