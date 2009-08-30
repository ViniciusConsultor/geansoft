using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public interface IPieceMove
    {
        Enums.Action PieceMoveIn(Pair<Position, Position> step);
        event PieceMoveIn PieceMoveInEvent;

        Enums.Action PieceMoveOut(Piece piece, Position pos);
        event PieceMoveOut PieceMoveOutEvent;

    }
    public delegate void PieceMoveIn(Enums.Action action, Pair<Position, Position> step, out Piece currPiece, out Piece movedPiece);
    public delegate void PieceMoveOut(Enums.Action action, Pair<Position, Position> step, out Piece currPiece, out Piece movedPiece);
}
