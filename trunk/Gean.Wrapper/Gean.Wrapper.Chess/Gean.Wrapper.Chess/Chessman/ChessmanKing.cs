using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKing : Chessman
    {
        public static ChessmanKing NewBlackKing = new ChessmanKing(Enums.GameSide.Black);
        public static ChessmanKing NewWhiteKing = new ChessmanKing(Enums.GameSide.White);

        public ChessmanKing(Enums.GameSide side) : this(side, ChessPosition.Empty) { }
        public ChessmanKing(Enums.GameSide side, ChessPosition position)
        {
            this.GameSide = side;
            this.IsMoved = false;
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

        /// <summary>
        /// 获取与设置该“王”棋子是否移动过，如移动过，将不能再易位。
        /// </summary>
        public bool IsMoved { get; internal set; }

        public override void MoveIn(ChessGame chessGame, ChessPosition tgtPosition)
        {
            base.MoveIn(chessGame, tgtPosition);
            this.IsMoved = true;
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
