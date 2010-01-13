using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Gean.Data.Demo
{
    class Main04_QueryColumns
    {
        public static void Do()
        {
            QueryColumns("Customers");
        }
        /// <summary>
        /// 查找指定数据表的所有列.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        private static void QueryColumns(string tableName)
        {
            SqlConnection conn = Main02_AdoNet.GetConnection();
            MSSqlHelper helper = MSSqlHelper.GetMsSqlHelper(conn.ConnectionString);
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
