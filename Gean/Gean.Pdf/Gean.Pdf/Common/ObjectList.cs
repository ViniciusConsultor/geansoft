using System;

namespace Gean.Pdf
{
    /// <summary>
    /// 增加的对象数量和文件偏移量
    /// </summary>
    internal class ObjectList : IComparable
    {
        private long _offset;
        public long Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }
        private uint _objNumber;
        public uint ObjNumber
        {
            get { return _objNumber; }
            set { _objNumber = value; }
        }

        public ObjectList(uint objectNum, long fileOffset)
        {
            _offset = fileOffset;
            _objNumber = objectNum;
        }
        #region IComparable Members

        public int CompareTo(object obj)
        {
            int result = 0;
            result = (this._objNumber.CompareTo(((ObjectList)obj)._objNumber));
            return result;
        }

        #endregion
    }
}
