using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Gean.Wrapper.PGN
{
    public class PGNReader : MarshalByRefObject, IDisposable
    {
        protected TextReader TextReader { get; set; }
        /// <summary>
        /// 获取或设置一个PGN文件中所有的单局棋记录的集合
        /// </summary>
        protected PGNRecord[] PGNRecordArray { get; set; }

        public PGNReader()
        { }

        public void Load(string fullpath)
        {
            this.TextReader = new StreamReader(fullpath);
            this.PGNRecordArray = Helper.GetPGNsByTextReader(this.TextReader);
        }

        #region IDisposable 成员

        public void Dispose()
        {
            throw new NotImplementedException();

        }

        #endregion

        static class Helper
        {
            public static PGNRecord[] GetPGNsByTextReader(TextReader reader)
            {
                List<PGNRecord> records = new List<PGNRecord>();
                List<StringBuilder> sbs = new List<StringBuilder>();

                PGNRecord record = new PGNRecord();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Equals(string.Empty))
                    {
                        if (record.PGNDefiner != null && !string.IsNullOrEmpty(record.Record.ToString()))
                        {
                            records.Add(record);
                        }
                        if (records.Contains(record))
                        {
                            record = new PGNRecord();
                        }
                    }
                    else
                    {
                        if (Regex.IsMatch(line, @"\[.*\]"))
                        {
                            StringPair sp = ParseLine(line);
                            record.PGNDefiner.Set<string>(sp.Key, sp.Value);
                        }
                        else
                        {
                            record.Record.Append(line);
                        }
                    }
                }
                if (!records.Contains(record))
                {
                    records.Add(record);
                }
                return records.ToArray();
            }

            public static StringPair ParseLine(string line)
            {
                line = line.Trim().Trim('[').Trim(']');
                string key = line.Substring(0, line.IndexOf(' '));
                string value = line.Substring(line.IndexOf(' ')).Trim().Trim('"');
                return new StringPair(key, value);
            }
        }

        struct StringPair
        {
            public string Key
            {
                get { return this._Key; }
                set { this._Key = value; }
            }
            private string _Key;

            public string Value
            {
                get { return this._Value; }
                set { this._Value = value; }
            }
            private string _Value;

            public StringPair(string key, string value)
            {
                this._Key = key;
                this._Value = value;
            }

            public override bool Equals(object obj)
            {
                StringPair sp = (StringPair)obj;
                if (!sp.Key.Equals(this._Key))
                {
                    return false;
                }
                if (!sp.Value.Equals(this._Value))
                {
                    return false;
                }
                return true;
            }
            public override int GetHashCode()
            {
                return unchecked(27 * this._Key.GetHashCode() ^ this._Value.GetHashCode());
            }
            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("{").Append(this._Key).Append("}");
                sb.Append("|");
                sb.Append("{").Append(this._Value).Append("}");
                return sb.ToString();
            }
        }

    }

    public class PGNRecord
    {
        public Definer PGNDefiner { get; set; }
        public StringBuilder Record { get; set; }
        public PGNRecord()
        {
            this.PGNDefiner = new Definer();
            this.Record = new StringBuilder();
        }

        public override bool Equals(object obj)
        {
            PGNRecord pr = obj as PGNRecord;
            if (!pr.PGNDefiner.Equals(this.PGNDefiner))
            {
                return false;
            }
            if (!pr.Record.Equals(this.Record))
            {
                return false;
            }
            return true;
        }
        public override int GetHashCode()
        {
            return unchecked(27 * this.PGNDefiner.GetHashCode() + this.Record.GetHashCode());
        }
    }
}




