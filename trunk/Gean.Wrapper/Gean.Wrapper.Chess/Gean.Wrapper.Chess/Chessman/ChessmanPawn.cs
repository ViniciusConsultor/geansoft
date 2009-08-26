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
                new ChessmanPawn(Enums.GameSide.Black, new ChessPosition(1,7)),
                new ChessmanPawn(Enums.GameSide.Black, new ChessPosition(2,7)),
                new ChessmanPawn(Enums.GameSide.Black, new ChessPosition(3,7)),
                new ChessmanPawn(Enums.GameSide.Black, new ChessPosition(4,7)),
                new ChessmanPawn(Enums.GameSide.Black, new ChessPosition(5,7)),
                new ChessmanPawn(Enums.GameSide.Black, new ChessPosition(6,7)),
                new ChessmanPawn(Enums.GameSide.Black, new ChessPosition(7,7)),
                new ChessmanPawn(Enums.GameSide.Black, new ChessPosition(8,7))
            };
            #endregion
        public static ChessmanPawn[] WhitePawns = new ChessmanPawn[]
            #region
            {
                new ChessmanPawn(Enums.GameSide.White, new ChessPosition(1,2)),
                new ChessmanPawn(Enums.GameSide.White, new ChessPosition(2,2)),
                new ChessmanPawn(Enums.GameSide.White, new ChessPosition(3,2)),
                new ChessmanPawn(Enums.GameSide.White, new ChessPosition(4,2)),
                new ChessmanPawn(Enums.GameSide.White, new ChessPosition(5,2)),
                new ChessmanPawn(Enums.GameSide.White, new ChessPosition(6,2)),
                new ChessmanPawn(Enums.GameSide.White, new ChessPosition(7,2)),
                new ChessmanPawn(Enums.GameSide.White, new ChessPosition(8,2))
            };
            #endregion

        public ChessmanPawn(Enums.GameSide side, ChessPosition position)
        {
            this.GameSide = side;
            switch (side)
            {
                case Enums.GameSide.White:
                    this.ChessmanType = Enums.ChessmanType.WhitePawn;
                    break;
                case Enums.GameSide.Black:
                    this.ChessmanType = Enums.ChessmanType.BlackPawn;
                    break;
            }
            this.IsCaptured = false;
            this._currPosition = this.SetCurrPosition(position);
        }

        protected override ChessPosition SetCurrPosition(ChessPosition position)
        {
            if (position.Equals(ChessPosition.Empty))
            {
                switch (this.GameSide)
                {
                    case Enums.GameSide.White:
                        return new ChessPosition(5, 1);
                    case Enums.GameSide.Black:
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
            return this.CurrPosition.GetPawnPositions(this.GameSide);
        }
    }
}
