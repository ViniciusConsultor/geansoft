using System;

namespace Gean.Module.Chess
{
    public class Enums
    {

        #region Action

        /// <summary>
        /// 棋招动作枚举,未分战方
        /// </summary>
        public enum Action
        {
            #region
            /// <summary>
            /// 一般棋招动作
            /// </summary>
            General = 0,
            ///// <summary>
            ///// 将军(“将军”在棋步中仅是某一棋步的结果，他事实是General或Kill棋步的结果)。
            ///// 故该枚举单独使用时指的是普通棋招，并该棋招产生了“将军”的结果。
            ///// </summary>
            Check = 1,
            /// <summary>
            /// 开局摆棋
            /// </summary>
            Opennings = 2,
            ///// <summary>
            ///// 有棋被杀死(捕获, 夺得, 俘获) 
            ///// </summary>
            Capture = 4,
            /// <summary>
            /// 将死，记号为“#”
            /// </summary>
            CheckMate = 8,
            /// <summary>
            /// 吃过路兵
            /// </summary>
            EnPassant = 16,
            /// <summary>
            /// 升变为后
            /// </summary>
            PromoteToQueen = 32,
            /// <summary>
            /// 升变为车
            /// </summary>
            PromoteToRook = 64,
            /// <summary>
            /// 升变为马
            /// </summary>
            PromoteToKnight = 128,
            /// <summary>
            /// 升变为象
            /// </summary>
            PromoteToBishop = 256,
            ///// <summary>
            ///// 王车长易位
            ///// </summary>
            QueenSideCastling = 512,
            ///// <summary>
            ///// 王车短易位
            ///// </summary>
            KingSideCastling = 1024,
            /// <summary>
            /// 无效
            /// </summary>
            Invalid = 2048,
            /// <summary>
            /// 杀棋并将军
            /// </summary>
            CaptureCheck = (Capture|Check),
            /// <summary>
            /// 王车易位
            /// </summary>
            Castling = (QueenSideCastling | KingSideCastling),
            /// <summary>
            /// 升变
            /// </summary>
            Promotion = (PromoteToBishop | PromoteToKnight | PromoteToQueen | PromoteToRook),
            /// <summary>
            /// 不升变
            /// </summary>
            NoPromotion = ~(PromoteToBishop | PromoteToKnight | PromoteToQueen | PromoteToRook),
            #endregion
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
                            action |= Action.CheckMate;
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
                return action;
            return action;
        }
        public static string FromAction(Action action)
        {
            switch (action)
            {
                case Action.Check:
                    return "+";
                case Action.CheckMate:
                    return "#";
                case Action.Capture:
                    return "x";
                case Action.PromoteToQueen:
                    return "q";
                case Action.PromoteToRook:
                    return "r";
                case Action.PromoteToKnight:
                    return "n";
                case Action.PromoteToBishop:
                    return "b";
                case Action.EnPassant:
                    return "x";

                #region return string.Empty;

                case Action.General:
                case Action.Opennings:
                    return string.Empty;

                case Action.QueenSideCastling:
                case Action.KingSideCastling:
                case Action.Castling:
                    return string.Empty;

                case Action.Promotion:
                case Action.NoPromotion:
                    return string.Empty;

                case Action.Invalid:
                default:
                    return string.Empty;

                #endregion

            }
        }

        #endregion

        #region Result

        /// <summary>
        /// 棋局结果
        /// </summary>
        public enum Result
        {
            /// <summary>
            /// 白胜
            /// </summary>
            WhiteWin,
            /// <summary>
            /// 黑胜
            /// </summary>
            BlackWin,
            /// <summary>
            /// 和棋
            /// </summary>
            Draw,
            /// <summary>
            /// 未知
            /// </summary>
            UnKnown,
        }

        public static Result ToResult(string value)
        {
            value = value.Replace(" ", "");
            if (value.Equals(ChessResult.ResultWhiteWin))
            {
                return Result.WhiteWin;
            }
            else if (value.Equals(ChessResult.ResultBlackWin))
            {
                return Result.BlackWin;
            }
            else if (value.Equals(ChessResult.ResultDraw1))
            {
                return Result.Draw;
            }
            else if (value.Equals(ChessResult.ResultDraw2))
            {
                return Result.Draw;
            }
            else if (value.Equals(ChessResult.ResultDraw3))
            {
                return Result.Draw;
            }
            else if (value.Equals(ChessResult.ResultUnKnown))
            {
                return Result.UnKnown;
            }
            else
            {
                return Result.UnKnown;
            }
        }
        public static string FromResult(Result result)
        {
            switch (result)
            {
                case Result.WhiteWin:
                    return ChessResult.ResultWhiteWin;
                case Result.BlackWin:
                    return ChessResult.ResultBlackWin;
                case Result.Draw:
                    return ChessResult.ResultDraw1;
                case Result.UnKnown:
                default:
                    return ChessResult.ResultUnKnown;
            }
        }

        #endregion

        #region GamePhase

