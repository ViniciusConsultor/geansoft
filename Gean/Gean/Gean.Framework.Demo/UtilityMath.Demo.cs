using System;
using System.Collections.Generic;
using System.Text;
using Gean.Math;

namespace Gean.Library.Demo
{
    partial class Program
    {
        /// <summary>
        /// 四舍五入
        /// </summary>
        public static void UtilityMath456Demo()
        {
            Console.WriteLine("1.25 = " + UtilityMath.Round(1.25M, 1));

            Console.WriteLine("\nComplated...........\n\n");
            Console.ReadKey();
        }
    }
}
