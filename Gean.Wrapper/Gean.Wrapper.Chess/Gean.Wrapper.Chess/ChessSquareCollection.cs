using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessSquareCollection : IList<ChessSquare>
    {
        List<ChessSquare> _squares = new List<ChessSquare>();

        /// <summary>
        /// 返回位于 SquareCollection 开始处的(最近发生的) Square 但不将其移除。
        /// </summary>
        /// <returns></returns>
        public ChessSquare Peek()
        {
            if (_squares.Count == 0)
                return null;
            return _squares[_squares.Count - 1];
        }

        #region IList<Square> 成员

        public int IndexOf(ChessSquare item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ChessSquare item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public ChessSquare this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<Square> 成员

        public void Add(ChessSquare item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ChessSquare item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ChessSquare[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(ChessSquare item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<Square> 成员

        public IEnumerator<ChessSquare> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
