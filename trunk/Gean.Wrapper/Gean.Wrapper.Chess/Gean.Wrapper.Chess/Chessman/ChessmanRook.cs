using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋子：车。
    /// </summary>
    public class ChessmanRook : Chessman
    {
        public static ChessmanRook Rook01 = new ChessmanRook(Enums.ChessmanSide.Black, new ChessPosition(1, 8));
        public static ChessmanRook Rook08 = new ChessmanRook(Enums.ChessmanSide.Black, new ChessPosition(8, 8));
        public static ChessmanRook Rook57 = new ChessmanRook(Enums.ChessmanSide.White, new ChessPosition(1, 1));
        public static ChessmanRook Rook64 = new ChessmanRook(Enums.ChessmanSide.White, new ChessPosition(8, 1));

        public ChessmanRook(Enums.ChessmanSide manSide, ChessPosition position)
        {
            this.ChessmanSide = manSide;
            switch (manSide)
            {
                case Enums.ChessmanSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhiteRook;
                    break;
                case Enums.ChessmanSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackRook;
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
            return this.CurrPosition.GetRookPositions();
        }
    }
}
