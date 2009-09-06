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

        //public override void MoveIn(ChessGame chessGame, Position tgtPosition)
        //{
        //    base.MoveIn(chessGame, tgtPosition);
        //    this.IsMoved = true;
        //}

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

        public override Positions GetEnablePositions()
        {
            Positions positions = new Positions();
            Position pos = _position;

            pos = _position.ShiftEast();
            if (pos != Position.Empty)
                positions.Add(pos);
            pos = _position.ShiftWest();
            if (pos != Position.Empty)
                positions.Add(pos);
            pos = _position.ShiftSouth();
            if (pos != Position.Empty)
                positions.Add(pos);
            pos = _position.ShiftNorth();
            if (pos != Position.Empty)
                positions.Add(pos);
            pos = _position.ShiftEastNorth();
            if (pos != Position.Empty)
                positions.Add(pos);
            pos = _position.ShiftEastSouth();
            if (pos != Position.Empty)
                positions.Add(pos);
            pos = _position.ShiftWestNorth();
            if (pos != Position.Empty)
                positions.Add(pos);
            pos = _position.ShiftWestSouth();
            if (pos != Position.Empty)
                positions.Add(pos);

            return positions;
        }
    }
}
