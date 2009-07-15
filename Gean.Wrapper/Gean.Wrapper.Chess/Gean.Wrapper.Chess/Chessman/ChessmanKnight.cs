using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    class ChessmanKnight : ChessmanBase
    {
        public ChessmanKnight(ChessboardGrid grid, Enums.ChessmanSide side)
            : base(grid, Enums.ChessmanType.Knight)
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

        public override ChessboardGrid[] GetGridsByPath()
        {
            throw new NotImplementedException();
        }
    }
}
