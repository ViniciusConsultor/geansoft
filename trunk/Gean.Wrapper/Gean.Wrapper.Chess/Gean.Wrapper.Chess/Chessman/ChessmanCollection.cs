using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanCollection : IList<Chessman>
    {
        List<Chessman> _chessmans = new List<Chessman>();

        #region IList<ChessmanBase> 成员

        public int IndexOf(Chessman item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Chessman item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Chessman this[int index]
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

        #region ICollection<ChessmanBase> 成员

        public void Add(Chessman item)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Chessman> chessmans)
        {
            this._chessmans.AddRange(chessmans);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Chessman item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Chessman[] array, int arrayIndex)
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

        public bool Remove(Chessman item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<ChessmanBase> 成员

        public IEnumerator<Chessman> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._chessmans.GetEnumerator();
        }

        #endregion
    }
}
