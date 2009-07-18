using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanPawn : Chessman
    {
        internal ChessmanPawn(Enums.ChessmanSide side, int column)
            : base(Enums.ChessmanType.Pawn, side)
        {
            Square square = new Square();
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    square = new Square(column, 2);
                    break;
                case Enums.ChessmanSide.Black:
                    square = new Square(column, 7);
                    break;
            }
            this.Squares.Add(square);
        }

        internal ChessmanPawn(Enums.ChessmanSide side, Square square)
            : base(Enums.ChessmanType.Pawn, side)
        {
            this.Squares.Add(square);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "P";
        }
    }
}
