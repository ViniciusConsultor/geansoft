using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public class ValueEventArgs<T> : EventArgs, IValueEventArgs<T>
    {
        public ValueEventArgs(T value)
        {
            this._value = value;
        }

        #region IValueEventArgs<T> Members

        private T _value;
        public T Value
        {
            get { return this._value; }
        }

        #endregion
    }
}
