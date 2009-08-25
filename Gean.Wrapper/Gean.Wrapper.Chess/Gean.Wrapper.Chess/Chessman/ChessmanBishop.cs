using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanBishop : Chessman
    {

        public static ChessmanBishop Bishop03 = new ChessmanBishop(Enums.ChessmanSide.Black, new ChessPosition(3, 8));
        public static ChessmanBishop Bishop06 = new ChessmanBishop(Enums.ChessmanSide.Black, new ChessPosition(6, 8));
        public static ChessmanBishop Bishop59 = new ChessmanBishop(Enums.ChessmanSide.White, new ChessPosition(3, 1));
        public static ChessmanBishop Bishop62 = new ChessmanBishop(Enums.ChessmanSide.White, new ChessPosition(6, 1));

        public ChessmanBishop(Enums.ChessmanSide manSide, ChessPosition position)
        {
            this.ChessmanSide = manSide;
            switch (manSide)
            {
                case Enums.ChessmanSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhiteBishop;
                    break;
                case Enums.ChessmanSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackBishop;
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
            return this.CurrPosition.GetBishopPositions();
        }

    }
}
