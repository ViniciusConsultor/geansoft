using System;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl
{
    public interface IChessBoard
    {
        Enums.GameSide CurrChessSide { get; }
        ChessGame ChessGame { get; }
    }
}
