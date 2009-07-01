using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    /// This class contains general Utility for the creation of pdf
    /// Creates the Header
    /// Creates XrefTable
    /// Creates the Trailer
    /// </summary>
    public class PdfDocument
    {
        private uint numTableEntries;
        private string table;
        private string infoDict;
        public PdfDocument()
        {
            numTableEntries = 0;
            table = null;
            infoDict = null;
        }
        /// <summary>
        /// Creates the xref table using the byte offsets in the array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public byte[] CreateXrefTable(long fileOffset, out int size)
        {
            //Store the Offset of the Xref table for startxRef
            try
            {
                ObjectList objList = new ObjectList(0, fileOffset);
                XrefEnteries.offsetArray.Add(objList);
                XrefEnteries.offsetArray.Sort();
                numTableEntries = (uint)XrefEnteries.offsetArray.Count;
                table = string.Format("xref\r\n{0} {1}\r\n0000000000 65535 f\r\n", 0, numTableEntries);
                for (int entries = 1; entries < numTableEntries; entries++)
                {
                    ObjectList obj = (ObjectList)XrefEnteries.offsetArray[entries];
                    table += obj.offset.ToString().PadLeft(10, '0');
                    table += " 00000 n\r\n";
                }
            }
            catch (Exception e)
            {
                Exception error = new Exception(e.Message + " In Utility.CreateXrefTable()");
                throw error;
            }
            return GetUTF8Bytes(table, out size);
        }
        /// <summary>
        /// Returns the Header
        /// </summary>
        /// <param name="version"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public byte[] GetHeader(string version, out int size)
        {
            string header = string.Format("%PDF-{0}\r%{1}\r\n", version, "\x82\x82");
            return GetUTF8Bytes(header, out size);
        }
        /// <summary>
        /// Creates the trailer and return the bytes array
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public byte[] GetTrailer(uint refRoot, uint refInfo, out int size)
        {
            string trailer = null;
            try
            {
                if (refInfo > 0)
                {
                    infoDict = string.Format("/Info {0} 0 R", refInfo);
                }
                //The sorted array will be already sorted to contain the file offset at the zeroth position
                ObjectList objList = (ObjectList)XrefEnteries.offsetArray[0];
                trailer = string.Format("trailer\n<</Size {0}/Root {1} 0 R {2}/ID[<5181383ede94727bcb32ac27ded71c68>" +
                    "<5181383ede94727bcb32ac27ded71c68>]>>\r\nstartxref\r\n{3}\r\n%%EOF\r\n"
                    , numTableEntries, refRoot, infoDict, objList.offset);

                XrefEnteries.offsetArray = null;
                PdfBase.inUseObjectNum = 0;
            }
            catch (Exception e)
            {
                Exception error = new Exception(e.Message + " In Utility.GetTrailer()");
                throw error;
            }

            return GetUTF8Bytes(trailer, out size);
        }
        /// <summary>
        /// Converts the string to byte array in utf 8 encoding
        /// </summary>
        /// <param name="str"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private byte[] GetUTF8Bytes(string str, out int size)
        {
            try
            {
                byte[] ubuf = Encoding.Unicode.GetBytes(str);
                Encoding enc = Encoding.GetEncoding(1252);
                byte[] abuf = Encoding.Convert(Encoding.Unicode, enc, ubuf);
                size = abuf.Length;
                return abuf;
            }
            catch (Exception e)
            {
                Exception error = new Exception(e.Message + " In Utility.GetUTF8Bytes()");
                throw error;
            }
        }
    }
}
