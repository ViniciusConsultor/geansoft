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
            this.ChessPositions.Push(point);
        }

        public ChessmanPawn(Enums.ChessmanSide side, ChessPosition point)
            : base(Enums.ChessmanType.Pawn, side)
        {
            this.ChessPositions.Push(point);
        }

        public override ChessPosition[] GetEnablePositions()
        {
            return this.ChessPositions.Peek().GetPawnPositions(this.ChessmanSide);
        }
    }
}
