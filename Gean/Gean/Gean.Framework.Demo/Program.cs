using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gean.Math;
using Gean.Chinese;

namespace Gean.Framework.Demo
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("  1. BigIntegerDemo");
            Console.WriteLine("  2. EncryptDemo");
            Console.WriteLine("  3. ChineseRmbDemo");
            Console.WriteLine("  4. Math.4she5ru");
            Console.WriteLine();
            string key = Console.ReadLine();
            switch (key)
            {
                default :
                    break;
                case "1":
                    BigIntegerDemo();
                    break;
                case "2":
                    EncryptDemo();
                    break;
                case "3":
                    ChineseRmbDemo();
                    break;
                case "4":
                    UtilityMath456Demo();
                    break;
            }
        }


    }
}
