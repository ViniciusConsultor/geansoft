using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{

    public class BitMove : ICloneable
    {
        private static BitMove nullMove = new BitMove();
        public static BitMove NullMove
        {
            get
            {
                return nullMove;
            }
        }
        public override bool Equals(object obj)
        {
            BitMove other = obj as BitMove;
            return other.prev == prev && current == other.current;
        }
        public override int GetHashCode()
        {
            return current.GetHashCode() + prev.GetHashCode();
        }

        public Enums.Action Action
        {
            get { return action; }
            set { action = value; }
        }
        private Enums.Action action;

        private ulong prev;

        public ulong Previous
        {
            get { return prev; }
            set { prev = value; }
        }

        private ulong current;

        public ulong Current
        {
            get { return current; }
            set { current = value; }
        }
        public override string ToString()
        {
            int cell1 = BitBoardDriver.BitToCell(Previous);
            int cell2 = BitBoardDriver.BitToCell(Current);
            string s = string.Format("{0}{1}", ToLiteral(cell1), ToLiteral(cell2));
            if (Action == Enums.Action.PromoteToQueen)
            {
                s += "q";
            }
            if (Action == Enums.Action.PromoteToRook)
            {
                s += "r";
            }
            if (Action == Enums.Action.PromoteToBishop)
            {
                s += "b";
            }
            if (Action == Enums.Action.PromoteToKnight)
            {
                s += "n";
            }
            return s;
        }

        private string ToLiteral(int cell1)
        {
            string z = "abcdefgh";
            return string.Format("{0}{1}", z[cell1 % 8], 8 - (int)cell1 / 8);
        }

        /// <summary>
        /// 
        /// <example>
        /// MOVE FORMAT
        /// MSB															 LSB
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// | | | | | | | | | | |x|s|s|s|p|p|p|c|c|c|e|e|e|e|e|e|s|s|s|s|s|s|
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// </example>
        /// </summary>
        /// <param name="p"></param>
        /// <param name="board"></param>
        /// <returns></returns>
        public static BitMove FromRaw(int p, IBitBoard board)
        {
            BitMove bm = new BitMove();
            int pCell = p & 0x3F;
            int cCell = (p >> 6) & 0x3f;
            bm.Previous = BitBoardDriver.CellToBit(pCell);
            bm.Current = BitBoardDriver.CellToBit(cCell);

            if ((pCell == (int)Enums.Cell.e1
                && cCell == (int)Enums.Cell.g1
                && 0 != (board.GetKing(Enums.ChessmanSide.White) & bm.Previous)
                )
                ||
                (pCell == (int)Enums.Cell.e8
                && cCell == (int)Enums.Cell.g8
                && 0 != (board.GetKing(Enums.ChessmanSide.Black) & bm.Previous)
                )
                )
                bm.Action |= Enums.Action.KingSideCastling;
            if ((pCell == (int)Enums.Cell.e1
                && cCell == (int)Enums.Cell.c1
                && 0 != (board.GetKing(Enums.ChessmanSide.White) & bm.Previous)
                )
                ||
                (pCell == (int)Enums.Cell.e8
                && cCell == (int)Enums.Cell.c8
                && 0 != (board.GetKing(Enums.ChessmanSide.Black) & bm.Previous)
                )
                )
                bm.Action |= Enums.Action.QueenSideCastling;
            Enums.Promotion prom = (Enums.Promotion)((p >> 15) & 7);
            switch (prom)
            {
                case Enums.Promotion.Bishop:
                    bm.Action |= Enums.Action.PromoteToBishop;
                    break;
                case Enums.Promotion.Knight:
                    bm.Action |= Enums.Action.PromoteToKnight;
                    break;
                case Enums.Promotion.Queen:
                    bm.Action |= Enums.Action.PromoteToQueen;
                    break;
                case Enums.Promotion.Rook:
                    bm.Action |= Enums.Action.PromoteToRook;
                    break;
            }
            Enums.BitChessman capt = (Enums.BitChessman)((p >> 12) & 7);
            switch (capt)
            {
                case Enums.BitChessman.Bishop:
                    bm.Action |= Enums.Action.BishopCapture;
                    break;
                case Enums.BitChessman.Knight:
                    bm.Action |= Enums.Action.KnightCapture;
                    break;
                case Enums.BitChessman.Pawn:
                    bm.Action |= Enums.Action.PawnCapture;
                    break;
                case Enums.BitChessman.Queen:
                    bm.Action |= Enums.Action.QueenCapture;
                    break;
                case Enums.BitChessman.Rook:
                    bm.Action |= Enums.Action.RookCapture;
                    break;
            }
            if (0 != ((p >> 22) & 1))
                bm.Action |= Enums.Action.EnPassant;
            return bm;
        }

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }

}
