using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Xml;

namespace Gean.Data
{
    public interface IAdoHelper
    {
        void AddUpdateEventHandlers(IDbDataAdapter dataAdapter, RowUpdatingHandler rowUpdatingHandler, RowUpdatedHandler rowUpdatedHandler);
        void AssignParameterValues(IDataParameter[] commandParameters, DataRow dataRow);
        void AssignParameterValues(IDataParameter[] commandParameters, object[] parameterValues);
        void AssignParameterValues(IDataParameterCollection commandParameters, DataRow dataRow);
        void AttachParameters(IDbCommand command, IDataParameter[] commandParameters);
        void CacheParameterSet(IDbConnection connection, string commandText, params IDataParameter[] commandParameters);
        void CacheParameterSet(string connectionString, string commandText, params IDataParameter[] commandParameters);
        void CleanParameterSyntax(IDbCommand command);
        void ClearCommand(IDbCommand command);
        IDbCommand CreateCommand(IDbConnection connection, string spName, params string[] sourceColumns);
        IDbCommand CreateCommand(string connectionString, string spName, params string[] sourceColumns);
        IDbCommand CreateCommand(IDbConnection connection, string commandText, CommandType commandType, params IDataParameter[] commandParameters);
        IDbCommand CreateCommand(string connectionString, string commandText, CommandType commandType, params IDataParameter[] commandParameters);
        void DeriveParameters(IDbCommand cmd);
        DataSet ExecuteDataset(IDbCommand command);
        DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText);
        DataSet ExecuteDataset(IDbConnection connection, string spName, params object[] parameterValues);
        DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText);
        DataSet ExecuteDataset(IDbTransaction transaction, string spName, params object[] parameterValues);
        DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText);
        DataSet ExecuteDataset(string connectionString, string spName, params object[] parameterValues);
        DataSet ExecuteDataset(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        DataSet ExecuteDataset(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        DataSet ExecuteDatasetTypedParams(IDbCommand command, DataRow dataRow);
        DataSet ExecuteDatasetTypedParams(IDbConnection connection, string spName, DataRow dataRow);
        DataSet ExecuteDatasetTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        DataSet ExecuteDatasetTypedParams(string connectionString, string spName, DataRow dataRow);
        int ExecuteNonQuery(IDbCommand command);
        int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText);
        int ExecuteNonQuery(IDbConnection connection, string spName, params object[] parameterValues);
        int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText);
        int ExecuteNonQuery(IDbTransaction transaction, string spName, params object[] parameterValues);
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText);
        int ExecuteNonQuery(string connectionString, string spName, params object[] parameterValues);
        int ExecuteNonQuery(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        int ExecuteNonQuery(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        int ExecuteNonQuery(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        int ExecuteNonQueryTypedParams(IDbCommand command, DataRow dataRow);
        int ExecuteNonQueryTypedParams(IDbConnection connection, string spName, DataRow dataRow);
        int ExecuteNonQueryTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        int ExecuteNonQueryTypedParams(string connectionString, string spName, DataRow dataRow);
        IDataReader ExecuteReader(IDbCommand command);
        IDataReader ExecuteReader(IDbCommand command, AdoConnectionOwnership connectionOwnership);
        IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText);
        IDataReader ExecuteReader(IDbConnection connection, string spName, params object[] parameterValues);
        IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText);
        IDataReader ExecuteReader(IDbTransaction transaction, string spName, params object[] parameterValues);
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText);
        IDataReader ExecuteReader(string connectionString, string spName, params object[] parameterValues);
        IDataReader ExecuteReader(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        IDataReader ExecuteReader(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        IDataReader ExecuteReader(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        IDataReader ExecuteReaderTypedParams(IDbCommand command, DataRow dataRow);
        IDataReader ExecuteReaderTypedParams(IDbConnection connection, string spName, DataRow dataRow);
        IDataReader ExecuteReaderTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        IDataReader ExecuteReaderTypedParams(string connectionString, string spName, DataRow dataRow);
        object ExecuteScalar(IDbCommand command);
        object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText);
        object ExecuteScalar(IDbConnection connection, string spName, params object[] parameterValues);
        object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText);
        object ExecuteScalar(IDbTransaction transaction, string spName, params object[] parameterValues);
        object ExecuteScalar(string connectionString, CommandType commandType, string commandText);
        object ExecuteScalar(string connectionString, string spName, params object[] parameterValues);
        object ExecuteScalar(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        object ExecuteScalar(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        object ExecuteScalar(string connectionString, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        object ExecuteScalarTypedParams(IDbCommand command, DataRow dataRow);
        object ExecuteScalarTypedParams(IDbConnection connection, string spName, DataRow dataRow);
        object ExecuteScalarTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        object ExecuteScalarTypedParams(string connectionString, string spName, DataRow dataRow);
        XmlReader ExecuteXmlReader(IDbCommand cmd);
        XmlReader ExecuteXmlReader(IDbConnection connection, CommandType commandType, string commandText);
        XmlReader ExecuteXmlReader(IDbConnection connection, string spName, params object[] parameterValues);
        XmlReader ExecuteXmlReader(IDbTransaction transaction, CommandType commandType, string commandText);
        XmlReader ExecuteXmlReader(IDbTransaction transaction, string spName, params object[] parameterValues);
        XmlReader ExecuteXmlReader(IDbConnection connection, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        XmlReader ExecuteXmlReader(IDbTransaction transaction, CommandType commandType, string commandText, params IDataParameter[] commandParameters);
        XmlReader ExecuteXmlReaderTypedParams(IDbCommand command, DataRow dataRow);
        XmlReader ExecuteXmlReaderTypedParams(IDbConnection connection, string spName, DataRow dataRow);
        XmlReader ExecuteXmlReaderTypedParams(IDbTransaction transaction, string spName, DataRow dataRow);
        void FillDataset(IDbCommand command, DataSet dataSet, string[] tableNames);
        void FillDataset(IDbConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);
        void FillDataset(IDbConnection connection, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);
        void FillDataset(IDbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);
        void FillDataset(IDbTransaction transaction, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);
        void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames);
        void FillDataset(string connectionString, string spName, DataSet dataSet, string[] tableNames, params object[] parameterValues);
        void FillDataset(IDbConnection connection, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params IDataParameter[] commandParameters);
        void FillDataset(IDbTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params IDataParameter[] commandParameters);
        void FillDataset(string connectionString, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames, params IDataParameter[] commandParameters);
        IDataParameter GetBlobParameter(IDbConnection connection, IDataParameter p);
        IDataParameter[] GetCachedParameterSet(IDbConnection connection, string commandText);
        IDataParameter[] GetCachedParameterSet(string connectionString, string commandText);
        IDbConnection GetConnection(string connectionString);
        IDbDataAdapter GetDataAdapter();
        IDataParameter[] GetDataParameters(int size);
        IDataParameter GetParameter();
        IDataParameter GetParameter(string name, object value);
        IDataParameter GetParameter(string name, DbType dbType, int size, ParameterDirection direction);
        IDataParameter GetParameter(string name, DbType dbType, int size, string sourceColumn, DataRowVersion sourceVersion);
        IDataParameter[] GetSpParameterSet(IDbConnection connection, string spName);
        IDataParameter[] GetSpParameterSet(string connectionString, string spName);
        IDataParameter[] GetSpParameterSet(IDbConnection connection, string spName, bool includeReturnValueParameter);
        IDataParameter[] GetSpParameterSet(string connectionString, string spName, bool includeReturnValueParameter);
        void PrepareCommand(IDbCommand command, IDbConnection connection, IDbTransaction transaction, CommandType commandType, string commandText, IDataParameter[] commandParameters, out bool mustCloseConnection);
        void RowUpdated(object obj, RowUpdatedEventArgs e);
        void RowUpdating(object obj, RowUpdatingEventArgs e);
        IDbCommand SetCommand(IDbCommand command, out bool mustCloseConnection);
        void UpdateDataset(IDbCommand insertCommand, IDbCommand deleteCommand, IDbCommand updateCommand, DataSet dataSet, string tableName);
        void UpdateDataset(IDbCommand insertCommand, IDbCommand deleteCommand, IDbCommand updateCommand, DataSet dataSet, string tableName, RowUpdatingHandler rowUpdatingHandler, RowUpdatedHandler rowUpdatedHandler);

    }

    public enum AdoConnectionOwnership
    {
        Internal = 0,
        External = 1,
    }

    public delegate void RowUpdatedHandler(object obj, RowUpdatedEventArgs e);

    public delegate void RowUpdatingHandler(object obj, RowUpdatingEventArgs e);

}
