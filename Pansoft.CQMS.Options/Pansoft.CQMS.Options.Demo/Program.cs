using System;
using System.Collections.Generic;
using System.Text;

namespace Pansoft.CQMS.Options.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            OptionManager om = new OptionManager();
            om.Initializes("");

            Console.ReadKey();
        }
    }
}
