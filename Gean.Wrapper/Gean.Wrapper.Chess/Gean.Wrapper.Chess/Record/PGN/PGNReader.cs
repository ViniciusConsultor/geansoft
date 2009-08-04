using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// Provides the parsing of the PGN standard game notation files
    /// as defined by the standard.
    /// </summary>
    public class PGNReader : IGameReader
    {

        #region  ===== delegate -> event =====

        public delegate void newGame(IGameReader iParser);
        public event newGame EventNewGame;
        public delegate void exitHeader(IGameReader iParser);
        public event exitHeader EventExitHeader;
        public delegate void enterVariation(IGameReader iParser);
        public event enterVariation EventEnterVariation;
        public delegate void exitVariation(IGameReader iParser);
        public event exitVariation EventExitVariation;
        public delegate void starting(IGameReader iParser);
        public event starting EventStarting;
        public delegate void finished(IGameReader iParser);
        public event finished EventFinished;
        public delegate void tagParsed(IGameReader iParser);
        public event tagParsed EventTagParsed;
        public delegate void nagParsed(IGameReader iParser);
        public event nagParsed EventNagParsed;
        public delegate void moveParsed(IGameReader iParser);
        public event moveParsed EventMoveParsed;
        public delegate void commentParsed(IGameReader iParser);
        public event commentParsed EventCommentParsed;
        public delegate void endMarkerParsed(IGameReader iParser);
        public event endMarkerParsed EventendMarkerParsed;

        #endregion

        private Regex _Regex;
        private StringBuilder _Value;
        private string _Data;
        private bool _NextGame;
        private int _PeriodCount;

        /// <summary>
        /// Saves the state of the parser as it enter into a variation.
        /// </summary>
        private Stack _SaveState;

        /// <summary>
        /// Allows access to the current state of the parser
        /// when an event has been fired.
        /// </summary>
        public Enums.GameReaderState State
        {
            get { return this._State; }
            set { this._State = value; }
        }
        private Enums.GameReaderState _State;
        private Enums.GameReaderState _PrevState;

        /// <summary>
        /// Contains the PGN tag information.
        /// </summary>
        public string Tag
        {
            get { return _Tag; }
        }
        string _Tag;

        /// <summary>
        /// Contains the values currently parsed, normally this
        /// is accessed from the event listeners as the parses
        /// signals it has found something.
        /// </summary>
        public string Value
        {
            get { return _Value.ToString(); }
        }

        /// <summary>
        /// File name to open and parse.
        /// </summary>
        public string Filename
        {
            get { return _Filename; }
            set { _Filename = value; }
        }
        string _Filename;

        /// <summary>
        /// Constructor the initializes our parser.
        /// </summary>
        public PGNReader()
        {
            _Regex = new Regex("^\\[([A-Za-z]*) \"(.*)\"", RegexOptions.Compiled);
            _Value = new StringBuilder();
            _State = Enums.GameReaderState.HEADER;
            _SaveState = new Stack();
            _PeriodCount = 0;
        }

        /// <summary>
        /// Adds a specific listner to the events fired by the parsing
        /// of the PGN data.
        /// </summary>
        /// <param name="ievents"></param>
        public void AddEvents(IGameReaderEvents ievents)
        {
            EventNewGame += new newGame(ievents.NewGame);
            EventExitHeader += new exitHeader(ievents.ExitHeader);
            EventEnterVariation += new enterVariation(ievents.EnterVariation);
            EventExitVariation += new exitVariation(ievents.ExitVariation);
            EventStarting += new starting(ievents.Starting);
            EventFinished += new finished(ievents.Finished);
            EventTagParsed += new tagParsed(ievents.TagParsed);
            EventNagParsed += new nagParsed(ievents.NagParsed);
            EventMoveParsed += new moveParsed(ievents.MoveParsed);
            EventCommentParsed += new commentParsed(ievents.CommentParsed);
            EventendMarkerParsed += new endMarkerParsed(ievents.EndMarker);

        }

        /// <summary>
        /// Remove a specific listner from the events fired by the parsing
        /// of the PGN data.
        /// </summary>
        /// <param name="ievents"></param>
        public void RemoveEvents(IGameReaderEvents ievents)
        {
            EventNewGame -= new newGame(ievents.NewGame);
            EventExitHeader += new exitHeader(ievents.ExitHeader);
            EventEnterVariation -= new enterVariation(ievents.EnterVariation);
            EventExitVariation -= new exitVariation(ievents.ExitVariation);
            EventStarting += new starting(ievents.Starting);
            EventFinished -= new finished(ievents.Finished);
            EventTagParsed -= new tagParsed(ievents.TagParsed);
            EventNagParsed -= new nagParsed(ievents.NagParsed);
            EventMoveParsed -= new moveParsed(ievents.MoveParsed);
            EventCommentParsed -= new commentParsed(ievents.CommentParsed);
            EventendMarkerParsed -= new endMarkerParsed(ievents.EndMarker);
        }

        /// <summary>
        /// Responsible for the main driver loop of the parser.  Here we read
        /// in the PGN file and produce events for the listening parties.
        /// </summary>
        public void Parse()
        {
            StringBuilder builder = new StringBuilder(1024);
            FileStream fs = null;
            BinaryReader reader = null;

            if (EventStarting != null)
                EventStarting(this);
            try
            {
                fs = new FileStream(_Filename, FileMode.Open, FileAccess.Read);
                reader = new BinaryReader(fs);

                long bytesread = 0;
                while (bytesread != fs.Length)
                {
                    byte aByte = reader.ReadByte();
                    switch (aByte)
                    {
                        case 0x0D:
                            break;
                        case 0x0A:
                            _Data = builder.ToString();
                            builder.Length = 0;
                            if (_Data.Length > 0)
                            {
                                // Check to make sure we're not inside a comment section then
                                //  see if we are at a game tag.
                                if (_State != Enums.GameReaderState.COMMENT && Regex.IsMatch(_Data, "^\\["))
                                {
                                    if (_NextGame == false)
                                    {
                                        CallEvent(_State);

                                        _NextGame = true;
                                        if (EventNewGame != null)
                                        {
                                            EventNewGame(this);
                                        }
                                    }
                                    _State = Enums.GameReaderState.HEADER;
                                    ParseTag(_Data);
                                    _Value.Length = 0;
                                }
                                else
                                {
                                    if (_NextGame)
                                    {
                                        _NextGame = false;
                                        if (EventExitHeader != null)
                                        {
                                            EventExitHeader(this);
                                        }
                                    }
                                    ParseDetail(_Data);
                                }
                            }
                            break;
                        default:
                            builder.Append((char)aByte);
                            break;
                    }
                    bytesread++;
                }

                CallEvent(_State);
            }
            catch (ApplicationException)
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    reader = null;
                }
            }
            if (EventFinished != null)
                EventFinished(this);
        }

        /// <summary>
        /// Handles the parsing of the actual game notation of the PGN file.
        /// </summary>
        /// <param name="line"></param>
        public void ParseDetail(string line)
        {
            foreach (char aChar in line)
            {
                // Handle any special processing of our text.
                switch (_State)
                {
                    case Enums.GameReaderState.COMMENT:
                        if (aChar == '}')
                        {
                            CallEvent(_State);
                        }
                        else
                            _Value.Append(aChar);
                        break;

                    case Enums.GameReaderState.NAGS:
                        if (aChar >= '0' && aChar <= '9')
                            _Value.Append(aChar);
                        else
                        {
                            CallEvent(_State);
                            HandleChar(aChar);
                        }
                        break;
                    case Enums.GameReaderState.COLOR:
                        if (aChar == '.')
                        {
                            _PeriodCount++;
                        }
                        else
                        {
                            _Value.Length = 0;
                            if (_PeriodCount == 1)
                            {
                                _State = Enums.GameReaderState.WHITE;
                            }
                            else if (_PeriodCount > 1)
                            {
                                _State = Enums.GameReaderState.BLACK;
                            }
                            HandleChar(aChar);
                            _PeriodCount = 0;
                        }
                        break;

                    default:
                        HandleChar(aChar);
                        break;
                }
            }
            // Ensure we add a space between comment lines that are broken appart.
            if (_State == Enums.GameReaderState.COMMENT)
                _Value.Append(' ');
            else
                CallEvent(_State);
        }

        /// <summary>
        /// Handles individual charaters which may signal a change in the
        /// parser's state.
        /// </summary>
        /// <param name="aChar"></param>
        void HandleChar(char aChar)
        {
            switch (aChar)
            {
                case '{':
                    CallEvent(_State);
                    _PrevState = _State;
                    _State = Enums.GameReaderState.COMMENT;
                    break;
                case '(':
                    if (EventEnterVariation != null)
                        EventEnterVariation(this);
                    _Value.Length = 0;
                    _SaveState.Push(_State);
                    break;
                case ')':
                    if (EventExitVariation != null)
                        EventExitVariation(this);
                    _Value.Length = 0;
                    _State = (Enums.GameReaderState)_SaveState.Pop();
                    break;
                case ' ':
                    // Only if we have some data do we want to fire an event.
                    CallEvent(_State);
                    break;
                case '.':
                    // We may have come across 6. e4 6... d5 as in our example data.
                    _State = Enums.GameReaderState.NUMBER;
                    CallEvent(_State);
                    _PeriodCount = 1;
                    break;
                case '$':
                    CallEvent(_State);
                    _PrevState = _State;
                    _State = Enums.GameReaderState.NAGS;
                    break;
                case '!':
                case '?':
                    if (_State != Enums.GameReaderState.CONVERT_NAG)
                    {
                        CallEvent(_State);
                        _PrevState = _State;
                        _State = Enums.GameReaderState.CONVERT_NAG;
                    }
                    _Value.Append(aChar);
                    break;
                case '-':
                    if (_State != Enums.GameReaderState.ENDMARKER &&
                       _Value.Length >= 1)
                    {
                        if ("012".IndexOf(_Value[_Value.Length - 1]) >= 0)
                            _State = Enums.GameReaderState.ENDMARKER;
                    }
                    _Value.Append(aChar);
                    break;
                case '*':
                    _State = Enums.GameReaderState.ENDMARKER;
                    _Value.Append(aChar);
                    break;
                case '\t':
                    break;
                default:
                    _Value.Append(aChar);
                    break;
            }
        }

        /// <summary>
        /// Calls the correct event based on the parsers state.
        /// </summary>
        /// <param name="state"></param>
        void CallEvent(Enums.GameReaderState state)
        {
            if (_Value.Length > 0)
            {
                switch (state)
                {
                    case Enums.GameReaderState.COMMENT:
                        if (EventCommentParsed != null)
                            EventCommentParsed(this);
                        _State = _PrevState;
                        break;
                    case Enums.GameReaderState.NAGS:
                        if (EventNagParsed != null)
                            EventNagParsed(this);
                        _State = _PrevState;
                        break;
                    case Enums.GameReaderState.CONVERT_NAG:
                        string nag = _Value.ToString();
                        _Value.Length = 0;
                        if (nag.Equals("!"))
                            _Value.Append('1');
                        else if (nag.Equals("?"))
                            _Value.Append('2');
                        else if (nag.Equals("!!"))
                            _Value.Append('3');
                        else if (nag.Equals("??"))
                            _Value.Append('4');
                        else if (nag.Equals("!?"))
                            _Value.Append('5');
                        else if (nag.Equals("?!"))
                            _Value.Append('6');
                        else
                            _Value.Append('0');
                        if (EventNagParsed != null)
                            EventNagParsed(this);
                        _State = _PrevState;
                        break;
                    case Enums.GameReaderState.NUMBER:
                        if (EventMoveParsed != null)
                            EventMoveParsed(this);
                        _State = Enums.GameReaderState.COLOR;
                        break;
                    case Enums.GameReaderState.WHITE:
                        if (EventMoveParsed != null)
                            EventMoveParsed(this);
                        _State = Enums.GameReaderState.BLACK;
                        break;
                    case Enums.GameReaderState.BLACK:
                        if (EventMoveParsed != null)
                            EventMoveParsed(this);
                        _State = Enums.GameReaderState.NUMBER;
                        break;
                    case Enums.GameReaderState.ENDMARKER:
                        if (EventendMarkerParsed != null)
                            EventendMarkerParsed(this);
                        _State = Enums.GameReaderState.HEADER;
                        break;
                }
            }
            // Always clear out our data as the handler should have used this value during the event.
            _Value.Length = 0;
        }

        /// <summary>
        /// Parses out the PGN tag and value from the game header.
        /// </summary>
        /// <param name="line"></param>
        public void ParseTag(string line)
        {
            int nleft = line.IndexOf('"');
            int nright = line.LastIndexOf('"');

            Match regMatch = _Regex.Match(line);
            if (regMatch.Groups.Count == 3)
            {
                // Call the events with the tag and tag value.
                if (EventTagParsed != null)
                {
                    _Tag = regMatch.Groups[1].Value;
                    _Value.Length = 0;
                    _Value.Append(regMatch.Groups[2].Value);
                    EventTagParsed(this);
                }
            }
        }

    }
}
