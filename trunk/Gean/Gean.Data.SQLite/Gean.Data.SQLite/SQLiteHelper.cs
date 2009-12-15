using System.Data;
using System.Data.SQLite;
using System.IO;
using System;

namespace Gean.Data.SQLite
{
    public class SQLiteHelper
    {
        ///<summary>
        /// 获得连接对象
        ///</summary>
        ///<returns></returns>
        public static SQLiteConnection GetSQLiteConnection(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new Exception();
            }
            SQLiteFile = filename;
            return new SQLiteConnection("Data Source=\"" + filename + "\"");
        }

        /// <summary>
        /// Gets or sets 数据库文件实体.
        /// </summary>
        /// <value>数据库文件实体.</value>
        public static string SQLiteFile { get; set; }

        private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, string cmdText, params object[] p)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = CommandType.Text;
            cmd.CommandTimeout = 30;
            if (p != null)
            {
                foreach (object parm in p)
                {
                    cmd.Parameters.AddWithValue(string.Empty, parm);
                }
            }
        }

        public static DataSet ExecuteDataset(string cmdText, params object[] p)
        {
            DataSet ds = new DataSet();
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection(SQLiteFile))
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(ds);
            }
            return ds;
        }

        public static DataRow ExecuteDataRow(string cmdText, params object[] p)
        {
            DataSet ds = ExecuteDataset(cmdText, p);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return ds.Tables[0].Rows[0];
            return null;
        }

        ///<summary>
        /// 返回受影响的行数
        ///</summary>
        ///<param name="cmdText">a</param>
        ///<param name="commandParameters">传入的参数</param>
        ///<returns></returns>
        public static int ExecuteNonQuery(string cmdText, params object[] p)
        {
            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection(SQLiteFile))
            {
                PrepareCommand(command, connection, cmdText, p);
                return command.ExecuteNonQuery();
            }
        }

        ///<summary>
        /// 返回SqlDataReader对象。实现DataReader方法，取出一条一条记录
        ///</summary>
        ///<param name="cmdText"></param>
        ///<param name="commandParameters">传入的参数</param>
        ///<returns></returns>
        public static SQLiteDataReader ExecuteReader(string cmdText, params object[] p)
        {
            SQLiteCommand command = new SQLiteCommand();
            SQLiteConnection connection = GetSQLiteConnection(SQLiteFile);
            try
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch
            {
                connection.Close();
                throw;
            }
        }

        ///<summary>
        /// 返回结果集中的第一行第一列，忽略其他行或列
        ///</summary>
        ///<param name="cmdText"></param>
        ///<param name="commandParameters">传入的参数</param>
        ///<returns></returns>
        public static object ExecuteScalar(string cmdText, params object[] p)
        {
            SQLiteCommand cmd = new SQLiteCommand();

            using (SQLiteConnection connection = GetSQLiteConnection(SQLiteFile))
            {
                PrepareCommand(cmd, connection, cmdText, p);
                return cmd.ExecuteScalar();
            }
        }

        ///<summary>
        /// 分页
        ///</summary>
        ///<param name="recordCount"></param>
        ///<param name="pageIndex"></param>
        ///<param name="pageSize"></param>
        ///<param name="cmdText"></param>
        ///<param name="countText"></param>
        ///<param name="p"></param>
        ///<returns></returns>
        public static DataSet ExecutePager(ref int recordCount, int pageIndex, int pageSize, string cmdText, string countText, params object[] p)
        {
            if (recordCount < 0)
                recordCount = int.Parse(ExecuteScalar(countText, p).ToString());

            DataSet ds = new DataSet();

            SQLiteCommand command = new SQLiteCommand();
            using (SQLiteConnection connection = GetSQLiteConnection(SQLiteFile))
            {
                PrepareCommand(command, connection, cmdText, p);
                SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                da.Fill(ds, (pageIndex - 1) * pageSize, pageSize, "result");
            }
            return ds;
        }

        /// <summary>
        /// 根据数据库中的数据类型返回DotNet的数据类型
        /// </summary>
        /// <param name="type">数据库中的数据类型</param>
        /// <returns>DotNet的数据类型</returns>
        public static Type GetType(string type)
        {
            switch (type.ToLower())
            {
                case "int":
                    return typeof(Int32);
                case "text":
                    return typeof(String);
                case "bigint":
                    return typeof(Int64);
                case "binary":
                    return typeof(System.Byte[]);
                case "bit":
                    return typeof(Boolean);
                case "char":
                    return typeof(String);
                case "datetime":
                    return typeof(System.DateTime);
                case "decimal":
                    return typeof(System.Decimal);
                case "float":
                    return typeof(System.Double);
                case "image":
                    return typeof(System.Byte[]);
                case "money":
                    return typeof(System.Decimal);
                case "nchar":
                    return typeof(String);
                case "ntext":
                    return typeof(String);
                case "numeric":
                    return typeof(System.Decimal);
                case "nvarchar":
                    return typeof(String);
                case "real":
                    return typeof(System.Single);
                case "smalldatetime":
                    return typeof(System.DateTime);
                case "smallint":
                    return typeof(Int16);
                case "smallmoney":
                    return typeof(System.Decimal);
                case "timestamp":
                    return typeof(System.DateTime);
                case "tinyint":
                    return typeof(System.Byte);
                case "uniqueidentifier":
                    return typeof(System.Guid);
                case "varbinary":
                    return typeof(System.Byte[]);
                case "varchar":
                    return typeof(String);
                case "Variant":
                    return typeof(Object);
                default:
                    return typeof(String);
            }
        }

    }

}