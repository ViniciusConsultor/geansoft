using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public class ValueIndexChangingEventArgs<T> : ValueChangingEventArgs<T>, IIndexEventArgs
    {
        public ValueIndexChangingEventArgs(T value, T newValue, int index)
            : base(value, newValue)
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
