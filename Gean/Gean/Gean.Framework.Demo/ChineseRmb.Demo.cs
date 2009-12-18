using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gean.Chinese;

namespace Gean.Framework.Demo
{
    partial class Program
    {
        public static void ChineseRmbDemo()
        {
            decimal num;

            num = 9999999999999.99M;
            Console.WriteLine(num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 9988554321.67M;
            Console.WriteLine(num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 9000000009.09M;
            Console.WriteLine(num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 9000000000M;
            Console.WriteLine(num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));


            num = 0M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 0.1M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 0.07M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 0.99M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 1.03M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 19.05M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 99.1M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 2000000080.07M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 9000000000009M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 9000009900009M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 9009900099009M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 9000900090009M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 9999999999999M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));
            num = 9000000000000M;
            Console.WriteLine("\n" + num.ToString() + " -> " + ChineseRmb.ToUpperChineseRmb(num));

            
            Console.WriteLine("\nComplated...........\n\n");
            Console.ReadKey();
        }
    }
}
