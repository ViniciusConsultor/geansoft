using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class PieceKnight : Piece
    {
        public static PieceKnight Knight02 = new PieceKnight(Enums.GameSide.Black, new Position(2, 8));
        public static PieceKnight Knight07 = new PieceKnight(Enums.GameSide.Black, new Position(7, 8));
        public static PieceKnight Knight58 = new PieceKnight(Enums.GameSide.White, new Position(2, 1));
        public static PieceKnight Knight63 = new PieceKnight(Enums.GameSide.White, new Position(7, 1));

        public PieceKnight(Enums.GameSide manSide, Position position)
            : base(position)
        {
            this.GameSide = manSide;
            switch (manSide)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteKnight;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackKnight;
                    break;
            }
        }

        public override Positions GetEnablePositions()
        {
            Positions positions = new Positions();
            Position aPos = _position.ShiftWestNorth();
            Position bPos = _position.ShiftEastNorth();
            Position cPos = _position.ShiftWestSouth();
            Position dPos = _position.ShiftEastSouth();
            Position pos;
            if (aPos != Position.Empty)
            {
                pos = aPos.ShiftNorth();
                if (pos != Position.Empty)
                    positions.Add(pos);
                pos = aPos.ShiftWest();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            if (bPos != Position.Empty)
            {
                pos = bPos.ShiftNorth();
                if (pos != Position.Empty)
                    positions.Add(pos);
                pos = bPos.ShiftEast();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            if (cPos != Position.Empty)
            {
                pos = cPos.ShiftWest();
                if (pos != Position.Empty)
                    positions.Add(pos);
                pos = cPos.ShiftSouth();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            if (dPos != Position.Empty)
            {
                pos = dPos.ShiftEast();
                if (pos != Position.Empty)
                    positions.Add(pos);
                pos = dPos.ShiftSouth();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }

            return positions;
        }
    }
}
