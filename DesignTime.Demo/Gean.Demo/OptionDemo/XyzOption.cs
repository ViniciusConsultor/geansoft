﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Gean.Options;

namespace Gean.Demo
{
    class XyzOption : XmlOption
    {
        #region 单件实例

        private XyzOption() { }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static XyzOption ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton() { Instance = new XyzOption(); }
            internal static readonly XyzOption Instance = null;
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
