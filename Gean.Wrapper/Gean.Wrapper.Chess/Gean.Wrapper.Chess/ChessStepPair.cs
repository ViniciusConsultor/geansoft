using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一个棋招。
    /// 棋招的定义是成对的<see>ChessStep</see>,它代表着双方各走了一步棋，
    /// 同时它拥有棋局中的对“步数”的编号。
    /// </summary>
    public class ChessStepPair
    {
        public ChessStep White { get; set; }
        public ChessStep Black { get; set; }
        public int Number { get; set; }

        public ChessStepPair(int number, ChessStep white, ChessStep black)
        {
            if (number <= 0)
            {
                throw new ArgumentException(number.ToString() + " cannot <=0");
            }
            this.Number = number;
            this.White = white;
            this.Black = black;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Number).Append(". ").Append(this.White).Append(' ').Append(this.Black);
            return sb.ToString();
        }
    }
}
