using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Xml;

namespace Gean.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IDGeneratorDemo();
        }

        private static void IDGeneratorDemo()
        {
            Console.WriteLine("Ready...");

            IDGeneratorDemo idgDemo = new IDGeneratorDemo();
            idgDemo.Do();

            Console.ReadKey();
        }
    }
}
