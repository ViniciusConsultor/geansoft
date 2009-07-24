using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanPawn : ChessmanBase
    {
        internal ChessmanPawn(Enums.ChessmanSide side, int column)
            : base(Enums.ChessmanType.Pawn, side)
        {
            ChessSquare square = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    {
                        square = new ChessSquare(column, 2);
                        break;
                    }
                case Enums.ChessmanSide.Black:
                    {
                        square = new ChessSquare(column, 7);
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
