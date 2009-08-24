using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanPawn : Chessman
    {
        public static ChessmanPawn[] BlackPawns = new ChessmanPawn[]
            #region
            {
                new ChessmanPawn(Enums.ChessmanSide.Black,new ChessPosition(1,7)),
                new ChessmanPawn(Enums.ChessmanSide.Black,new ChessPosition(2,7)),
                new ChessmanPawn(Enums.ChessmanSide.Black,new ChessPosition(3,7)),
                new ChessmanPawn(Enums.ChessmanSide.Black,new ChessPosition(4,7)),
                new ChessmanPawn(Enums.ChessmanSide.Black,new ChessPosition(5,7)),
                new ChessmanPawn(Enums.ChessmanSide.Black,new ChessPosition(6,7)),
                new ChessmanPawn(Enums.ChessmanSide.Black,new ChessPosition(7,7)),
                new ChessmanPawn(Enums.ChessmanSide.Black,new ChessPosition(8,7))
            };
            #endregion
        public static ChessmanPawn[] WhitePawns = new ChessmanPawn[]
            #region
            {
                new ChessmanPawn(Enums.ChessmanSide.White,new ChessPosition(1,8)),
                new ChessmanPawn(Enums.ChessmanSide.White,new ChessPosition(2,8)),
                new ChessmanPawn(Enums.ChessmanSide.White,new ChessPosition(3,8)),
                new ChessmanPawn(Enums.ChessmanSide.White,new ChessPosition(4,8)),
                new ChessmanPawn(Enums.ChessmanSide.White,new ChessPosition(5,8)),
                new ChessmanPawn(Enums.ChessmanSide.White,new ChessPosition(6,8)),
                new ChessmanPawn(Enums.ChessmanSide.White,new ChessPosition(7,8)),
                new ChessmanPawn(Enums.ChessmanSide.White,new ChessPosition(8,8))
            };
            #endregion

        public ChessmanPawn(Enums.ChessmanSide side, ChessPosition position)
        {
            this.ChessmanSide = side;
            switch (side)
            {
                case Enums.ChessmanSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhitePawn;
                    break;
                case Enums.ChessmanSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackPawn;
                    break;
            }
            this.IsCaptured = false;
            this.CurrPosition = this.SetCurrPosition(position);
        }

        protected override ChessPosition SetCurrPosition(ChessPosition position)
        {
            if (position.Equals(ChessPosition.Empty))
            {
                switch (this.ChessmanSide)
                {
                    case Enums.ChessmanSide.White:
                        return new ChessPosition(5, 1);
                    case Enums.ChessmanSide.Black:
                        return new ChessPosition(5, 8);
                    default:
                        throw new ChessmanException(ExString.ChessPositionIsEmpty);
                }
            }
            else
                return position;
        }

        public override ChessPosition[] GetEnablePositions()
        {
            return this.CurrPosition.GetKingPositions();
        }
    }
}
