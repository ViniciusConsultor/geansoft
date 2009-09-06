﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class PieceQueen : Piece
    {
        public static PieceQueen NewBlackQueen = new PieceQueen(Enums.GameSide.Black);
        public static PieceQueen NewWhiteQueen = new PieceQueen(Enums.GameSide.White);

        public PieceQueen(Enums.GameSide side) : this(side, Position.Empty) { }
        public PieceQueen(Enums.GameSide side, Position position)
            : base(position)
        {
            this.GameSide = side;
            switch (side)
            {
                case Enums.GameSide.White:
                    this.PieceType = Enums.PieceType.WhiteQueen;
                    break;
                case Enums.GameSide.Black:
                    this.PieceType = Enums.PieceType.BlackQueen;
                    break;
            }
        }

        protected override Position InitPosition(Position position)
        {
            if (position == Position.Empty)
            {
                switch (this.GameSide)
                {
                    case Enums.GameSide.White:
                        return new Position(4, 1);
                    case Enums.GameSide.Black:
                        return new Position(4, 8);
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
            positions.AddRange(PieceRook.RookShift(_position));
            positions.AddRange(PieceBishop.BishopShift(_position));
            return positions;
        }
    }
}
