using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKing : Chessman
    {
        internal ChessmanKing(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.King, side)
        {
            Square square = new Square();
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    square = new Square(5, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    square = new Square(5, 8);
                    break;
            }
            this.Squares.Add(square);
        }

        internal ChessmanKing(Enums.ChessmanSide side, Square square)
            : base(Enums.ChessmanType.King, side)
        {
            this.Squares.Add(square);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "K";
        }
    }
}
