using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKnight : Chessman
    {
        //public ChessmanKnight(Enums.ChessmanSide side, Enums.ChessGridSide gridSide)
        //    : base(Enums.ChessmanType.Knight, side)
        //{
        //    this.CurrPosition = Chessman.GetOpenningsPosition(side, gridSide, 2, 7);
        //}

        //public ChessmanKnight(Enums.ChessmanSide side, ChessPosition pos)
        //    : base(Enums.ChessmanType.Knight, side)
        //{
        //    this.CurrPosition = pos;
        //}

        //public override ChessPosition[] GetEnablePositions()
        //{
        //    throw new NotImplementedException();
        //}
        protected override ChessPosition SetCurrPosition(ChessPosition position)
        {
            throw new NotImplementedException();
        }

        public override ChessPosition[] GetEnablePositions()
        {
            throw new NotImplementedException();
        }
    }
}
