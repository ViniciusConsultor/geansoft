﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    class ChessmanKing : ChessmanBase
    {
        public ChessmanKing(ChessboardGrid grid, Enums.ChessmanSide side)
            : base(grid, Enums.ChessmanType.King)
        {
            this.ChessmanSide = side;
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "K";
        }

        public override ChessboardGrid[] GetGridsByPath()
        {
            throw new NotImplementedException();
        }
    }
}
