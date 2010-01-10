using System;
using System.Collections.Generic;

using System.Text;

namespace Gean
{
    public class ValueChangedEventArgs<T> : ValueEventArgs<T>, IOldValueEventArgs<T>
    {
        public ValueChangedEventArgs(T value, T oldValue)
            : base(value)
        {
            this._oldValue = oldValue;
        }

        #region IOldValueEventArgs<T> Members

        private T _oldValue;
        public T OldValue
        {
            get { return this._oldValue; }
        }

        #endregion
    }
}
