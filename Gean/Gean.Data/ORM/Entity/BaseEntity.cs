using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;

namespace Gean.Data.Entity
{
    [Serializable]
    public class BaseEntity
    {
        [System.Xml.Serialization.XmlElement(ElementName = "PrimaryKey")]
        public string PrimaryKey { get; private set; }

        public override string ToString()
        {
            return UtilityString.ToString(this);
        }
    }
}
