using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;

namespace Gean
{
    static public class UtilityConvert
    {
        /// <summary>
        /// Converts the value from string.
        /// </summary>
        public static T FromString<T>(string v, T defaultValue)
        {
            if (string.IsNullOrEmpty(v))
                return defaultValue;
            if (typeof(T) == typeof(string))
                return (T)(object)v;
            try
            {
                TypeConverter c = TypeDescriptor.GetConverter(typeof(T));
                return (T)c.ConvertFromInvariantString(v);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Converts the value to string.
        /// </summary>
        public static string ToString<T>(T val)
        {
            if (typeof(T) == typeof(string))
            {
                string s = (string)(object)val;
                return string.IsNullOrEmpty(s) ? null : s;
            }
            try
            {
                TypeConverter c = TypeDescriptor.GetConverter(typeof(T));
                string s = c.ConvertToInvariantString(val);
                return string.IsNullOrEmpty(s) ? null : s;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //1、   将字节数组转化为数值
        public static int ConvertBytesToInt(byte[] arrByte, int offset)
        {
            return BitConverter.ToInt32(arrByte, offset);
        }

        //2、   将数值转化为字节数组
        //第二个参数设置是不是需要把得到的字节数组反转，因为Windows操作系统中整形的高低位是反转转之后保存的。
        public static byte[] ConvertIntToBytes(int value, bool reverse)
        {
            byte[] ret = BitConverter.GetBytes(value);
            if (reverse)
                Array.Reverse(ret);
            return ret;
        }

        //3、   将字节数组转化为16进制字符串
        //第二个参数的含义同上。
        public static string ConvertBytesToHex(byte[] arrByte, bool reverse)
        {
            StringBuilder sb = new StringBuilder();
            if (reverse)
                Array.Reverse(arrByte);
            foreach (byte b in arrByte)
                sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        //4、   将16进制字符串转化为字节数组
        public static byte[] ConvertHexToBytes(string value)
        {
            int len = value.Length / 2;
            byte[] ret = new byte[len];
            for (int i = 0; i < len; i++)
                ret[i] = (byte)(Convert.ToInt32(value.Substring(i * 2, 2), 16));
            return ret;
        }

        /// <summary>
        /// 字节数组转换成字符串
        /// </summary>
        public static string BytesToString(byte[] data, Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
            {
                if (data[0] == 239 && data[1] == 187 && data[2] == 191)
                {
                    return encoding.GetString(data, 3, data.Length - 3);
                }
            }
            return encoding.GetString(data);
        }

        static public DateTime StringToDateTime(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return DateTime.Now;
            }
            return DateTime.Parse(str);
        }

        static public DateTime StringToDateTime(string str, string format)
        {
            if (string.IsNullOrEmpty(str))
            {
                return DateTime.Now;
            }
            return DateTime.ParseExact(str, format, null);
        }

        static public Decimal StringToDecimal(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 1;
            }
            return Decimal.Parse(str);
        }

        static public bool StringToBool(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return bool.Parse(str);
        }

        static public int StringToInt(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return int.Parse(str);
        }

        static public long StringToLong(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return long.Parse(str);
        }

        static public float StringToFloat(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return float.Parse(str);
        }

        /// <summary>
        /// 将字符串数组转换按某种符号分开的字符串
        /// </summary>
        static public string StringArrayToString(string[] stringArray, char c)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string str in stringArray)
            {
                sb.Append(str).Append(c);
            }
            return sb.ToString().TrimEnd(c);
        }

        #region Image和base64之间的转换

        static public string ImageToBase64(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();
            ///jpg格式，则直接读内存。否则先读成Image，再转成jpg格式
            if (ext != ".jpg" && ext != ".jpeg")
            {
                Image image = Image.FromFile(filePath);
                return ImageToBase64(image);
            }
            else
            {
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                return System.Convert.ToBase64String(bytes);
            }
        }

        static public string ImageToBase64(Image image)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, ImageFormat.Jpeg);
            byte[] bytes = memory.ToArray();
            return System.Convert.ToBase64String(bytes);
        }

        static public Image Base64ToImage(string base64String)
        {
            byte[] bytes = System.Convert.FromBase64String(base64String);
            MemoryStream memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return Image.FromStream(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        static public void Base64ToImage(string base64String, string filePath)
        {
            byte[] bytes = System.Convert.FromBase64String(base64String);
            System.IO.File.WriteAllBytes(filePath, bytes);
        }

        #endregion

        #region object和base64之间的转换

        static public string FileToBase64(string filePath)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            return System.Convert.ToBase64String(bytes);
        }

        static public Icon Base64ToIcon(string base64String)
        {
            byte[] bytes = System.Convert.FromBase64String(base64String);
            MemoryStream memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return new Icon(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        static public System.Windows.Forms.Cursor Base64ToCursor(string base64String)
        {
            byte[] bytes = System.Convert.FromBase64String(base64String);
            MemoryStream memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return new System.Windows.Forms.Cursor(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        static public byte[] Base64ToByteArray(string base64String)
        {
            return System.Convert.FromBase64String(base64String);
        }

        #endregion

    }
}