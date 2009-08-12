using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Gean
{
    public class ValueCancelEventArgs<T> : CancelEventArgs, IValueEventArgs<T>
    {
        public ValueCancelEventArgs(T value)
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
