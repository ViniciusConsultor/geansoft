using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class Enums
    {
        /// <summary>
        /// 王车易位
        /// </summary>
        public enum Castling
        {
            /// <summary>
            /// 嘛也不是
            /// </summary>
            None = 0,
            /// <summary>
            /// 王车短易位
            /// </summary>
            KingSide,
            /// <summary>
            /// 王车长易位
            /// </summary>
            QueenSide,
        }

        /// <summary>
        /// 棋招动作枚举
        /// </summary>
        public enum Action
        {
            /// <summary>
            /// 嘛也不是
            /// </summary>
            None = 0,
            /// <summary>
            /// 普通棋招
            /// </summary>
            General = 1,
            /// <summary>
            /// 有棋被杀死
            /// </summary>
            Kill = 2,
            /// <summary>
            /// 将军
            /// </summary>
            Check = 4,
            /// <summary>
            /// 王车易位
            /// </summary>
            Castling = 8,
            /// <summary>
            /// 开局摆棋
            /// </summary>
            Opennings =16,
            /// <summary>
            /// 杀棋并将军
            /// </summary>
            KillAndCheck = Kill | Check,
        }

        public static Action GetFlag(Action value, Action flag)
        {
            value = value & (Action.KillAndCheck ^ flag);
            return value;
        }

        public class ActionGridPair
        {
            public ChessGrid Grid { get; private set; }
            public Enums.Action Action { get; private set; }
            public ActionGridPair(ChessGrid rid, Enums.Action action)
            {
                this.Grid = rid;
                this.Action = action;
            }
        }


        /// <summary>
        /// 棋子的战方：黑棋，白棋
        /// </summary>
        public enum ChessmanSide : byte
        {
            None = 0, White, Black,
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
            None = 0, Black, White,
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
            Rook,
            /// <summary>
            /// 马((中古时代的)武士, 骑士)
            /// </summary>
            Knight,
            /// <summary>
            /// 象((基督教某些教派管辖大教区的)主教)
            /// </summary>
            Bishop,
            /// <summary>
            /// 皇后
            /// </summary>
            Queen,
            /// <summary>
            /// 王
            /// </summary>
            King,
            /// <summary>
            /// 兵
            /// </summary>
            Pawn,
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

    }
}
