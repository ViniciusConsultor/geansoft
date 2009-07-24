using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessStepPairSequenceCollection : IDictionary<int, ChessStepPairSequence>
    {
        Dictionary<int, ChessStepPairSequence> _sequences = new Dictionary<int, ChessStepPairSequence>();

        #region IDictionary<int,ChessStepPairSequence> 成员

        public void Add(int key, ChessStepPairSequence value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(int key)
        {
            throw new NotImplementedException();
        }

        public ICollection<int> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(int key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(int key, out ChessStepPairSequence value)
        {
            throw new NotImplementedException();
        }

        public ICollection<ChessStepPairSequence> Values
        {
            get { throw new NotImplementedException(); }
        }

        public ChessStepPairSequence this[int key]
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

        #region ICollection<KeyValuePair<int,ChessStepPairSequence>> 成员

        public void Add(KeyValuePair<int, ChessStepPairSequence> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<int, ChessStepPairSequence> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<int, ChessStepPairSequence>[] array, int arrayIndex)
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

        public bool Remove(KeyValuePair<int, ChessStepPairSequence> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<int,ChessStepPairSequence>> 成员

        public IEnumerator<KeyValuePair<int, ChessStepPairSequence>> GetEnumerator()
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
