using System;
using System.IO;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// For reference see: "Portable Game Notation Specification 
    /// and Implementation Guide"  section 16.1: FEN.  
    /// </summary>
    public class FenNotation : IPosition, IPositionEvents
    {
        /// <summary>
        /// Holds our board position based on squares 0 thru 63.
        /// </summary>
        private StringBuilder coBoard;

        #region Events
        public delegate void EventPlacePiece(FenChessmans piece, int square);
        public event EventPlacePiece EventPlacePieceHandler;

        /// Defines the color hooks that allow call back to set who's move it is.
        public delegate void EventSideToMove(bool bColor);
        public event EventSideToMove EventSideToMoveHandler;

        public delegate void EventCastling(bool WK, bool WQ, bool BK, bool BQ);
        public event EventCastling EventCastlingHandler;

        public delegate void EventEnpassant(string Enpassant);
        public event EventEnpassant EventEnpassantHandler;
        public delegate void EventHalfMoves(int number);
        public event EventHalfMoves EventHalfMovesHandler;
        public delegate void EventFullMoves(int number);
        public event EventFullMoves EventFullMovesHandler;

        public delegate void EventFinished();
        public event EventFinished EventFinishedHandler;
        public delegate void EventStarting();
        public event EventStarting EventStartingHandler;

        #endregion Events

        /// <summary>
        /// The color to move given the current position.
        /// Must be 'w' or 'b'.
        /// </summary>
        public char Color
        {
            get { return coActiveColor; }
            set
            {
                if (value == 'w' || value == 'b')
                    coActiveColor = value;
                else
                    throw new Exception("Specify: 'w' or 'b'");
            }
        }
        private char coActiveColor;

        /// <summary>
        /// If true then white can still castle king side.
        /// </summary>
        public bool WhiteCastleKing
        {
            get { return coWKCastle; }
            set { coWKCastle = value; }
        }
        private bool coWKCastle;

        /// <summary>
        /// If true then white can still castle queen side.
        /// </summary>    
        public bool WhiteCastleQueen
        {
            get { return coWQCastle; }
            set { coWQCastle = value; }
        }
        private bool coWQCastle;

        /// <summary>
        /// If true then black can still castle king side.
        /// </summary>
        public bool BlackCastleKing
        {
            get { return coBKCastle; }
            set { coBKCastle = value; }
        }
        private bool coBKCastle;

        /// <summary>
        /// If true then black can still castle queen side.
        /// </summary>
        public bool BlackCastleQueen
        {
            get { return coBQCastle; }
            set { coBQCastle = value; }
        }
        private bool coBQCastle;

        /// <summary>
        /// Algebraic square for enpassant captures or '-'.
        /// </summary>
        public string Enpassant
        {
            get { return coEnPassant; }
            set { coEnPassant = value; }
        }
        private string coEnPassant;

        /// <summary>
        /// Number of half moves to determine the 50 move rule.
        /// </summary>
        public int HalfMove
        {
            get { return coHalfMove; }
            set { coHalfMove = value; }
        }
        private int coHalfMove;

        /// <summary>
        /// Number of completed move cycles, i.e. after black moves.
        /// </summary>
        public int FullMove
        {
            get { return coFullMove; }
            set { coFullMove = value; }
        }
        private int coFullMove;

        public string Format
        {
            get { return coFormat; }
            set { coFormat = value; }
        }
        private string coFormat = "{0} {1} {2} {3} {4} {5}";

        /// <summary>
        /// Indexer into the board.  Returns an upper case character for white and
        /// a lower case character for black.  The index number equals the board
        /// square.  Characters are 'PKQNBR' for white and 'pkqnbr' for black.
        /// </summary>
        public char this[int ndx]
        {
            get { return coBoard[ndx]; }
            set
            {
                string str = "KQRBNPkqrbnp";
                if (str.IndexOf(value) >= 0)
                    coBoard[ndx] = value;
                else
                    throw new Exception("Invalid piece value (" + value + ") use one of: " + str);
            }
        }

        /// <summary>
        /// Constructs a class by parsing a FEN notation string.
        /// </summary>
        /// <param name="str">A valid FEN notation data string</param>
        public FenNotation(string str)
        {
            coBoard = new StringBuilder(64, 64);
            Parse(str);
        }

        /// <summary>
        /// Constructs a default FEN notation object with the board cleared,
        /// all castling available, white to move.
        /// </summary>
        public FenNotation()
        {
            coBoard = new StringBuilder(64, 64);
            Clear();
        }

        /// <summary>
        /// Clears the notation to an empty board, white to move, all castling available.
        /// </summary>
        public void Clear()
        {
            coBoard.Length = 0;
            coBoard.Append(' ', 64);
            coEnPassant = "-";
            coActiveColor = 'w';
            coWKCastle = true;
            coWQCastle = true;
            coBKCastle = true;
            coBQCastle = true;
            coHalfMove = 0;
            coFullMove = 0;
        }

        /// <summary>
        /// Creates our FEN notation string.
        /// </summary>
        /// <returns>FEN notation</returns>
        public override string ToString()
        {
            string[] parms = new string[6];
            StringBuilder note = new StringBuilder();
            int count = 0;

            for (int ndx = 56; ndx >= 0; ndx -= 8)
            {
                count = 0;
                for (int cnt = 0; cnt < 8; cnt++)
                {
                    char achar = coBoard[ndx + cnt];
                    if (achar == ' ')
                        count++;
                    else
                    {
                        if (count > 0)
                            note.Append(count.ToString());
                        count = 0;
                        note.Append(achar);
                    }
                }

                if (count > 0)
                    note.Append(count.ToString());
                if (ndx != 0)
                    note.Append('/');
            }

            parms[0] = note.ToString();
            note.Length = 0;
            //note.Append(' ');
            parms[1] = coActiveColor.ToString();
            //note.Append(coActiveColor);
            //note.Append(' ');


            if (coWKCastle | coWQCastle | coBKCastle | coBQCastle)
            {
                if (coWKCastle)
                    note.Append('K');
                if (coWQCastle)
                    note.Append('Q');
                if (coBKCastle)
                    note.Append('k');
                if (coBQCastle)
                    note.Append('q');
            }
            else
                note.Append('-');

            parms[2] = note.ToString();
            note.Length = 0;

            //      note.Append(' ');
            //      note.Append(coEnPassant);
            parms[3] = coEnPassant;
            //      note.Append(' ');
            parms[4] = coHalfMove.ToString();
            //      note.Append(coHalfMove.ToString());
            //      note.Append(' ');
            parms[5] = coFullMove.ToString();
            //      note.Append(coFullMove.ToString());
            return string.Format(Format, parms);

            //return note.ToString();
        }

        #region IPosition Members
        /// <summary>
        /// Parses our FEN notation into our class.
        /// For example: rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
        /// </summary>
        /// <param name="str"></param>
        public void Parse(string str)
        {
            // Clear all current settings.
            Clear();
            int ndx = 56;
            int cnt = 0;
            string[] note = str.Split(' ');


            // 16.1.3.1: Parse piece placement data
            string[] row = note[0].Split('/');
            if (row.Length != 8)
                throw new ArgumentException("Invalid board specification, " + row.Length + " ranks are defined, there should be 8.");

            if (EventStartingHandler != null)
                EventStartingHandler();

            foreach (string line in row)
            {
                cnt = 0;
                foreach (char achar in line)
                {
                    if (achar >= '0' && achar <= '9')
                        cnt += (int)(achar - '0');
                    else
                    {
                        if (FenConvertChessman.FromFEN(achar) != FenChessmans.None)
                        {
                            if (cnt > 7)  // This check needed here to avoid overrunning index below under some error conditions.
                                throw new ArgumentException("Invalid board specification, rank " + (ndx / 8 + 1) + " has more then 8 items specified.");
                            if (EventPlacePieceHandler != null)
                                EventPlacePieceHandler(FenConvertChessman.FromFEN(achar), ndx + cnt);
                            this[ndx + cnt] = achar;
                        }
                        cnt++;
                    }
                }

                if (cnt == 0) // Allow null lines = /8/
                    cnt += 8;

                if (cnt != 8)
                    throw new ArgumentException("Invalid board specification, rank " + (ndx / 8 + 1) + " has " + cnt + " items specified, there should be 8.");

                ndx -= 8;
            }

            if (note.Length >= 2)
            {
                // 16.1.3.2: Parse active color
                if (note[1].Length > 0)
                {
                    char colorchar = Char.ToLower(note[1][0]);
                    if (colorchar.Equals('w') || colorchar.Equals('b'))
                    {
                        Color = colorchar;
                        if (EventSideToMoveHandler != null)
                            EventSideToMoveHandler(colorchar.Equals('w') ? true : false);
                    }
                    else
                        throw new ArgumentException("Invalid color designation, use w or b as 2nd field separated by spaces.");

                    if (note[1].Length != 1)
                        throw new ArgumentException("Invalid color designation, 2nd field is " + note[1].Length + " chars long, only 1 allowed.");
                }
            }

            // 16.1.3.3: Parse castling availability
            bool WK = false;
            bool WQ = false;
            bool BK = false;
            bool BQ = false;

            if (note.Length >= 3)
            {
                foreach (char achar in note[2])
                {
                    switch (achar)
                    {
                        case 'K':
                            WK = true;
                            break;
                        case 'Q':
                            WQ = true;
                            break;
                        case 'k':
                            BK = true;
                            break;
                        case 'q':
                            BQ = true;
                            break;
                        case '-':
                            break;
                        default:
                            throw new Exception("Invalid castle privileges designation, use: KQkq or -");
                    }
                }
            }
            if (EventCastlingHandler != null)
                EventCastlingHandler(WK, WQ, BK, BQ);

            try
            {
                if (note.Length >= 4)
                {
                    // 16.1.3.4: Parse en passant target square such as "e3"
                    coEnPassant = note[3];
                    if (coEnPassant.Length == 2)
                    {
                        if (Color.Equals('w'))
                        {
                            if (coEnPassant[1] != '6')
                                throw new Exception("Invalid target square for white En passant captures: " + coEnPassant.ToString());
                        }
                        else
                        {
                            if (coEnPassant[1] != '3')
                                throw new Exception("Invalid target square for black En passant captures: " + coEnPassant.ToString());
                        }

                        if (EventEnpassantHandler != null)
                            EventEnpassantHandler(coEnPassant);
                    }
                }

                if (note.Length >= 5)
                {
                    // 16.1.3.5: Parse halfmove clock, count of half-move since last pawn advance or unit capture
                    coHalfMove = Int16.Parse(note[4]);
                    if (EventHalfMovesHandler != null)
                        EventHalfMovesHandler(coHalfMove);
                }

                if (note.Length >= 6)
                {
                    // 16.1.3.6: Parse fullmove number, increment after each black move
                    coFullMove = Int16.Parse(note[5]);
                    if (EventFullMovesHandler != null)
                        EventFullMovesHandler(coFullMove);
                }
            }
            catch
            {
            }
            if (EventFinishedHandler != null)
                EventFinishedHandler();
        }

        public void Parse(Stream stream)
        {
            throw new Exception("Not implemented for FenPosition");
        }


        public void AddEvents(IPositionEvents ievents)
        {
            EventPlacePieceHandler += new EventPlacePiece(ievents.PlacePiece);
            EventSideToMoveHandler += new EventSideToMove(ievents.SetSideToMove);
            EventCastlingHandler += new EventCastling(ievents.SetCastling);
            EventEnpassantHandler += new EventEnpassant(ievents.SetEnpassant);
            EventHalfMovesHandler += new EventHalfMoves(ievents.SetHalfMoves);
            EventFullMovesHandler += new EventFullMoves(ievents.SetFullMoves);
            EventFinishedHandler += new EventFinished(ievents.Finished);
            EventStartingHandler += new EventStarting(ievents.Starting);
        }
        public void RemoveEvents(IPositionEvents ievents)
        {
            EventPlacePieceHandler -= new EventPlacePiece(ievents.PlacePiece);
            EventSideToMoveHandler -= new EventSideToMove(ievents.SetSideToMove);
            EventCastlingHandler -= new EventCastling(ievents.SetCastling);
            EventEnpassantHandler -= new EventEnpassant(ievents.SetEnpassant);
            EventHalfMovesHandler -= new EventHalfMoves(ievents.SetHalfMoves);
            EventFullMovesHandler -= new EventFullMoves(ievents.SetFullMoves);
            EventFinishedHandler -= new EventFinished(ievents.Finished);
            EventStartingHandler -= new EventStarting(ievents.Starting);
        }

        #endregion

        #region IPositionEvents Members
        /// <summary>
        /// Implementation of IPositionEvents 
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="square"></param>
        public void PlacePiece(FenChessmans piece, int square)
        {
            this[square] = FenConvertChessman.ToFEN(piece);
        }
        /// <summary>
        /// Implementation of IPositionEvents
        /// </summary>
        /// <param name="bColor"></param>
        public void SetSideToMove(bool bColor)
        {
            this.Color = bColor ? 'w' : 'b';
        }
        /// <summary>
        /// Implementation of IPositionEvents
        /// </summary>
        /// <param name="WK"></param>
        /// <param name="WQ"></param>
        /// <param name="BK"></param>
        /// <param name="BQ"></param>
        public void SetCastling(bool WK, bool WQ, bool BK, bool BQ)
        {
            this.WhiteCastleKing = WK;
            this.WhiteCastleQueen = WQ;
            this.BlackCastleKing = BK;
            this.BlackCastleQueen = BQ;
        }
        /// <summary>
        /// Implementation of IPositionEvents
        /// </summary>
        /// <param name="enpassant"></param>
        public void SetEnpassant(string enpassant)
        {
            this.Enpassant = enpassant;
        }
        /// <summary>
        /// Implementation of IPositionEvents
        /// </summary>
        /// <param name="number"></param>
        public void SetHalfMoves(int number)
        {
            HalfMove = number;
        }
        /// <summary>
        /// Implementation of IPositionEvents
        /// </summary>
        /// <param name="number"></param>
        public void SetFullMoves(int number)
        {
            FullMove = number;
        }
        /// <summary>
        /// Implementation of IPositionEvents
        /// </summary>
        public void Finished()
        {
        }
        /// <summary>
        /// Implementation of IPositionEvents
        /// </summary>
        public void Starting()
        {
            Clear();
        }

        #endregion
    }
}
