using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Net;
using Gean.Math;

namespace Gean.Data.Demo
{

    /// <summary>
    /// 提取自Hibernate。经测试：生成生成500万个ID需要时间（毫秒）：60331，每秒生成：82000
    /// </summary>
    public class UUIDGenerator
    {
        private static readonly int _IP;
        static UUIDGenerator()
        {
            int ipadd;
            try
            {
                ipadd = BytesHelper.toInt(Dns.GetHostAddresses(Dns.GetHostName())[0].GetAddressBytes());
            }
            catch
            {
                ipadd = 0;
            }
            _IP = ipadd;
        }

        private static short _counter = (short)0;

        private static readonly int _JVM = (int)(DateTime.Now.Millisecond >> 8);

        public UUIDGenerator()
        {
        }

        /// <summary>
        /// Unique across JVMs on this machine (unless they load this class in the same quater second - very unlikely)
        /// </summary>
        /// <returns></returns>
        protected int GetJVM()
        {
            return _JVM;
        }

        /// <summary>
        /// Unique in a millisecond for this JVM instance (unless there are &gt; Short.MAX_VALUE instances created in a millisecond)
        /// </summary>
        /// <returns></returns>
        protected short GetCount()
        {
            if (_counter < 0)
                _counter = 0;
            return _counter++;
        }

        /// <summary>
        /// Unique in a local network
        /// </summary>
        /// <returns></returns>
        protected int GetIP()
        {
            return _IP;
        }

        /// <summary>
        /// Unique down to millisecond
        /// </summary>
        /// <returns></returns>
        protected short GetHiTime()
        {
            return (short)(DateTime.Now.Millisecond >> 32);
        }

        protected int GetLoTime()
        {
            return DateTime.Now.Millisecond;
        }

        private static readonly String SEPERATOR = " | ";

        protected String Format(int intval)
        {
            String formatted = UtilityConvert.ConvertBase(10, Convert.ToString(intval), 16);
            StringBuilder buf = new StringBuilder("00000000");
            buf.Append(formatted).Remove(0, buf.Length - 8);//.Replace(8 - formatted.Length, 8, formatted);
            return buf.ToString();
        }

        protected String Format(short shortval)
        {
            String formatted = UtilityConvert.ConvertBase(10, Convert.ToString(shortval), 16);
            StringBuilder sb = new StringBuilder("0000");
            sb.Append(formatted).Remove(0, sb.Length - 4);
            return sb.ToString();
        }

        public String Generate()
        {
            return new StringBuilder(36)
                .Append(Format(GetIP()))
                .Append(UUIDGenerator.SEPERATOR)
                .Append(Format(GetJVM()))
                .Append(UUIDGenerator.SEPERATOR)
                .Append(Format(GetHiTime()))
                .Append(UUIDGenerator.SEPERATOR)
                .Append(Format(GetLoTime()))
                .Append(UUIDGenerator.SEPERATOR)
                .Append(Format(GetCount())).ToString();
        }

        //public static void main(String[] args) throws Exception {
        //    for(int i=0;i<100;i++)
        //    System.out.println(new UUIDGenerator().generate());
        //}

    }
    public class BytesHelper
    {
        private BytesHelper() { }

        public static int toInt(byte[] bytes)
        {
            int result = 0;
            for (int i = 0; i < 4; i++)
            {
                result = (result << 8) - Byte.MinValue + (int)bytes[i];
            }
            return result;
        }

        public static short toShort(byte[] bytes)
        {
            return (short)(((-(short)Byte.MinValue + (short)bytes[0]) << 8) - (short)Byte.MinValue + (short)bytes[1]);
        }

        public static byte[] toBytes(int value)
        {
            byte[] result = new byte[4];
            for (int i = 3; i >= 0; i--)
            {
                result[i] = (byte)((0xFFL & value) + Byte.MinValue);
                value >>= 8;
            }
            return result;
        }

        public static byte[] toBytes(short value)
        {
            byte[] result = new byte[2];
            for (int i = 1; i >= 0; i--)
            {
                result[i] = (byte)((0xFFL & value) + Byte.MinValue);
                value >>= 8;
            }
            return result;
        }
    }
}
