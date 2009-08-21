using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKing : ChessPiece
    {
        public ChessmanKing(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.King, side)
        {
            ChessPosition point = ChessPosition.Empty;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    point = new ChessPosition(5, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    point = new ChessPosition(5, 8);
                    break;
            }
            this.ChessPositions.Push(point);
        }

        public ChessmanKing(Enums.ChessmanSide side, ChessPosition point)
            : base(Enums.ChessmanType.King, side)
        {
            this.ChessPositions.Push(point);
        }

        public override ChessPosition[] GetEnablePositions()
        {
            throw new NotImplementedException();
        }
    }
}
