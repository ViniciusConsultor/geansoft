using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public interface IGame : IPieceMove, IEnumerable<Piece>
    {
        Pieces ActivedPieces { get; }
        Pieces MovedPieces { get; }
        Piece this[int dot] { get; }
    }
}
