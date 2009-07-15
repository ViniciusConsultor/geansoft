using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class Enums
    {

        /// <summary>
        /// 棋子的战方：黑棋，白棋
        /// </summary>
        public enum ChessmanSide
        {
            White, Black,
        }

        /// <summary>
        /// 黑格，白格
        /// </summary>
        public enum ChessboardGridSide
        {
            Black, White,
        }

        /// <summary>
        /// 获取另一方
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public static ChessmanSide GetOtherSide(ChessmanSide ct)
        {
            if (ct == ChessmanSide.Black) return ChessmanSide.White;
            return ChessmanSide.Black;
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
            Pawn
        }


    }
}
