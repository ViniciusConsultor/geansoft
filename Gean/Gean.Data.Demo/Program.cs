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
            Main03_SQLTextBuilder.Do();

            Console.WriteLine();
            SelcetColumns("Customers");

            Console.ReadKey();
        }

        private static void SelcetColumns(string tableName)
        {
            SqlConnection conn = Main02_AdoNet.GetConnection();
            MsSqlHelper helper = MsSqlHelper.GetMsSqlHelper(conn.ConnectionString);
            SqlParameter param = new SqlParameter("@TableName", tableName);
            SqlDataReader reader = helper.ExecuteReader(System.Data.CommandType.Text, Gean.Data.Resources.SQLString.GetColumnsByTable, param);

            while (reader.Read())
            {
                Console.Write(reader["TableName"]);
                Console.Write("\t");
                string str = (string)reader["ColumsName"];
                Console.Write(str);
                if (str.Length <= 7)
                {
                    Console.Write("\t\t\t");
                }
                else
                {
                    Console.Write("\t\t");
                }
                str = (string)reader["DbDataType"]; 
                Console.Write(str);
                if (str.Length <= 7)
                {
                    Console.Write("\t\t");
                }
                else
                {
                    Console.Write("\t");
                }
                Console.WriteLine(reader["Length"]);
            }
        }

    }
}
