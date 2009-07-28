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
            ChessGrid point = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    {
                        point = new ChessGrid(column, 2);
                        break;
                    }
                case Enums.ChessmanSide.Black:
                    {
                        point = new ChessGrid(column, 7);
                        break;
                    }
            }
            this.ChessGrids.Add(point);
        }

        internal ChessmanPawn(Enums.ChessmanSide side, ChessGrid point)
            : base(Enums.ChessmanType.Pawn, side)
        {
            this.ChessGrids.Add(point);
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
