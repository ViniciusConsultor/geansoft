using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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
        private StringBuilder _board;

        #region Events

        public delegate void EventPlacePiece(Chessman piece, int square);
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
            get { return _color; }
            set
            {
                if (value == 'w' || value == 'b')
                    _color = value;
                else
                    throw new Exception("Specify: 'w' or 'b'");
            }
        }
        private char _color;

        /// <summary>
        /// If true then white can still castle king side.
        /// </summary>
        public bool WhiteCastleKing
        {
            get { return _WKCastle; }
            set { _WKCastle = value; }
        }
        private bool _WKCastle;

        /// <summary>
        /// If true then white can still castle queen side.
        /// </summary>    
        public bool WhiteCastleQueen
        {
            get { return _WQCastle; }
            set { _WQCastle = value; }
        }
        private bool _WQCastle;

        /// <summary>
        /// If true then black can still castle king side.
        /// </summary>
        public bool BlackCastleKing
        {
            get { return _BKCastle; }
            set { _BKCastle = value; }
        }
        private bool _BKCastle;

        /// <summary>
        /// If true then black can still castle queen side.
        /// </summary>
        public bool BlackCastleQueen
        {
            get { return _BQCastle; }
            set { _BQCastle = value; }
        }
        private bool _BQCastle;

        /// <summary>
        /// Algebraic square for enpassant captures or '-'.
        /// </summary>
        public string EnPassant
        {
            get { return _EnPassant; }
            set { _EnPassant = value; }
        }
        private string _EnPassant;

        /// <summary>
        /// Number of half moves to determine the 50 move rule.
        /// </summary>
        public int HalfMove
        {
            get { return _HalfMove; }
            set { _HalfMove = value; }
        }
        private int _HalfMove;

        /// <summary>
        /// Number of completed move cycles, i.e. after black moves.
        /// </summary>
        public int FullMove
        {
            get { return _FullMove; }
            set { _FullMove = value; }
        }
        private int _FullMove;

        public string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }
        private string _Format = "{0} {1} {2} {3} {4} {5}";

        /// <summary>
        /// Indexer into the board.  Returns an upper case character for white and
        /// a lower case character for black.  The index number equals the board
        /// square.  Characters are 'PKQNBR' for white and 'pkqnbr' for black.
        /// </summary>
        public char this[int ndx]
        {
            get { return _board[ndx]; }
            set
            {
                string str = "KQRBNPkqrbnp";
                if (str.IndexOf(value) >= 0)
                    _board[ndx] = value;
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
            _board = new StringBuilder(64, 64);
            Parse(str);
        }

        /// <summary>
        /// Constructs a default FEN notation object with the board cleared,
        /// all castling available, white to move.
        /// </summary>
        public FenNotation()
        {
            _board = new StringBuilder(64, 64);
            Clear();
        }

        /// <summary>
        /// Clears the notation to an empty board, white to move, all castling available.
        /// </summary>
        public void Clear()
        {
            _board.Length = 0;
            _board.Append(' ', 64);
            _EnPassant = "-";
            _color = 'w';
            _WKCastle = true;
            _WQCastle = true;
            _BKCastle = true;
            _BQCastle = true;
            _HalfMove = 0;
            _FullMove = 0;
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
                    char achar = _board[ndx + cnt];
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
            parms[1] = _color.ToString();

            if (_WKCastle | _WQCastle | _BKCastle | _BQCastle)
            {
                if (_WKCastle)
                    note.Append('K');
                if (_WQCastle)
                    note.Append('Q');
                if (_BKCastle)
                    note.Append('k');
                if (_BQCastle)
                    note.Append('q');
            }
            else
                note.Append('-');

            parms[2] = note.ToString();
            note.Length = 0;

            parms[3] = _EnPassant;
            parms[4] = _HalfMove.ToString();
            parms[5] = _FullMove.ToString();
            return string.Format(Format, parms);
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
                        #region 此处使用开源的代码但未编译通过
                        // TODO: ConvertPiece
                        //if (ConvertPiece.FromFEN(achar) != Pieces.None)
                        //{
                        //    if (cnt > 7)  // This check needed here to avoid overrunning index below under some error conditions.
                        //        throw new ArgumentException("Invalid board specification, rank " + (ndx / 8 + 1) + " has more then 8 items specified.");
                        //    if (EventPlacePieceHandler != null)
                        //        EventPlacePieceHandler(ConvertPiece.FromFEN(achar), ndx + cnt);
                        //    this[ndx + cnt] = achar;
                        //}
                        #endregion
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
                    _EnPassant = note[3];
                    if (_EnPassant.Length == 2)
                    {
                        if (Color.Equals('w'))
                        {
                            if (_EnPassant[1] != '6')
                                throw new Exception("Invalid target square for white En passant captures: " + _EnPassant.ToString());
                        }
                        else
                        {
                            if (_EnPassant[1] != '3')
                                throw new Exception("Invalid target square for black En passant captures: " + _EnPassant.ToString());
                        }

                        if (EventEnpassantHandler != null)
                            EventEnpassantHandler(_EnPassant);
                    }
                }

                if (note.Length >= 5)
                {
                    // 16.1.3.5: Parse halfmove clock, count of half-move since last pawn advance or unit capture
                    _HalfMove = Int16.Parse(note[4]);
                    if (EventHalfMovesHandler != null)
                        EventHalfMovesHandler(_HalfMove);
                }

                if (note.Length >= 6)
                {
                    // 16.1.3.6: Parse fullmove number, increment after each black move
                    _FullMove = Int16.Parse(note[5]);
                    if (EventFullMovesHandler != null)
                        EventFullMovesHandler(_FullMove);
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
        public void PlacePiece(Chessman chessman, int square)
        {
            // TODO: ConvertPiece
            //this[square] = ConvertPiece.ToFEN(chessman);
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
            this.EnPassant = enpassant;
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
