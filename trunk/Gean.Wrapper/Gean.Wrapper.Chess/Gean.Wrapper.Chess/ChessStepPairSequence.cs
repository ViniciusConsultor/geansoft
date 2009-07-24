using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一个棋招序列（IList集合,集合的元素为<see>ChessStepPair</see>）。
    /// 它可能描述的是一整局棋，也可能是描述的是一整局棋的一部份，如变招的描述与记录。
    /// </summary>
    public class ChessStepPairSequence : IList<ChessStepPair>
    {
        List<ChessStepPair> _steps = new List<ChessStepPair>();

        public int Number { get; set; }
        public string UserId { get; set; }

        #region IList<ChessStepPair> 成员

        public int IndexOf(ChessStepPair item)
        {
            return _steps.IndexOf(item);
        }

        public void Insert(int index, ChessStepPair item)
        {
            _steps.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _steps.RemoveAt(index);
        }

        public ChessStepPair this[int index]
        {
            get { return _steps[index]; }
            set { _steps[index] = value; }
        }

        #endregion

        #region ICollection<ChessStepPair> 成员

        public void Add(ChessStepPair item)
        {
            _steps.Add(item);
        }

        public void Clear()
        {
            _steps.Clear();
        }

        public bool Contains(ChessStepPair item)
        {
            return _steps.Contains(item);
        }

        public void CopyTo(ChessStepPair[] array, int arrayIndex)
        {
            _steps.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _steps.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ChessStepPair item)
        {
            return _steps.Remove(item);
        }

        #endregion

        #region IEnumerable<ChessStepPair> 成员

        public IEnumerator<ChessStepPair> GetEnumerator()
        {
            return _steps.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _steps.GetEnumerator();
        }

        #endregion

        internal string SequenceToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ChessStepPair pair in this._steps)
            {
                sb.Append(pair.ToString());
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return Utility.AccessorialItemToString('&', this.Number, this.UserId, this.SequenceToString());
        }

    }
}
