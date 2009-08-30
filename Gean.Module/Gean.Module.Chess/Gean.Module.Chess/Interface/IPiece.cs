using System;
namespace Gean.Module.Chess
{
    interface IPiece
    {
        Enums.GameSide GameSide { get; }
        bool IsCaptured { get; }
        Enums.PieceType PieceType { get; }
        Position Position { get; }
    }
}
