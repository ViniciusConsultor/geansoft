using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class PiecePawn : Piece
    {
        public static PiecePawn[] BlackPawns = new PiecePawn[]
            #region
            {
                new PiecePawn(Enums.GameSide.Black, new Position(1,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(2,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(3,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(4,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(5,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(6,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(7,7)),
                new PiecePawn(Enums.GameSide.Black, new Position(8,7))
            };
            #endregion
        public static PiecePawn[] WhitePawns = new PiecePawn[]
            #region
            {
                new PiecePawn(Enums.GameSide.White, new Position(1,2)),
                new PiecePawn(Enums.GameSide.White, new Position(2,2)),
                new PiecePawn(Enums.GameSide.White, new Position(3,2)),
                new PiecePawn(Enums.GameSide.White, new Position(4,2)),
                new PiecePawn(Enums.GameSide.White, new Position(5,2)),
                new PiecePawn(Enums.GameSide.White, new Position(6,2)),
                new PiecePawn(Enums.GameSide.White, new Position(7,2)),
                new PiecePawn(Enums.GameSide.White, new Position(8,2))
            };
            #endregion

        public PiecePawn(Enums.GameSide side, Position position)
            : base(position)
        {
            this.GameSide = side;
            this.EnableEnPassanted = false;
            switch (side)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhitePawn;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackPawn;
                    break;
            }
        }

        /// <summary>
        /// 获取与设置能被对方吃过路兵
        /// </summary>
        public bool EnableEnPassanted { get; internal set; }

        protected override Position InitPosition(Position position)
        {
            if (position.Equals(Position.Empty))
            {
                switch (this.GameSide)
                {
                    case Enums.GameSide.White:
                        return new Position(5, 1);
                    case Enums.GameSide.Black:
                        return new Position(5, 8);
                    default:
                        throw new PieceException(ExString.PositionIsError);
                }
            }
            else
                return position;
        }

        public override Position[] GetEnablePositions()
        {
            List<Position> positions = new List<Position>();
            Position pos = Position.Empty;
            if (this.GameSide == Enums.GameSide.White)
            {
                if (_position.Y < 1) return null;

                pos = _position.ShiftWestNorth();
                if (pos != Position.Empty)
                    positions.Add(pos);
                pos = _position.ShiftEastNorth();
                if (pos != Position.Empty)
                    positions.Add(pos);
                pos = _position.ShiftNorth();
                if (pos != Position.Empty)
                    positions.Add(pos);

                if (_position.Y == 1)
                {
                    pos = pos.ShiftNorth();
                    if (pos != Position.Empty)
                        positions.Add(pos);
                }
            }
            if (this.GameSide == Enums.GameSide.Black)
            {
                if (_position.Y > 6) return null;

                pos = _position.ShiftWestSouth();
                if (pos != Position.Empty)
                    positions.Add(pos);
                pos = _position.ShiftEastSouth();
                if (pos != Position.Empty)
                    positions.Add(pos);
                pos = _position.ShiftSouth();
                if (pos != Position.Empty)
                    positions.Add(pos);

                if (_position.Y == 6)
                {
                    pos = pos.ShiftSouth();
                    if (pos != Position.Empty)
                        positions.Add(pos);
                }
            }
            return positions.ToArray();
        }
    }
}
