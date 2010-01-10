using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public class RangeEventArgs : EventArgs, IRangeEventArgs
    {
        public RangeEventArgs(int index, int count)
        {
            this._index = index;
            this._count = count;
        }

        #region IRangeEventArgs Members

        private int _count;
        public int Count
        {
            get { return this._count; }
        }

        #endregion

        #region IIndexEventArgs Members

        private int _index;
        public int Index
        {
            get { return this._index; }
        }

        #endregion
    }
}