        /// <summary>
        /// 棋局阶段
        /// </summary>
        public enum ChessGamePhase
        {
            /// <summary>
            /// 开局
            /// </summary>
            Opening,
            /// <summary>
            ///  中局
            /// </summary>
            Middlegame,
            /// <summary>
            /// 残局
            /// </summary>
            Ending,
        }

        #endregion

        #region ChessGridSide

        /// <summary>
        /// 黑格，白格
        /// </summary>
        public enum ChessGridSide
        {
            White = 0, Black = 1,
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

        #endregion

        #region GameSide

        /// <summary>
        /// 棋局当前战方：黑棋，白棋
        /// </summary>
        public enum GameSide
        {
            White = 0, Black = 1
        }

        public static char FormGameSide(GameSide side)
        {
            if (side == GameSide.White) return 'w';
            if (side == GameSide.Black) return 'b';
            throw new ChessException();
        }
        public static GameSide ToGameSide(char c)
        {
            if (c == 'w') return GameSide.White;
            if (c == 'b') return GameSide.Black;
            throw new ChessException();
        }
        /// <summary>
        /// 获取棋的另一战方
        /// </summary>
        public static GameSide GetOtherGameSide(GameSide currSide)
        {
            if (currSide == GameSide.Black)
                return GameSide.White;
            return GameSide.Black;
        }

        #endregion

        #region ChessmanType

        /// <summary>
        /// 棋子类型的枚举。
        /// 车Rook，马Knight，象Bishop，后Queen，王King，兵Pawn。
        /// 中文简称王后车象马兵英文简称K Q R B N P
        /// </summary>
        public enum PieceType
        {
            /// <summary>
            /// 嘛也不是
            /// </summary>
            None        = 0,
            /// <summary>
            /// White's king
            /// </summary>
            WhiteKing   = 1,
            /// <summary>
            /// White's queen
            /// </summary>
            WhiteQueen  = 2,
            /// <summary>
            /// White's rook
            /// </summary>
            WhiteRook   = 4,
            /// <summary>
            /// White's bishop
            /// </summary>
            WhiteBishop = 8,
            /// <summary>
            /// White's knight 
            /// </summary>
            WhiteKnight = 16,
            /// <summary>
            /// White's pawn
            /// </summary>
            WhitePawn   = 32,
            /// <summary>
            /// Black's king
            /// </summary>
            BlackKing   = 64,
            /// <summary>
            /// Black's queen
            /// </summary>
            BlackQueen  = 128,
            /// <summary>
            /// Black's rook
            /// </summary>
            BlackRook   = 256,
            /// <summary>
            /// Black's bishop
            /// </summary>
            BlackBishop = 512,
            /// <summary>
            /// Black's knight
            /// </summary>
            BlackKnight = 1024,
            /// <summary>
            /// Black's pawn
            /// </summary>
            BlackPawn   = 2048,
            /// <summary>
            /// Used to hide all kings from displaying on the board.
            /// </summary>
            AllKings    = WhiteKing   | BlackKing,
            /// <summary>
            /// Used to hide all kings from displaying on the board.
            /// </summary>
            AllQueens   = WhiteQueen  | BlackQueen,
            /// <summary>
            /// Used to hide all queens from displaying on the board.
            /// </summary>
            AllRooks    = WhiteRook   | BlackRook,
            /// <summary>
            /// Used to hide all rooks from displaying on the board.
            /// </summary>
            AllBishops  = WhiteBishop | BlackBishop,
            /// <summary>
            /// Used to hide all bishops from displaying on the board.
            /// </summary>
            AllKnights  = WhiteKnight | BlackKnight,
            /// <summary>
            /// Used to hide all knights from displaying on the board.
            /// </summary>
            AllPawns    = WhitePawn   | BlackPawn,
            /// <summary>
            /// Used to hide all the pieces.
            /// </summary>
            All = AllBishops | AllKings | AllKnights | AllPawns | AllQueens | AllRooks
        }

