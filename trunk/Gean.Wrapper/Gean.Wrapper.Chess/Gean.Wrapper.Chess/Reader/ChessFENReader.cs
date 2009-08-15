using System;
using System.IO;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// ���������FEN��ʽ������6��ASCII�ַ�����ɵĴ���(�˴�5���ո����)����6�δ�������������ǣ�
    /// (1) ��ʾ������ÿ�е����ӣ�����FEN��ʽ������Ҫ���֣������Ǵӵ� 8���߿�ʼ˳�������� 1����
    /// (�׷����£�����������)���� a�߿�ʼ˳������h�ߣ��׷������Դ�д��ĸ��PNBRQK����ʾ���ڷ�����
    /// ��Сд ��pnbrqk����ʾ������Ӣ�ı�ʾ����ÿ����ĸ����������볣��涨��ͬ�����ִ���һ������
    /// �ϵ������ո񣬷�б�ܡ�/�� ��ʾ����һ�����ߵ�������
����/// (2) �ֵ���һ�����ӣ�
����/// (3) ÿ�����÷�������ͺ����Ƿ񻹴��ڡ�������λ���Ŀ��ܣ�
����/// (4) �Ƿ���ڳԹ�·���Ŀ��ܣ���·���Ǿ����ĸ����ӵģ�
����/// (5) ���һ�γ��ӻ��߽�������ֽ��еĲ���(��غ���)�������жϡ�50�غ���Ȼ���š���
    /// (6) ��ֵĻغ�����
    /// |example: rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1
    /// |example: r1bq1rk1/pp2ppbp/2np1np1/8/2PNP3/2N1B3/PP2BPPP/R2QK2R w KQ - 0 9
    /// |example: 5n2/5Q2/3pp1pk/2P1b3/1P2P2P/r3q3/2R3BP/2R4K b - - 0 37
    /// |example: 8/8/7k/2p3Qp/1P1bq3/8/6RP/7K b - - 0 48
    /// |example: rnbqkbnr/ppp1ppp1/7p/3pP3/8/8/PPPP1PPP/RNBQKBNR w KQkq d6 0 3
    /// </summary>
    public class ChessFENReader : IFENReader, IFENReaderEvents
    {
        /// <summary>
        /// Holds our board position based on squares 0 thru 63.
        /// </summary>
        private StringBuilder _FENstring;

        #region Events

        public delegate void EventChessmanPosition(Enums.FenChessmans piece, int square);
        public event EventChessmanPosition EventChessmanPositionHandler;

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

        #region Properties

        /// <summary>
        /// The color to move given the current position.
        /// Must be 'w' or 'b'.
        /// </summary>
        public char Color
        {
            get { return _activeColor; }
            set
            {
                if (value == 'w' || value == 'b')
                    _activeColor = value;
                else
                    throw new Exception("Specify: 'w' or 'b'");
            }
        }
        private char _activeColor;

        /// <summary>
        /// If true then white can still castle king side.
        /// </summary>
        public bool WhiteCastleKing
        {
            get { return _WKCastle; }
            set { _WKCastle = value; }
        }
        private bool _WKCastle = false;

        /// <summary>
        /// If true then white can still castle queen side.
        /// </summary>    
        public bool WhiteCastleQueen
        {
            get { return _WQCastle; }
            set { _WQCastle = value; }
        }
        private bool _WQCastle = false;

        /// <summary>
        /// If true then black can still castle king side.
        /// </summary>
        public bool BlackCastleKing
        {
            get { return _BKCastle; }
            set { _BKCastle = value; }
        }
        private bool _BKCastle = false;

        /// <summary>
        /// If true then black can still castle queen side.
        /// </summary>
        public bool BlackCastleQueen
        {
            get { return _BQCastle; }
            set { _BQCastle = value; }
        }
        private bool _BQCastle = false;

        /// <summary>
        /// Algebraic square for enpassant captures or '-'.
        /// </summary>
        public string Enpassant
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
            get { return _halfMove; }
            set { _halfMove = value; }
        }
        private int _halfMove;

        /// <summary>
        /// Number of completed move cycles, i.e. after black moves.
        /// </summary>
        public int FullMove
        {
            get { return _fullMove; }
            set { _fullMove = value; }
        }
        private int _fullMove;

        public string Format
        {
            get { return _format; }
            set { _format = value; }
        }
        private string _format = "{0} {1} {2} {3} {4} {5}";

        /// <summary>
        /// Indexer into the board.  Returns an upper case character for white and
        /// a lower case character for black.  The index number equals the board
        /// square.  Characters are 'PKQNBR' for white and 'pkqnbr' for black.
        /// </summary>
        public char this[int ndx]
        {
            get { return _FENstring[ndx]; }
            set
            {
                string str = "KQRBNPkqrbnp";
                if (str.IndexOf(value) >= 0)
                    _FENstring[ndx] = value;
                else
                    throw new Exception("Invalid piece value (" + value + ") use one of: " + str);
            }
        }

        #endregion

        /// <summary>
        /// Constructs a class by parsing a FEN notation string.
        /// </summary>
        /// <param name="str">A valid FEN notation data string</param>
        public ChessFENReader(string str)
        {
            _FENstring = new StringBuilder(64, 64);
            this.Parse(str);
        }

        /// <summary>
        /// Constructs a default FEN notation object with the board cleared,
        /// all castling available, white to move.
        /// </summary>
        public ChessFENReader()
        {
            _FENstring = new StringBuilder(64, 64);
            Clear();
        }

        /// <summary>
        /// Clears the notation to an empty board, white to move, all castling available.
        /// </summary>
        public void Clear()
        {
            _FENstring.Length = 0;
            _FENstring.Append(' ', 64);
            _EnPassant = "-";
            _activeColor = 'w';
            _WKCastle = false;
            _WQCastle = false;
            _BKCastle = false;
            _BQCastle = false;
            _halfMove = 0;
            _fullMove = 0;
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
                    char achar = _FENstring[ndx + cnt];
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
            parms[1] = _activeColor.ToString();


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
            parms[4] = _halfMove.ToString();
            parms[5] = _fullMove.ToString();
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
            this.Clear();
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
                        if (Enums.FromFEN(achar) != Enums.FenChessmans.None)
                        {
                            if (cnt > 7)  // This check needed here to avoid overrunning index below under some error conditions.
                                throw new ArgumentException("Invalid board specification, rank " + (ndx / 8 + 1) + " has more then 8 items specified.");
                            if (EventChessmanPositionHandler != null)
                                EventChessmanPositionHandler(Enums.FromFEN(achar), ndx + cnt);
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
                            this._WKCastle = true;
                            WK = true;
                            break;
                        case 'Q':
                            this._WQCastle = true;
                            WQ = true;
                            break;
                        case 'k':
                            this._BKCastle = true;
                            BK = true;
                            break;
                        case 'q':
                            this._BQCastle = true;
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
                    _halfMove = Int16.Parse(note[4]);
                    if (EventHalfMovesHandler != null)
                        EventHalfMovesHandler(_halfMove);
                }

                if (note.Length >= 6)
                {
                    // 16.1.3.6: Parse fullmove number, increment after each black move
                    _fullMove = Int16.Parse(note[5]);
                    if (EventFullMovesHandler != null)
                        EventFullMovesHandler(_fullMove);
                }
            }
            catch
            {
            }
            if (EventFinishedHandler != null)
                EventFinishedHandler();
        }

        public void AddEvents(IFENReaderEvents ievents)
        {
            EventChessmanPositionHandler += new EventChessmanPosition(ievents.ChessmanPosition);
            EventSideToMoveHandler += new EventSideToMove(ievents.SetSideToMove);
            EventCastlingHandler += new EventCastling(ievents.SetCastling);
            EventEnpassantHandler += new EventEnpassant(ievents.SetEnpassant);
            EventHalfMovesHandler += new EventHalfMoves(ievents.SetHalfMoves);
            EventFullMovesHandler += new EventFullMoves(ievents.SetFullMoves);
            EventFinishedHandler += new EventFinished(ievents.Finished);
            EventStartingHandler += new EventStarting(ievents.Starting);
        }

        public void RemoveEvents(IFENReaderEvents ievents)
        {
            EventChessmanPositionHandler -= new EventChessmanPosition(ievents.ChessmanPosition);
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
        public void ChessmanPosition(Enums.FenChessmans man, int square)
        {
            this[square] = Enums.ToFEN(man);
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
        /// (Null Method)
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
