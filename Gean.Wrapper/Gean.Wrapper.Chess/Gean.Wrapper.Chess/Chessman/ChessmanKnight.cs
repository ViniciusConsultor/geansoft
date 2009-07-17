using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    class ChessmanKnight : Chessman
    {
        public ChessmanKnight(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.Knight, side)
        {
            this.ChessmanSide = side;
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "N";
        }
    }
}
