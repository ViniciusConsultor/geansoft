using System;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// Summary description for FENParser.
    /// </summary>
    public class FEN
    {

        public static string GameStartPosition
        {
            get { return "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"; }
        }

        public static void SetBoardPosition(string strFEN)
        {
            string strActiveColour = "w";
            string strCastlingRights = "";
            string strEnPassant = "-";
            string strHalfMoveClock = "0";
            string strFullMoveNumber = "1";

            int intPos;

            Game.FENStartPosition = strFEN;

            Game.CaptureAllPieces();
            Game.DemoteAllPieces();

            // Break up the string into its various parts
            // rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1

            strFEN += " ";

            // Piece Placement
            intPos = strFEN.IndexOf(" ");
            char[] acharPiecePlacement = strFEN.ToCharArray(0, intPos);
            strFEN = strFEN.Substring(intPos + 1);

            // Active Colour
            intPos = strFEN.IndexOf(" ");
            if (intPos > -1)
            {
                strActiveColour = strFEN.Substring(0, intPos);
                strFEN = strFEN.Substring(intPos + 1);
            }

            // Castling Rights
            intPos = strFEN.IndexOf(" ");
            if (intPos > -1)
            {
                strCastlingRights = strFEN.Substring(0, intPos);
                strFEN = strFEN.Substring(intPos + 1);
            }

            // En passant
            intPos = strFEN.IndexOf(" ");
            if (intPos > -1)
            {
                strEnPassant = strFEN.Substring(0, intPos);
                strFEN = strFEN.Substring(intPos + 1);
            }

            // Half move clock
            intPos = strFEN.IndexOf(" ");
            if (intPos > -1)
            {
                strHalfMoveClock = strFEN.Substring(0, intPos);
                strFEN = strFEN.Substring(intPos + 1);
            }

            // Full move number
            intPos = strFEN.IndexOf(" ");
            if (intPos > -1)
            {
                strFullMoveNumber = strFEN.Substring(0, intPos);
            }

            // Match FEN pieces against actual pieces, and move them onto the board

            // Pass 1: Match piece name and location exactly
            SetPiecePlacement(ref acharPiecePlacement, false, false);
            // Pass 2: Match piece name
            SetPiecePlacement(ref acharPiecePlacement, true, false);
            // Pass 3: For non-pawns and not the king, allow pawns to be promoted to the named piece
            SetPiecePlacement(ref acharPiecePlacement, true, true);


            // Set player to play
            Game.PlayerToPlay = (strActiveColour == "b" ? Game.PlayerBlack : Game.PlayerWhite);


            // Set castling rights
            ChessPiece pieceRook;
            // White King's Rook
            if ((pieceRook = Board.GetPiece(7, 0)) != null && pieceRook.Name == ChessPiece.enmName.Rook && pieceRook.Player.Colour == Player.enmColour.White)
            {
                pieceRook.NoOfMoves = strCastlingRights.LastIndexOf("K") >= 0 ? 0 : 1;
            }
            // Black King's Rook
            if ((pieceRook = Board.GetPiece(7, 7)) != null && pieceRook.Name == ChessPiece.enmName.Rook && pieceRook.Player.Colour == Player.enmColour.Black)
            {
                pieceRook.NoOfMoves = strCastlingRights.LastIndexOf("k") >= 0 ? 0 : 1;
            }
            // White Queen's Rook
            if ((pieceRook = Board.GetPiece(0, 0)) != null && pieceRook.Name == ChessPiece.enmName.Rook && pieceRook.Player.Colour == Player.enmColour.White)
            {
                pieceRook.NoOfMoves = strCastlingRights.LastIndexOf("Q") >= 0 ? 0 : 1;
            }
            // Black Queen's Rook
            if ((pieceRook = Board.GetPiece(0, 7)) != null && pieceRook.Name == ChessPiece.enmName.Rook && pieceRook.Player.Colour == Player.enmColour.Black)
            {
                pieceRook.NoOfMoves = strCastlingRights.LastIndexOf("q") >= 0 ? 0 : 1;
            }


            // Half move (50 move draw) clock.
            Game.FiftyMoveDrawBase = int.Parse(strHalfMoveClock);


            // Full move number. Default 1. Must be defined before En Passant.
            Game.TurnNo = ((int.Parse(strFullMoveNumber) - 1) << 1);
            if (Game.PlayerToPlay.Colour == Player.enmColour.Black)
            {
                Game.TurnNo++; // Always odd for the previous White's move 
            }

            // En Passant
            if (strEnPassant[0] != '-')
            {
                int indFile = Board.FileFromName(Convert.ToString(strEnPassant[0]));
                int indRank = Int32.Parse(Convert.ToString(strEnPassant[1]));
                if (indRank == 6)   // if strFen = "e6"
                    indRank = 4;   // last move was e7-e5 so indRank = 6 - 2 = 4
                // else if indRank = 3, strFen = "e3" last move was e2-e4 so indRank = 3
                ChessPiece piecePassed = Board.GetPiece(indFile, indRank);
                piecePassed.NoOfMoves = 1;
                piecePassed.LastMoveTurnNo = Game.TurnNo;
            }

            // Recalculate the hashkey for the current position.
            Board.EstablishHashKey();

            VerifyPiecePlacement(ref acharPiecePlacement);
        }

        private static void SetPiecePlacement(ref char[] acharPiecePlacement, bool blnAnyLocation, bool blnAllowPromotion)
        {
            // Setup piece placement
            int intRank = 7;
            int intFile = 0;
            for (int intIndex = 0; intIndex < acharPiecePlacement.Length; intIndex++)
            {
                switch (acharPiecePlacement[intIndex])
                {
                    case '.':
                        // Indicates a processed piece, so move on
                        intFile++;
                        break;

                    case '/':
                        intFile = 0;
                        intRank--;
                        break;

                    case 'K':
                    case 'Q':
                    case 'R':
                    case 'B':
                    case 'N':
                    case 'P':
                    case 'k':
                    case 'q':
                    case 'r':
                    case 'b':
                    case 'n':
                    case 'p':
                        MovePieceToFENPosition(ref acharPiecePlacement[intIndex], intFile, intRank, blnAnyLocation, blnAllowPromotion);
                        intFile++;
                        break;

                    default:
                        if (char.IsDigit(acharPiecePlacement[intIndex]))
                        {
                            intFile += int.Parse(acharPiecePlacement[intIndex].ToString());
                        }
                        else
                        {
                            throw new FENValidationException("Unknow character in FEN string:" + acharPiecePlacement[intIndex]);
                        }
                        break;
                }
            }
        }

        private static void VerifyPiecePlacement(ref char[] acharPiecePlacement)
        {
            // Check to see if there were any pieces left that we couldnt place.
            string strPieceName = "";

            for (int intIndex = 0; intIndex < acharPiecePlacement.Length; intIndex++)
            {
                switch (acharPiecePlacement[intIndex])
                {
                    case 'K':
                        strPieceName += " White King";
                        break;
                    case 'Q':
                        strPieceName += " White Queen";
                        break;
                    case 'R':
                        strPieceName += " White Rook";
                        break;
                    case 'B':
                        strPieceName += " White Bishop";
                        break;
                    case 'N':
                        strPieceName += " White Knight";
                        break;
                    case 'P':
                        strPieceName += " White Pawn";
                        break;
                    case 'k':
                        strPieceName += " Black King";
                        break;
                    case 'q':
                        strPieceName += " Black Queen";
                        break;
                    case 'r':
                        strPieceName += " Black Rook";
                        break;
                    case 'b':
                        strPieceName += " Black Bishop";
                        break;
                    case 'n':
                        strPieceName += " Black Knight";
                        break;
                    case 'p':
                        strPieceName += " Black Pawn";
                        break;
                }
            }
            if (strPieceName != "")
            {
                throw new FENValidationException("Unable to place the following pieces: " + strPieceName);
            }
        }

        private static void MovePieceToFENPosition(ref char charToken, int intFile, int intRank, bool blnAnyLocation, bool blnAllowPromotion)
        {
            ChessPiece.enmName piecename = ChessPiece.enmName.King;
            Player player = charToken.ToString() == charToken.ToString().ToUpper() ? Game.PlayerWhite : Game.PlayerBlack;

            switch (charToken.ToString().ToUpper())
            {
                case "K":
                    piecename = ChessPiece.enmName.King;
                    break;
                case "Q":
                    piecename = ChessPiece.enmName.Queen;
                    break;
                case "R":
                    piecename = ChessPiece.enmName.Rook;
                    break;
                case "B":
                    piecename = ChessPiece.enmName.Bishop;
                    break;
                case "N":
                    piecename = ChessPiece.enmName.Knight;
                    break;
                case "P":
                    piecename = ChessPiece.enmName.Pawn;
                    break;
            }

            // Try to find the required piece in from the available pool of captured 
            // pieces that haven't been placed on the board yet.
            ChessPiece pieceToUse = null;
            foreach (ChessPiece pieceCaptured in player.OtherPlayer.CapturedEnemyPieces)
            {
                if (
                    (pieceCaptured.Name == piecename || blnAllowPromotion && pieceCaptured.Name == ChessPiece.enmName.Pawn)
                    &&
                    (pieceCaptured.StartLocation == Board.GetSquare(intFile, intRank) || blnAnyLocation)
                    )
                {
                    pieceToUse = pieceCaptured;
                    break;
                }
            }
            if (pieceToUse != null)
            {
                Square square = Board.GetSquare(intFile, intRank);
                pieceToUse.Uncapture(0);
                square.Piece = pieceToUse;
                pieceToUse.Square = square;
                pieceToUse.NoOfMoves = blnAnyLocation ? 1 : 0;
                if (pieceToUse.Name != piecename)
                {
                    pieceToUse.Promote(piecename);
                }
                // Mark the token in the original FEN string with a * to indicate that the piece has been processed
                charToken = '.';
            }
        }

        #region Validate

        /// <summary>Check if the array of strings represents a valid FEN position</summary>
        /// <param name="strFEN">FEN chess board position string</param>
        /// <returns>true if it is a FEN position otherwise false</returns>
        /// <remarks><list type="number"><item>Field 1: <see cref="FenCheck1PiecePlace"/>(arrStrFen[0]) Piece placement data</item>
        /// <item>Field 2: <see cref="FenCheck2Color"/>(arrStrFen[1]) Active color</item>
        /// <item>Field 3: <see cref="FenCheck3Castle"/>(arrStrFen[2]) Castling availability</item>
        /// <item>Field 4: <see cref="FenCheck4EnPassant"/>(arrStrFen[3]) En passant target square coordonates</item>
        /// <item>Field 5: <see cref="FenCheck5Counter50MoveDraw"/>(arrStrFen[4]) number of ply since the last capture or pawn move</item>
        /// <item>Field 6: <see cref="FenCheck6NbrMove"/>(arrStrFen[5]) Full move number</item></list></remarks>
        public static void Validate(string strFEN)
        {
            char[] arrDelimiter = " ".ToCharArray();
            string[] arrStrFen = strFEN.Split(arrDelimiter);
            int intFields = arrStrFen.Length;

            if (intFields < 1 || intFields > 6)
            {
                throw new FENValidationException("1000: A FEN string must 1 to 6 fields separated by spaces\n e.g. rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            }

            if (intFields >= 2) FenCheck2Color(arrStrFen[1]);
            if (intFields >= 3) FenCheck3Castle(arrStrFen[2]);
            if (intFields >= 4) FenCheck4EnPassant(arrStrFen[3]);
            if (intFields >= 5) FenCheck5Counter50MoveDraw(arrStrFen[4]);
            if (intFields >= 6) FenCheck6NbrMove(arrStrFen[5]);
        } // end FenCheckPosition 

        /// <summary>FEN helper naming each field</summary>
        /// <param name="iField">1..6</param>
        /// <returns>"FEN field {iField}: help message"</returns>
        /// <remarks>Used in a Warning message</remarks>
        private static string FenHlpMsg(int iField)
        {
            switch (iField)
            {
                case 1: return "FEN field 1: Piece placement data.\n";
                case 2: return "FEN field 2: Active color.\n";
                case 3: return "FEN field 3: Castling availability.\n";
                case 4: return "FEN field 4: En passant target square coordonates.\n";
                case 5: return "FEN field 5: Nbr of half move without capture or pawn move.\n";
                case 6: return "FEN field 6: Full move number.\n";
            }
            return "";

        } // end FenHlpMsg

        /// <summary>Check the squares in the FEN position</summary>
        /// <param name="strFen">field 1 of the FEN string: "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR"</param>
        /// <returns>true if all squares OK otherwise false</returns>
        private static bool FenCheck1PiecePlace(string strFen)
        {
            int iNbrK = 0, iNbrQ = 0, iNbrR = 0, iNbrB = 0, iNbrN = 0, iNbrP = 0; // White
            int iNbrk = 0, iNbrq = 0, iNbrr = 0, iNbrb = 0, iNbrn = 0, iNbrp = 0; // Black
            int iNbrSquare = 0, iNbrSlash = 0;
            for (int indChar = 0; indChar < strFen.Length; indChar++)
            {
                switch (strFen[indChar])
                {
                    case 'K': iNbrK++; iNbrSquare++; break;
                    case 'k': iNbrk++; iNbrSquare++; break;
                    case 'Q': iNbrQ++; iNbrSquare++; break;
                    case 'q': iNbrq++; iNbrSquare++; break;
                    case 'R': iNbrR++; iNbrSquare++; break;
                    case 'r': iNbrr++; iNbrSquare++; break;
                    case 'B': iNbrB++; iNbrSquare++; break;
                    case 'b': iNbrb++; iNbrSquare++; break;
                    case 'N': iNbrN++; iNbrSquare++; break;
                    case 'n': iNbrn++; iNbrSquare++; break;
                    case 'P': iNbrP++; iNbrSquare++; break;
                    case 'p': iNbrp++; iNbrSquare++; break;
                    case '/': iNbrSlash++;
                        if (iNbrSquare != 8)
                            throw new FENValidationException("1010: " + FenHlpMsg(1) +
                                string.Format("The rank {0} does not contain 8 squares", iNbrSlash));
                        iNbrSquare = 0;
                        break;

                    default:
                        if ((strFen[indChar] < '1') || (strFen[indChar] > '8'))
                            throw new FENValidationException("1020: " + FenHlpMsg(1) + "Expected character 'KQRBNP/kqrbnp' '1'..'8'");
                        else
                            iNbrSquare += System.Convert.ToInt32(strFen[indChar].ToString());
                        break;
                }
            }
            if (iNbrSlash != 7)
                throw new FENValidationException("1030: " + FenHlpMsg(1) + "The string must have 7 slash separators");
            if (iNbrSquare != 8)
                throw new FENValidationException("1040: " + FenHlpMsg(1) + "The last rank does not contain 8 squares");
            if (iNbrK != 1)
                throw new FENValidationException("1050: " + FenHlpMsg(1) + "No White King");
            if (iNbrk != 1)
                throw new FENValidationException("1060: " + FenHlpMsg(1) + "No Black King");
            if ((iNbrQ > 9) || (iNbrB > 9) || (iNbrN > 9) || (iNbrP > 8))
                throw new FENValidationException("1070: " + FenHlpMsg(1) + "Too many White Q or B or N or P");
            if ((iNbrq > 9) || (iNbrb > 9) || (iNbrn > 9) || (iNbrp > 8))
                throw new FENValidationException("1080: " + FenHlpMsg(1) + "Too many Black q or b or n or p");

            return true;
        } // end FenCheck1PiecePlace

        /// <summary>Check the active color in the FEN string</summary>
        /// <param name="strFen">field 2 of the FEN string: "w" or "b"</param>
        /// <returns>true if 'w' or 'b' otherwise false</returns>
        private static bool FenCheck2Color(string strFen)
        {
            if ((strFen != "w") && (strFen != "b"))
                throw new FENValidationException("1200: " + FenHlpMsg(2) + "The active color must be 'w' or 'b'");
            return true;

        } // end FenCheck2Color

        /// <summary>Check the castling availability in the FEN string</summary>
        /// <param name="strFen">field 3 of the FEN string: "KQkq", ..., "-"</param>
        /// <returns>true if [KQkq] or '-' otherwise false</returns>
        /// <remarks>If White/Black King could O-O or O-O-O then strFen = "KQkq"</remarks>
        private static bool FenCheck3Castle(string strFen)
        {
            int iNbrDash = 0, iNbrK = 0, iNbrQ = 0, iNbrk = 0, iNbrq = 0;
            for (int indChar = 0; indChar < strFen.Length; indChar++)
            {
                switch (strFen[indChar])
                {
                    case 'K': iNbrK++; break;
                    case 'k': iNbrk++; break;
                    case 'Q': iNbrQ++; break;
                    case 'q': iNbrq++; break;
                    case '-': iNbrDash++; break;
                    default:
                        throw new FENValidationException("1300: " + FenHlpMsg(3) + "Expected character 'KQkq-'");
                }
            }
            if ((iNbrK > 1) || (iNbrQ > 1) || (iNbrk > 1) || (iNbrq > 1) || (iNbrDash > 1))
                throw new FENValidationException("1310: " + FenHlpMsg(3) + "At least one occurrence of 'KQkq-'");
            if ((iNbrDash == 1) && ((iNbrK == 1) || (iNbrQ == 1) || (iNbrk == 1) || (iNbrq == 1)))
                throw new FENValidationException("1320: " + FenHlpMsg(3) + "'KQkq' or exclusive '-'");
            return true;

        } // end FenCheck3Castle

        /// <summary>Check the capture square En Passant in the FEN string</summary>
        /// <param name="strFen">field 4 of the FEN string: "e3", "e6", ..., "-"</param>
        /// <remarks>If the last move was e2-e4, then strFen = "e3"</remarks>
        /// <returns>true if [abcdefgh](3|6) or '-' otherwise false</returns>
        private static bool FenCheck4EnPassant(string strFen)
        {
            if (((strFen[0] < 'a') || (strFen[0] > 'h')) && (strFen[0] != '-'))
                throw new FENValidationException("1400: " + FenHlpMsg(4) + "Expected character 'abcdefgh-'");

            if (strFen[0] == '-')
            {
                if (strFen.Length > 1)
                    throw new FENValidationException("1410: " + FenHlpMsg(4) + "No expected character after '-'");
            }
            else if (((strFen[0] >= 'a') && (strFen[0] <= 'h')) &&
                (((strFen.Length == 2) && (strFen[1] != '3') && (strFen[1] != '6')) ||
                (strFen.Length > 2)))
                throw new FENValidationException("1420: " + FenHlpMsg(4) + "After the pawn file, expect the rank '3' or '6'");
            return true;

        } // end FenCheck4EnPassant

        /// <summary>Check the half move number in the FEN string</summary>
        /// <param name="strFen">field 5 of the FEN string: 0..100</param>
        /// <remarks>Represent the number of ply after a capture or a pawn move</remarks>
        /// <returns>true if half move number 0..100 otherwise false</returns>
        private static bool FenCheck5Counter50MoveDraw(string strFen)
        {
            if (strFen.Length > 2)
                throw new FENValidationException("1500: " + FenHlpMsg(5) + "1 or 2 digits for the nbr of ply for rule of 50 moves");
            int iNbrHalfMove;
            try { iNbrHalfMove = Int32.Parse(strFen); }
            catch { throw new FENValidationException("1510: " + FenHlpMsg(5) + "Expect a half move number for the rule of 50 moves"); }
            if ((iNbrHalfMove < 0) || (iNbrHalfMove > 100))
                throw new FENValidationException("1520: " + FenHlpMsg(5) + "Expect a non negative half move number <= 100");
            return true;

        } // end FenCheck5Counter50MoveDraw

        /// <summary>Check the full move number in the FEN string</summary>
        /// <param name="strFen">field 6 of the FEN string: 1..200</param>
        /// <returns>true if full move number 1..200 otherwise false</returns>
        private static bool FenCheck6NbrMove(string strFen)
        {
            int iNbrFullMove;
            try { iNbrFullMove = Int32.Parse(strFen); }
            catch { throw new FENValidationException("1600: " + FenHlpMsg(6) + "Expect a full move number"); }
            if ((iNbrFullMove < 1) || (iNbrFullMove > 200))
                throw new FENValidationException("1610: " + FenHlpMsg(6) + "Expect a positive full move number <= 200");
            return true;

        } // end FenCheck6NbrMove

        #endregion

        #region GetBoardPosition

        /// <summary>Extraction the current position in FEN: Forsyth-Edwards Notation</summary>
        /// <returns>string of the FEN position with 6 fields separated by " "
        /// <list type="number"><item>Piece placement data: <see cref="FenGet1Pieces"/>(strbFen, strbCouldCastlingW, strbCouldCastlingB)</item>
        /// <item>Active color: " w " or " b "</item>
        /// <item>Castling availability: "KQkq" or "-" see <see cref="FenGet3CastlingFuture"/>(...)</item>
        /// <item>En passant target square coordonates: <see cref="FenGet4EnPassant"/>()</item>
        /// <item>Number of ply since the last pawn advance or capturing move: <see cref="FenGet5Counter50MoveDraw"/>()</item>
        /// <item>Full move number = Game.TurnNo \ 2 + 1</item></list></returns>
        /// <example><list type="bullet"><item>"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1" initial position</item>
        /// <item>"rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1" after 1. e4</item>
        /// <item>"rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq e6 0 2" after 1. e4 e5</item>
        /// <item>"rnbqkbnr/pppp1ppp/8/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2" after 2. Nf3</item>
        /// <item>"r1bqkbnr/pppp1ppp/2n5/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQkq - 2 3" after 2. Nf3 Nc6</item>
        /// <item>"r1bqkbnr/pppp1ppp/2n5/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 3 3" after 3. Bb5</item>
        /// <item>"r1bqkbnr/1ppp1ppp/p1n5/1B2p3/4P3/5N2/PPPP1PPP/RNBQK2R w KQkq - 0 4" after 3. Bb5 a6</item>
        /// </list></example>
        public static string GetBoardPosition()
        {
            Game.SuspendPondering();
            StringBuilder strbFen = new StringBuilder();

            // Field 1: Piece placement data
            FenGet1Pieces(strbFen);

            // Field 2: Active color
            strbFen.Append((Game.PlayerToPlay.Colour == Player.enmColour.White) ? " w " : " b ");

            // Field 3: Castling availability
            bool bIsCastleW = FenGet3CastlingFuture(Game.PlayerWhite.King, strbFen);
            bool bIsCastleB = FenGet3CastlingFuture(Game.PlayerBlack.King, strbFen);
            if (!bIsCastleW && !bIsCastleB)
                strbFen.Append("-"); // No castling availability for either side

            // Field 4: En passant target square coordonates
            strbFen.Append(FenGet4EnPassant());

            // Field 5: number of Halfmove clock or ply since the last pawn advance or capturing move.
            strbFen.Append(FenGet5Counter50MoveDraw());

            // Field 6: Full move number
            strbFen.Append(((Game.TurnNo >> 1) + 1).ToString()); // Incremented at each move of Blacks

            Game.ResumePondering();
            return strbFen.ToString();

        } // end FenGetPosition 

        /// <summary>FEN piece placement string by rank 7..0 and by file 0..7</summary>
        /// <param name="strbFen">string builder FEN of the chessboard</param>
        /// <remarks><list type="bullet"><item>strbFen = "rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR" for the intial position</item>
        /// <item>"r" = Black rook, "n" = Black knight,... "p" = Black pawn</item>
        /// <item>"R" = White rook, "N" = White knight,... "P" = White pawn</item>
        /// <item>"4P3" means 4 empty squares before the White pawn, then 3 empty squares</item>
        /// <item>"8" means empty rank</item></list></remarks>
        private static void FenGet1Pieces(StringBuilder strbFen)
        {
            for (int iNbrEmptySquare = 0, indRank = Board.H_COUNT - 1; indRank >= 0; indRank--)
            {
                if (indRank != Board.H_COUNT - 1)
                    strbFen.Append('/'); // Separator between 2 ranks

                for (int indFile = 0; indFile < Board.V_COUNT; indFile++) // Browse by column
                {
                    Square squareThis = Board.GetSquare(indFile, indRank);
                    ChessPiece pieceThis = squareThis.Piece;
                    if (pieceThis == null)
                        iNbrEmptySquare++;
                    else
                    {
                        if (iNbrEmptySquare > 0)
                        {   // Nbr of empty squares before the piece
                            strbFen.Append(iNbrEmptySquare.ToString());
                            iNbrEmptySquare = 0;
                        }
                        if (pieceThis.Player.Colour == Player.enmColour.Black)
                            strbFen.Append(pieceThis.Abbreviation.ToLower()); // Blacks in lowercase
                        else
                            strbFen.Append(pieceThis.Abbreviation);
                    }
                }
                if (iNbrEmptySquare > 0)
                {   // Nbr of empty squares after the last piece
                    strbFen.Append(iNbrEmptySquare.ToString());
                    iNbrEmptySquare = 0;
                }
            }
        } // end FenGet1Pieces

        /// <summary>FEN notation of castling availability of the King in the future</summary>
        /// <param name="pieceK">the White or Black King</param>
        /// <param name="strbFen"><list type="bullet"><item>append "K" if White castling availability <see cref="PieceKing.CouldCastleKingSide">Rook-side</see></item>
        /// <item>append "Q" if White castling availability both <see cref="PieceKing.CouldCastleQueenSide">Queen-side</see></item>
        /// <item>append "k" if Black castling availability both Rook-side</item>
        /// <item>append "q" if Black castling availability both Queen-side</item>
        /// <item>append "KQkq" if O-O and O-O-O for White first then Black</item></list></param>
        private static bool FenGet3CastlingFuture(ChessPiece pieceK, System.Text.StringBuilder strbFen)
        {
            bool bIsFutureCastling = false;
            if (((PieceKing)pieceK.Top).CanCastleKingSide)
            {   // King could castle Rook-side in the future
                strbFen.Append((pieceK.Player.Colour == Player.enmColour.White) ? "K" : "k");
                bIsFutureCastling = true;
            }
            if (((PieceKing)pieceK.Top).CanCastleQueenSide)
            {   // King could castle Queen-side in the future
                strbFen.Append((pieceK.Player.Colour == Player.enmColour.White) ? "Q" : "q");
                bIsFutureCastling = true;
            }
            return bIsFutureCastling;

        } // end FenGet3CastlingFuture

        /// <summary>FEN string indicating the potential square target for a capture en passant</summary>
        /// <returns><list type="bullet"><item>"e3" if the last move was e2-e4</item>
        /// <item>"e6" if the last move was e7-e5</item>
        /// <item>" - " if the last move was not a pawn move of 2 squares</item></list></returns>
        private static string FenGet4EnPassant()
        {
            if ((Game.MoveHistory.Count > 0) &&
                (Game.MoveHistory.Last.Piece.Name == SharpChess.Piece.enmName.Pawn) &&
                (Game.MoveHistory.Last.From.File == Game.MoveHistory.Last.To.File) &&
                (((Game.MoveHistory.Last.From.Rank == Game.MoveHistory.Last.To.Rank + 2) &&
                (Game.MoveHistory.Last.Piece.Player.Colour == Player.enmColour.Black)) ||
                ((Game.MoveHistory.Last.From.Rank == Game.MoveHistory.Last.To.Rank - 2) &&
                (Game.MoveHistory.Last.Piece.Player.Colour == Player.enmColour.White))))
            {
                return " " + Game.MoveHistory.Last.From.FileName +
                    ((Game.MoveHistory.Last.Piece.Player.Colour == Player.enmColour.White) ?
                    "3 " : "6 "); // The case between From and To
            }
            else
                return " - "; // There is not en passant target square

        } // end FenGet4EnPassant

        /// <summary>FEN string of the number of ply since the last pawn advance or capturing move</summary>
        /// <returns><see cref="Game.FiftyMoveDrawCounter"/> + " "</returns>
        private static string FenGet5Counter50MoveDraw()
        {
            return String.Format("{0} ", (Game.MoveHistory.Count > 0 ? Game.MoveHistory.Last.FiftyMoveDrawCounter : Game.FiftyMoveDrawBase));

        } // end FenGet5Counter50MoveDraw

        #endregion

    }
}
