using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    public class ValueIndexEventArgs<T> : ValueEventArgs<T>
    {
        public ValueIndexEventArgs(T value, int index)
            : base(value)
        {
            this._index = index;
        }

        private int _index;
        public int Index
        {
            get { return this._index; }
        }
    }
}
