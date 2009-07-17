using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 开局的棋子状态类
    /// </summary>
    internal class ChessmanState
    {
        public Chessman Chessman { get; private set; }
        public Square Square { get; private set; }
        public ChessmanState(Chessman chessman, Square square)
        {
            this.Chessman = chessman;
            this.Square = square;
        }
        public override string ToString()
        {
            return this.Chessman.ToString() + " " + this.Square.ToString();
        }
    }
}
