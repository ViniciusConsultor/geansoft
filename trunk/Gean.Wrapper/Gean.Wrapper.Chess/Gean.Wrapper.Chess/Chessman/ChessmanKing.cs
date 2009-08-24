using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKing : Chessman
    {
        public ChessmanKing(Enums.ChessmanSide side) : this(side, ChessPosition.Empty) { }
        public ChessmanKing(Enums.ChessmanSide side, ChessPosition position)
        {
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhiteKing;
                    break;
                case Enums.ChessmanSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackKing;
                    break;
            }
            this.IsCaptured = false;
            this.CurrPosition = this.SetCurrPosition(position);
        }
        //public ChessmanKing(Enums.ChessmanSide side)
        //    : base(Enums.ChessmanType.King, side)
        //{
        //    switch (side)
        //    {
        //        case Enums.ChessmanSide.White:
        //            this.CurrPosition = new ChessPosition(5, 1);
        //            break;
        //        case Enums.ChessmanSide.Black:
        //            this.CurrPosition = new ChessPosition(5, 8);
        //            break;
        //    }
        //    //this.ChessPositions.Push(point);
        //}

        //public ChessmanKing(Enums.ChessmanSide side, ChessPosition pos)
        //    : base(Enums.ChessmanType.King, side)
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
