using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一个棋招。
    /// 棋招的定义是成对的<see>ChessStep</see>,它代表着双方各走了一步棋，
    /// 同时它拥有棋局中的对“步数”的编号。
    /// </summary>
    public class ChessStepPair : ISequenceItem, IStepTree 
    {
        public ChessStep White { get; set; }
        public ChessStep Black { get; set; }
        public int Number { get; set; }

        #region IStepTree 成员

        public object Parent { get; set; }

        public bool HasChildren
        {
            get
            {
                if (this.Items == null) return false;
                if (this.Items.Count <= 0) return false;
                return true;
            }
        }

        public ChessSequence Items { get; set; }

        #endregion

        internal ChessStepPair()
        { 
        }

        public ChessStepPair(int number, ChessStep white, ChessStep black)
        {
            if (number <= 0)
            {
                throw new ArgumentException(number.ToString() + " cannot <= 0");
            }
            this.Number = number;
            this.White = white;
            this.Black = black;
        }

        public void Add(ChessChoices choices)
        {
            this.Items = choices;
        }

        public string Value 
        {
            get { return this.ToString(); }
            set
            {
                ChessStepPair pair = Parse(value);
                this.Number = pair.Number;
                this.White = pair.White;
                this.Black = pair.Black;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Number).Append(". ").Append(this.White).Append(' ').Append(this.Black);
            return sb.ToString();
        }
        public override int GetHashCode()
        {
            return unchecked(7 * (White.GetHashCode() + Black.GetHashCode() + Number.GetHashCode()));
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is System.DBNull) return false;

            ChessStepPair csp = (ChessStepPair)obj;
            if (!this.Number.Equals(csp.Number)) return false;
            if (!UtilityEquals.Equals(this.White, csp.White)) return false;
            if (!UtilityEquals.Equals(this.Black, csp.Black)) return false;
            return true;
        }

        public static ChessStepPair Parse(string value)
        { 
            if (string.IsNullOrEmpty(value))
                throw new ArgumentOutOfRangeException(value);

            int number = 0;

            int n;
            value = value.Trim();
            n = value.IndexOf('.');
            number = int.Parse(value.Substring(0, n));
            return ChessStepPair.Parse(number, value.Substring(n + 1));
        }

        public static ChessStepPair Parse(int number, string value)
        {
            string[] steps = value.Split(' ');

            ChessStep white = null;
            ChessStep black = null;
            for (int i = 0; i < steps.Length; i++)
            {
                if (string.IsNullOrEmpty(steps[i]))
                    continue;
                if ((steps[i].StartsWith("(") && steps[i].EndsWith(")")) ||
                    (steps[i].StartsWith("[") && steps[i].EndsWith("]")))
                {
                    int m = 333;
                }
                if (white == null)
                    white = ChessStep.Parse(steps[i], Enums.ChessmanSide.White);
                else if (black == null)
                    black = ChessStep.Parse(steps[i], Enums.ChessmanSide.Black);
            }

            return new ChessStepPair(number, white, black);
        }
    }
}
