using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    /// This class represents individual pages within the pdf. 
    /// The contents of the page belong to this class
    /// </summary>
    public class PageDictionary : PdfBase
    {
        private string page;
        private string pageSize;
        private string fontRef;
        private string resourceDict, contents;
        public PageDictionary()
        {
            resourceDict = null;
            contents = null;
            pageSize = null;
            fontRef = null;
        }
        /// <summary>
        /// Create The Pdf page
        /// </summary>
        /// <param name="refParent"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void CreatePage(uint refParent, PdfPageSize pSize)
        {
            Exception error = new Exception("In PageDict.CreatePage(),PageTree.ObjectNum Invalid");
            if (refParent < 1 || refParent > PdfBase.inUseObjectNum)
                throw error;

            pageSize = string.Format("[0 0 {0} {1}]", pSize.Width, pSize.Height);
            page = string.Format("{0} 0 obj<</Type /Page/Parent {1} 0 R/Rotate 0/MediaBox {2}/CropBox {2}",
                this.objectNum, refParent, pageSize);
        }
        /// <summary>
        /// Add Resource to the pdf page
        /// </summary>
        /// <param name="font"></param>
        public void AddResource(FontDictionary font, uint contentRef)
        {
            fontRef += string.Format("/{0} {1} 0 R", font.font, font.objectNum);
            if (contentRef > 0)
            {
                contents = string.Format("/Contents {0} 0 R", contentRef);
            }
        }
        /// <summary>
        /// Get the Page Dictionary to be written to the file
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public byte[] GetPageDict(long filePos, out int size)
        {
            resourceDict = string.Format("/Resources<</Font<<{0}>>/ProcSet[/PDF/Text]>>", fontRef);
            page += resourceDict + contents + ">>\rendobj\r";
            return this.GetUTF8Bytes(page, filePos, out size);
        }
    }
}
