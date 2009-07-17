using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class SquareCollection : IList<Square>
    {
        List<Square> _squares = new List<Square>();

        #region IList<Square> 成员

        public int IndexOf(Square item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Square item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public Square this[int index]
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

        public void Add(Square item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Square item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Square[] array, int arrayIndex)
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

        public bool Remove(Square item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<Square> 成员

        public IEnumerator<Square> GetEnumerator()
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
