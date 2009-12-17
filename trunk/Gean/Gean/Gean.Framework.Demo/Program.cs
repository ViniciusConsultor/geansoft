using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gean.Math;

namespace Gean.Framework.Demo
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("  1. BigIntegerDemo");
            Console.WriteLine("  2. EncryptDemo");
            Console.WriteLine();
            string key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    BigIntegerDemo();
                    break;
                case "2":
                    EncryptDemo();
                    break;
            }
        }


    }
}
