using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKnight : Chessman
    {
        public static ChessmanKnight Knight02 = new ChessmanKnight(Enums.ChessmanSide.Black, new ChessPosition(2, 8));
        public static ChessmanKnight Knight07 = new ChessmanKnight(Enums.ChessmanSide.Black, new ChessPosition(7, 8));
        public static ChessmanKnight Knight58 = new ChessmanKnight(Enums.ChessmanSide.White, new ChessPosition(2, 1));
        public static ChessmanKnight Knight63 = new ChessmanKnight(Enums.ChessmanSide.White, new ChessPosition(7, 1));

        public ChessmanKnight(Enums.ChessmanSide manSide, ChessPosition position)
        {
            this.ChessmanSide = manSide;
            switch (manSide)
            {
                case Enums.ChessmanSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhiteKnight;
                    break;
                case Enums.ChessmanSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackKnight;
                    break;
            }
            this.IsCaptured = false;
            this.CurrPosition = this.SetCurrPosition(position);
        }

        protected override ChessPosition SetCurrPosition(ChessPosition position)
        {
            return position;
        }

        public override ChessPosition[] GetEnablePositions()
        {
            return this.CurrPosition.GetKnightPositions();
        }    }
}
