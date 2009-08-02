using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一个棋招序列（IList集合,集合的元素为<see>ChessStepPair</see>）。
    /// 它可能描述的是一整局棋，也可能是描述的是一整局棋的一部份，如变招的描述与记录。
    /// </summary>
    public class ChessStepPairSequence : BylawItem, IList<ChessStepPair>
    {

        protected List<ChessStepPair> ChessStepPairs { get; private set; }

        public ChessStepPairSequence(string userId, string sequence, int number)
            : base(number, userId, sequence, '#')
        {
            this.ChessStepPairs = new List<ChessStepPair>();
        }

        public override string ToString()
        {
            return Utility.BylawItemToString('&', this.Number, this.UserID, this.SequenceToString());
        }
        protected string SequenceToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ChessStepPair pair in this.ChessStepPairs)
            {
                sb.Append(pair.ToString()).Append(' ');
            }
            return sb.ToString();
        }

        #region IList<ChessStepPair> 成员

        public int IndexOf(ChessStepPair item)
        {
            return this.ChessStepPairs.IndexOf(item);
        }

        public void Insert(int index, ChessStepPair item)
        {
            this.ChessStepPairs.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.ChessStepPairs.RemoveAt(index);
        }

        public ChessStepPair this[int index]
        {
            get { return this.ChessStepPairs[index]; }
            set { this.ChessStepPairs[index] = value; }
        }

        #endregion

        #region ICollection<ChessStepPair> 成员

        public void Add(ChessStepPair item)
        {
            this.ChessStepPairs.Add(item);
        }

        public void Clear()
        {
            this.ChessStepPairs.Clear();
        }

        public bool Contains(ChessStepPair item)
        {
            return this.ChessStepPairs.Contains(item);
        }

        public void CopyTo(ChessStepPair[] array, int arrayIndex)
        {
            this.ChessStepPairs.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.ChessStepPairs.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ChessStepPair item)
        {
            return this.ChessStepPairs.Remove(item);
        }

        #endregion

        #region IEnumerable<ChessStepPair> 成员

        public IEnumerator<ChessStepPair> GetEnumerator()
        {
            return this.ChessStepPairs.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.ChessStepPairs.GetEnumerator();
        }

        #endregion
    }
}
