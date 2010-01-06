using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Gean.Data
{
    /// <summary>
    /// The interface of database.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// ConnectionString
        /// </summary>
        string ConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// The state of connection.
        /// </summary>
        ConnectionState ConnectionState
        {
            get;
        }

        /// <summary>
        /// Connection to database.
        /// </summary>
        IDbConnection Connection
        {
            get;
        }

        /// <summary>
        /// DataAdapter
        /// </summary>
        IDbDataAdapter DataAdapter
        {
            get;
        }

        /// <summary>
        /// Command
        /// </summary>
        IDbCommand Command
        {
            get;
        }

        /// <summary>
        /// Transaction
        /// </summary>
        IDbTransaction Transaction
        {
            get;
        }

        /// <summary>
        /// Parameters
        /// </summary>
        IDataParameterCollection Parameters
        {
            get;
        }

        /// <summary>
        /// DatabaseType
        /// </summary>
        DatabaseType DatabaseType
        {
            get;
        }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        /// <returns></returns>
        bool Close();

        /// <summary>
        /// Connects to a database.
        /// </summary>
        /// <returns></returns>
        bool Open();

        void ClearAllPools();
        void ClearPool();

        /// <summary>
        /// 连接另一个数据库
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        bool ChangeDatabase(string databaseName);

        /// <summary>
        /// Begins a database transaction .
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Begins a database transaction with specified active time.
        /// </summary>
        /// <param name="activeTime"></param>
        /// <returns></returns>
        IDbTransaction BeginTransaction(double activeTime);

        /// <summary>
        /// Begins a database transaction with specified IsolationLevel value.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Begins a database transaction with specified IsolationLevel value and active time.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <param name="activeTime"></param>
        /// <returns></returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel, double activeTime);

        /// <summary>
        /// Commits the database transaction.
        /// </summary>
        /// <returns></returns>
        bool Commit();

        /// <summary>
        /// Rolls the database transaction.
        /// </summary>
        /// <returns></returns>
        bool Rollback();

        /// <summary>
        /// Executes a procedure or sql statement ,and builds an IDataReader.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string commandText, bool isProcedure);

        /// <summary>
        /// Executes a procedure or sql statement ,and builds an IDataReader.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string commandText, bool isProcedure,IDataParameter parameter);

        /// <summary>
        /// Executes a procedure or sql statement ,and builds an IDataReader.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string commandText, bool isProcedure, IList<IDataParameter> parameters);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string commandText, bool isProcedure);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string commandText, bool isProcedure, IDataParameter parameter);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string commandText, bool isProcedure, IList<IDataParameter> parameters);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns the first column of the first row of the resultset.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, bool isProcedure);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns the first column of the first row of the resultset.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, bool isProcedure, IDataParameter parameter);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns the first column of the first row of the resultset.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, bool isProcedure, IList<IDataParameter> parameters);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns a DataSet.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string commandText, bool isProcedure);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns a DataSet.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string commandText, bool isProcedure, IDataParameter parameter);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns a DataSet.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string commandText, bool isProcedure, IList<IDataParameter> parameters);
       
        /// <summary>
        /// Executes a procedure or sql statement ,and returns a DataTable.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string commandText, bool isProcedure);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns a DataTable.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string commandText, bool isProcedure, IDataParameter parameter);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns a DataTable.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string commandText, bool isProcedure, IList<IDataParameter> parameters);
       
        /// <summary>
        /// Creates a TableCommandExecutor.
        /// </summary>
        /// <returns></returns>
        ITableCommandExecutor CreateTableCommandExecutor();

        /// <summary>
        /// Creates a ViewCommandExecutor.
        /// </summary>
        /// <returns></returns>
        IViewCommandExecutor CreateViewCommandExecutor();

    }
}
