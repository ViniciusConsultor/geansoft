using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Pdf
{
    /// <summary>
    /// Models the Catalog dictionary within a pdf file. This is the first created object. 
    /// It contains referencesto all other objects within the List of Pdf Objects.
    /// </summary>
    public class CatalogDictionary : PdfBase
    {
        private string catalog;
        public CatalogDictionary()
        {

        }
        /// <summary>
        ///Returns the Catalog Dictionary 
        /// </summary>
        /// <param name="refPageTree"></param>
        /// <returns></returns>
        public byte[] GetCatalogDict(uint refPageTree, long filePos, out int size)
        {
            Exception error = new Exception(" In CatalogDict.GetCatalogDict(), PageTree.objectNum Invalid");
            if (refPageTree < 1)
            {
                throw error;
            }
            catalog = string.Format("{0} 0 obj<</Type /Catalog/Lang(EN-US)/Pages {1} 0 R>>\rendobj\r",
                this.objectNum, refPageTree);
            return this.GetUTF8Bytes(catalog, filePos, out size);
        }

    }
}
