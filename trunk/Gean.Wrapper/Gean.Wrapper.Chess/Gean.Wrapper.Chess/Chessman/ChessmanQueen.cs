using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanQueen : Chessman
    {
        //public ChessmanQueen(Enums.ChessmanSide side)
        //    : base(Enums.ChessmanType.Queen, side)
        //{
        //    switch (side)
        //    {
        //        case Enums.ChessmanSide.White:
        //            this.CurrPosition = new ChessPosition(4, 1);
        //            break;
        //        case Enums.ChessmanSide.Black:
        //            this.CurrPosition = new ChessPosition(4, 8);
        //            break;
        //    }
        //}

        //public ChessmanQueen(Enums.ChessmanSide side, ChessPosition pos)
        //    : base(Enums.ChessmanType.Queen, side)
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
