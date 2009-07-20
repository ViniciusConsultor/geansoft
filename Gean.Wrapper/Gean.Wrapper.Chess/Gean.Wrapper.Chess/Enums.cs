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
            /// 王车短易位
            /// </summary>
            KingSide,
            /// <summary>
            /// 王车长易位
            /// </summary>
            QueenSide,
            /// <summary>
            /// 嘛也不是
            /// </summary>
            None,
        }

        /// <summary>
        /// 辅助的棋招的动作说明
        /// </summary>
        public enum AccessorialAction
        {
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
            /// 杀棋并将军
            /// </summary>
            KillAndCheck = Kill | Check,
        }

        public static AccessorialAction GetFlag(AccessorialAction value, AccessorialAction flag)
        {
            value = value & (AccessorialAction.KillAndCheck ^ flag);
            return value;
        }

        /// <summary>
        /// 棋子的战方：黑棋，白棋
        /// </summary>
        public enum ChessmanSide
        {
            White, Black,
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
        public enum ChessSquareSide
        {
            Black, White,
        }

        /// <summary>
        /// 获取棋格的另一方
        /// </summary>
        public static ChessSquareSide GetOtherGridSide(ChessSquareSide side)
        {
            if (side == ChessSquareSide.Black)
                return ChessSquareSide.White;
            return ChessSquareSide.Black;
        }

        /// <summary>
        /// 棋子类型的枚举。
        /// 车Rook，马Knight，象Bishop，后Queen，王King，兵Pawn。
        /// 中文简称王后车象马兵英文简称K Q R B N P
        /// </summary>
        public enum ChessmanType
        {
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
            /// <summary>
            /// 嘛也不是
            /// </summary>
            Nothing
        }

        /// <summary>
        /// 返回将指定的字符解析出的棋子类型
        /// </summary>
        /// <param name="c">指定的字符</param>
        /// <returns></returns>
        public static Enums.ChessmanType ParseChessmanType(char c)
        {
            Enums.ChessmanType manType;
            switch (c)
            {
                case 'O'://王车易位
                    manType = Enums.ChessmanType.Nothing;
                    break;
                case 'K':
                    manType = Enums.ChessmanType.King;
                    break;
                case 'Q':
                    manType = Enums.ChessmanType.Queen;
                    break;
                case 'R':
                    manType = Enums.ChessmanType.Rook;
                    break;
                case 'N':
                    manType = Enums.ChessmanType.Knight;
                    break;
                case 'B':
                    manType = Enums.ChessmanType.Bishop;
                    break;
                default:
                    throw new ChessStepParseException(c.ToString());
            }
            return manType;
        }

    }
}
