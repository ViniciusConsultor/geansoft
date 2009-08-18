using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Gean.Utility
{
    /// <summary>
    /// 一些简单的基于网络的小型扩展方法
    /// </summary>
    public sealed class UtilityNet
    {
        [DllImport("netapi32.dll", EntryPoint = "NetMessageBufferSend", CharSet = CharSet.Unicode)]
        private static extern int NetMessageBufferSend(
          string servername,
          string msgname,
          string fromname,
          [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 4)] byte[] buf,
          [MarshalAs(UnmanagedType.U4)] int buflen);

        /// <summary>
        /// 系统信使服务:发送消息
        /// </summary>
        /// <param name="fromName">发送人</param>
        /// <param name="toName">接收人(机器名或者IP)</param>
        /// <param name="message">消息内容</param>
        /// <returns></returns>
        public static bool MessageBufferSend(string fromName, string toName, string message)
        {
            byte[] buf = System.Text.Encoding.Unicode.GetBytes(message);
            return NetMessageBufferSend(null, toName, fromName, buf, buf.Length) == 0;
        }
    }
}
