using System;
using System.Collections.Generic;

using System.Text;

namespace Gean
{
    public class ValueIndexChangedEventArgs<T> : ValueChangedEventArgs<T>, IIndexEventArgs
    {
        public ValueIndexChangedEventArgs(T value, T oldValue, int index)
            : base(value, oldValue)
        {
            this._index = index;
        }
        #region IIndexEventArgs Members

        private int _index;
        public int Index
        {
            get { return this._index; }
        }

        #endregion
    }
}
