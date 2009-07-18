using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanQueen : Chessman
    {
        internal ChessmanQueen(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.Queen, side)
        {
            Square square = new Square();
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    square = new Square(4, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    square = new Square(4, 8);
                    break;
            }
            this.Squares.Add(square);
        }

        internal ChessmanQueen(Enums.ChessmanSide side, Square square)
            : base(Enums.ChessmanType.Queen, side)
        {
            this.Squares.Add(square);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "Q";
        }
    }
}