        /// <summary>
        /// 返回将指定的字符解析出的棋子类型
        /// </summary>
        /// <param name="c">指定的字符</param>
        /// <returns></returns>
        public static PieceType ToPieceType(char c)
        {
            return Enums.ToPieceType(c.ToString());
        }
        public static PieceType ToPieceType(string c)
        {
            if (string.IsNullOrEmpty(c))
                throw new ArgumentNullException("Cannot Null or Empty");
            if (c.Length != 1)
                throw new FormatException(c);

            PieceType manType = PieceType.None;
            switch (c[0])
            {
                #region case
                case 'K':
                    manType = PieceType.WhiteKing;
                    break;
                case 'Q':
                    manType = PieceType.WhiteQueen;
                    break;
                case 'R':
                    manType = PieceType.WhiteRook;
                    break;
                case 'B':
                    manType = PieceType.WhiteBishop;
                    break;
                case 'N':
                    manType = PieceType.WhiteKnight;
                    break;
                case 'P':
                    manType = PieceType.WhitePawn;
                    break;
                case 'k':
                    manType = PieceType.BlackKing;
                    break;
                case 'q':
                    manType = PieceType.BlackQueen;
                    break;
                case 'r':
                    manType = PieceType.BlackRook;
                    break;
                case 'b':
                    manType = PieceType.BlackBishop;
                    break;
                case 'n':
                    manType = PieceType.BlackKnight;
                    break;
                case 'p':
                    manType = PieceType.BlackPawn;
                    break;
                #endregion
            }
            return manType;
        }
        public static string FromPieceType(PieceType type)
        {
            string man = "";
            switch (type)
            {
                #region case
                case PieceType.WhiteKing:
                    man = "K";
                    break;
                case PieceType.WhiteQueen:
                    man = "Q";
                    break;
                case PieceType.WhiteRook:
                    man = "R";
                    break;
                case PieceType.WhiteBishop:
                    man = "B";
                    break;
                case PieceType.WhiteKnight:
                    man = "N";
                    break;
                case PieceType.WhitePawn:
                    man = "P";
                    break;
                case PieceType.BlackKing:
                    man = "k";
                    break;
                case PieceType.BlackQueen:
                    man = "q";
                    break;
                case PieceType.BlackRook:
                    man = "r";
                    break;
                case PieceType.BlackBishop:
                    man = "b";
                    break;
                case PieceType.BlackKnight:
                    man = "n";
                    break;
                case PieceType.BlackPawn:
                    man = "p";
                    break;
                #endregion
            }
            return man;
        }
        public static GameSide ToGameSideByPieceType(PieceType type)
        {
            switch (type)
            {
                case PieceType.WhiteKing:
                case PieceType.WhiteQueen:
                case PieceType.WhiteRook:
                case PieceType.WhiteBishop:
                case PieceType.WhiteKnight:
                case PieceType.WhitePawn:
                    return GameSide.White;
                case PieceType.BlackKing:
                case PieceType.BlackQueen:
                case PieceType.BlackRook:
                case PieceType.BlackBishop:
                case PieceType.BlackKnight:
                case PieceType.BlackPawn:
                    return GameSide.Black;
                case PieceType.AllKings:
                case PieceType.AllQueens:
                case PieceType.AllRooks:
                case PieceType.AllBishops:
                case PieceType.AllKnights:
                case PieceType.AllPawns:
                case PieceType.All:
                case PieceType.None:
                default:
                    throw new RecordException(string.Format(ExString.PieceTypeIsError, type));
            }
        }

        #endregion

        #region PGNReaderState
        /// <summary>
        /// 当PGN文件解析时的解析状态
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
        #endregion

        #region Orientation
        /// <summary>
        /// 棋盘中的方向枚举。
        /// </summary>
        public enum Orientation
        {
            None = 0,
            /// <summary>
            /// 横方向
            /// </summary>
            Rank = 1,
            /// <summary>
            /// 纵方向
            /// </summary>
            File = 2,
            /// <summary>
            /// 斜方向
            /// </summary>
            Diagonal = 4,
        }
        #endregion

    }
}

/*

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
}
public static void FenChessmansToType(FenChessmans fenChessmans, out Enums.GameSide manSide, out Enums.ChessmanType manType)
{
    manSide = GameSide.White;
    manType = ChessmanType.None;
    switch (fenChessmans)
    {
        case FenChessmans.WhiteKing:
            manSide = GameSide.White;
            manType = ChessmanType.King;
            break;
        case FenChessmans.WhiteQueen:
            manSide = GameSide.White;
            manType = ChessmanType.Queen;
            break;
        case FenChessmans.WhiteRook:
            manSide = GameSide.White;
            manType = ChessmanType.Rook;
            break;
        case FenChessmans.WhiteBishop:
            manSide = GameSide.White;
            manType = ChessmanType.Bishop;
            break;
        case FenChessmans.WhiteKnight:
            manSide = GameSide.White;
            manType = ChessmanType.Knight;
            break;
        case FenChessmans.WhitePawn:
            manSide = GameSide.White;
            manType = ChessmanType.Pawn;
            break;
        case FenChessmans.BlackKing:
            manSide = GameSide.Black;
            manType = ChessmanType.King;
            break;
        case FenChessmans.BlackQueen:
            manSide = GameSide.Black;
            manType = ChessmanType.Queen;
            break;
        case FenChessmans.BlackRook:
            manSide = GameSide.Black;
            manType = ChessmanType.Rook;
            break;
        case FenChessmans.BlackBishop:
            manSide = GameSide.Black;
            manType = ChessmanType.Bishop;
            break;
        case FenChessmans.BlackKnight:
            manSide = GameSide.Black;
            manType = ChessmanType.Knight;
            break;
        case FenChessmans.BlackPawn:
            manSide = GameSide.Black;
            manType = ChessmanType.Pawn;
            break;
        case FenChessmans.None:
        case FenChessmans.OpenHand:
        case FenChessmans.ClosedHand:
        case FenChessmans.Delete:
        case FenChessmans.AllKings:
        case FenChessmans.AllQueens:
        case FenChessmans.AllRooks:
        case FenChessmans.AllBishops:
        case FenChessmans.AllKnights:
        case FenChessmans.AllPawns:
        case FenChessmans.AllNonPawns:
        case FenChessmans.AllMinors:
        case FenChessmans.All:
        default:
            break;
    }
}

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
*/
