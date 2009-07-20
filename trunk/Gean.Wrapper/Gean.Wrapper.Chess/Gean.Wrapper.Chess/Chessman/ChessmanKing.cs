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
            ChessSquare square = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    square = new ChessSquare(5, 1, Enums.ChessSquareSide.White);
                    break;
                case Enums.ChessmanSide.Black:
                    square = new ChessSquare(5, 8, Enums.ChessSquareSide.Black);
                    break;
            }
            this.Squares.Add(square);
        }

        internal ChessmanKing(Enums.ChessmanSide side, ChessSquare square)
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
