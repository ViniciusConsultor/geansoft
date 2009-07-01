using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    /// This is the base object for all objects used within the pdf.
    /// </summary>
    public class PdfBase
    {
        /// <summary>
        /// Stores the Object Number
        /// </summary>
        internal static uint inUseObjectNum;
        public uint objectNum;
        //private UTF8Encoding utf8;
        private SourceCodeCollection Xref;
        /// <summary>
        /// Constructor increments the object number to 
        /// reflect the currently used object number
        /// </summary>
        protected PdfBase()
        {
            if (inUseObjectNum == 0)
                Xref = new SourceCodeCollection();
            inUseObjectNum++;
            objectNum = inUseObjectNum;
        }
        ~PdfBase()
        {
            objectNum = 0;
        }
        /// <summary>
        /// Convert the unicode string 16 bits to unicode bytes. 
        /// This is written to the file to create Pdf 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected byte[] GetUTF8Bytes(string str, long filePos, out int size)
        {
            ObjectList objList = new ObjectList(objectNum, filePos);
            byte[] abuf;
            try
            {
                byte[] ubuf = Encoding.Unicode.GetBytes(str);
                Encoding enc = Encoding.GetEncoding(1252);
                abuf = Encoding.Convert(Encoding.Unicode, enc, ubuf);
                size = abuf.Length;
                SourceCodeCollection.OffsetArray.Add(objList);
            }
            catch (Exception e)
            {
                string str1 = string.Format("{0},In PdfObjects.GetBytes()", objectNum);
                Exception error = new Exception(e.Message + str1);
                throw error;
            }
            return abuf;
        }

    }
}
