using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public class ValueChangingEventArgs<T> : ValueCancelEventArgs<T>, INewValueEventArgs<T>
    {
        public ValueChangingEventArgs(T value, T newValue)
            : base(value)
        {
            this._newValue = newValue;
        }

        #region INewValueEventArgs<T> Members

        private T _newValue;
        public T NewValue
        {
            get { return this._newValue; }
            set { this._newValue = value; }
        }

        #endregion
    }
}
