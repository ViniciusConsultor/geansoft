using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanBishop : Chessman
    {
        public ChessmanBishop(Enums.ChessmanSide side) : this(side, ChessPosition.Empty) { }
        public ChessmanBishop(Enums.ChessmanSide side, ChessPosition position)
        {
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhiteQueen;
                    break;
                case Enums.ChessmanSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackQueen;
                    break;
            }
            this.IsCaptured = false;
            this.CurrPosition = this.SetCurrPosition(position);
        }

        protected override ChessPosition SetCurrPosition(ChessPosition position)
        {
            if (position.Equals(ChessPosition.Empty))
            {
                if (this.ChessmanSide == Enums.ChessmanSide.White)
                    return new ChessPosition(4, 1);
                else
                    return new ChessPosition(4, 8);
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
