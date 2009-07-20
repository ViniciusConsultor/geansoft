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
            ChessSquare square = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    {
                        Enums.ChessSquareSide squareSide = Enums.ChessSquareSide.White;
                        if (column % 2 != 0)
                        {
                            squareSide = Enums.ChessSquareSide.Black;
                        }
                        square = new ChessSquare(column, 2, squareSide);
                        break;
                    }
                case Enums.ChessmanSide.Black:
                    {
                        Enums.ChessSquareSide squareSide = Enums.ChessSquareSide.Black;
                        if (column % 2 != 0)
                        {
                            squareSide = Enums.ChessSquareSide.White;
                        }
                        square = new ChessSquare(column, 7, squareSide);
                        break;
                    }
            }
            this.Squares.Add(square);
        }

        internal ChessmanPawn(Enums.ChessmanSide side, ChessSquare square)
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
