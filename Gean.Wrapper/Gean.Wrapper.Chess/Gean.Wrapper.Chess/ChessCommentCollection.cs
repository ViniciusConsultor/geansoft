using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessCommentCollection : IList<ChessComment>
    {
        List<ChessComment> _comments = new List<ChessComment>();

        #region IList<ChessComment> 成员

        public int IndexOf(ChessComment item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ChessComment item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public ChessComment this[int index]
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

        #region ICollection<ChessComment> 成员

        public void Add(ChessComment item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ChessComment item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ChessComment[] array, int arrayIndex)
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

        public bool Remove(ChessComment item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<ChessComment> 成员

        public IEnumerator<ChessComment> GetEnumerator()
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
