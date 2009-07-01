using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    ///Store information about the document,Title, Author, Company, 
    /// </summary>
    public class InfoDictionary : PdfBase
    {
        private string info;
        public InfoDictionary()
        {
            info = null;
        }
        /// <summary>
        /// Fill the Info Dict
        /// </summary>
        /// <param name="title"></param>
        /// <param name="author"></param>
        public void SetInfo(string title, string author, string company)
        {
            info = string.Format("{0} 0 obj<</ModDate({1})/CreationDate({1})/Title({2})/Creator(CRM FactFind)" +
                "/Author({3})/Producer(CRM FactFind)/Company({4})>>\rendobj\r",
                this.objectNum, GetDateTime(), title, author, company);

        }
        /// <summary>
        /// Get the Document Information Dictionary
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public byte[] GetInfoDict(long filePos, out int size)
        {
            return GetUTF8Bytes(info, filePos, out size);
        }
        /// <summary>
        /// Get Date as Adobe needs ie similar to ISO/IEC 8824 format
        /// </summary>
        /// <returns></returns>
        private string GetDateTime()
        {
            DateTime universalDate = DateTime.UtcNow;
            DateTime localDate = DateTime.Now;
            string pdfDate = string.Format("D:{0:yyyyMMddhhmmss}", localDate);
            TimeSpan diff = localDate.Subtract(universalDate);
            int uHour = diff.Hours;
            int uMinute = diff.Minutes;
            char sign = '+';
            if (uHour < 0)
                sign = '-';
            uHour = Math.Abs(uHour);
            pdfDate += string.Format("{0}{1}'{2}'", sign, uHour.ToString().PadLeft(2, '0'), uMinute.ToString().PadLeft(2, '0'));
            return pdfDate;
        }

    }
}
