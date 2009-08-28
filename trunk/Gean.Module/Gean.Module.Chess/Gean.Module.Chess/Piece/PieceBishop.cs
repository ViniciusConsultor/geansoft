using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class PieceBishop : Piece
    {

        public static PieceBishop Bishop03 = new PieceBishop(Enums.GameSide.Black, new Position(3, 8));
        public static PieceBishop Bishop06 = new PieceBishop(Enums.GameSide.Black, new Position(6, 8));
        public static PieceBishop Bishop59 = new PieceBishop(Enums.GameSide.White, new Position(3, 1));
        public static PieceBishop Bishop62 = new PieceBishop(Enums.GameSide.White, new Position(6, 1));

        public PieceBishop(Enums.GameSide manSide, Position position)
            : base(position)
        {
            this.GameSide = manSide;
            switch (manSide)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteBishop;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackBishop;
                    break;
            }
        }

        public override Position[] GetEnablePositions()
        {
            List<Position> positions = new List<Position>();
            Position pos = _position;

            while (pos != Position.Empty)
            {
                pos = pos.ShiftEastNorth();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            pos = _position;
            while (pos != Position.Empty)
            {
                pos = pos.ShiftEastSouth();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            pos = _position;
            while (pos != Position.Empty)
            {
                pos = pos.ShiftWestNorth();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            pos = _position;
            while (pos != Position.Empty)
            {
                pos = pos.ShiftWestSouth();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }

            return positions.ToArray();
        }
    }
}
