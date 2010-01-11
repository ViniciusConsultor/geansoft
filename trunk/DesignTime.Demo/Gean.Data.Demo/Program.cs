using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using Gean.Data.Resources;
using System.Data.SqlTypes;

namespace Gean.Data.Demo
{
    public static class Program
    {
        static void Main(string[] args)
        {
            //Main01_SqlConnectionStringBuilder.Do();
            //Main02_AdoNet.Do();
            //Main03_SQLTextBuilder.Do();
            Main05_ORM.Do();

            Console.ReadKey();
        }


    }
}
