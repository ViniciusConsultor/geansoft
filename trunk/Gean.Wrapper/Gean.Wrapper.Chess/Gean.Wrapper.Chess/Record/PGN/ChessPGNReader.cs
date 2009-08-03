using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Gean.Wrapper.Chess
{
    public class ChessPGNReader : MarshalByRefObject
    {
        protected TextReader TextReader { get; set; }
        /// <summary>
        /// 获取或设置一个PGN文件中所有的单局棋记录的集合
        /// </summary>
        protected ChessRecord[] ChessRecords { get; set; }

        public ChessPGNReader()
        { }

        public void Load(string fullpath)
        {
            this.TextReader = new StreamReader(fullpath);
            this.ChessRecords = Helper.GetPGNsByTextReader(this.TextReader);
        }

        static class Helper
        {
            public static ChessRecord[] GetPGNsByTextReader(TextReader reader)
            {
                List<ChessRecord> records = new List<ChessRecord>();
                List<StringBuilder> sbs = new List<StringBuilder>();

                ChessRecord record = new ChessRecord();
                StringBuilder sequenceBuilder = new StringBuilder();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line.Equals(string.Empty))
                    {
                        if (record.Definer != null)
                        {
                            record.Sequence = ChessSequence.Parse(sequenceBuilder.ToString());
                            records.Add(record);
                        }
                        if (records.Contains(record))
                        {
                            sequenceBuilder = new StringBuilder();
                            record = new ChessRecord();
                        }
                    }
                    else
                    {
                        if (Regex.IsMatch(line, @"\[.*\]"))
                        {
                            StringPair sp = ParseLine(line);
                            record.Definer.Set<string>(sp.Key, sp.Value);
                        }
                        else
                        {
                            sequenceBuilder.Append(line).Append(' ');
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
                if (!sp.Key.Equals(this._Key)) return false;
                if (!sp.Value.Equals(this._Value)) return false;
                return true;
            }
            public override int GetHashCode()
            {
                return unchecked(3 * (this._Key.GetHashCode() + this._Value.GetHashCode()));
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
}