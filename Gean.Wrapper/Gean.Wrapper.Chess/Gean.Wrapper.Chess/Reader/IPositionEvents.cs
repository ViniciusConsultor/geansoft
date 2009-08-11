using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// Defines the events that the IPosition class will need to fire to allow
    ///   the proper setup of a chess position.
    /// </summary>
    public interface IPositionEvents
    {
        /// <summary>
        /// Used to inform a subscriber that a new piece needs to be
        ///  placed onto a square.
        ///  </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        void PlacePiece(FenChessmans piece, int square);
        /// <summary>
        /// Used to inform a subscriber who's move it is.    
        /// </summary>
        /// <param name="bColor">True for white else false for black</param>
        void SetSideToMove(bool bColor);
        /// <summary>
        /// Used to inform a subscriber the state of castling.
        /// </summary>
        /// <param name="WK"></param>
        /// <param name="WQ"></param>
        /// <param name="BK"></param>
        /// <param name="BQ"></param>
        void SetCastling(bool WK, bool WQ, bool BK, bool BQ);
        /// <summary>
        /// The target square for Enpassant Captures.  Note per the standard
        /// There does not nessasarily need to be a pawn on either sided that
        /// can fullfill the capture.
        /// </summary>
        /// <param name="Enpassant"></param>
        void SetEnpassant(string Enpassant);
        void SetHalfMoves(int number);
        void SetFullMoves(int number);
        /// <summary>
        /// Called when we are done parsing/setting up the position.
        /// </summary>
        void Finished();
        /// <summary>
        /// Called when we are about to begin 
        /// </summary>
        void Starting();
    }
}
