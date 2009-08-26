using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKing : Chessman
    {
        public static ChessmanKing BlackKing = new ChessmanKing(Enums.GameSide.Black);
        public static ChessmanKing WhiteKing = new ChessmanKing(Enums.GameSide.White);
        public ChessmanKing(Enums.GameSide side) : this(side, ChessPosition.Empty) { }
        public ChessmanKing(Enums.GameSide side, ChessPosition position)
        {
            this.GameSide = side;
            switch (side)
            {
                case Enums.GameSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhiteKing;
                    break;
                case Enums.GameSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackKing;
                    break;
            }
            this.IsCaptured = false;
            this._currPosition = this.SetCurrPosition(position);
        }

        protected override ChessPosition SetCurrPosition(ChessPosition position)
        {
            if (position == ChessPosition.Empty)
            {
                switch (this.GameSide)
                {
                    case Enums.GameSide.White:
                        return new ChessPosition(5, 1);
                    case Enums.GameSide.Black:
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
