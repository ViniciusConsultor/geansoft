using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess.Engine
{
    public enum MoveFlag
    {
        Nothing = 0,
        Check = 1,
        Mate = 2,
        Capture = 4,
        PromoteToQueen = 8,
        PromoteToRook = 16,
        PromoteToKnight = 32,
        PromoteToBishop = 64,
        EnPassant = 128,
        QueenSideCastle = 256,
        KingSideCastle = 512,
        PawnCapture = 1024,
        KnightCapture = 2048,
        BishopCapture = 4096,
        RookCapture = 8192,
        QueenCapture = 16384,
        BreakShortCastle = 32768,
        BreakLongCastle = 65536,
        NoPromotion = ~(PromoteToBishop | PromoteToKnight | PromoteToQueen | PromoteToRook),
        Promotion = (PromoteToBishop | PromoteToKnight | PromoteToQueen | PromoteToRook),
        Castling = (QueenSideCastle | KingSideCastle)
    }

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
            string s = string.Format("{0}{1}", ToLiteral(cell1), ToLiteral(cell2));
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
            return string.Format("{0}{1}", z[cell1 % 8], 8 - (int)cell1 / 8);
        }

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

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

            if ((pCell == (int)Cell.e1
                && cCell == (int)Cell.g1
                && 0 != (board.GetKing(Side.White) & bm.Previous)
                )
                ||
                (pCell == (int)Cell.e8
                && cCell == (int)Cell.g8
                && 0 != (board.GetKing(Side.Black) & bm.Previous)
                )
                )
                bm.Flag |= MoveFlag.KingSideCastle;
            if ((pCell == (int)Cell.e1
                && cCell == (int)Cell.c1
                && 0 != (board.GetKing(Side.White) & bm.Previous)
                )
                ||
                (pCell == (int)Cell.e8
                && cCell == (int)Cell.c8
                && 0 != (board.GetKing(Side.Black) & bm.Previous)
                )
                )
                bm.Flag |= MoveFlag.QueenSideCastle;
            PromotionEnum prom = (PromotionEnum)((p >> 15) & 7);
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
            PieceEnum capt = (PieceEnum)((p >> 12) & 7);
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

    /// <summary>
    /// The piece containined in a square
    /// To describe the part only 4 bit is necessary ( this is significant only for move packing )
    /// The part color is maskable with the part.
    /// </summary>
    public enum PieceEnum
    {
        PieceMask = 0xF, None = 0, Pawn = 1, Knight = 2, King = 3, Rook = 4, Bishop = 8, Queen = 12, IsSliding = 12, DiagSliding = 8, StraightSliding = 4, White = 16, Black = 32
    }

    public enum PromotionEnum
    {
        None = 0, Knight = 1, Bishop = 2, Rook = 3, Queen = 4
    }

    /// <summary>
    /// Enumerate the cells... the enum value is an index in the 0x88 board
    /// </summary>
    public enum Cell
    {
        a1 = 0,
        b1 = 1,
        c1 = 2,
        d1 = 3,
        e1 = 4,
        f1 = 5,
        g1 = 6,
        h1 = 7,

        a2 = 16,
        b2 = 17,
        c2 = 18,
        d2 = 19,
        e2 = 20,
        f2 = 21,
        g2 = 22,
        h2 = 23,

        a3 = 32,
        b3 = 33,
        c3 = 34,
        d3 = 35,
        e3 = 36,
        f3 = 37,
        g3 = 38,
        h3 = 39,

        a4 = 48,
        b4 = 49,
        c4 = 50,
        d4 = 51,
        e4 = 52,
        f4 = 53,
        g4 = 54,
        h4 = 55,

        a5 = 64,
        b5 = 65,
        c5 = 66,
        d5 = 67,
        e5 = 68,
        f5 = 69,
        g5 = 70,
        h5 = 71,

        a6 = 80,
        b6 = 81,
        c6 = 82,
        d6 = 83,
        e6 = 84,
        f6 = 85,
        g6 = 86,
        h6 = 87,

        a7 = 96,
        b7 = 97,
        c7 = 98,
        d7 = 99,
        e7 = 100,
        f7 = 101,
        g7 = 102,
        h7 = 103,

        a8 = 112,
        b8 = 113,
        c8 = 114,
        d8 = 115,
        e8 = 116,
        f8 = 117,
        g8 = 118,
        h8 = 119,

        Invalid = 0x88
    }

}
