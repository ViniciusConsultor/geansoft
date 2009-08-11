using System;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// Defines some basic conversion routines.
    /// </summary>
    public class FenConvertChessman
    {
        /// <summary>
        /// Returns a FEN piece representation base on our 
        /// internal piece enumeration.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static FenChessmans FromFEN(char piece)
        {
            FenChessmans aPiece = FenChessmans.None;

            switch (piece)
            {
                case 'K':
                    aPiece = FenChessmans.WhiteKing;
                    break;
                case 'Q':
                    aPiece = FenChessmans.WhiteQueen;
                    break;
                case 'R':
                    aPiece = FenChessmans.WhiteRook;
                    break;
                case 'B':
                    aPiece = FenChessmans.WhiteBishop;
                    break;
                case 'N':
                    aPiece = FenChessmans.WhiteKnight;
                    break;
                case 'P':
                    aPiece = FenChessmans.WhitePawn;
                    break;
                case 'k':
                    aPiece = FenChessmans.BlackKing;
                    break;
                case 'q':
                    aPiece = FenChessmans.BlackQueen;
                    break;
                case 'r':
                    aPiece = FenChessmans.BlackRook;
                    break;
                case 'b':
                    aPiece = FenChessmans.BlackBishop;
                    break;
                case 'n':
                    aPiece = FenChessmans.BlackKnight;
                    break;
                case 'p':
                    aPiece = FenChessmans.BlackPawn;
                    break;
            }
            return aPiece;
        }
        /// <summary>
        /// Returns a FEN piece representation base on our 
        /// internal piece enumeration.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static char ToFEN(FenChessmans piece)
        {
            char aPiece = ' ';

            switch (piece)
            {
                case FenChessmans.WhiteKing:
                    aPiece = 'K';
                    break;
                case FenChessmans.WhiteQueen:
                    aPiece = 'Q';
                    break;
                case FenChessmans.WhiteRook:
                    aPiece = 'R';
                    break;
                case FenChessmans.WhiteBishop:
                    aPiece = 'B';
                    break;
                case FenChessmans.WhiteKnight:
                    aPiece = 'N';
                    break;
                case FenChessmans.WhitePawn:
                    aPiece = 'P';
                    break;
                case FenChessmans.BlackKing:
                    aPiece = 'k';
                    break;
                case FenChessmans.BlackQueen:
                    aPiece = 'q';
                    break;
                case FenChessmans.BlackRook:
                    aPiece = 'r';
                    break;
                case FenChessmans.BlackBishop:
                    aPiece = 'b';
                    break;
                case FenChessmans.BlackKnight:
                    aPiece = 'n';
                    break;
                case FenChessmans.BlackPawn:
                    aPiece = 'p';
                    break;
            }
            return aPiece;
        }
        /// <summary>
        /// Translates an internal piece enumeration into an algebraic piece character.
        /// </summary>
        /// <param name="piece"></param>
        /// <returns>Character representing our piece.</returns>
        public static char ToNotation(FenChessmans piece)
        {
            char aPiece = ' ';
            switch (piece)
            {
                case FenChessmans.WhiteKing:
                case FenChessmans.BlackKing:
                    aPiece = 'K';
                    break;
                case FenChessmans.WhiteQueen:
                case FenChessmans.BlackQueen:
                    aPiece = 'Q';
                    break;
                case FenChessmans.WhiteRook:
                case FenChessmans.BlackRook:
                    aPiece = 'R';
                    break;
                case FenChessmans.WhiteBishop:
                case FenChessmans.BlackBishop:
                    aPiece = 'B';
                    break;
                case FenChessmans.WhiteKnight:
                case FenChessmans.BlackKnight:
                    aPiece = 'N';
                    break;
                case FenChessmans.WhitePawn:
                case FenChessmans.BlackPawn:
                    aPiece = 'P';
                    break;
            }
            return aPiece;
        }
    }
}
