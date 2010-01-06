using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Collections.Specialized;
using Microsoft.ApplicationBlocks.Data;
using System.Diagnostics;

namespace Gean.Data
{
    /// <summary>
    /// 访问Microsoft Sql Server数据库的助手类。一般情况下都已挂在DataAcess的类型中，不必单独使用。
    /// </summary>
    public class MsSqlHelper : IAcessHelper
    {
        /// <summary>
        /// 私有化<see cref="MsSqlHelper"/>构造函数。
        /// </summary>
        /// <param name="sqlSb">数据库的连接字符串的生成器</param>
        protected MsSqlHelper(SqlConnectionStringBuilder sqlSb)
            : this(sqlSb.ConnectionString)
        {
        }

        /// <summary>
        /// 私有化<see cref="MsSqlHelper"/>构造函数。
        /// </summary>
        /// <param name="connectionString">数据库的连接字符串</param>
        protected MsSqlHelper(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.Connection = new SqlConnection(connectionString);

            _helpers.Add(this.ConnectionString, this);
        }

        private static bool _isInit = false;
        private static Dictionary<string, MsSqlHelper> _helpers = new Dictionary<string, MsSqlHelper>(5);

        public static MsSqlHelper GetMsSqlHelper()
        {
            Debug.Assert(_helpers != null || _helpers.Count <= 0, "MsSqlHelper had NOT initialized!");
            foreach (var item in _helpers)
            {
                return item.Value;
            }
            return null;
        }

        public static MsSqlHelper GetMsSqlHelper(string connectionString)
        {
            return GetMsSqlHelper(connectionString, 1);
        }

        public static MsSqlHelper GetMsSqlHelper(string connectionString, int helperCount)
        {
            if (_helpers == null)
            {
                _helpers = new Dictionary<string, MsSqlHelper>(helperCount * 2);
            }
            if (_helpers.ContainsKey(connectionString))
            {
                return _helpers[connectionString];
            }
            else
            {
                return new MsSqlHelper(connectionString);
            }
        }

        #region IAcessHelper 成员

        public string ConnectionString { get; protected set; }

        /// <summary>
        /// Gets or sets 一个到Sql Server数据库的连接。
        /// </summary>
        /// <value>The connection.</value>
        public SqlConnection Connection { get; private set; }

        /// <summary>
        /// 简化创建指定的存储过程与该存储过程可选的参数，返回一个SQL命令对象。
        /// </summary>
        /// <param name="spName">指定的存储过程名。</param>
        /// <param name="sourceColumns">一个SQL命令对象。</param>
        /// <returns></returns>
        public SqlCommand CreateCommand(string spName, params string[] sourceColumns)
        {
            return SqlHelper.CreateCommand(Connection, spName, sourceColumns);
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteDataset(Connection, commandType, commandText, commandParameters);
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteDataset(Connection, commandType, commandText);
        }

        public DataSet ExecuteDataset(string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteDataset(Connection, spName, parameterValues);
        }

        public DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteDataset((SqlTransaction)transaction, commandType, commandText);
        }

        public DataSet ExecuteDataset(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteDataset((SqlTransaction)transaction, spName, parameterValues);
        }

