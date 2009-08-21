using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKnight : ChessPiece
    {
        public ChessmanKnight(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
            : base(Enums.ChessmanType.Knight, side)
        {
            ChessPosition point = ChessPiece.GetOpenningsPoint(side, gridSide, 2, 7);
            this.ChessPositions.Push(point);
        }

        public ChessmanKnight(Enums.ChessmanSide side, ChessPosition point)
            : base(Enums.ChessmanType.Knight, side)
        {
            this.ChessPositions.Push(point);
        }

        public override ChessPosition[] GetEnablePositions()
        {
            throw new NotImplementedException();
        }
    }
}
