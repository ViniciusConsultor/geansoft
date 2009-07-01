using System.Collections;

namespace Gean.Pdf
{
    /// <summary>
    /// Holds the Byte offsets of the objects used in the Pdf Document
    /// </summary>
    internal class SourceCodeCollection
    {
        private static ArrayList _offsetArray;
        public static ArrayList OffsetArray
        {
            get { return SourceCodeCollection._offsetArray; }
            set { SourceCodeCollection._offsetArray = value; }
        }

        public SourceCodeCollection()
        {
            _offsetArray = new ArrayList();
        }
    }
}
