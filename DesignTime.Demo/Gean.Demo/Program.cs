using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Gean.Options;
using System.Collections.Specialized;
using System.Xml;

namespace Gean.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ready...");

            StringCollection sc = UtilityFile.SearchDirectory(OptionService.ApplicationStartPath, "*.option");

            XmlDocument[] docs = new XmlDocument[sc.Count];
            int q = 0;
            foreach (var file in sc)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                docs[q] = doc;
                q++;
            }

            OptionService.ME.Initializes(docs);

            Console.WriteLine(SOMSOption.ME.Key);
            Console.WriteLine(SOMSOption.ME.Value);
            Console.WriteLine(XyzOption.ME.Key);
            Console.WriteLine(XyzOption.ME.Value);
            Console.WriteLine(AbcdOption.ME.Key);
            Console.WriteLine(AbcdOption.ME.Value);

            Console.WriteLine("Complate...");
            Console.ReadKey();
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
