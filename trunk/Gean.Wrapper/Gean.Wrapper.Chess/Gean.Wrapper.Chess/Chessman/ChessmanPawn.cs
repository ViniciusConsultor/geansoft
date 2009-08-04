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
            ChessPosition point = ChessPosition.Empty;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    {
                        point = new ChessPosition(column, 2);
                        break;
                    }
                case Enums.ChessmanSide.Black:
                    {
                        point = new ChessPosition(column, 7);
                        break;
                    }
            }
            this.ChessPoints.Push(point);
        }

        public ChessmanPawn(Enums.ChessmanSide side, ChessPosition point)
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
