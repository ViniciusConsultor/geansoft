using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Pdf
{

    /// <summary>
    /// Holds the Byte offsets of the objects used in the Pdf Document
    /// </summary>
    internal class XrefEnteries
    {
        internal static ArrayList offsetArray;

        internal XrefEnteries()
        {
            offsetArray = new ArrayList();
        }
    }

    /// <summary>
    /// For Adding the Object number and file offset
    /// </summary>
    internal class ObjectList : IComparable
    {
        internal long offset;
        internal uint objNum;

        internal ObjectList(uint objectNum, long fileOffset)
        {
            offset = fileOffset;
            objNum = objectNum;
        }
        #region IComparable Members

        public int CompareTo(object obj)
        {

            int result = 0;
            result = (this.objNum.CompareTo(((ObjectList)obj).objNum));
            return result;
        }

        #endregion
    }
}
