using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gean.Encrypt;

namespace Gean.Library.Demo
{
    partial class Program
    {
        public static void EncryptDemo()
        {
            string encryptString = DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString();
            
            Console.WriteLine();
            Console.WriteLine("DefaultKey = " + UtilityEncrypt.DefaultKey);
            Console.WriteLine("\n待加密的字符串：" + encryptString);

            Console.WriteLine("\nMD5 加密结果：" + UtilityEncrypt.GetMd5String(encryptString));
            Console.WriteLine("\nMD5 加密结果：" + UtilityEncrypt.GetSHA256String(encryptString));

            string e = UtilityEncrypt.EncodeDESString(encryptString, "12345678");
            Console.WriteLine("\nDES 加密结果：" + e);
            Console.WriteLine("\nDES 解密结果：" + UtilityEncrypt.DecodeDESString(e, "12345678"));


            Console.WriteLine("\nComplated...........\n\n");
            Console.ReadKey();
        }
    }
}
