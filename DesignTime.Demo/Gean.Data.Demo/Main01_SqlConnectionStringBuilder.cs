using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data.OracleClient;
using System.Data.SQLite;
using System.Data.OleDb;

namespace Gean.Data.Demo
{
    public class Main01_SqlConnectionStringBuilder
    {
        internal static void Do()
        {
            SqlConnectionStringBuilder sqlConnSb = new SqlConnectionStringBuilder();
            sqlConnSb.DataSource = @"NSIMPLE-P4MAN\SQLEXPRESS";
            sqlConnSb.InitialCatalog = "Northwind";
            sqlConnSb.IntegratedSecurity = true;

            SqlConnection cn = new SqlConnection(sqlConnSb.ConnectionString);
            cn.Open();

            Console.WriteLine(sqlConnSb.ConnectionString);
            Console.WriteLine(cn.State);
            Console.WriteLine();

            Console.WriteLine("OracleConnectionStringBuilder");
            string ostr = "Data Source=myOracle;User Id=myUsername;Password=myPassword;Min Pool Size=10;Connection Lifetime=120;";
            OracleConnectionStringBuilder oraConnSb = new OracleConnectionStringBuilder(ostr);
            Console.WriteLine(oraConnSb.ConnectionString);
            Console.WriteLine(oraConnSb.DataSource);
            Console.WriteLine(oraConnSb.UserID);
            Console.WriteLine(oraConnSb.Password);
            Console.WriteLine(oraConnSb.MinPoolSize);
            Console.WriteLine(oraConnSb.LoadBalanceTimeout);

            Console.WriteLine();
            Console.WriteLine("MySqlConnectionStringBuilder");
            string mysqlStr = "Server=myServerAddress;Database=myDataBase;Uid=myUsername;Pwd=myPassword;default command timeout=20;";
            MySqlConnectionStringBuilder mysqlConnSb = new MySqlConnectionStringBuilder(mysqlStr);
            Console.WriteLine(mysqlConnSb.ConnectionString);
            Console.WriteLine(mysqlConnSb.Server);
            Console.WriteLine(mysqlConnSb.Database);
            Console.WriteLine(mysqlConnSb.UserID);
            Console.WriteLine(mysqlConnSb.Password);
            Console.WriteLine(mysqlConnSb.DefaultCommandTimeout);

            Console.WriteLine();
            Console.WriteLine("SQLiteConnectionStringBuilder");
            string sqliteStr = "Data Source=filename;Version=3;Pooling=False;Max Pool Size=100;";
            SQLiteConnectionStringBuilder sqliteConnSb = new SQLiteConnectionStringBuilder(sqliteStr);
            Console.WriteLine(sqliteConnSb.ConnectionString);
            Console.WriteLine(sqliteConnSb.DataSource);
            Console.WriteLine(sqliteConnSb.Version);
            Console.WriteLine(sqliteConnSb.Pooling);
            Console.WriteLine(sqliteConnSb.MaxPageCount);

            Console.WriteLine("==========================");
            Console.WriteLine();
        }
    }

}
