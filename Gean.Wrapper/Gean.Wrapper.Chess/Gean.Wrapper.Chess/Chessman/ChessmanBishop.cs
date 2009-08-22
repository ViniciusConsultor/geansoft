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
            this.CurrPosition = Chessman.GetOpenningsPosition(side, gridSide, 3, 6);
        }

        public ChessmanBishop(Enums.ChessmanSide side, ChessPosition pos)
            : base(Enums.ChessmanType.Bishop, side)
        {
            this.CurrPosition = pos;
        }

        public override ChessPosition[] GetEnablePositions()
        {
            throw new NotImplementedException();
        }
    }
}
