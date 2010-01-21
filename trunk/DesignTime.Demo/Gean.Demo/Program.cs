using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Gean.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ready...");

            IDGeneratorDemo idgDemo = new IDGeneratorDemo();
            idgDemo.Do();

            Console.ReadKey();
        }
    }
}
