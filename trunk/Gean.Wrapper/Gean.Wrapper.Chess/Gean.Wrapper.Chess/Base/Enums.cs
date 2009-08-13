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
            /// <summary>
            /// 无效动作
            /// </summary>
            Invalid = 0,
            /// <summary>
            /// 普通棋招
            /// </summary>
            General = 1,
            /// <summary>
            /// 有棋被杀死
            /// </summary>
            Kill = 2,
            /// <summary>
            /// 将军(“将军”在棋步中仅是某一棋步的结果，他事实是General或Kill棋步的结果)。
            /// 故该枚举单独使用时指的是普通棋招，并该棋招产生了“将军”的结果。
            /// </summary>
            Check = 4,
            /// <summary>
            /// 王车短易位
            /// </summary>
            KingSideCastling = 8,
            /// <summary>
            /// 王车长易位
            /// </summary>
            QueenSideCastling = 16,
            /// <summary>
            /// 开局摆棋
            /// </summary>
            Opennings = 32,
            /// <summary>
            /// 升变
            /// </summary>
            Promotion = 64,
            /// <summary>
            /// 吃过路兵
            /// </summary>
            EnPassant = 128,
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

    }
}
