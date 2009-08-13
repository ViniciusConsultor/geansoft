using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.Chess.Engine
{

    public interface IBitBoard
    {
        unsafe ulong* RawData { get; }
        int Flags { get; }
        void DoMove(int move);
        void UndoMove(int move);
        Side ToMove { get; }
        ulong GetPawns(Side s);
        ulong GetKnights(Side s);
        ulong GetBishops(Side s);
        ulong GetRooks(Side s);
        ulong GetQueens(Side s);
        ulong GetKing(Side s);
        void SetBoard(string fen);
        string SavePos();
        void Dump(TextWriter tw);
    }

    public class BitBoardDriver
    {
        public BitBoardDriver(IBitBoard IBitBoard, IMoveGenerator IMoveGenerator)
        {
            this.Board = IBitBoard;
            this.moveGenerator = IMoveGenerator;
        }

        public IMoveGenerator MoveGenerator
        {
            get { return moveGenerator; }
        }
        IMoveGenerator moveGenerator;
        Stack<int> moveHistory = new Stack<int>();

        public IBitBoard Board { get; private set; }
        public void RealizeMove(BitMove bm)
        {
            // check move validity...
            int[] moves = new int[200];
            int count = moveGenerator.GetMoves(Board, moves, 0, MoveGenerationMode.All);
            int compatible = 0;
            for (int i = 0; i < count; ++i)
            {
                BitMove fromGen = BitMove.FromRaw(moves[i], Board);
                if (bm.Equals(fromGen))
                {
                    compatible = moves[i];
                    break;
                }
            }
            if (compatible == 0)
            {
                throw new Exception("Invalid move:" + bm.ToString());
            }
            if (!moveGenerator.IsMoveLegal(Board, compatible))
            {
                throw new Exception("Invalid move:" + bm.ToString());
            }
            moveHistory.Push(compatible);
            Board.DoMove(compatible);
            if (moveGenerator.InCheck(Board, Flip(Board.ToMove)))
            {
                Board.UndoMove(compatible);
                moveHistory.Pop();
                throw new Exception("Invalid move ( in check ):" + bm.ToString());
            }
        }
        public void Undo()
        {
            int move = moveHistory.Pop();
            Board.UndoMove(move);
        }
        public void SetBoard(string fen)
        {
            Board.SetBoard(fen);
        }
        public string SavePos()
        {
            return Board.SavePos();
        }

        public Side ToMove
        {
            get { return Board.ToMove; }
        }
        public static Side Flip(Side s)
        {
            return s == Side.White ? Side.Black : Side.White;
        }
        public ulong GetPawns(Side s)
        {
            return Board.GetPawns(s);
        }
        public ulong GetKnights(Side s)
        {
            return Board.GetKnights(s);
        }
        public ulong GetBishops(Side s)
        {
            return Board.GetBishops(s);
        }
        public ulong GetRooks(Side s)
        {
            return Board.GetRooks(s);
        }
        public ulong GetQueens(Side s)
        {
            return Board.GetQueens(s);
        }
        public ulong GetKing(Side s)
        {
            return Board.GetKing(s);
        }

        #region Utility

        const ulong m1 = 0x5555555555555555UL; //binary: 0101...
        const ulong m2 = 0x3333333333333333UL; //binary: 00110011..
        const ulong m4 = 0x0f0f0f0f0f0f0f0fUL; //binary:  4 zeros,  4 ones ...
        const ulong h01 = 0x0101010101010101UL; //the sum of 256 to the power of 0,1,2,3...
        public static int HowMany(ulong l)
        {

            //hamming weight
            l -= (l >> 1) & m1;             //put count of each 2 bits into those 2 bits
            l = (l & m2) + ((l >> 2) & m2); //put count of each 4 bits into those 4 bits 
            l = (l + (l >> 4)) & m4;        //put count of each 8 bits into those 8 bits 
            return (int)((l * h01) >> 56);  //returns left 8 bits of x + (x<<8) + (x<<16) + (x<<24) + ... 

        }
        public static ulong CellToBit(int cell)
        {
            return 1UL << cell;
        }

        static int[] index64 = new int[64]
        {
            #region
            63,  0, 58,  1, 59, 47, 53,  2,
            60, 39, 48, 27, 54, 33, 42,  3,
            61, 51, 37, 40, 49, 18, 28, 20,
            55, 30, 34, 11, 43, 14, 22,  4,
            62, 57, 46, 52, 38, 26, 32, 41,
            50, 36, 17, 19, 29, 10, 13, 21,
            56, 45, 25, 31, 35, 16,  9, 12,
            44, 24, 15,  8, 23,  7,  6,  5
            #endregion
        };

        public unsafe static int BitToCell(ulong bit)
        {
            return index64[(((bit & (ulong)(-(long)bit)) * 0x07EDD5E59A4E28C2UL)) >> 58];
        }

        static Regex algebric = new Regex("(?'start'[abcdefgh][12345678])[-]*(?'dest'[abcdefgh][12345678])(?'flag'[:x#+qnrb]*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        static private ulong[] masks = new ulong[]
        {
            #region
            0x00000000FFFFFFFFUL,
            0x000000000000FFFFUL,
            0x00000000000000FFUL,
            0x000000000000000FUL,
            0x0000000000000003UL,
            1
            #endregion
        };
        static private void Bindex(ICollection<int> ires, int depth, ulong number, int pos, int mindex)
        {
            if (number == 0)
                return;
            if (depth == 1)
            {
                if (number != 0)
                {
                    ires.Add(pos);
                }
                return;
            }
            else
            {
                int half = depth / 2;
                Bindex(ires, half, number >> half, pos + half, mindex + 1);
                Bindex(ires, half, number & masks[mindex], pos, mindex + 1);
            }
        }
        static public ICollection<int> Unpack(ulong bits)
        {
            IList<int> x = new List<int>();
            while (bits != 0)
            {
                int z = BitToCell(bits & (~bits + 1));
                bits &= (0XFFFFFFFFFFFFFFFUL << z + 1);
                x.Add(z);
            }
            return x;
        }
        static public unsafe int Unpack(ulong bits, int* target)
        {
            int i = 0;
            while (bits != 0)
            {
                int z = BitToCell(bits & (~bits + 1));
                bits &= (0XFFFFFFFFFFFFFFFUL << z + 1);
                *target = z;
                target++;
                i++;
            }
            return i;
        }
        static public int ToCell(string p)
        {
            string cols = "abcdefgh";
            string tokens = p.ToLower().Trim();
            int col = cols.IndexOf(tokens[0]);
            int row = int.Parse(new string(tokens[1], 1));
            return (8 - row) * 8 + col;
        }
        public bool InCheck(Side side, ulong bit)
        {
            int cell = BitToCell(bit);
            return moveGenerator.InCheck(Board, new int[] { cell }, side);
        }
        public BitMove LasMoveWhite
        {
            get { return lastMoveWhite; }
            set { lastMoveWhite = value; }
        }
        private BitMove lastMoveWhite = new BitMove();
        public BitMove LasMoveBlack
        {
            get { return lastMoveBlack; }
            set { lastMoveBlack = value; }
        }
        private BitMove lastMoveBlack = new BitMove();
        static private int[] centerDist = new int[]
        {
            #region
            565,
            490,
            425,
            376,
            350,
            352,
            381,
            433,
            500,
            415,
            340,
            281,
            250,
            258,
            301,
            367,
            447,
            354,
            265,
            191,
            150,
            170,
            235,
            320,
            412,
            312,
            213,
            118,
            0,
            106,
            201,
            300,
            400,
            300,
            201,
            106,
            0,
            117,
            213,
            312,
            412,
            320,
            235,
            169,
            150,
            190,
            265,
            353,
            447,
            367,
            301,
            257,
            250,
            280,
            340,
            415,
            500,
            432,
            381,
            351,
            350,
            375,
            425,
            489
            #endregion
        };

        /// <summary>
        /// Returns the distance from a cell to the center in 1/100 of cell 
        /// Approximate value range from 0 - 500
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public static int DistanceFromCenter(int cell)
        {
            return centerDist[cell];
        }
        public bool InCheck(Side side)
        {
            return moveGenerator.InCheck(Board, side);
        }

        #endregion

        #region Zobrist
        /*
        ZobristKey
        Zobrist就是把一个棋盘编码成HashKey的一种映射关系表，表里面全是随机数，用于棋类比赛人工智能
        神经网络中的模拟退火技术，BP网初始权值随机设定神经网络里的初始权值通常就是随机数。此外为防
        止出现局部极小训练不收敛，使用随机技术是常有的事。
        为什么要编码呢，其实就是为了把棋盘压缩成 1个32位或64为长整型，这样比较两个局面等价于比较两
        个整数，是不是超级简单而且快速了呢?
        */

        static ulong[, ,] ZobristKeyGen = new ulong[6, 2, 64];
        const ulong CastleBlackRandom = 0x86726af5ec1ea6fdUL;
        const ulong CastleWhiteRandom = 0x02a263f7e51ea66dUL;
        const ulong LongCastleBlackRandom = 0x80ee6569eba4a6fdUL;
        const ulong LongCastleWhiteRandom = 0x91116ef5cb1ea247UL;
        const ulong BlackRandom = 0x8e56a1468ab34112UL;
        static void BuildZobristKeyGen()
        {
            Dictionary<ulong, bool> generated = new Dictionary<ulong, bool>();
            Random rnd = new Random();
            for (int i = 0; i < 6; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    for (int k = 0; k < 64; ++k)
                    {
                        ulong n = 0;
                        do
                        {
                            n = (ulong)(uint)rnd.Next() | ((ulong)(uint)rnd.Next()) << 32;
                            //n = Rand.Random64();
                        }
                        while (generated.ContainsKey(n));
                        generated[n] = true;
                        ZobristKeyGen[i, j, k] = n;
                    }
                }
            }
        }
        ulong zKey;
        public ulong ZKey
        {
            get { return zKey; }
        }
        public void CalcZobristKey(Side sideToMove)
        {
            zKey = ZobristKey(sideToMove);
        }
        public ulong ZobristKey(Side c)
        {
            ulong key = 0;
            ulong p = 1UL;
            for (int i = 0; i < 64; ++i, p <<= 1)
            {
                #region
                //White
                if (0 != (p & GetBishops(Side.White)))
                {
                    key ^= ZobristKeyGen[2, 0, i];
                }
                if (0 != (p & GetKnights(Side.White)))
                {
                    key ^= ZobristKeyGen[1, 0, i];
                }
                if (0 != (p & GetRooks(Side.White)))
                {
                    key ^= ZobristKeyGen[3, 0, i];
                }
                if (0 != (p & GetQueens(Side.White)))
                {
                    key ^= ZobristKeyGen[4, 0, i];
                }
                if (0 != (p & GetKing(Side.White)))
                {
                    key ^= ZobristKeyGen[5, 0, i];
                }
                if (0 != (p & GetPawns(Side.White)))
                {
                    key ^= ZobristKeyGen[0, 0, i];
                }
                //black
                if (0 != (p & GetBishops(Side.Black)))
                {
                    key ^= ZobristKeyGen[2, 1, i];
                }
                if (0 != (p & GetKnights(Side.Black)))
                {
                    key ^= ZobristKeyGen[1, 1, i];
                }
                if (0 != (p & GetRooks(Side.Black)))
                {
                    key ^= ZobristKeyGen[3, 1, i];
                }
                if (0 != (p & GetQueens(Side.Black)))
                {
                    key ^= ZobristKeyGen[4, 1, i];
                }
                if (0 != (p & GetKing(Side.Black)))
                {
                    key ^= ZobristKeyGen[5, 1, i];
                }
                if (0 != (p & GetPawns(Side.Black)))
                {
                    key ^= ZobristKeyGen[0, 1, i];
                }
                #endregion
            }
            //if (whiteCanCastle)
            //    key ^= CastleWhiteRandom;
            //if (whiteCanCastleLong)
            //    key ^= LongCastleWhiteRandom;
            //if (blackCanCastle)
            //    key ^= CastleBlackRandom;
            //if (blackCanCastleLong)
            //    key ^= LongCastleBlackRandom;
            if (c == Side.Black)
                key ^= BlackRandom;
            return key;
        }

        #endregion

        #region PieceSquare

        public enum GamePhase
        {
            Opening, Middle, End
        }

        public int GetExtimatedBoardPositionalValue(Side s, GamePhase phase)
        {
            /*
             int valueWhite = 0, valueBlack = 0;
             foreach (int cell in Unpack(WhitePawns))
             {
                 if (phase == GamePhase.End)
                     valueWhite += endgamePawnSquareBonuses[63 - cell];
                 else
                     if (phase == GamePhase.Opening)
                         valueWhite += openingPawnSquareBonuses[63 - cell];
                     else
                         valueWhite += pawnSquareBonuses[63 - cell];

             }
             foreach (int cell in Unpack(WhiteKing))
             {
                 if (phase == GamePhase.End)
                     valueWhite += endgameKingSquareBonuses[63 - cell];
                 else
                     valueWhite += kingSquareBonuses[63 - cell];
             }
             foreach (int cell in Unpack(WhiteQueen))
             {
                 valueWhite += queenSquareBonuses[63 - cell];
             }
             foreach (int cell in Unpack(WhiteRooks))
             {
                 valueWhite += rookSquareBonuses[63 - cell];
             }
             foreach (int cell in Unpack(WhiteKnights))
             {
                 valueWhite += knightSquareBonuses[63 - cell];
             }
             foreach (int cell in Unpack(WhiteBishops))
             {
                 valueWhite += bishopSquareBonuses[63 - cell];
             }
             //black
             foreach (int cell in Unpack(BlackPawns))
             {
                 if (phase == GamePhase.End)
                     valueBlack += endgamePawnSquareBonuses[cell];
                 else
                     if (phase == GamePhase.Opening)
                         valueBlack += openingPawnSquareBonuses[cell];
                     else
                         valueBlack += pawnSquareBonuses[cell];

             }
             foreach (int cell in Unpack(BlackKing))
             {
                 if (phase == GamePhase.End)
                     valueBlack += endgameKingSquareBonuses[cell];
                 else
                     valueBlack += kingSquareBonuses[cell];
             }
             foreach (int cell in Unpack(BlackQueen))
             {
                 valueBlack += queenSquareBonuses[cell];
             }
             foreach (int cell in Unpack(BlackRooks))
             {
                 valueBlack += rookSquareBonuses[cell];
             }
             foreach (int cell in Unpack(BlackKnights))
             {
                 valueBlack += knightSquareBonuses[cell];
             }
             foreach (int cell in Unpack(BlackBishops))
             {
                 valueBlack += bishopSquareBonuses[cell];
             }

             return s == Side.White ? valueWhite - valueBlack : valueBlack - valueWhite;
         }

         public int GetMoveBonus(BitMove bm, GamePhase phase)
         {
             if ( (bm.Previous & WhitePieces) != 0)
             {
                 int cellFrom = 63-BitToCell(bm.Previous);
                 int cellTo = 63-BitToCell(bm.Current);
                 if ( (bm.Previous & WhitePawns) != 0)
                 {
                     if (phase == GamePhase.End)
                         return endgamePawnSquareBonuses[cellTo] - endgamePawnSquareBonuses[cellFrom];
                     else
                         if( phase == GamePhase.Opening )
                             return openingPawnSquareBonuses[cellTo] - openingPawnSquareBonuses[cellFrom];
                             else
                             if( phase == GamePhase.Middle )
                                 return pawnSquareBonuses[cellTo] - pawnSquareBonuses[cellFrom];

                 }
                 else
                 if ((bm.Previous & WhiteKing) != 0)
                 { 
                     if (phase == GamePhase.End)
                         return endgameKingSquareBonuses[cellTo] - endgameKingSquareBonuses[cellFrom];
                     else
                         return kingSquareBonuses[cellTo] - kingSquareBonuses[cellFrom];
                 }
                 else
                 if ( (bm.Previous & WhiteQueen) != 0)
                 {
                     return queenSquareBonuses[cellTo] - queenSquareBonuses[cellFrom];
                 }
                 else
                 if ( (bm.Previous & WhiteRooks) != 0)
                 {
                     return rookSquareBonuses[cellTo] - rookSquareBonuses[cellFrom];
                 }
                 else
                 if ((bm.Previous & WhiteBishops) != 0)
                 {
                     return bishopSquareBonuses[cellTo] - bishopSquareBonuses[cellFrom];
                 }
                 else
                 if ((bm.Previous & WhiteKnights) != 0)
                 {
                     return knightSquareBonuses[cellTo] - knightSquareBonuses[cellFrom];
                 }
             }
             else
             if ((bm.Previous & BlackPieces) != 0)
             {
                 int cellFrom = BitToCell(bm.Previous);
                 int cellTo = BitToCell(bm.Current);
                 if ((bm.Previous & BlackPawns) != 0)
                 {
                     if (phase == GamePhase.End)
                         return endgamePawnSquareBonuses[cellTo] - endgamePawnSquareBonuses[cellFrom];
                     else
                         if (phase == GamePhase.Opening)
                             return openingPawnSquareBonuses[cellTo] - openingPawnSquareBonuses[cellFrom];
                         else
                             if (phase == GamePhase.Middle)
                                 return pawnSquareBonuses[cellTo] - pawnSquareBonuses[cellFrom];

                 }
                 else
                     if ((bm.Previous & BlackKing) != 0)
                     {
                         if (phase == GamePhase.End)
                             return endgameKingSquareBonuses[cellTo] - endgameKingSquareBonuses[cellFrom];
                         else
                             return kingSquareBonuses[cellTo] - kingSquareBonuses[cellFrom];
                     }
                     else
                         if ((bm.Previous & BlackQueen) != 0)
                         {
                             return queenSquareBonuses[cellTo] - queenSquareBonuses[cellFrom];
                         }
                         else
                             if ((bm.Previous & BlackRooks) != 0)
                             {
                                 return rookSquareBonuses[cellTo] - rookSquareBonuses[cellFrom];
                             }
                             else
                                 if ((bm.Previous & BlackBishops) != 0)
                                 {
                                     return bishopSquareBonuses[cellTo] - bishopSquareBonuses[cellFrom];
                                 }
                                 else
                                     if ((bm.Previous & BlackKnights) != 0)
                                     {
                                         return knightSquareBonuses[cellTo] - knightSquareBonuses[cellFrom];
                                     }
             }*/
            return 0;
        }

        static int[] openingPawnSquareBonuses = new int[]
        #region
        {   
            -2, -2, -2, -2, -2, -2, -2, -2, 
             2,  2, -2, -9,-10, -2,  2,  2, 
            -4, -4,  3,  6,  6,  3, -4, -4, 
            -4, -2,  4, 25, 25,  4, -2, -4, 
             0,  0,  6, 16, 16,  6,  0,  0, 
             0,  0,  6, 16, 16,  6,  0,  0, 
             0,  0,  6, 16, 16,  6,  0,  0, 
             0,  0,  0,  0,  0,  0,  0,  0  
        };
        #endregion

        static int[] pawnSquareBonuses = new int[]
        #region
        {   
            -2, -2,  0,  0,  0,  0, -2, -2, 
             2,  2,  2, -2, -2,  2,  2,  2, 
             2,  2,  3,  4,  4,  3,  2,  2, 
             3,  4,  4, 20, 20,  4,  4,  3, 
             3,  6, 12, 18, 18, 12,  6,  3, 
             4,  8, 14, 20, 20, 14,  8,  4, 
             5, 10, 16, 24, 24, 16, 10,  5, 
             0,  0,  0,  0,  0,  0,  0,  0  
        };
        #endregion

        static int[] endgamePawnSquareBonuses = new int[] 
        #region
        {   
             0,  0,  0,  0,  0,  0,  0,  0, 
             0,  0,  0,  0,  0,  0,  0,  0, 
             0,  0,  0,  2,  2,  0,  0,  0, 
             3,  8,  9, 10, 10,  9,  8,  3, 
             6, 12, 14, 18, 18, 14, 12,  6, 
             9, 16, 19, 25, 25, 19, 16,  9, 
            14, 20, 25, 30, 30, 25, 20, 14, 
             0,  0,  0,  0,  0,  0,  0,  0, 
        };
        #endregion

        static int[] bishopSquareBonuses = new int[] 
        #region
        {   
	        -12, -16, -18, -19, -19, -19, -16, -12,
	        -8,   -1,  -4,  -5,  -5,  -4,  -1,  -8,
	        -11,  -4,   4,   2,   2,   4,  -4, -11,
	        -12,  -6,   2,  10,  10,   2,  -6, -12,
	        -12,  -6,   2,  10,  10,   2,  -6, -12,
	        -11,  -4,   4,   2,   2,   4,  -4, -11,
	         -8,  -1,  -4,  -5,  -5,  -4,  -1,  -8,
	         -4,  -8, -10, -11, -11, -10,  -8,  -4
        };
        #endregion

        static int[] knightSquareBonuses = new int[] 
        #region
        {
	        -16,  2,  22, 36, 36, 22,   2, -16,
	        -62, -44,-12,  2,  2,-12, -44, -62,
	        -48, -18, 14, 30, 30, 14, -18, -48,
	        -40, -10, 24, 40, 40, 24, -10, -40,
	        -42, -12, 22, 38, 38, 22, -12, -42,
	        -54, -24,  8, 24, 24,  8, -24, -54,
	        -72, -54,-22, -8, -8,-22, -54, -72,
	        -86, -68,-48,-34,-34,-48, -68, -86
        };
        #endregion

        static int[] queenSquareBonuses = new int[]
        #region
        {
	        -10, -6, -2,  1,  1, -2, -6, -10,
	        -11, -3,  2,  5,  5,  2, -3, -11,
	         -9,  0,  8, 11, 11,  8,  0,  -9,
	         -8,  0,  8, 16, 16,  8,  0,  -8,
	         -8,  0,  8, 16, 16,  8,  0,  -8,
	         -8,  0,  8, 11, 11,  8,  0,  -8,
	        -10, -2,  2,  5,  5,  2, -2, -10,
	        -13, -9, -4, -1, -1, -4, -9, -13
        };
        #endregion

        static int[] rookSquareBonuses = new int[]
        #region
        {
	        -13,  -6,  0,  3,  3,  0, -6, -13,
	         -9,  -4,  2,  5,  5,  2, -4,  -9,
	         -6,   0,  4,  6,  6,  4,  0,  -6,
	         -6,   0,  4,  6,  6,  4,  0,  -6,
	         -6,   0,  4,  6,  6,  4,  0,  -6,
	         -6,   0,  4,  7,  7,  4,  0,  -6,
	         -9,  -3,  3,  5,  5,  3, -3,  -9,
	        -15,  -7, -2,  1,  1, -2, -7, -15
        };
        #endregion

        static int[] kingSquareBonuses = new int[]
        #region
        {
           24, 24, 24, 16, 16,  0, 32, 32,
           24, 20, 16, 12, 12, 16, 20, 24,
           16, 12,  8,  4,  4,  8, 12, 16,
           12,  8,  4,  0,  0,  4,  8, 12,
            6,  0,  0,  0,  0,  0,  0,  6,
            0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0,
            0,  0,  0,  0,  0,  0,  0,  0
        };
        #endregion

        static int[] endgameKingSquareBonuses = new int[]
        #region
        {
            0,  6, 12, 18, 18, 12,  6,  0,
            6, 12, 18, 24, 24, 18, 12,  6,
           12, 18, 24, 32, 32, 24, 18, 12,
           18, 24, 32, 48, 48, 32, 24, 18,
           18, 24, 32, 48, 48, 32, 24, 18,
           12, 18, 24, 32, 32, 24, 18, 12,
            6, 12, 18, 24, 24, 18, 12,  6,
            0,  6, 12, 18, 18, 12,  6,  0
        };
        #endregion

        #endregion

        //public static Movement ParseNotation(string s, BitBoardDriver b, Side side)
        /*{

            Match m = algebric.Match(s);
            if (m.Success)
            {
                Movement move = new Movement(
                                        ToCell(m.Groups["start"].Value)
                                        , ToCell(m.Groups["dest"].Value)
                                        , ToFlag(m.Groups["flag"].Value));
                return move;
            }
            else
            {
                PGNParser pgn = new PGNParser(b);
                Movement move = pgn.ParseSingleMove(s, side);
                return move;
            }
        }*/
        //private static MoveFlag ToFlag(string p)
        /*{
            MoveFlag f = MoveFlag.Nothing;
            if (!string.IsNullOrEmpty(p))
            {
                foreach (char c in p.ToLower())
                {
                    switch (c)
                    {
                        case '+':
                            f |= MoveFlag.Check;
                            break;
                        case '#':
                            f |= MoveFlag.Mate;
                            break;
                        case 'x':
                        case ':':
                            f |= MoveFlag.Capture;
                            break;
                        case 'q':
                            f |= MoveFlag.PromoteToQueen;
                            break;
                        case 'r':
                            f |= MoveFlag.PromoteToRook;
                            break;
                        case 'n':
                            f |= MoveFlag.PromoteToKnight;
                            break;
                        case 'b':
                            f |= MoveFlag.PromoteToBishop;
                            break;
                    }
                }
            }
            else
                return MoveFlag.Nothing;
            return f;
        }*/

    }
}
