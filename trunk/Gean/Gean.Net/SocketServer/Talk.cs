using System;
using System.Collections.Generic;
using System.Text;
using Gean.Net.CSUST.Net;

namespace Gean.Net
{
    /// <summary>
    /// 对一次Socket连接的封装，我们理解这是一次客户端与服务器的对话或交谈(Talk)。
    /// </summary>
    public class Talk : TSessionBase
    {
        /// <summary>
        /// override.重写错误处理方法, 返回消息给客户端
        /// </summary>
        protected override void OnDatagramDelimiterError()
        {
            base.OnDatagramDelimiterError();
            base.SendDatagram("datagram delimiter error");
        }

        /// <summary>
        /// override.重写错误处理方法, 返回消息给客户端
        /// </summary>
        protected override void OnDatagramOversizeError()
        {
            base.OnDatagramOversizeError();

            base.SendDatagram("datagram over size");
        }

        /// <summary>
        /// override.重写 AnalyzeDatagram 方法, 调用数据存储方法
        /// </summary>
        protected override void AnalyzeDatagram(byte[] datagramBytes)
        {

            string datagramText = Encoding.ASCII.GetString(datagramBytes);

            string clientName = string.Empty;
            int datagramTextLength = 0;

            int n = datagramText.IndexOf(',');  // 格式为 <C12345,0000000000,****>
            if (n >= 1)
            {
                clientName = datagramText.Substring(1, n - 1);
                try
                {
                    datagramTextLength = int.Parse(datagramText.Substring(n + 1, 10));
                }
                catch
                {
                    datagramTextLength = 0;
                }
            }

            base.OnDatagramAccepted();  // 模拟接收到一个完整的数据包

            if (!string.IsNullOrEmpty(clientName) && datagramTextLength > 0)
            {

                if (datagramTextLength == datagramBytes.Length)
                {
                    base.SendDatagram("<OK: " + clientName + ", datagram length = " + datagramTextLength.ToString() + ">");

                    this.Store(datagramBytes);
                    base.OnDatagramHandled();  // 模拟已经处理（存储）了数据包
                }
                else
                {
                    base.SendDatagram("<ERROR: " + clientName + ", error length, datagram length = " + datagramTextLength.ToString() + ">");
                    base.OnDatagramError();  // 错误包
                }
            }
            else if (string.IsNullOrEmpty(clientName))
            {
                base.SendDatagram("client: no name, datagram length = " + datagramTextLength.ToString());
                base.OnDatagramError();
            }
            else if (datagramTextLength == 0)
            {
                base.SendDatagram("client: " + clientName + ", datagram length = " + datagramTextLength.ToString());
                base.OnDatagramError();  // 错误包
            }
        }

        /// <summary>
        /// 自定义的数据存储方法
        /// </summary>
        private void Store(byte[] datagramBytes)
        {
            if (this.DatabaseObj == null)
            {
                return;
            }

            DataStore db = this.DatabaseObj as DataStore;
            db.Store(datagramBytes, this);
        }
    }
}
