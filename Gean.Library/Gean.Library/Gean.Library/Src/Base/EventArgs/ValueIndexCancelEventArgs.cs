using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public class ValueIndexCancelEventArgs<T> : ValueIndexEventArgs<T>, ICancelEventArgs
    {
        public ValueIndexCancelEventArgs(T value, int index)
            : base(value, index)
        { }

        #region ICancelEventArgs Members

        private bool _isCanceled;
        public bool IsCanceled
        {
            get { return this._isCanceled; }
        }

        public void Cancel()
        {
            this._isCanceled = true;
        }

        #endregion
    }
}
