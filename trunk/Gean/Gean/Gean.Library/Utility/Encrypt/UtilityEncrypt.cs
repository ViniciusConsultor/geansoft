using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Gean.Encrypt
{
    /// <summary>
    /// 针对加解密方法的扩展
    /// 2009-12-17 17:56:04
    /// </summary>
    public class UtilityEncrypt
    {
        /// <summary>
        /// 本框架设定的缺省密钥字符串，只读状态。
        /// </summary>
        public static readonly string DefaultKey = "98$Mp6?W";
        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        private static MD5 _MD5;

        static UtilityEncrypt()
        {
            _MD5 = MD5.Create();
        }


        /// <summary>
        /// 获得一个字符串的MD5值
        /// </summary>
        /// <param name="input">一个字符串</param>
        /// <returns></returns>
        public static string GetMd5String(string input)
        {
            byte[] data = _MD5.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 得到一个指定的字符串的MD5值后，与提供的一个指定的MD5值进行比较
        /// </summary>
        /// <param name="input">指定的字符串</param>
        /// <param name="hash">需进行比较的一个指定MD5值</param>
        /// <returns></returns>
        public static bool VerifyMd5String(string input, string hash)
        {
            string hashOfInput = GetMd5String(input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取用SHA256对一个指定的原始字符串进行加密后的字符串
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>SHA256结果</returns>
        public static string GetSHA256String(string str)
        {
            byte[] SHA256Data = Encoding.UTF8.GetBytes(str);
            SHA512Managed Sha256 = new SHA512Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
        }

        /// <summary>
        /// 获取用DES方法加密 [一个指定的待加密的字符串] 后的字符串
        /// </summary>
        /// <param name="encryptString">一个指定的待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        public static string EncodeDESString(string encryptString, string encryptKey)
        {
            encryptKey = UtilityString.GetSubString(encryptKey, 0, 8, "");
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            DESCryptoServiceProvider dCSP = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string DecodeDESString(string decryptString, string decryptKey)
        {
            try
            {
                decryptKey = UtilityString.GetSubString(decryptKey, 0, 8, "");
                decryptKey = decryptKey.PadRight(8, ' ');
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider DCSP = new DESCryptoServiceProvider();

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
    }

    /// <summary>
    /// 对传入的字符串进行Hash运算，返回通过Hash算法加密过的字串。
    /// 方法：此静态类提供MD5，SHA1，SHA256，SHA512等四种算法，加密字串的长度依次增大。
    /// </summary>
    public class HashEncrypt
    {
        /// <summary>
        /// 本框架设定的缺省密钥字符串，只读状态。
        /// </summary>
        private static readonly string _defaultKey = "98$Mp6?W";

        /// <summary>
        /// 计算输入数据的 MD5 哈希值
        /// </summary>
        /// <param name="strIN">输入的数据.</param>
        /// <returns></returns>
        public static string MD5Encrypt(string strIN)
        {
            byte[] tmpByte;
            MD5 md5 = new MD5CryptoServiceProvider();
            tmpByte = md5.ComputeHash(StringToBytes(strIN));
            md5.Clear();

            return BytesToString(tmpByte);
        }

        /// <summary>
        /// 计算输入数据的 SHA1 哈希值
        /// </summary>
        /// <param name="strIN">输入的数据.</param>
        /// <returns></returns>
        public static string SHA1Encrypt(string strIN)
        {
            byte[] tmpByte;
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            tmpByte = sha1.ComputeHash(StringToBytes(strIN));
            sha1.Clear();

            return BytesToString(tmpByte);
        }

        /// <summary>
        /// 计算输入数据的 SHA256 哈希值
        /// </summary>
        /// <param name="strIN">输入的数据.</param>
        /// <returns></returns>
        public static string SHA256Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();

            tmpByte = sha256.ComputeHash(StringToBytes(strIN));
            sha256.Clear();

            return BytesToString(tmpByte);

        }

        /// <summary>
        /// 计算输入数据的 SHA512 哈希值
        /// </summary>
        /// <param name="strIN">输入的数据.</param>
        /// <returns></returns>
        public static string SHA512Encrypt(string strIN)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA512 sha512 = new SHA512Managed();

            tmpByte = sha512.ComputeHash(StringToBytes(strIN));
            sha512.Clear();

            return BytesToString(tmpByte);
        }

        /// <summary>
        /// 使用DES加密
        /// </summary>
        /// <param name="originalValue">待加密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(string encryptedValue, string key)
        {
            return DESEncrypt(encryptedValue, key, key);
        }

        /// <summary>
        /// 使用DES加密
        /// </summary>
        /// <param name="originalValue">待加密的字符串</param>
        /// <param name="key">密钥(注意：长度8)</param>
        /// <param name="vector">对称算法的初始化向量(注意：长度8)</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(string encryptedValue, string key, string vector)
        {
            key += _defaultKey;
            vector += _defaultKey;
            key = key.Substring(0, 8);
            vector = vector.Substring(0, 8);

            SymmetricAlgorithm sa = new DESCryptoServiceProvider();
            sa.Key = StringToBytes(key);
            sa.IV = StringToBytes(vector);

            ICryptoTransform ct = sa.CreateEncryptor();//创建对称加密器对象
            byte[] bytes = StringToBytes(encryptedValue);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return BytesToString(ms.ToArray());
        }

        /// <summary>
        /// 使用DES解密
        /// </summary>
        /// <param name="decryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(注意：长度8)</param>
        /// <param name="vector">对称算法的初始化向量(注意：长度8)</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(string decryptedValue, string key, string vector)
        {
            key += _defaultKey;
            vector += _defaultKey;
            key = key.Substring(0, 8);
            vector = vector.Substring(0, 8);

            SymmetricAlgorithm sa = new DESCryptoServiceProvider();
            sa.Key = StringToBytes(key);
            sa.IV = StringToBytes(vector);

            ICryptoTransform ct = sa.CreateDecryptor();//创建对称解密器对象
            byte[] bytes = StringToBytes(decryptedValue);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(bytes, 0, bytes.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return BytesToString(ms.ToArray());
        }

        /// <summary>
        /// 使用DES解密
        /// </summary>
        /// <param name="decryptedValue">待解密的字符串</param>
        /// <param name="key">密钥(最大长度8)</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(string decryptedValue, string key)
        {
            return DESDecrypt(decryptedValue, key, key);
        }

        /// <summary>
        /// 将字节型数组转换成数字
        /// </summary>
        /// <param name="byteValue">The byte value.</param>
        /// <returns></returns>
        private static string BytesToString(byte[] byteValue)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < byteValue.Length; i++)
            {
                sb.Append(byteValue[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将Key字符串转换为字节型数组
        /// </summary>
        /// <param name="strKey">The STR key.</param>
        /// <returns></returns>
        private static byte[] StringToBytes(string strKey)
        {
            UTF8Encoding asciiEncoding = new UTF8Encoding();
            return asciiEncoding.GetBytes(strKey);
        }

    }
}
