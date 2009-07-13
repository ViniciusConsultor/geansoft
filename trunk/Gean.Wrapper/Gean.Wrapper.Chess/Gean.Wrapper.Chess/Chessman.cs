using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 棋子类
    /// </summary>
    public class Chessman
    {
        /// <summary>
        /// 获取对该实例棋子的具体表现
        /// </summary>
        public ChessmanWord Man { get; private set; }
        /// <summary>
        /// 获取对该实例棋子是黑棋或是白棋
        /// </summary>
        public ChessBoth Both { get; set; }

        /// <summary>
        /// 该棋子类型的构造函数
        /// </summary>
        /// <param name="man">棋子的具体表现</param>
        /// <param name="both">黑棋或是白棋</param>
        public Chessman(ChessmanWord man, ChessBoth both)
        {
            this.Man = man;
            this.Both = both;
        }
        public override string ToString()
        {
            return this.Man.ToString();
        }
        public string ToSimpleString()
        {
            switch (this.Man)
            {
                case ChessmanWord.Rook:
                    return "R";
                case ChessmanWord.Knight:
                    return "N";
                case ChessmanWord.Bishop:
                    return "B";
                case ChessmanWord.Queen:
                    return "Q";
                case ChessmanWord.King:
                    return "K";
                case ChessmanWord.Pawn:
                    return "P";
                default:
                    Debug.Fail(this.Man.ToString());
                    return string.Empty;
            }
        }
    }

    /// <summary>
    /// 黑棋，白棋
    /// </summary>
    public enum ChessBoth
    {
        /* 黑棋，白棋 */
        /// <summary>
        /// 白棋
        /// </summary>
        White,
        /// <summary>
        /// 黑棋
        /// </summary>
        Black,
    }

    /// <summary>
    /// 棋子的枚举。
    /// 车Rook，马Knight，象Bishop，后Queen，王King，兵Pawn。
    /// 中文简称王后车象马兵英文简称K Q R B N P
    /// </summary>
    public enum ChessmanWord
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