        public DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteDataset((SqlTransaction)transaction, commandType, commandText, commandParameters);
        }

        public DataSet ExecuteDatasetTypedParams(string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteDatasetTypedParams(Connection, spName, dataRow);
        }

        public DataSet ExecuteDatasetTypedParams(IDbTransaction transaction, string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteDatasetTypedParams((SqlTransaction)transaction, spName, dataRow);
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteNonQuery(Connection, commandType, commandText);
        }

        public int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery(Connection, commandType, commandText, commandParameters);
        }

        public int ExecuteNonQuery(string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteNonQuery(Connection, spName, parameterValues);
        }

        public int ExecuteNonQuery(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteNonQuery((SqlTransaction)transaction, spName, parameterValues);
        }

        public int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteNonQuery((SqlTransaction)transaction, commandType, commandText, commandParameters);
        }

        public int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteNonQuery((SqlTransaction)transaction, commandType, commandText);
        }

        public int ExecuteNonQueryTypedParams(IDbTransaction transaction, string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteNonQuery((SqlTransaction)transaction, spName, dataRow);
        }

        public int ExecuteNonQueryTypedParams(string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteNonQuery(Connection, spName, dataRow);
        }

        public SqlDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteReader(Connection, commandType, commandText);
        }

        public SqlDataReader ExecuteReader(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteReader((SqlTransaction)transaction, spName, parameterValues);
        }

        public SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteReader(Connection, commandType, commandText, commandParameters);
        }

        public SqlDataReader ExecuteReader(string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteReader(Connection, spName, parameterValues);
        }

        public SqlDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteReader((SqlTransaction)transaction, commandType, commandText);
        }

        public SqlDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteReader((SqlTransaction)transaction, commandType, commandText, commandParameters);
        }

        public SqlDataReader ExecuteReaderTypedParams(IDbTransaction transaction, string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteReader((SqlTransaction)transaction, spName, dataRow);
        }

        public SqlDataReader ExecuteReaderTypedParams(string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteReaderTypedParams(Connection, spName, dataRow);
        }

        public object ExecuteScalar(string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteScalar(Connection, spName, parameterValues);
        }

        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteScalar(Connection, commandType, commandText);
        }

        public object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteScalar((SqlTransaction)transaction, commandType, commandText);
        }

        public object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteScalar(Connection, commandType, commandText, commandParameters);
        }

        public object ExecuteScalar(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteScalar((SqlTransaction)transaction, spName, parameterValues);
        }

        public object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteScalar((SqlTransaction)transaction, commandType, commandText, commandParameters);
        }

        public object ExecuteScalarTypedParams(string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteScalarTypedParams(Connection, spName, dataRow);
        }

        public object ExecuteScalarTypedParams(IDbTransaction transaction, string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteScalarTypedParams((SqlTransaction)transaction, spName, dataRow);
        }

        public XmlReader ExecuteXmlReader(CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteXmlReader(Connection, commandType, commandText);
        }

        public XmlReader ExecuteXmlReader(string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteXmlReader(Connection, spName, parameterValues);
        }

        public XmlReader ExecuteXmlReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteXmlReader(Connection, commandType, commandText, commandParameters);
        }

        public XmlReader ExecuteXmlReader(IDbTransaction transaction, string spName, params object[] parameterValues)
        {
            return SqlHelper.ExecuteXmlReader((SqlTransaction)transaction, spName, parameterValues);
        }

        public XmlReader ExecuteXmlReader(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return SqlHelper.ExecuteXmlReader((SqlTransaction)transaction, commandType, commandText, commandParameters);
        }

        public XmlReader ExecuteXmlReader(IDbTransaction transaction, CommandType commandType, string commandText)
        {
            return SqlHelper.ExecuteXmlReader((SqlTransaction)transaction, commandType, commandText);
        }

        public XmlReader ExecuteXmlReaderTypedParams(string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteXmlReaderTypedParams(Connection, spName, dataRow);
        }

        public XmlReader ExecuteXmlReaderTypedParams(IDbTransaction transaction, string spName, DataRow dataRow)
        {
            return SqlHelper.ExecuteXmlReaderTypedParams((SqlTransaction)transaction, spName, dataRow);
        }

        public void FillDataset(string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            SqlHelper.FillDataset(Connection, spName, dataSet, tableNames, parameterValues);
        }

        public void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            SqlHelper.FillDataset(Connection, commandType, commandText, dataSet, tableNames);
        }

        public void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            SqlHelper.FillDataset(Connection, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public void FillDataset(IDbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters)
        {
            SqlHelper.FillDataset((SqlTransaction)transaction, commandType, commandText, dataSet, tableNames, commandParameters);
        }

        public void FillDataset(IDbTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            SqlHelper.FillDataset((SqlTransaction)transaction, spName, dataSet, tableNames, parameterValues);
        }

        public void FillDataset(IDbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            SqlHelper.FillDataset((SqlTransaction)transaction, commandType, commandText, dataSet, tableNames);
        }

        public void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName)
        {
            SqlHelper.UpdateDataset(insertCommand, deleteCommand, updateCommand, dataSet, tableName);
        }

        #endregion
    }
}
