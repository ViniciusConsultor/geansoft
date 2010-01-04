using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using Microsoft.ApplicationBlocks.Data;

namespace Gean.Data.Demo
{
    public class Main02_AdoNet_Demo
    {
        internal static void Do()
        {
            SqlConnectionStringBuilder sqlConnSb = new SqlConnectionStringBuilder();
            sqlConnSb.DataSource = @"NSIMPLE-P4MAN\SQLEXPRESS";
            sqlConnSb.InitialCatalog = "Northwind";
            sqlConnSb.IntegratedSecurity = true;
            SqlConnection cn = new SqlConnection(sqlConnSb.ConnectionString);

            SelectDemo(cn);
            UpdateDemo(cn);
        }

        private static void UpdateDemo(SqlConnection cn)
        {
            throw new NotImplementedException();
        }

        private static void SelectDemo(SqlConnection cn)
        {
            SqlCommand command = new SqlCommand();
            string cmd = @"
                    SELECT
                        *
                    FROM
                        dbo.Employees
                    ";
            SqlDataReader reader = SqlHelper.ExecuteReader(cn, CommandType.Text, cmd);
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0}, {1}", reader[0], reader[1]));
            }

            reader.Close();
        }
    }
}