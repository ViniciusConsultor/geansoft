using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class PieceKing : Piece
    {
        public static PieceKing NewBlackKing = new PieceKing(Enums.GameSide.Black);
        public static PieceKing NewWhiteKing = new PieceKing(Enums.GameSide.White);

        public PieceKing(Enums.GameSide side) : this(side, Position.Empty) { }
        public PieceKing(Enums.GameSide side, Position position)
            : base(position)
        {
            this.GameSide = side;
            this.IsMoved = false;
            switch (side)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteKing;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackKing;
                    break;
            }
        }

        /// <summary>
        /// 获取与设置该“王”棋子是否移动过，如移动过，将不能再易位。
        /// </summary>
        public bool IsMoved { get; internal set; }

        protected override Position InitPosition(Position position)
        {
            if (position == Position.Empty)
            {
                switch (this.GameSide)
                {
                    case Enums.GameSide.White:
                        return new Position(5, 1);
                    case Enums.GameSide.Black:
                        return new Position(5, 8);
                    default:
                        throw new PieceException(ExString.PositionIsEmpty);
                }
            }
            else
                return position;
        }

        public override bool GetEnablePositions(ISituation situation, out Positions enableMovein, out Positions enableCapture)
        {
            enableMovein = new Positions();
            enableCapture = new Positions();
            Position pos = Position.Empty;

            Position.Shift(situation, _position, _position.ShiftEast(), enableMovein, enableCapture);
            Position.Shift(situation, _position, _position.ShiftNorth(), enableMovein, enableCapture);
            Position.Shift(situation, _position, _position.ShiftSouth(), enableMovein, enableCapture);
            Position.Shift(situation, _position, _position.ShiftWest(), enableMovein, enableCapture);
            Position.Shift(situation, _position, _position.ShiftEastNorth(), enableMovein, enableCapture);
            Position.Shift(situation, _position, _position.ShiftEastSouth(), enableMovein, enableCapture);
            Position.Shift(situation, _position, _position.ShiftWestNorth(), enableMovein, enableCapture);
            Position.Shift(situation, _position, _position.ShiftWestSouth(), enableMovein, enableCapture);

            return true;
        }

    }
}
