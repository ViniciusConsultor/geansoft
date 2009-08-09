namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// Internal naming for Pieces, also used for indexing into an array of cursors.
    /// </summary>
    public enum FenChessmans
    {
        /// <summary>
        /// No piece or empty square.
        /// </summary>
        None,
        /// <summary>
        /// White's king
        /// </summary>
        WhiteKing,
        /// <summary>
        /// White's queen
        /// </summary>
        WhiteQueen,
        /// <summary>
        /// White's rook
        /// </summary>
        WhiteRook,
        /// <summary>
        /// White's bishop
        /// </summary>
        WhiteBishop,
        /// <summary>
        /// White's knight 
        /// </summary>
        WhiteKnight,
        /// <summary>
        /// White's pawn
        /// </summary>
        WhitePawn,
        /// <summary>
        /// Black's king
        /// </summary>
        BlackKing,
        /// <summary>
        /// Black's queen
        /// </summary>
        BlackQueen,
        /// <summary>
        /// Black's rook
        /// </summary>
        BlackRook,
        /// <summary>
        /// Black's bishop
        /// </summary>
        BlackBishop,
        /// <summary>
        /// Black's knight
        /// </summary>
        BlackKnight,
        /// <summary>
        /// Black's pawn
        /// </summary>
        BlackPawn,
        /// <summary>
        /// Open hand cursor.
        /// </summary>
        OpenHand,
        /// <summary>
        /// Closed hand cursor.
        /// </summary>
        ClosedHand,
        /// <summary>
        /// Remove of the piece.
        /// </summary>
        Delete,
        /// <summary>
        /// Used to hide all kings from displaying on the board.
        /// </summary>
        AllKings,
        /// <summary>
        /// Used to hide all kings from displaying on the board.
        /// </summary>
        AllQueens,
        /// <summary>
        /// Used to hide all queens from displaying on the board.
        /// </summary>
        AllRooks,
        /// <summary>
        /// Used to hide all rooks from displaying on the board.
        /// </summary>
        AllBishops,
        /// <summary>
        /// Used to hide all bishops from displaying on the board.
        /// </summary>
        AllKnights,
        /// <summary>
        /// Used to hide all knights from displaying on the board.
        /// </summary>
        AllPawns,
        /// <summary>
        /// Used to hide all pawns from displaying on the board.
        /// </summary>
        AllNonPawns,
        /// <summary>
        /// Used to hide all the non-pawns from displaying on the board.
        /// </summary>
        AllMinors,
        /// <summary>
        /// Used to hide all the pieces.
        /// </summary>
        All
    };
}
