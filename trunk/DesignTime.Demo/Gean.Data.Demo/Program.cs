using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using Gean.Data.Resources;
using System.Data.SqlTypes;
using System.Xml;
using System.Diagnostics;
using System.IO;
using Gean.Math;
using System.Threading;

namespace Gean.Data.Demo
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //Main01_SqlConnectionStringBuilder.Do();
            //Main02_AdoNet.Do();
            //Main03_SQLTextBuilder.Do();
            //Main05_ORM.Do();
            Main06_ID.DoTest();
            //Main06_ID.DoTimeTest();

            Console.WriteLine("Please Press any key...");
            Console.ReadKey();
        }



    }
}
