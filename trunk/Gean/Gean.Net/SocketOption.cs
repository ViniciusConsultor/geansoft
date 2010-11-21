﻿using System;
using System.Collections.Generic;
using System.Text;
using Gean.Options;
using System.Net;
using System.Xml;
using Gean.Net.Common;
using Gean.Net.Common.Interfaces;

namespace Gean.Net
{
    public class SocketOption : XmlOption
    {
        #region 单件实例

        private SocketOption() { }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static SocketOption ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton() { Instance = new SocketOption(); }
            internal static readonly SocketOption Instance = null;
        }

        #endregion

        #region 用户可更改的选项

        /// <summary>
        /// Gets or sets 服务器IP地址
        /// </summary>
        /// <value>The IP address.</value>
        public string IPAddress { get; set; }
        /// <summary>
        /// Gets or sets 服务器的端口
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        #endregion

        #region 程序员配置的选项

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
        /// <summary>
        /// Gets or sets 客户端类型.
        /// </summary>
        /// <value>The type of the client.</value>
        public int ClientType { get; private set; }
        /// <summary>
        /// Gets or sets 从消息中解析命令字的接口
        /// </summary>
        /// <value>The command parser.</value>
        public ICommandParser CommandParser { get; private set; }
        /// <summary>
        /// Gets or sets 一个协议数据组装器.
        /// </summary>
        /// <value>The combin.</value>
        public IProtocolCombin Combin { get; private set; }

        #endregion

        protected override void Load(System.Xml.XmlElement source)
        {
            XmlElement serverElement = (XmlElement)source.SelectSingleNode("server");
            System.Net.IPAddress tempIp;
            if (System.Net.IPAddress.TryParse(serverElement.GetAttribute("serverIp"), out tempIp))
            {
                this.IPAddress = serverElement.GetAttribute("serverIp");
            }
            this.Port = int.Parse(serverElement.GetAttribute("port"));

            XmlElement baseElement = (XmlElement)source.SelectSingleNode("base");
            this.HeartRange = int.Parse(baseElement.GetAttribute("heartRange"));
            this.IsAsync = bool.Parse(baseElement.GetAttribute("isAsync"));
            this.ClientType = int.Parse(baseElement.GetAttribute("clientType"));

            XmlElement verifyElement = (XmlElement)source.SelectSingleNode("verifyConn");
            this.VerifyConnCommandString = verifyElement.InnerText.Trim();

            this.CommandParser = OptionHelper.InterfaceBuilder<ICommandParser>(source.SelectSingleNode("commandParser")).Value; ;
            this.Combin = OptionHelper.InterfaceBuilder<IProtocolCombin>(source.SelectSingleNode("combin")).Value;
        }

        public override System.Xml.XmlElement GetChangedDatagram()
        {
            throw new NotImplementedException();
        }
    }
}