/*

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Gean.Wrapper.PGN
{
    public class PGNReader
    {
        protected FileStream FileStream { get; private set; }
        protected char Char { get; private set; }
        protected int LineCount { get; private set; }
        protected int CharInLine { get; private set; }
        protected int TokenIdx { get; private set; }
        protected char[] Token { get; private set; }

        private static int TOKENSIZE = 49;

        public static int EOF_CHAR = '&';
        public static int TT_NONE = 0;
        public static int TT_IDENTIFIER = 1;
        public static int TT_NUMBER = 2;
        public static int TT_SEPARATOR = 3;
        public static int TT_STRING = 4;
        public static int TT_OUTCOME = 5;
        public static int TT_OPEN_TAG = 6;
        public static int TT_CLOSE_TAG = 7;

        public int tokenType;
        public int nval;
        public bool validParameters;
        public char piece;
        public int sourceX;
        public int sourceY;
        public int destX;
        public int destY;
        public string promotionPiece;

        public PGNReader(FileStream stream)
        {
            this.FileStream = stream;
            this.Token = new char[TOKENSIZE];
            this.LineCount = 1;
            this.CharInLine = 0;
            this.Char = ' ';
        }

        private void atoi()
        {
            int i, start;
            bool negative;

            if (Token[0] == '-')
            {
                negative = true;
                start = 1;
            }
            else
            {
                negative = false;
                start = 0;
            }

            nval = 0;
            for (i = start; i < TokenIdx; i++)
            {
                nval = 10 * nval + (Token[i] - '0');
            }
            if (negative)
            { nval = nval * -1;
            }
        }

        private void ReadByte()
        {
            try
            {
                Char = (char)FileStream.ReadByte();
                CharInLine++;
            }
            catch (IOException e)
            {
                Console.WriteLine("I/O error");
                //e.printStackTrace();
            }
            if (Char == '\n')
            {
                Char = ' ';
                LineCount++;
                CharInLine = 0;
            }
        }

        private int getLineNumber()
        {
            return LineCount;
        }

        public char lookaheadCh()
        {
            SkipBlanks();
            return Char;
        }

        private bool IsEOF()
        {
            return (Char == Convert.ToChar(-1));
        }

        private bool IsRank(char c)
        {
            return (c >= '1' && c <= '8');
        }

        private bool IsFile(char c)
        {
            return (c >= 'a' && c <= 'h');
        }

        private bool IsDigit()
        {
            return (Char >= '0' && Char <= '9');
        }

        private bool IsAlpha()
        {
            return
              (Char >= 'a' && Char <= 'z') ||
              (Char >= 'A' && Char <= 'Z');
        }

        private bool IsPiece(char c)
        {
            return c == 'P' || c == 'R' || c == 'N' || c == 'B' || c == 'Q' || c == 'K';
        }

        public bool EOFToken()
        {
            return (Char == EOF_CHAR);
        }

        private void SkipLine()
        {
            try
            {
                while (Char != '\n')
                    Char = (char)FileStream.ReadByte();
                CharInLine = 0;
            }
            catch (IOException e)
            {
                Console.WriteLine("I/O error");
                //e.printStackTrace();
            }
            LineCount++;
            ReadByte();
        }

        private void SkipBlanks()
        {
            if (Char == EOF_CHAR) // already at end of file
                return;

            while (true)
            {
                if (IsEOF())
                {
                    Char = Convert.ToChar(EOF_CHAR);
                    return;
                }
                if (Char == ';') // comment
                    SkipLine();
                else
                    if (Char == '{')
                        SkipComment();
                    else
                        if (Char == ' ' || Char == 13 || Char == 9 || Char == '\n')
                            ReadByte();
                        else
                            return;
            }
        }

        private void SkipComment()
        {
            bool done = false;
            ReadByte();
            while (!done)
            {
                if (IsEOF())
                {
                    Char = Convert.ToChar(EOF_CHAR);
                    done = true;
                }
                else
                {
                    if (Char == '}')
                    {
                        ReadByte();
                        done = true;
                    }
                    else
                        ReadByte();
                }
            }
        }

        private void GetWinLossDraw()
        {
            tokenType = TT_IDENTIFIER;
            while (IsDigit() || Char == '-' || Char == '/')
            {
                Token[TokenIdx++] = Char;
                ReadByte();
            }
            String testOutcome = new String(Token, 0, TokenIdx);
            if (testOutcome.Equals("1-0"))
            {
                tokenType = TT_OUTCOME;
            }
            else
                if (testOutcome.Equals("1/2-1/2"))
                {
                    tokenType = TT_OUTCOME;
                }
                else
                    if (testOutcome.Equals("1/2"))
                    {
                        tokenType = TT_OUTCOME;
                        Token[3] = '-';
                        Token[4] = '1';
                        Token[5] = '/';
                        Token[6] = '2';
                        TokenIdx = 7;
                    }
                    else
                        if (testOutcome.Equals("0-1"))
                        {
                            tokenType = TT_OUTCOME;
                        }
        }

        public string GetToken(bool whiteMove)
        {
            promotionPiece = null;
            SkipBlanks();
            if (Char == EOF_CHAR)
                return "";

            TokenIdx = 0;

            if (Char == '(' ||
                Char == ')' ||
                Char == ',' ||
                Char == '.' ||
                Char == '+')
            {
                tokenType = TT_SEPARATOR;
                Token[TokenIdx++] = Char;
                ReadByte();
            }

            else
                if (Char == '[')
                {
                    tokenType = TT_OPEN_TAG;
                    Token[TokenIdx++] = Char;
                    ReadByte();
                }

                else
                    if (Char == ']')
                    {
                        tokenType = TT_CLOSE_TAG;
                        Token[TokenIdx++] = Char;
                        ReadByte();
                    }

                    else
                        if (Char == '*')
                        {
                            tokenType = TT_OUTCOME;
                            Token[TokenIdx++] = Char;
                            ReadByte();
                        }

                        else
                            if (IsDigit() || Char == '-')
                            {
                                tokenType = TT_NUMBER;
                                if (Char == '-')
                                {
                                    Token[TokenIdx++] = Char;
                                    ReadByte();
                                }
                                while (IsDigit())
                                {
                                    Token[TokenIdx++] = Char;
                                    ReadByte();
                                    if (Char == '-' || Char == '/')
                                        GetWinLossDraw();
                                }

                                if (tokenType == TT_NUMBER)
                                {
                                    if (TokenIdx == 0)
                                        Token[TokenIdx++] = '0';
                                    else
                                        if (TokenIdx == 1 && Token[0] == '-')
                                            Token[0] = '0';

                                    atoi();
                                }
                            }

                            else // id
                                if (IsAlpha())
                                {
                                    tokenType = TT_IDENTIFIER;
                                    Token[TokenIdx++] = Char;
                                    ReadByte();
                                    while (!(Char == ' ' || Char == 13 || Char == 9 || Char == '\n'))
                                    {
                                        if (TokenIdx < TOKENSIZE)
                                        {
                                            Token[TokenIdx++] = Char;
                                        }
                                        ReadByte();
                                    }
                                    GetMoveParameters(whiteMove);
                                }

                                else
                                    if (Char == '\"')
                                    {
                                        tokenType = TT_STRING;
                                        ReadByte();
                                        while (Char != '\"' && TokenIdx < TOKENSIZE)
                                        {
                                            Token[TokenIdx++] = Char;
                                            ReadByte();
                                        }
                                        ReadByte();
                                    }

                                    else
                                    {
                                        tokenType = TT_NONE;
                                        Token[TokenIdx++] = Char;
                                        String stringCh = "";
                                        stringCh += Char;
                                        Console.WriteLine(getLineNumber() + " bad character " + stringCh);
                                        ReadByte();
                                    }

            return new String(Token, 0, TokenIdx);
        }

        private int getRank(char ch)
        {
            return 7 - (ch - '1');
        }

        private int getFile(char ch)
        {
            return ch - 'a';
        }

        private void GetMoveParameters(bool whiteMove)
        {
            int[] file;
            int[] rank;
            file = new int[2];
            rank = new int[2];
            piece = ' ';
            sourceX = -1;
            sourceY = -1;
            destX = -1;
            destY = -1;

            if (checkCastle(whiteMove))
                return;

            int fileCount = 0;
            int rankCount = 0;
            for (int i = 0; i < TokenIdx; i++)
            {
                if (IsFile(Token[i]))
                {
                    if (fileCount == 2)
                        break;
                    file[fileCount] = getFile(Token[i]);
                    fileCount++;
                }
                else
                    if (IsRank(Token[i]))
                    {
                        if (rankCount == 2)
                            break;
                        rank[rankCount] = getRank(Token[i]);
                        rankCount++;
                    }
            }

            if (IsFile(Token[0]))
                piece = 'P';
            else
            {
                piece = Token[0];
                if (!IsPiece(piece))
                    piece = ' ';
            }

            // check promotion piece
            promotionPiece = null;
            if (piece == 'P')
            {
                for (int i = 1; i < TokenIdx; i++)
                {
                    if (IsPiece(Token[i]))
                    {
                        char color;
                        if (whiteMove)
                            color = 'W';
                        else
                            color = 'B';
                        promotionPiece = color.ToString() + Token[i].ToString();
                        break;
                    }
                }
            }

            if (fileCount == 1)
                destX = file[0];
            else
            {
                sourceX = file[0];
                destX = file[1];
            }

            if (rankCount == 1)
                destY = rank[0];
            else
            {
                sourceY = rank[0];
                destY = rank[1];
            }

            if (destX == -1 || destY == -1 || piece == ' ')
                validParameters = false;
            else
                validParameters = true;
        }

        private bool checkCastle(bool whiteMove)
        {
            if (TokenIdx >= 3 &&
                Token[0] == 'O' && Token[1] == '-' && Token[2] == 'O')
            {
                piece = 'K';
                if (TokenIdx >= 5 && Token[3] == '-' && Token[4] == 'O')
                {
                    if (whiteMove)
                    {
                        sourceX = 4;
                        sourceY = 7;
                        destX = 2;
                        destY = 7;
                    }
                    else
                    {
                        sourceX = 4;
                        sourceY = 0;
                        destX = 2;
                        destY = 0;
                    }
                    validParameters = true;
                    tokenType = TT_IDENTIFIER;
                    return true;
                }
                else
                {
                    if (whiteMove)
                    {
                        sourceX = 4;
                        sourceY = 7;
                        destX = 6;
                        destY = 7;
                    }
                    else
                    {
                        sourceX = 4;
                        sourceY = 0;
                        destX = 6;
                        destY = 0;
                    }
                    validParameters = true;
                    tokenType = TT_IDENTIFIER;
                    return true;
                }
            }
            else
                return false;
        }

    }
}


*/