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
        public static ChessmanRook Rook01 = new ChessmanRook(Enums.GameSide.Black, new ChessPosition(1, 8));
        public static ChessmanRook Rook08 = new ChessmanRook(Enums.GameSide.Black, new ChessPosition(8, 8));
        public static ChessmanRook Rook57 = new ChessmanRook(Enums.GameSide.White, new ChessPosition(1, 1));
        public static ChessmanRook Rook64 = new ChessmanRook(Enums.GameSide.White, new ChessPosition(8, 1));

        public ChessmanRook(Enums.GameSide manSide, ChessPosition position)
        {
            this.GameSide = manSide;
            switch (manSide)
            {
                case Enums.GameSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhiteRook;
                    break;
                case Enums.GameSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackRook;
                    break;
            }
            this.IsCaptured = false;
            this._currPosition = this.SetCurrPosition(position);
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
