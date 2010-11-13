using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Gean.Options;

namespace Gean.Demo
{
    class SOMSOption : XmlOption
    {
        #region 单件实例

        private SOMSOption() { }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static SOMSOption ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton() { Instance = new SOMSOption(); }
            internal static readonly SOMSOption Instance = null;
        }

        #endregion

        public string Key { get; private set; }
        public string Value { get; private set; }

        public override XmlElement GetChangedDatagram()
        {
            throw new NotImplementedException();
        }

        protected override void Load(XmlElement source)
        {
            XmlElement element = (XmlElement)source.SelectSingleNode("domain");
            this.Key = element.GetAttribute("key");
            this.Value = element.GetAttribute("value");
        }

    }
}
