using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.OracleClient;
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

            Console.WriteLine("==========================");
            Console.WriteLine();
        }
    }

}
