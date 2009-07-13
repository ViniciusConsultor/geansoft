using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    public enum ChessBoth
    {
        White, Black,
    }

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

        //中文简称王后车象马兵英文简称K Q R B N P
    }

    public class Chessman
    {
        public ChessmanWord Man { get; set; }
        public Chessman(ChessmanWord man)
        {
            this.Man = man;
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
}
