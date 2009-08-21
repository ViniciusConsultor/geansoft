using System;
using System.Text;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    public class Board
    {
        public enum enmOrientation
        {
            White, Black
        }

        public const byte H_COUNT = 8;
        /// <summary>
        /// ��������
        /// </summary>
        public const byte V_COUNT = 8;
        public const byte Matrix_Width = 16;
        public const byte Square_Count = 128;

        public static ulong HashCodeA = 0;
        public static ulong HashCodeB = 0;
        public static ulong PawnHashCodeA = 0;
        public static ulong PawnHashCodeB = 0;

        private static enmOrientation _orientation = enmOrientation.White;
        private static Square[] _squares = new Square[H_COUNT * Matrix_Width];

        static Board()
        {
            for (int intOrdinal = 0; intOrdinal < Square_Count; intOrdinal++)
            {
                _squares[intOrdinal] = new Square(intOrdinal);
            }
        }

        public static void Flip()
        {
            _orientation = _orientation == enmOrientation.White ? enmOrientation.Black : enmOrientation.White;
        }

        public static enmOrientation Orientation
        {
            get { return _orientation; }
            set { _orientation = value; }
        }

        public static Square GetSquare(int Ordinal)
        {
            return (Ordinal & 0x88) == 0 ? _squares[Ordinal] : null;
        }

        public static Square GetSquare(int File, int Rank)
        {
            return (OrdinalFromFileRank(File, Rank) & 0x88) == 0 ? _squares[OrdinalFromFileRank(File, Rank)] : null;
        }

        public static Square GetSquare(string Label)
        {
            return _squares[OrdinalFromFileRank(FileFromName(Label.Substring(0, 1)), int.Parse(Label.Substring(1, 1)) - 1)];
        }

        public static int FileFromName(string FileName)
        {
            switch (FileName)
            {
                case "a":
                    return 0;
                case "b":
                    return 1;
                case "c":
                    return 2;
                case "d":
                    return 3;
                case "e":
                    return 4;
                case "f":
                    return 5;
                case "g":
                    return 6;
                case "h":
                    return 7;
            }
            return -1;
        }

        public static ChessPiece GetPiece(int Ordinal)
        {
            return (Ordinal & 0x88) == 0 ? _squares[Ordinal].Piece : null;
        }

        public static ChessPiece GetPiece(int File, int Rank)
        {
            return (OrdinalFromFileRank(File, Rank) & 0x88) == 0 ? _squares[OrdinalFromFileRank(File, Rank)].Piece : null;
        }

        private static int OrdinalFromFileRank(int File, int Rank)
        {
            return (Rank << 4) | File;
        }

        public static void LineThreatenedBy(Player player, Squares squares, Square squareStart, int Offset)
        {
            int intOrdinal = squareStart.Ordinal;
            Square square;

            intOrdinal += Offset;
            while ((square = Board.GetSquare(intOrdinal)) != null)
            {

                if (square.Piece == null)
                {
                    squares.Add(square);
                }
                else if (square.Piece.Player.Colour != player.Colour && square.Piece.IsCapturable)
                {
                    squares.Add(square);
                    break;
                }
                else
                {
                    break;
                }
                intOrdinal += Offset;
            }
        }

        public static void AppendPiecePath(Moves moves, ChessPiece piece, Player player, int Offset, Moves.enmMovesType movesType)
        {
            int intOrdinal = piece.Square.Ordinal;
            Square square;

            intOrdinal += Offset;
            while ((square = Board.GetSquare(intOrdinal)) != null)
            {

                if (square.Piece == null)
                {
                    if (movesType == Moves.enmMovesType.All)
                    {
                        moves.Add(0, 0, Move.enmName.Standard, piece, piece.Square, square, null, 0, 0);
                    }
                }
                else if (square.Piece.Player.Colour != player.Colour && square.Piece.IsCapturable)
                {
                    moves.Add(0, 0, Move.enmName.Standard, piece, piece.Square, square, square.Piece, 0, 0);
                    break;
                }
                else
                {
                    break;
                }
                intOrdinal += Offset;
            }
        }

        public static ChessPiece LinesFirstPiece(Player.enmColour colour, ChessPiece.enmName PieceName, Square squareStart, int Offset)
        {
            int intOrdinal = squareStart.Ordinal;
            Square square;

            intOrdinal += Offset;
            while ((square = Board.GetSquare(intOrdinal)) != null)
            {

                if (square.Piece == null)
                {
                }
                else if (square.Piece.Player.Colour != colour)
                {
                    return null;
                }
                else if (square.Piece.Name == PieceName || square.Piece.Name == ChessPiece.enmName.Queen)
                {
                    return square.Piece;
                }
                else
                {
                    return null;
                }
                intOrdinal += Offset;
            }
            return null;
        }

        public static int LineIsOpen(Player.enmColour colour, Square squareStart, int Offset)
        {
            int intOrdinal = squareStart.Ordinal;
            int intSquareCount = 0;
            int intPenalty = 0;
            Square square;

            intOrdinal += Offset;

            while (intSquareCount <= 2 && ((square = Board.GetSquare(intOrdinal)) != null && (square.Piece == null || (square.Piece.Name != ChessPiece.enmName.Pawn && square.Piece.Name != ChessPiece.enmName.Rook) || square.Piece.Player.Colour != colour)))
            {
                intPenalty += 75;
                intSquareCount++;
                intOrdinal += Offset;
            }
            return intPenalty;
        }

        public static void EstablishHashKey()
        {
            ChessPiece piece;
            HashCodeA = 0UL;
            HashCodeB = 0UL;
            PawnHashCodeA = 0UL;
            PawnHashCodeB = 0UL;
            for (int intOrdinal = 0; intOrdinal < Square_Count; intOrdinal++)
            {
                piece = Board.GetPiece(intOrdinal);
                if (piece != null)
                {
                    HashCodeA ^= piece.HashCodeAForSquareOrdinal(intOrdinal);
                    HashCodeB ^= piece.HashCodeBForSquareOrdinal(intOrdinal);
                    if (piece.Name == ChessPiece.enmName.Pawn)
                    {
                        PawnHashCodeA ^= piece.HashCodeAForSquareOrdinal(intOrdinal);
                        PawnHashCodeB ^= piece.HashCodeBForSquareOrdinal(intOrdinal);
                    }
                }
            }
        }

#if DEBUG
        #region
        public static string DebugString
        {
            get
            {
                Square square;
                ChessPiece piece;
                string strOutput = "";
                int intOrdinal = Board.Square_Count - 1;

                for (int intRank = 0; intRank < Board.H_COUNT; intRank++)
                {
                    for (int intFile = 0; intFile < Board.V_COUNT; intFile++)
                    {
                        square = Board.GetSquare(intOrdinal);
                        if (square != null)
                        {
                            if ((piece = square.Piece) != null)
                            {
                                strOutput += piece.Abbreviation;
                            }
                            else
                            {
                                strOutput += (square.Colour == Square.enmColour.White ? "." : "#");
                            }
                        }
                        strOutput += Convert.ToChar(13) + Convert.ToChar(10);

                        intOrdinal--;
                    }
                }
                return strOutput;
            }
        }

        /// <summary>Display info on the game at the right of the chessboard</summary>
        /// <param name="indRank">the rank in the chessboard</param>
        /// <param name="strbBoard">output buffer</param>
        /// <remarks>Display the captured pieces and the MoveHistory</remarks>
        private static void DebugGameInfo(int indRank, ref StringBuilder strbBoard)
        {
            strbBoard.Append(":"); strbBoard.Append(indRank); strbBoard.Append(" ");
            switch (indRank)
            {
                case 0:
                case 7:
                    Pieces piecesCaptureList = (indRank == 7) ?
                       Game.PlayerWhite.CapturedEnemyPieces : Game.PlayerBlack.CapturedEnemyPieces;
                    if (piecesCaptureList.Count > 1)
                    {
                        strbBoard.Append("x ");
                        foreach (ChessPiece pieceCaptured in piecesCaptureList)
                            strbBoard.Append((pieceCaptured.Name == ChessPiece.enmName.Pawn) ? "" : pieceCaptured.Abbreviation +
                               pieceCaptured.Square.Name + " ");
                    }
                    break;

                case 5:
                    int iTurNoSave = Game.TurnNo; // Backup TurNo
                    Game.TurnNo -= Game.PlayerToPlay.SearchDepth;
                    for (int indMov = Math.Max(1, Game.MoveHistory.Count - Game.PlayerToPlay.MaxSearchDepth);
                          indMov < Game.MoveHistory.Count; indMov++)
                    {
                        Move moveThis = Game.MoveHistory[indMov];
                        if (moveThis.Piece.Player.Colour == Player.enmColour.White)
                        {
                            strbBoard.Append(indMov >> 1); strbBoard.Append(". ");
                        }
                        //moveThis.PgnSanFormat(false); // Contextual to Game.TurNo
                        strbBoard.Append(moveThis.Description + " ");
                        Game.TurnNo++;
                    }
                    Game.TurnNo = iTurNoSave; // Restore TurNo
                    break;
            }
            strbBoard.Append("\n");

        } // end DebugGameInfo

        public static string DebugGetBoard()
        {
            StringBuilder strbBoard = new StringBuilder(160);
            Square square;
            strbBoard.Append("  0 1 2 3 4 5 6 7 :PlayerToPlay = ");
            strbBoard.Append(((Game.PlayerToPlay.Colour == Player.enmColour.White) ? "White\n" : "Black\n"));
            for (int indRank = 7; indRank >= 0; indRank--)
            {
                strbBoard.Append(indRank + 1); strbBoard.Append(":");
                for (int indFile = 0; indFile < 8; indFile++)
                {
                    square = Board.GetSquare(indFile, indRank);
                    if (square != null)
                    {
                        if (square.Piece == null)
                            strbBoard.Append(". ");
                        else
                        {
                            if (square.Piece.Player.Colour == Player.enmColour.White)
                                strbBoard.Append(square.Piece.Abbreviation);
                            else
                                strbBoard.Append(square.Piece.Abbreviation.ToLower());
                            strbBoard.Append(" ");
                        }
                    }
                }
                DebugGameInfo(indRank, ref strbBoard);
            }
            strbBoard.Append("  a b c d e f g h :TurnNo = ");
            strbBoard.Append(Game.TurnNo);
            return strbBoard.ToString();

        } // end DebugGetBoard

        /// <summary>Display the chessboard in the Immediate Windows</summary>
        /// <remarks>VS.NET menu "Debug"/"Windows"/"Immediate" </remarks>
        /// <example>Board.DebugDisplay()</example>
        /// <returns>""</returns>
        public static string DebugDisplay()
        {
            Debug.Write(DebugGetBoard());
            Debug.Write(". ");
            return " ";
        } // end DebugDisplay
        #endregion
#endif

    }
}
