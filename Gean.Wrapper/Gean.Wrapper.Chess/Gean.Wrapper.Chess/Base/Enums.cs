using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class Enums
    {
        /// <summary>
        /// 棋招动作枚举
        /// </summary>
        public enum Action
        {
            General = 0,
            ///// <summary>
            ///// 将军(“将军”在棋步中仅是某一棋步的结果，他事实是General或Kill棋步的结果)。
            ///// 故该枚举单独使用时指的是普通棋招，并该棋招产生了“将军”的结果。
            ///// </summary>
            Check = 1,
            /// <summary>
            /// 伙伴
            /// </summary>
            Mate = 2,
            ///// <summary>
            ///// 有棋被杀死(捕获, 夺得, 俘获) 
            ///// </summary>
            Capture = 4,
            PromoteToQueen = 8,
            PromoteToRook = 16,
            PromoteToKnight = 32,
            PromoteToBishop = 64,
            EnPassant = 128,
            ///// <summary>
            ///// 王车长易位
            ///// </summary>
            QueenSideCastling = 256,
            ///// <summary>
            ///// 王车短易位
            ///// </summary>
            KingSideCastling = 512,
            PawnCapture = 1024,
            KnightCapture = 2048,
            BishopCapture = 4096,
            RookCapture = 8192,
            QueenCapture = 16384,
            BreakShortCastle = 32768,
            BreakLongCastle = 65536,
            NoPromotion = ~(PromoteToBishop | PromoteToKnight | PromoteToQueen | PromoteToRook),
            ///// <summary>
            ///// 升变
            ///// </summary>
            Promotion = (PromoteToBishop | PromoteToKnight | PromoteToQueen | PromoteToRook),
            Castling = (QueenSideCastling | KingSideCastling),
            Opennings = 131072,
            Invalid = 262144,
        }

        public static Action ToAction(string value)
        {
            Action action = Action.General;
            if (!string.IsNullOrEmpty(value))
            {
                foreach (char c in value.ToLower())
                {
                    switch (c)
                    {
                        #region case
                        case '+':
                            action |= Action.Check;
                            break;
                        case '#':
                            action |= Action.Mate;
                            break;
                        case 'x':
                        case ':':
                            action |= Action.Capture;
                            break;
                        case 'q':
                            action |= Action.PromoteToQueen;
                            break;
                        case 'r':
                            action |= Action.PromoteToRook;
                            break;
                        case 'n':
                            action |= Action.PromoteToKnight;
                            break;
                        case 'b':
                            action |= Action.PromoteToBishop;
                            break;
                        #endregion
                    }
                }
            }
            else
            {
                return Action.General;
            }
            return action;
        }

        public enum GamePhase
        {
            Opening, Middle, End
        }

        /// <summary>
        /// 当有同行与同列的棋子可能产生同样的棋步，描述取坐标的哪个值来表示。
        /// </summary>
        public enum SameOrientation
        {
            None = 0, 
            /// <summary>
            /// 横方向
            /// </summary>
            Horizontal,
            /// <summary>
            /// 纵方向
            /// </summary>
            Vertical,
        }

        /// <summary>
        /// 棋子的战方：黑棋，白棋
        /// </summary>
        public enum ChessmanSide
        {
            None = 0, White = 1, Black = 2,
        }

        /// <summary>
        /// 获取棋的另一战方
        /// </summary>
        public static ChessmanSide GetOtherSide(ChessmanSide side)
        {
            if (side == ChessmanSide.Black)
                return ChessmanSide.White;
            return ChessmanSide.Black;
        }

        /// <summary>
        /// 黑格，白格
        /// </summary>
        public enum ChessGridSide
        {
            None = 0, White = 1, Black = 2,
        }

        /// <summary>
        /// 获取棋格的另一方
        /// </summary>
        public static ChessGridSide GetOtherGridSide(ChessGridSide side)
        {
            if (side == ChessGridSide.Black)
                return ChessGridSide.White;
            return ChessGridSide.Black;
        }

        /// <summary>
        /// 棋子类型的枚举。
        /// 车Rook，马Knight，象Bishop，后Queen，王King，兵Pawn。
        /// 中文简称王后车象马兵英文简称K Q R B N P
        /// </summary>
        public enum ChessmanType
        {
            /// <summary>
            /// 嘛也不是
            /// </summary>
            None = 0,
            /// <summary>
            /// 车
            /// </summary>
            Rook = 1,
            /// <summary>
            /// 马((中古时代的)武士, 骑士)
            /// </summary>
            Knight = 2,
            /// <summary>
            /// 象((基督教某些教派管辖大教区的)主教)
            /// </summary>
            Bishop = 4,
            /// <summary>
            /// 皇后
            /// </summary>
            Queen = 8,
            /// <summary>
            /// 王
            /// </summary>
            King = 16,
            /// <summary>
            /// 兵
            /// </summary>
            Pawn = 32,
            ///// <summary>
            ///// 升变
            ///// </summary>
            //Promotion=64,
        }

        public static string ChessmanTypeToString(ChessmanType type)
        {
            switch (type)
            {
                case ChessmanType.Rook:
                    return "R";
                case ChessmanType.Knight:
                    return "N";
                case ChessmanType.Bishop:
                    return "B";
                case ChessmanType.Queen:
                    return "Q";
                case ChessmanType.King:
                    return "K";
                case ChessmanType.Pawn:
                    return "P";
                case ChessmanType.None:
                default:
                    return "";
            }
        }

        /// <summary>
        /// 返回将指定的字符解析出的棋子类型
        /// </summary>
        /// <param name="c">指定的字符</param>
        /// <returns></returns>
        public static Enums.ChessmanType StringToChessmanType(char c)
        {
            return Enums.StringToChessmanType(c.ToString());
        }
        public static Enums.ChessmanType StringToChessmanType(string c)
        {
            if (string.IsNullOrEmpty(c))
                throw new ArgumentNullException("Cannot Null or Empty");
            if (c.Length != 1)
                throw new FormatException(c);

            Enums.ChessmanType manType;
            switch (c.ToUpperInvariant())
            {
                case "O"://王车易位
                    manType = Enums.ChessmanType.None;
                    break;
                case "K":
                    manType = Enums.ChessmanType.King;
                    break;
                case "Q":
                    manType = Enums.ChessmanType.Queen;
                    break;
                case "R":
                    manType = Enums.ChessmanType.Rook;
                    break;
                case "N":
                    manType = Enums.ChessmanType.Knight;
                    break;
                case "B":
                    manType = Enums.ChessmanType.Bishop;
                    break;
                default:
                    throw new ChessRecordException(c.ToString());
            }
            return manType;
        }

        /// <summary>
        /// The different states our parse may be in when firing events.
        /// </summary>
        public enum PGNReaderState
        {
            /// <summary>
            /// Parsing the header information
            /// </summary>
            Header,
            /// <summary>
            /// Parsing the number of a move
            /// </summary>
            Number,
            /// <summary>
            /// Parsing the color to move
            /// </summary>
            Color,
            /// <summary>
            /// Parsing white's move information
            /// </summary>
            White,
            /// <summary>
            /// Parsing black's move information.
            /// </summary>
            Black,
            /// <summary>
            /// Parsing a comment.
            /// </summary>
            Comment,
            /// <summary>
            /// Finished parsing a comment.
            /// </summary>
            EndComment,
            /// <summary>
            /// Parsing a NAG.
            /// </summary>
            Nags,
            /// <summary>
            /// Convert a Nag to text.
            /// </summary>
            ConvertNag,
            /// <summary>
            /// END.
            /// </summary>
            EndMarker,
        }

        /// <summary>
        /// Internal naming for Pieces, also used for indexing into an array of cursors.
        /// </summary>
        public enum FenChessmans
        {
            /// <summary>
            /// No piece or empty square.
            /// </summary>
            None,
            /// <summary>
            /// White's king
            /// </summary>
            WhiteKing,
            /// <summary>
            /// White's queen
            /// </summary>
            WhiteQueen,
            /// <summary>
            /// White's rook
            /// </summary>
            WhiteRook,
            /// <summary>
            /// White's bishop
            /// </summary>
            WhiteBishop,
            /// <summary>
            /// White's knight 
            /// </summary>
            WhiteKnight,
            /// <summary>
            /// White's pawn
            /// </summary>
            WhitePawn,
            /// <summary>
            /// Black's king
            /// </summary>
            BlackKing,
            /// <summary>
            /// Black's queen
            /// </summary>
            BlackQueen,
            /// <summary>
            /// Black's rook
            /// </summary>
            BlackRook,
            /// <summary>
            /// Black's bishop
            /// </summary>
            BlackBishop,
            /// <summary>
            /// Black's knight
            /// </summary>
            BlackKnight,
            /// <summary>
            /// Black's pawn
            /// </summary>
            BlackPawn,
            /// <summary>
            /// Open hand cursor.
            /// </summary>
            OpenHand,
            /// <summary>
            /// Closed hand cursor.
            /// </summary>
            ClosedHand,
            /// <summary>
            /// Remove of the piece.
            /// </summary>
            Delete,
            /// <summary>
            /// Used to hide all kings from displaying on the board.
            /// </summary>
            AllKings,
            /// <summary>
            /// Used to hide all kings from displaying on the board.
            /// </summary>
            AllQueens,
            /// <summary>
            /// Used to hide all queens from displaying on the board.
            /// </summary>
            AllRooks,
            /// <summary>
            /// Used to hide all rooks from displaying on the board.
            /// </summary>
            AllBishops,
            /// <summary>
            /// Used to hide all bishops from displaying on the board.
            /// </summary>
            AllKnights,
            /// <summary>
            /// Used to hide all knights from displaying on the board.
            /// </summary>
            AllPawns,
            /// <summary>
            /// Used to hide all pawns from displaying on the board.
            /// </summary>
            AllNonPawns,
            /// <summary>
            /// Used to hide all the non-pawns from displaying on the board.
            /// </summary>
            AllMinors,
            /// <summary>
            /// Used to hide all the pieces.
            /// </summary>
            All
        };

        /// <summary>
        /// Returns a FEN piece representation base on our 
        /// internal piece enumeration.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static FenChessmans FromFEN(char piece)
        {
            FenChessmans aPiece = FenChessmans.None;
            switch (piece)
            {
                #region case
                case 'K':
                    aPiece = FenChessmans.WhiteKing;
                    break;
                case 'Q':
                    aPiece = FenChessmans.WhiteQueen;
                    break;
                case 'R':
                    aPiece = FenChessmans.WhiteRook;
                    break;
                case 'B':
                    aPiece = FenChessmans.WhiteBishop;
                    break;
                case 'N':
                    aPiece = FenChessmans.WhiteKnight;
                    break;
                case 'P':
                    aPiece = FenChessmans.WhitePawn;
                    break;
                case 'k':
                    aPiece = FenChessmans.BlackKing;
                    break;
                case 'q':
                    aPiece = FenChessmans.BlackQueen;
                    break;
                case 'r':
                    aPiece = FenChessmans.BlackRook;
                    break;
                case 'b':
                    aPiece = FenChessmans.BlackBishop;
                    break;
                case 'n':
                    aPiece = FenChessmans.BlackKnight;
                    break;
                case 'p':
                    aPiece = FenChessmans.BlackPawn;
                    break;
                #endregion
            }
            return aPiece;
        }
        /// <summary>
        /// Returns a FEN piece representation base on our 
        /// internal piece enumeration.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static char ToFEN(FenChessmans piece)
        {
            char aPiece = ' ';
            switch (piece)
            {
                #region case
                case FenChessmans.WhiteKing:
                    aPiece = 'K';
                    break;
                case FenChessmans.WhiteQueen:
                    aPiece = 'Q';
                    break;
                case FenChessmans.WhiteRook:
                    aPiece = 'R';
                    break;
                case FenChessmans.WhiteBishop:
                    aPiece = 'B';
                    break;
                case FenChessmans.WhiteKnight:
                    aPiece = 'N';
                    break;
                case FenChessmans.WhitePawn:
                    aPiece = 'P';
                    break;
                case FenChessmans.BlackKing:
                    aPiece = 'k';
                    break;
                case FenChessmans.BlackQueen:
                    aPiece = 'q';
                    break;
                case FenChessmans.BlackRook:
                    aPiece = 'r';
                    break;
                case FenChessmans.BlackBishop:
                    aPiece = 'b';
                    break;
                case FenChessmans.BlackKnight:
                    aPiece = 'n';
                    break;
                case FenChessmans.BlackPawn:
                    aPiece = 'p';
                    break;
                #endregion
            }
            return aPiece;
        }
        /// <summary>
        /// Translates an internal piece enumeration into an algebraic piece character.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns>Character representing our piece.</returns>
        public static char ToNotation(FenChessmans piece)
        {
            char aPiece = ' ';
            switch (piece)
            {
                #region case
                case FenChessmans.WhiteKing:
                case FenChessmans.BlackKing:
                    aPiece = 'K';
                    break;
                case FenChessmans.WhiteQueen:
                case FenChessmans.BlackQueen:
                    aPiece = 'Q';
                    break;
                case FenChessmans.WhiteRook:
                case FenChessmans.BlackRook:
                    aPiece = 'R';
                    break;
                case FenChessmans.WhiteBishop:
                case FenChessmans.BlackBishop:
                    aPiece = 'B';
                    break;
                case FenChessmans.WhiteKnight:
                case FenChessmans.BlackKnight:
                    aPiece = 'N';
                    break;
                case FenChessmans.WhitePawn:
                case FenChessmans.BlackPawn:
                    aPiece = 'P';
                    break;
                #endregion
            }
            return aPiece;
        }

        /// <summary>
        /// The piece containined in a square
        /// To describe the part only 4 bit is necessary ( this is significant only for move packing )
        /// The part color is maskable with the part.
        /// </summary>
        public enum BitChessman
        {
            PieceMask = 0xF,
            None = 0,
            Pawn = 1,
            Knight = 2,
            King = 3,
            Rook = 4,
            Bishop = 8,
            Queen = 12,
            /// <summary>
            /// 所有方向 = Queen
            /// </summary>
            IsSliding = 12,
            /// <summary>
            /// 斜向 = Bishop
            /// </summary>
            DiagonalSliding = 8,
            /// <summary>
            /// 直向 = Rook
            /// </summary>
            StraightSliding = 4,
            White = 16,
            Black = 32
        }

        public enum Promotion
        {
            None = 0,
            Knight = 1,
            Bishop = 2,
            Rook = 3,
            Queen = 4
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
}
