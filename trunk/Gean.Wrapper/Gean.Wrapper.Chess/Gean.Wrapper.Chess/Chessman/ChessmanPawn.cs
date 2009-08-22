using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanPawn : Chessman
    {
        public ChessmanPawn(Enums.ChessmanSide side, int column)
            : base(Enums.ChessmanType.Pawn, side)
        {
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    {
                        this.CurrPosition = new ChessPosition(column, 2);
                        break;
                    }
                case Enums.ChessmanSide.Black:
                    {
                        this.CurrPosition = new ChessPosition(column, 7);
                        break;
                    }
            }
        }

        public ChessmanPawn(Enums.ChessmanSide side, ChessPosition pos)
            : base(Enums.ChessmanType.Pawn, side)
        {
            this.CurrPosition = pos;
        }

        public override ChessPosition[] GetEnablePositions()
        {
            return this.CurrPosition.GetPawnPositions(this.ChessmanSide);
        }
    }
}
