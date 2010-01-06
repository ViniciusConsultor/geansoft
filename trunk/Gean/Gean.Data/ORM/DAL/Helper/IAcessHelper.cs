using System;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace Gean.Data
{
    public interface IAcessHelper
    {
        string ConnectionString { get; }

        SqlCommand CreateCommand(string spName, params string[] sourceColumns);
        DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        DataSet ExecuteDataset(CommandType commandType, string commandText);
        DataSet ExecuteDataset(string spName, params object[] parameterValues);
        DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText);
        DataSet ExecuteDataset(IDbTransaction transaction, string spName, params object[] parameterValues);
        DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        DataSet ExecuteDatasetTypedParams(string spName, DataRow dataRow);
        DataSet ExecuteDatasetTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        int ExecuteNonQuery(CommandType commandType, string commandText);
        int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        int ExecuteNonQuery(string spName, params object[] parameterValues);
        int ExecuteNonQuery(IDbTransaction transaction, string spName, params object[] parameterValues);
        int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText);
        int ExecuteNonQueryTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        int ExecuteNonQueryTypedParams(string spName, DataRow dataRow);
        SqlDataReader ExecuteReader(CommandType commandType, string commandText);
        SqlDataReader ExecuteReader(IDbTransaction transaction, string spName, params object[] parameterValues);
        SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        SqlDataReader ExecuteReader(string spName, params object[] parameterValues);
        SqlDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText);
        SqlDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        SqlDataReader ExecuteReaderTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        SqlDataReader ExecuteReaderTypedParams(string spName, DataRow dataRow);
        object ExecuteScalar(string spName, params object[] parameterValues);
        object ExecuteScalar(CommandType commandType, string commandText);
        object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText);
        object ExecuteScalar(CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        object ExecuteScalar(IDbTransaction transaction, string spName, params object[] parameterValues);
        object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        object ExecuteScalarTypedParams(string spName, DataRow dataRow);
        object ExecuteScalarTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        XmlReader ExecuteXmlReader(CommandType commandType, string commandText);
        XmlReader ExecuteXmlReader(string spName, params object[] parameterValues);
        XmlReader ExecuteXmlReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        XmlReader ExecuteXmlReader(IDbTransaction transaction, string spName, params object[] parameterValues);
        XmlReader ExecuteXmlReader(IDbTransaction transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters);
        XmlReader ExecuteXmlReader(IDbTransaction transaction, CommandType commandType, string commandText);
        XmlReader ExecuteXmlReaderTypedParams(string spName, DataRow dataRow);
        XmlReader ExecuteXmlReaderTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        void FillDataset(string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);
        void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);
        void FillDataset(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters);
        void FillDataset(IDbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params SqlParameter[] commandParameters);
        void FillDataset(IDbTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);
        void FillDataset(IDbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);
        void UpdateDataset(SqlCommand insertCommand, SqlCommand deleteCommand, SqlCommand updateCommand, DataSet dataSet, string tableName);
    }
}
