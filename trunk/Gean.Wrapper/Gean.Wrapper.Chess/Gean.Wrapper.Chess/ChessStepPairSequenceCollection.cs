using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋招序列的集合，一般应用为一局棋的变招。
    /// （IList集合,集合的元素为<see>ChessStepPairSequence</see>）
    /// </summary>
    public class ChessStepPairSequenceCollection : IList<ChessStepPairSequence>
    {
        List<ChessStepPairSequence> _list = new List<ChessStepPairSequence>();

        public override string ToString()
        {
            return base.ToString();
        }

        public override bool Equals(object obj)
        {
            ChessStepPairSequenceCollection objcon = (ChessStepPairSequenceCollection)obj;
            return objcon._list.Equals(this);
        }
        public override int GetHashCode()
        {
            return unchecked(17 * _list.GetHashCode());
        }

        #region IList<ChessStepSequence> 成员

        public int IndexOf(ChessStepPairSequence item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, ChessStepPairSequence item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public ChessStepPairSequence this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }

        #endregion

        #region ICollection<ChessStepSequence> 成员

        public void Add(ChessStepPairSequence item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(ChessStepPairSequence item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(ChessStepPairSequence[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ChessStepPairSequence item)
        {
            return _list.Remove(item);
        }

        #endregion

        #region IEnumerable<ChessStepSequence> 成员

        public IEnumerator<ChessStepPairSequence> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        #endregion
    }
}
