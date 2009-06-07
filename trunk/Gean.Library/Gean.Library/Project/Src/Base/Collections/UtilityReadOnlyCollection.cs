using System;
using System.Collections;
using System.Collections.Generic;

namespace Gean
{
    /// <summary>
    /// Wraps any collection to make it read-only.
    /// </summary>
    public sealed class UtilityReadOnlyCollection<T> : ICollection<T>
    {
        readonly ICollection<T> _Collection;

        public UtilityReadOnlyCollection(ICollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            this._Collection = collection;
        }

        public int Count
        {
            get { return _Collection.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        void ICollection<T>.Add(T item)
        {
            throw new NotSupportedException();
        }

        void ICollection<T>.Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(T item)
        {
            return _Collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _Collection.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_Collection).GetEnumerator();
        }
    }
}
