using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    class ChessmanPawn : Chessman
    {
        public ChessmanPawn(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.Pawn, side)
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
