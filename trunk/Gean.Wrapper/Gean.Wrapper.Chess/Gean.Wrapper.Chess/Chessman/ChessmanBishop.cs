using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    class ChessmanBishop : ChessmanBase
    {
        public ChessmanBishop(ChessboardGrid grid, Enums.ChessmanSide side)
            : base(grid, Enums.ChessmanType.Bishop)
        {
            this.ChessmanSide = side;
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "B";
        }

        public override ChessboardGrid[] GetGridsByPath()
        {
            throw new NotImplementedException();
        }
    }
}
