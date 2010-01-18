using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Net;

namespace Gean.Data.Demo
{
    public class IDGenerator
    {
        private static Int32 _counter = 0;
        private static string[] _timeFlag;

        static IDGenerator()
        {
            _timeFlag = new string[527040];
            for (int i = 0; i < _timeFlag.Length; i++)
            {
                _timeFlag[i] = "KKKK";
            }
        }

        private static void FillTimeFLag()
        {
            //string srcAllChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //int len;
            //if (len > srcAllChar.Length) { return false; }
            //try
            //{
            //    Char[] chr = srcAllChar.ToCharArray();

            //    File.WriteAllText(@"e:\My Desktop\file0.txt", "", Encoding.Default);

            //    for (int i = 0; i < len; i++) // 处理 i个字符长度的
            //    {
            //        StreamReader sr = new StreamReader(@"e:\My Desktop\file" + i + ".txt", Encoding.Default);
            //        StreamWriter sw = new StreamWriter(@"e:\My Desktop\file" + (i + 1) + ".txt", false, Encoding.Default);
            //        string str = "";
            //        while (true)
            //        {
            //            str = sr.ReadLine();
            //            if (str == null && i != 0)
            //            {
            //                break;
            //            }
            //            if (i == 0)
            //            {
            //                str = "";
            //            }
            //            for (int j = 0; j < chr.Length; j++)
            //            {
            //                if (str.IndexOf(chr[j]) == -1)   //保证不重复
            //                {
            //                    sw.WriteLine(str + chr[j].ToString());
            //                }
            //            }
            //            if (i == 0) { break; }
            //        }
            //        sr.Close();
            //        sw.Close();
            //    }
            //    File.Delete(@"e:\My Desktop\file0.txt");
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //return true;
        }


        protected virtual string GetSeperator()
        {
            return "";
        }
        protected virtual string GetUserFlag()
        {
            return "o";
        }
        protected virtual string GetSequenceName()
        {
            return "";
        }
        protected virtual string GetDateTimeFlag()
        {
            int time = DateTime.Now.DayOfYear * DateTime.Now.Hour * DateTime.Now.Minute;
            return _timeFlag[time];
        }
        protected virtual string GetSecond()
        {
            return Convert.ToString(DateTime.Now.Second);
        }
        protected Int32 GetCount()
        {
            int n = 2;//希望仅出现4位计数标志
            if (_counter < GetMaxCount(n) - 1)
            {
                _counter++;
            }
            else
            {
                _counter = 1;
            }
            return _counter;
        }

        public string Generate()
        {
            return (new StringBuilder())
                .Append(GetUserFlag())
                .Append(GetSeperator())
                .Append(GetSequenceName())
                .Append(GetSeperator())
                .Append(GetDateTimeFlag())
                .Append(GetSeperator())
                .Append(GetSecond())
                .Append(GetSeperator())
                .Append(GetCount())
                .ToString();
        }

        private int GetMaxCount(int n)
        {
            int max = 1;
            for (int i = 0; i < n; i++)
            {
                max *= 10;
            }
            return max;
        }
    }


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
