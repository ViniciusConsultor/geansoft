using System;
using System.Collections.Generic;
using System.Text;
using Gean.Options;
using System.Net;
using System.Xml;

namespace Gean.Net.KeepSocket
{
    public class KeepSocketOption : XmlOption
    {
        #region 单件实例

        private KeepSocketOption() { }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static KeepSocketOption ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton() { Instance = new KeepSocketOption(); }
            internal static readonly KeepSocketOption Instance = null;
        }

        #endregion

        public string IPAddress { get; private set; }
        public int Port { get; private set; }
        /// <summary>
        /// Gets or sets 心跳间隔.
        /// </summary>
        /// <value>The heart range.</value>
        public int HeartRange { get; private set; }
        /// <summary>
        /// Gets or sets 长连接校验命令串.
        /// </summary>
        /// <value>The verify conn command string.</value>
        public string VerifyConnCommandString { get; private set; }
        /// <summary>
        /// Gets or sets 是否采用异步传输.
        /// </summary>
        /// <value><c>true</c> 使用异步传输; otherwise, <c>false</c>.</value>
        public bool IsAsync { get; private set; }

        protected override void Load(System.Xml.XmlElement source)
        {
            XmlElement verifyElement = (XmlElement)source.SelectSingleNode("verifyConn");
            this.VerifyConnCommandString = verifyElement.InnerText.Trim();

            XmlElement baseElement = (XmlElement)source.SelectSingleNode("base");
            System.Net.IPAddress tempIp;
            if (System.Net.IPAddress.TryParse(baseElement.GetAttribute("serverIp"), out tempIp))
            {
                this.IPAddress = baseElement.GetAttribute("serverIp");
            }
            this.Port = int.Parse(baseElement.GetAttribute("port"));
            this.HeartRange = int.Parse(baseElement.GetAttribute("heartRange"));
            this.IsAsync = bool.Parse(baseElement.GetAttribute("isAsync"));
        }

        public override System.Xml.XmlElement GetChangedDatagram()
        {
            throw new NotImplementedException();
        }
    }
}
