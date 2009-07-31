using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKnight : Chessman
    {
        public ChessmanKnight(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
            : base(Enums.ChessmanType.Knight, side)
        {
            ChessPoint point = Chessman.GetOpenningsPoint(side, gridSide, 2, 7);
            this.ChessPoints.Push(point);
        }

        public ChessmanKnight(Enums.ChessmanSide side, ChessPoint point)
            : base(Enums.ChessmanType.Knight, side)
        {
            this.ChessPoints.Push(point);
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
