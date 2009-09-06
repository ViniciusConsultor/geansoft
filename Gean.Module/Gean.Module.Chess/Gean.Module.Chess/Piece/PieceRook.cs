using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    /// <summary>
    /// 棋子：车。
    /// </summary>
    public class PieceRook : Piece
    {
        public static PieceRook Rook01 = new PieceRook(Enums.GameSide.Black, new Position(1, 8));
        public static PieceRook Rook08 = new PieceRook(Enums.GameSide.Black, new Position(8, 8));
        public static PieceRook Rook57 = new PieceRook(Enums.GameSide.White, new Position(1, 1));
        public static PieceRook Rook64 = new PieceRook(Enums.GameSide.White, new Position(8, 1));

        public PieceRook(Enums.GameSide manSide, Position position)
            : base(position)
        {
            this.GameSide = manSide;
            switch (manSide)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteRook;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackRook;
                    break;
            }
        }

        public override Positions GetEnablePositions()
        {
            return RookShift(_position);
        }

        internal static Positions RookShift(Position srcPos)
        {
            Positions positions = new Positions();

            Position pos = srcPos;
            while (pos != Position.Empty)
            {
                pos = pos.ShiftEast();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            pos = srcPos;
            while (pos != Position.Empty)
            {
                pos = pos.ShiftWest();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            pos = srcPos;
            while (pos != Position.Empty)
            {
                pos = pos.ShiftNorth();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            pos = srcPos;
            while (pos != Position.Empty)
            {
                pos = pos.ShiftSouth();
                if (pos != Position.Empty)
                    positions.Add(pos);
            }
            return positions;
        }
    }
}
