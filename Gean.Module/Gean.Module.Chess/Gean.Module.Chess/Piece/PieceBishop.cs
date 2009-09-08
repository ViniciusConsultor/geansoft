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

        public override bool GetEnablePositions(ISituation situation, out Positions enableMovein, out Positions enableCapture)
        {
            enableMovein = new Positions();
            enableCapture = new Positions();
            PieceBishop.BishopShift(this.GameSide, situation, _position, enableMovein, enableCapture);
            return true;
        }

        internal static void BishopShift(
            Enums.GameSide side, ISituation situation, Position position,
            Positions moveInPs, Positions capturePs)
        {
            bool enableMoved = true;
            Position tgtPos = Position.Empty;

            tgtPos = position;
            while (enableMoved)
            {
                tgtPos = tgtPos.ShiftEastNorth();
                enableMoved = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }
            tgtPos = position;
            while (enableMoved)
            {
                tgtPos = tgtPos.ShiftEastSouth();
                enableMoved = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }
            tgtPos = position;
            while (enableMoved)
            {
                tgtPos = tgtPos.ShiftWestNorth();
                enableMoved = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }
            tgtPos = position;
            while (enableMoved)
            {
                tgtPos = tgtPos.ShiftWestSouth();
                enableMoved = Position.Shift(side, situation, tgtPos, moveInPs, capturePs);
            }
        }
    }
}
