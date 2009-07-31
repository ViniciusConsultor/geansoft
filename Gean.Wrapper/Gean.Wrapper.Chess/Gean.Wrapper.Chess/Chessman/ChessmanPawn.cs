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
            ChessPoint point = ChessPoint.Empty;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    {
                        point = new ChessPoint(column, 2);
                        break;
                    }
                case Enums.ChessmanSide.Black:
                    {
                        point = new ChessPoint(column, 7);
                        break;
                    }
            }
            this.ChessPoints.Push(point);
        }

        public ChessmanPawn(Enums.ChessmanSide side, ChessPoint point)
            : base(Enums.ChessmanType.Pawn, side)
        {
            this.ChessPoints.Push(point);
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
