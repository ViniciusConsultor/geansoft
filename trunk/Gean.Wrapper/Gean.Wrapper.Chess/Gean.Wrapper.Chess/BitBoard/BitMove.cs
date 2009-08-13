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
	
        private MoveFlag flag;

        public MoveFlag Flag
        {
            get { return flag; }
            set { flag = value; }
        }
	
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
            string s =  string.Format("{0}{1}", ToLiteral(cell1), ToLiteral(cell2));
            if (Flag == MoveFlag.PromoteToQueen)
            {
                s += "q";
            }
            if (Flag == MoveFlag.PromoteToRook)
            {
                s += "r";
            }
            if (Flag == MoveFlag.PromoteToBishop)
            {
                s += "b";
            }
            if (Flag == MoveFlag.PromoteToKnight)
            {
                s += "n";
            }
            return s;
        }
        
        private string ToLiteral(int cell1)
        {
            string z = "abcdefgh";
            return string.Format("{0}{1}", z[cell1 % 8], 8-(int)cell1 / 8 );
        }

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
        // MOVE FORMAT
        // MSB															 LSB
        // +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        // | | | | | | | | | | |x|s|s|s|p|p|p|c|c|c|e|e|e|e|e|e|s|s|s|s|s|s|
        // +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        //
        public static BitMove FromRaw(int p,IBitBoard board)
        {
            BitMove bm = new BitMove();
            int pCell = p & 0x3F;
            int cCell = (p >> 6) & 0x3f;
            bm.Previous = BitBoardDriver.CellToBit(pCell);
            bm.Current = BitBoardDriver.CellToBit(cCell);

            if( (pCell == (int)Cell.e1
                && cCell == (int)Cell.g1
                && 0 != (board.GetKing(Enums.ChessmanSide.White) & bm.Previous)
                )
                ||
                (pCell == (int)Cell.e8
                && cCell == (int)Cell.g8
                && 0 != (board.GetKing(Enums.ChessmanSide.Black) & bm.Previous)
                )
                )
                bm.Flag |= MoveFlag.KingEnums.ChessmanSideCastle;
            if ((pCell == (int)Cell.e1
                && cCell == (int)Cell.c1
                && 0 != (board.GetKing(Enums.ChessmanSide.White) & bm.Previous)
                )
                ||
                (pCell == (int)Cell.e8
                && cCell == (int)Cell.c8
                && 0 != (board.GetKing(Enums.ChessmanSide.Black) & bm.Previous)
                )
                )
                bm.Flag |= MoveFlag.QueenCastle;
            PromotionEnum prom = (PromotionEnum)((p >> 15)&7);
            switch (prom)
            {
                case PromotionEnum.Bishop:
                    bm.Flag |= MoveFlag.PromoteToBishop;
                    break;
                case PromotionEnum.Knight:
                    bm.Flag |= MoveFlag.PromoteToKnight;
                    break;
                case PromotionEnum.Queen:
                    bm.Flag |= MoveFlag.PromoteToQueen;
                    break;
                case PromotionEnum.Rook:
                    bm.Flag |= MoveFlag.PromoteToRook;
                    break;
            }
            PieceEnum capt = (PieceEnum)((p >> 12)&7);
            switch (capt)
            {
                case PieceEnum.Bishop:
                    bm.Flag |= MoveFlag.BishopCapture;
                    break;
                case PieceEnum.Knight:
                    bm.Flag |= MoveFlag.KnightCapture;
                    break;
                case PieceEnum.Pawn:
                    bm.Flag |= MoveFlag.PawnCapture;
                    break;
                case PieceEnum.Queen:
                    bm.Flag |= MoveFlag.QueenCapture;
                    break;
                case PieceEnum.Rook:
                    bm.Flag |= MoveFlag.RookCapture;
                    break;
            }
            if (0 != ((p >> 22) & 1))
                bm.Flag |= MoveFlag.EnPassant;
            return bm;                        
        }
    }
}
