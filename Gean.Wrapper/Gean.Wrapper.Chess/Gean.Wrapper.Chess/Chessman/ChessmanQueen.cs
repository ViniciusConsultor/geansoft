using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanQueen : Chessman
    {
        public static ChessmanQueen BlackQueen = new ChessmanQueen(Enums.GameSide.Black);
        public static ChessmanQueen WhiteQueen = new ChessmanQueen(Enums.GameSide.White);
        public ChessmanQueen(Enums.GameSide side) : this(side, ChessPosition.Empty) { }
        public ChessmanQueen(Enums.GameSide side, ChessPosition position)
        {
            this.GameSide = side;
            switch (side)
            {
                case Enums.GameSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhiteQueen;
                    break;
                case Enums.GameSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackQueen;
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
                        return new ChessPosition(4, 1);
                    case Enums.GameSide.Black:
                        return new ChessPosition(4, 8);
                    default:
                        throw new ChessmanException(ExString.ChessPositionIsEmpty);
                }
            }
            else
                return position;
        }

        public override ChessPosition[] GetEnablePositions()
        {
            return this.CurrPosition.GetQueenPositions();
        }
    }
}
