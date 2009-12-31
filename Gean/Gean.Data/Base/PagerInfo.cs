using System;
using System.Xml.Serialization;

namespace Gean.Data
{
    [Serializable]
    public class PagerInfo
    {
        [XmlElement(ElementName = "CurrenetPageIndex")]
        public int CurrenetPageIndex { get; set; }

        [XmlElement(ElementName = "PageSize")]
        public int PageSize { get; set; }

        [XmlElement(ElementName = "RecordCount")]
        public int RecordCount { get; set; }
    }
}
