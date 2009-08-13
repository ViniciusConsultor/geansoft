﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Gean
{
    /// <summary>
    /// 定义一些基础的转换方法(对系统方法的一些扩展)
    /// Defines some basic conversion routines.
    /// </summary>
    public static class UtilityConvert
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

        #region 补足位数
        /// <summary>
        /// 指定字符串的固定长度，如果字符串小于固定长度，
        /// 则在字符串的前面补足零，可设置的固定长度最大为9位
        /// </summary>
        /// <param name="text">原始字符串</param>
        /// <param name="limitedLength">字符串的固定长度</param>
        public static string RepairZero(string text, int limitedLength)
        {
            //补足0的字符串
            StringBuilder sb = new StringBuilder();

            //补足0
            for (int i = 0; i < limitedLength - text.Length; i++)
            {
                sb.Append("0");
            }

            //连接text
            sb.Append(text);

            //返回补足0的字符串
            return sb.ToString();
        }
        #endregion

        #region 各进制数间转换
        /// <summary>
        /// 实现各进制数间的转换。如：ConvertBase("15", 10, 16)表示将10进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(string value, int from, int to)
        {
            try
            {
                int intValue = Convert.ToInt32(value, from);  //先转成10进制
                string result = Convert.ToString(intValue, to);  //再转成目标进制
                if (to == 2)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {
                return "0";
            }
        }
        #endregion

        #region byte[]的相关转换


        /// <summary>
        /// 将字节数组转化为数值
        /// </summary>
        /// <param name="arrByte"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int BytesToInt(byte[] arrByte, int offset)
        {
            return BitConverter.ToInt32(arrByte, offset);
        }

        /// <summary>
        /// 将数值转化为字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="reverse">是否需要把得到的字节数组反转，因为Windows操作系统中整形的高低位是反转转之后保存的。</param>
        /// <returns></returns>
        public static byte[] IntToBytes(int value, bool reverse)
        {
            byte[] ret = BitConverter.GetBytes(value);
            if (reverse)
                Array.Reverse(ret);
            return ret;
        }

        /// <summary>
        /// 将字节数组转化为16进制字符串
        /// </summary>
        /// <param name="arrByte"></param>
        /// <param name="reverse">是否需要把得到的字节数组反转，因为Windows操作系统中整形的高低位是反转转之后保存的。</param>
        /// <returns></returns>
        public static string BytesToHex(byte[] arrByte, bool reverse)
        {
            StringBuilder sb = new StringBuilder();
            if (reverse)
                Array.Reverse(arrByte);
            foreach (byte b in arrByte)
                sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        /// <summary>
        /// 将16进制字符串转化为字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(string value)
        {
            int len = value.Length / 2;
            byte[] ret = new byte[len];
            for (int i = 0; i < len; i++)
                ret[i] = (byte)(Convert.ToInt32(value.Substring(i * 2, 2), 16));
            return ret;
        }


        #endregion

        #region 使用指定字符集将string转换成byte[]
        /// <summary>
        /// 将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>        
        public static byte[] StringToBytes(string text)
        {
            return Encoding.Default.GetBytes(text);
        }

        /// <summary>
        /// 使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }

        #endregion

        #region 使用指定字符集将byte[]转换成string
        /// <summary>
        /// 将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>        
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// 使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
            {
                if (bytes[0] == 239 && bytes[1] == 187 && bytes[2] == 191)
                {
                    return encoding.GetString(bytes, 3, bytes.Length - 3);
                }
            }
            return encoding.GetString(bytes);
        }
        #endregion

        #region 将流转换成字符串
        /// <summary>
        /// 将流转换成字符串,同时关闭该流
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">字符编码</param>
        public static string StreamToString(Stream stream, Encoding encoding)
        {
            //获取的文本
            string streamText;

            //读取流
            try
            {
                using (StreamReader reader = new StreamReader(stream, encoding))
                {
                    streamText = reader.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                stream.Close();
            }

            //返回文本
            return streamText;
        }

        /// <summary>
        /// 将流转换成字符串,同时关闭该流
        /// </summary>
        /// <param name="stream">流</param>
        public static string StreamToString(Stream stream)
        {
            return StreamToString(stream, Encoding.Default);
        }
        #endregion

        #region 将byte[]转换成int
        /// <summary>
        /// 将byte[]转换成int
        /// </summary>
        /// <param name="data">需要转换成整数的byte数组</param>
        public static int BytesToInt32(byte[] data)
        {
            //如果传入的字节数组长度小于4,则返回0
            if (data.Length < 4)
            {
                return 0;
            }

            //定义要返回的整数
            int num = 0;

            //如果传入的字节数组长度大于4,需要进行处理
            if (data.Length >= 4)
            {
                //创建一个临时缓冲区
                byte[] tempBuffer = new byte[4];

                //将传入的字节数组的前4个字节复制到临时缓冲区
                Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

                //将临时缓冲区的值转换成整数，并赋给num
                num = BitConverter.ToInt32(tempBuffer, 0);
            }

            //返回整数
            return num;
        }
        #endregion

        #region 转换为日期
        /// <summary>
        /// 将数据转换为日期,如果数据无效则返回"1900-1-1"
        /// </summary>
        /// <param name="date">日期</param>
        public static DateTime ToDateTime(object date)
        {
            try
            {
                if (Checking.IsNullOrEmpty(date))
                {
                    return Convert.ToDateTime("1900-1-1");
                }
                else
                {
                    return Convert.ToDateTime(date);
                }
            }
            catch
            {
                return Convert.ToDateTime("1900-1-1");
            }
        }
        #endregion

        #region 转换为SmartDate
        /// <summary>
        /// 转换为SmartDate
        /// </summary>
        /// <param name="data">日期格式的数据</param>
        public static SmartDate ToSmartDate(object data)
        {
            //有效性验证
            if (Checking.IsNullOrEmpty(data))
            {
                return new SmartDate(true);
            }

            try
            {
                return new SmartDate(data.ToString());
            }
            catch
            {
                return new SmartDate(true);
            }

        }
        #endregion

        #region 将数据转换为GUID
        /// <summary>
        /// 将数据转换为GUID
        /// </summary>
        /// <param name="data"></param>
        public static Guid ToGuid(object data)
        {
            //有效性验证
            if (Checking.IsNullOrEmpty(data))
            {
                return Guid.Empty;
            }

            try
            {
                return new Guid(data.ToString());
            }
            catch
            {
                return Guid.Empty;
            }
        }
        #endregion

        #region 将数据转换为整型

        #region 重载1
        /// <summary>
        /// 将数据转换为整型
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        public static int ToInt32<T>(T data)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty<T>(data))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(data);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 重载2
        /// <summary>
        /// 将数据转换为整型
        /// </summary>
        /// <param name="data">要转换的数据</param>
        public static int ToInt32(object data)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty(data))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(data);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #endregion

        #region 将数据转换为字符串
        /// <summary>
        /// 将数据转换为字符串
        /// </summary>
        /// <param name="data">数据</param>
        public static string ToString(object data)
        {
            //有效性验证
            if (data == null)
            {
                return string.Empty;
            }

            return data.ToString();
        }
        #endregion

        #region 将数据转换为布尔型

        #region 重载1
        /// <summary>
        /// 将数据转换为布尔型
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        public static bool ToBoolean<T>(T data)
        {
            try
            {
                //如果为空则返回false
                if (Checking.IsNullOrEmpty<T>(data))
                {
                    return false;
                }
                else
                {
                    return Convert.ToBoolean(data);
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 重载2
        /// <summary>
        /// 将数据转换为布尔型
        /// </summary>
        /// <param name="data">要转换的数据</param>
        public static bool ToBoolean(object data)
        {
            try
            {
                //如果为空则返回false
                if (Checking.IsNullOrEmpty(data))
                {
                    return false;
                }
                else
                {
                    return Convert.ToBoolean(data);
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #endregion

        #region 将数据转换为单精度浮点型

        #region 重载1
        /// <summary>
        /// 将数据转换为单精度浮点型
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        public static float ToFloat<T>(T data)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty<T>(data))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToSingle(data);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 重载2
        /// <summary>
        /// 将数据转换为单精度浮点型
        /// </summary>
        /// <param name="data">要转换的数据</param>
        public static float ToFloat(object data)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty<object>(data))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToSingle(data);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #endregion

        #region 将数据转换为双精度浮点型

        #region 重载1
        /// <summary>
        /// 将数据转换为双精度浮点型
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        public static double ToDouble<T>(T data)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty<T>(data))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(data);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 重载2
        /// <summary>
        /// 将数据转换为双精度浮点型,并设置小数位
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        /// <param name="decimals">小数的位数</param>
        public static double ToDouble<T>(T data, int decimals)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty<T>(data))
                {
                    return 0;
                }
                else
                {
                    double temp = Convert.ToDouble(data);
                    return Math.Round(temp, decimals);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 重载3
        /// <summary>
        /// 将数据转换为双精度浮点型
        /// </summary>
        /// <param name="data">要转换的数据</param>
        public static double ToDouble(object data)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty(data))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(data);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 重载4
        /// <summary>
        /// 将数据转换为双精度浮点型,并设置小数位
        /// </summary>
        /// <param name="data">要转换的数据</param>
        /// <param name="decimals">小数的位数</param>
        public static double ToDouble(object data, int decimals)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty<object>(data))
                {
                    return 0;
                }
                else
                {
                    double temp = Convert.ToDouble(data);
                    return Math.Round(temp, decimals);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #endregion

        #region 将数据转换为Decimal类型

        #region 重载1
        /// <summary>
        /// 将数据转换为Decimal类型
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        public static decimal ToDecimal<T>(T data)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty<T>(data))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(data);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 重载2
        /// <summary>
        /// 将数据转换为Decimal类型
        /// </summary>
        /// <typeparam name="T">数据的类型</typeparam>
        /// <param name="data">要转换的数据</param>
        /// <param name="decimals">小数的位数</param>
        public static decimal ToDecimal<T>(T data, int decimals)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty<T>(data))
                {
                    return 0;
                }
                else
                {
                    decimal temp = Convert.ToDecimal(data);
                    return Math.Round(temp, decimals);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 重载3
        /// <summary>
        /// 将数据转换为Decimal类型
        /// </summary>
        /// <param name="data">要转换的数据</param>
        public static decimal ToDecimal(object data)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty(data))
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(data);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 重载4
        /// <summary>
        /// 将数据转换为Decimal类型
        /// </summary>
        /// <param name="data">要转换的数据</param>
        /// <param name="decimals">小数的位数</param>
        public static decimal ToDecimal(object data, int decimals)
        {
            try
            {
                //如果为空则返回0
                if (Checking.IsNullOrEmpty<object>(data))
                {
                    return 0;
                }
                else
                {
                    decimal temp = Convert.ToDecimal(data);
                    return Math.Round(temp, decimals);
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #endregion

        #region 将数据转换为指定类型

        #region 重载一
        /// <summary>
        /// 将数据转换为指定类型
        /// </summary>
        /// <param name="data">转换的数据</param>
        /// <param name="targetType">转换的目标类型</param>
        public static object ConvertTo(object data, Type targetType)
        {
            //如果数据为空，则返回
            if (Checking.IsNullOrEmpty(data))
            {
                return null;
            }

            try
            {
                //如果数据实现了IConvertible接口，则转换类型
                if (data is IConvertible)
                {
                    return Convert.ChangeType(data, targetType);
                }
                else
                {
                    return data;
                }
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 重载二
        /// <summary>
        /// 将数据转换为指定类型
        /// </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        /// <param name="data">转换的数据</param>
        public static T ConvertTo<T>(object data)
        {
            //如果数据为空，则返回
            if (Checking.IsNullOrEmpty(data))
            {
                return default(T);
            }

            try
            {
                //如果数据是T类型，则直接转换
                if (data is T)
                {
                    return (T)data;
                }

                //如果目标类型是枚举
                if (typeof(T).BaseType == typeof(Enum))
                {
                    return UtilityEnums.GetInstance<T>(data);
                }

                //如果数据实现了IConvertible接口，则转换类型
                if (data is IConvertible)
                {
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
            catch
            {
                return default(T);
            }
        }
        #endregion

        #endregion

        #region Image和base64之间的转换

        public static string ImageToBase64(string filePath)
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

        public static string ImageToBase64(Image image)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, ImageFormat.Jpeg);
            byte[] bytes = memory.ToArray();
            return System.Convert.ToBase64String(bytes);
        }

        public static Image Base64ToImage(string base64String)
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

        public static void Base64ToImage(string base64String, string filePath)
        {
            byte[] bytes = System.Convert.FromBase64String(base64String);
            System.IO.File.WriteAllBytes(filePath, bytes);
        }

        #endregion

        #region object和base64之间的转换

        public static string FileToBase64(string filePath)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            return System.Convert.ToBase64String(bytes);
        }

        public static Icon Base64ToIcon(string base64String)
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

        public static Cursor Base64ToCursor(string base64String)
        {
            byte[] bytes = System.Convert.FromBase64String(base64String);
            MemoryStream memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return new Cursor(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        public static byte[] Base64ToByteArray(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        #endregion

    }
}