using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanBishop : Chessman
    {
        public ChessmanBishop(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
            : base(Enums.ChessmanType.Bishop, side)
        {
            ChessPosition point = Chessman.GetOpenningsPoint(side, gridSide, 3, 6);
            this.ChessPoints.Push(point);
        }

        public ChessmanBishop(Enums.ChessmanSide side, ChessPosition point)
            : base(Enums.ChessmanType.Bishop, side)
        {
            this.ChessPoints.Push(point);
        }

        public override void InitializeComponent()
        {
        }

        public override string ToSimpleString()
        {
            return "B";
        }
    }
}
