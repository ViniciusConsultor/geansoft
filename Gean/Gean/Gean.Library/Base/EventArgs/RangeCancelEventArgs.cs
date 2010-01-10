using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public class RangeCancelEventArgs : RangeEventArgs, ICancelEventArgs
    {
        public RangeCancelEventArgs(int index, int count)
            : base(index, count)
        { }

        #region ICancelEventArgs Members

        private bool _isCanceled;
        public bool IsCanceled
        {
            get { return this._isCanceled; }
        }

        public void Cancel()
        {
            this._isCanceled |= true;
        }

        #endregion
    }
}
