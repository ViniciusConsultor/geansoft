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
        public static readonly string DefaultKey = "Gean.Encrypt";

        private static MD5 _MD5;

        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        /// <summary>
        /// 获得一个字符串的MD5值
        /// </summary>
        /// <param name="input">一个字符串</param>
        /// <returns></returns>
        static public string GetMd5String(string input)
        {
            if (_MD5 == null)
            {
                _MD5 = MD5.Create();
            }

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
        static public bool VerifyMd5String(string input, string hash)
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
            SHA256Managed Sha256 = new SHA256Managed();
            byte[] Result = Sha256.ComputeHash(SHA256Data);
            return Convert.ToBase64String(Result);  //返回长度为44字节的字符串
        }

        /// <summary>
        /// 获取用DES方法加密一个指定的待加密的字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
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
}
