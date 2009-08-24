using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKing : Chessman
    {
        public ChessmanKing(Enums.ChessmanSide side) : this(side, ChessPosition.Empty) { }
        public ChessmanKing(Enums.ChessmanSide side, ChessPosition position)
        {
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhiteKing;
                    break;
                case Enums.ChessmanSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackKing;
                    break;
            }
            this.IsCaptured = false;
            this.CurrPosition = this.SetCurrPosition(position);
        }

        protected override ChessPosition SetCurrPosition(ChessPosition position)
        {
            if (position.Equals(ChessPosition.Empty))
            {
                switch (this.ChessmanSide)
                {
                    case Enums.ChessmanSide.White:
                        return new ChessPosition(5, 1);
                    case Enums.ChessmanSide.Black:
                        return new ChessPosition(5, 8);
                    default:
                        throw new ChessmanException(ExString.ChessPositionIsEmpty);
                }
            }
            else
                return position;
        }

        public override ChessPosition[] GetEnablePositions()
        {
            return this.CurrPosition.GetKingPositions();
        }
    }
}
