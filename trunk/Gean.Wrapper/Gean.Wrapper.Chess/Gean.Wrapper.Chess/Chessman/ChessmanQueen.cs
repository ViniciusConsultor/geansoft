﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanQueen : Chessman
    {
        internal ChessmanQueen(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.Queen, side)
        {
            ChessSquare square = null;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    square = new ChessSquare(4, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    square = new ChessSquare(4, 8);
                    break;
            }
            this.Squares.Add(square);
        }

        internal ChessmanQueen(Enums.ChessmanSide side, ChessSquare square)
            : base(Enums.ChessmanType.Queen, side)
        {
            this.Squares.Add(square);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "Q";
        }
    }
}