using System;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public interface IChessBoard
    {
        Enums.ChessmanSide CurrChessSide { get; }
        void MoveIn(ChessPosition srcPos, ChessPosition tgtPos);
        ChessGame OwnedChessGame { get; }
    }
}
