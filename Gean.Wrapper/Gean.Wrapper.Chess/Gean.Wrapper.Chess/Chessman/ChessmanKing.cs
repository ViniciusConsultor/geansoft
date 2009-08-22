using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanKing : Chessman
    {
        public ChessmanKing(Enums.ChessmanSide side)
            : base(Enums.ChessmanType.King, side)
        {
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    this.CurrPosition = new ChessPosition(5, 1);
                    break;
                case Enums.ChessmanSide.Black:
                    this.CurrPosition = new ChessPosition(5, 8);
                    break;
            }
            //this.ChessPositions.Push(point);
        }

        public ChessmanKing(Enums.ChessmanSide side, ChessPosition pos)
            : base(Enums.ChessmanType.King, side)
        {
            this.CurrPosition = pos;
        }

        public override ChessPosition[] GetEnablePositions()
        {
            throw new NotImplementedException();
        }
    }
}
