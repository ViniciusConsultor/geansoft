using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    class ChessmanPawn : ChessmanBase
    {
        public ChessmanPawn(ChessboardGrid grid, Enums.ChessmanSide side)
            : base(grid, Enums.ChessmanType.Pawn)
        {
            this.ChessmanSide = side;
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
