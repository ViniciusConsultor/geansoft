﻿using System;
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
        /// 本框架设定的缺省密钥字符串，只读状态。一般不建议使用。
        /// </summary>
        public static readonly string DefaultKey = "98$Mp6?W";

        /// <summary>  
        /// 自动生成一个密钥。
        /// </summary>  
        /// <returns>返回生成的密钥</returns>  
        public static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.  
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            // Use the Automatically generated key for Encryption.  
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

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

        /// <summary>
        /// 获取用DES方法加密 [一个指定的待加密的字符串] 后的字符串
        /// </summary>
        /// <param name="encryptString">一个指定的待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        public static string DESEncrypt(string encryptString, string encryptKey)
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
        public static string DESDecrypt(string decryptString, string decryptKey)
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

        /// <summary>  
        /// 加密文件  
        /// </summary>  
        /// <param name="sInputFilename">要加密的文件</param>  
        /// <param name="sOutputFilename">加密后保存的文件</param>  
        /// <param name="sKey">密钥</param>  
        public static void EncryptFile(string inputFilename, string outputFilename, string key)
        {
            using (FileStream fsInput = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                byte[] bytearrayinput = new byte[fsInput.Length];
                fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                fsInput.Close();

                FileStream fsEncrypted = new FileStream(outputFilename,
                   FileMode.OpenOrCreate,
                   FileAccess.Write);
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();

                DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(key);

                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                cryptostream.Close();
                fsEncrypted.Close();
            }
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="sInputFilename">要解密的文件</param>  
        /// <param name="sOutputFilename">解决后保存的文件</param>  
        /// <param name="sKey">密钥</param>  
        public static void DecryptFile(string inputFilename, string outputFilename, string key)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.  
            //Set secret key For DES algorithm.  
            DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
            //Set initialization vector.  
            DES.IV = ASCIIEncoding.ASCII.GetBytes(key);

            //Create a file stream to read the encrypted file back.  
            using (FileStream fsread = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                //Create a DES decryptor from the DES instance.  
                ICryptoTransform desdecrypt = DES.CreateDecryptor();
                //Create crypto stream set to read and do a  
                //DES decryption transform on incoming bytes.  
                CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
                //Print the contents of the decrypted file.  
                StreamWriter fsDecrypted = new StreamWriter(outputFilename);
                fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
                fsDecrypted.Flush();
                fsDecrypted.Close();
            }
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="sInputFilename">要解密的文件路径</param>  
        /// <param name="sKey">密钥</param>  
        /// <returns>返回内容</returns>  
        public static string DecryptFile(string inputFilename, string key)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.  
            //Set secret key For DES algorithm.  
            DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
            //Set initialization vector.  
            DES.IV = ASCIIEncoding.ASCII.GetBytes(key);

            //Create a file stream to read the encrypted file back.  
            using (FileStream fsread = new FileStream(inputFilename, FileMode.Open, FileAccess.Read))
            {
                byte[] byt = new byte[fsread.Length];
                fsread.Read(byt, 0, byt.Length);
                fsread.Flush();
                fsread.Close();
                //Create a DES decryptor from the DES instance.  
                ICryptoTransform desdecrypt = DES.CreateDecryptor();
                MemoryStream ms = new MemoryStream();
                CryptoStream cryptostreamDecr = new CryptoStream(ms, desdecrypt, CryptoStreamMode.Write);
                cryptostreamDecr.Write(byt, 0, byt.Length);
                cryptostreamDecr.FlushFinalBlock();
                cryptostreamDecr.Close();
                return Encoding.UTF8.GetString(ms.ToArray()).Trim();
            }
        }
    }
}