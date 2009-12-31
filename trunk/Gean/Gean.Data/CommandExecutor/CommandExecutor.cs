using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Gean.Data.Resources;

namespace Gean.Data
{
    /// <summary>
    /// Executes sql statement or stored procedure
    /// </summary>
    internal class CommandExecutor : ITableCommandExecutor
    {

        private IDatabase _database;
        private bool _isInTransaction;

        #region Properties

        public IDatabase Database
        {
            get
            {
                return _database;
            }
        }

        public bool IsInTransaction
        {
            get
            {
                return _isInTransaction;
            }
        }

        #endregion


        public CommandExecutor(IDatabase database)
        {
            if (database == null)
            {
                throw new CommandException(MsgResource.InvalidArguments + "IDatabase database");
            }
            _database = database;
        }


        //public IDataParameter CreateParameter(string parameterName, object parameterValue)
        //{
        //    return this._database.CreateParameter(parameterName, parameterValue);
        //}

        //public List<IDataParameter> CreateParameters(string[] parameterNames, object[] parameterValues)
        //{
        //    return this._database.CreateParameters(parameterNames, parameterValues);
        //}

        #region  Transaction


        /// <summary>
        /// Begins a transaction , it's effective only when executing Add,Update and Delete operations .
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            return this.BeginTransaction(1000*60);
        }

        /// <summary>
        /// Begins a database transaction with specified active time.
        /// </summary>
        /// <param name="activeTime"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(double activeTime)
        {
            if (string.IsNullOrEmpty(_database.ConnectionString))
            {
                throw new CommandException(MsgResource.ConnectionStringMissing);
            }

            IDbTransaction transaction = null;
            if (_database.ConnectionState == ConnectionState.Closed)
            {
                bool r = _database.Open();
                if (r)
                {
                     transaction= _database.BeginTransaction(activeTime);
                     _isInTransaction = (transaction != null);
                }
                else
                {
                    _isInTransaction = false;
                }
            }
            else if (_database.ConnectionState == ConnectionState.Open && !_isInTransaction)
            {
                transaction = _database.BeginTransaction(activeTime);
                _isInTransaction = (transaction != null);
            }
            return transaction;
        }

        /// <summary>
        /// Begins a database transaction with specified IsolationLevel value.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return this.BeginTransaction(isolationLevel, 1000 * 60);
        }

        /// <summary>
        /// Begins a database transaction with specified IsolationLevel value and active time.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <param name="activeTime"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel, double activeTime)
        {
            if (string.IsNullOrEmpty(_database.ConnectionString))
            {
                throw new CommandException(MsgResource.ConnectionStringMissing);
            }

            IDbTransaction transaction = null;
            if (_database.ConnectionState == ConnectionState.Closed)
            {
                bool r = _database.Open();
                if (r)
                {
                    transaction = _database.BeginTransaction(isolationLevel,activeTime);
                    _isInTransaction = (transaction != null);
                }
                else
                {
                    _isInTransaction = false;
                }
            }
            else if (_database.ConnectionState == ConnectionState.Open && !_isInTransaction)
            {
                transaction = _database.BeginTransaction(isolationLevel, activeTime);
                _isInTransaction = (transaction != null);
            }
            return transaction;

        }

        /// <summary>
        /// Commits a transaction.
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            if (_database.ConnectionState == ConnectionState.Open)
            {
                bool r = _database.Commit();
                if (r)
                {
                    _isInTransaction = false;
                    return _database.Close();
                }
            }
            return false;
        }

        /// <summary>
        /// Rollbacks a transaction.
        /// </summary>
        /// <returns></returns>
        public bool Rollback()
        {
            if (_database.ConnectionState == ConnectionState.Open)
            {
                bool r = _database.Rollback();
                if (r)
                {
                    _isInTransaction = false;
                    return _database.Close();
                }
            }
            return false;
        }

        /// <summary>
        /// Closes the connection with database.
        /// </summary>
        private void CloseConnection()
        {
            if (!_isInTransaction && this._database.ConnectionState == ConnectionState.Open)
            {
                this._database.Close();
            }
        }

        private void OpenConnection()
        {
            if (string.IsNullOrEmpty(_database.ConnectionString))
            {
                throw new ConnectionException(MsgResource.ConnectionStringMissing, _database.ConnectionString);
            }
           
            if (this._database.ConnectionState == ConnectionState.Closed)
            {
                this._database.Open();
            }
        }


        #endregion  //Transaction



        #region ExecuteReader



        public IDataReader ExecuteReader(string commandText, bool isProcedure)
        {
            OpenConnection();
            IDataReader reader = _database.ExecuteReader(commandText, isProcedure);
            //CloseConnection();
            return reader;
        }

        public IDataReader ExecuteReader(string commandText, bool isProcedure, IDataParameter parameter)
        {
            OpenConnection();
            IDataReader reader = _database.ExecuteReader(commandText, isProcedure, parameter);
            //CloseConnection();
            return reader;
        }

        public IDataReader ExecuteReader(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            OpenConnection();
            IDataReader reader = _database.ExecuteReader(commandText, isProcedure, parameters);
            //CloseConnection();
            return reader;
        }


        #endregion


        #region ExecuteNonQuery


        /// <summary>
        /// Executes a SQL statement or procedure ,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, bool isProcedure)
        {
            OpenConnection();
            int count = _database.ExecuteNonQuery(commandText, isProcedure);
            CloseConnection();
            return count;
        }

        /// <summary>
        /// Executes a procedure ,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameter"></param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, bool isProcedure, IDataParameter parameter)
        {
            OpenConnection();
            int count = _database.ExecuteNonQuery(commandText, isProcedure, parameter);
            CloseConnection();
            return count;
        }

        /// <summary>
        /// Executes a procedure ,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            OpenConnection();
            int count = _database.ExecuteNonQuery(commandText, isProcedure, parameters);
            CloseConnection();
            return count;
        }

        //public int ExecuteNonQuery(string commandText, bool isProcedure, IDataParameterCollection parameters)
        //{
        //    OpenConnection();
        //    int count = _database.ExecuteNonQuery(commandText, isProcedure, parameters);
        //    CloseConnection();
        //    return count;
        //}

        #endregion


        #region ExecuteScalar


        /// <summary>
        ///  Executes the query ,and returns the first column of the first row .
        /// </summary>
        /// <param name="commandText">a SQL statement or procedure</param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, bool isProcedure)
        {
            OpenConnection();
            object obj = _database.ExecuteScalar(commandText, isProcedure);
            CloseConnection();
            return obj;
        }

        /// <summary>
        /// Executes the query ,and returns the first column of the first row .
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, bool isProcedure, IDataParameter parameter)
        {
            OpenConnection();
            object obj = _database.ExecuteScalar(commandText, isProcedure, parameter);
            CloseConnection();
            return obj;
        }

        /// <summary>
        ///  Executes the query ,and returns the first column of the first row .
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            OpenConnection();
            object obj = _database.ExecuteScalar(commandText, isProcedure, parameters);
            CloseConnection();
            return obj;
        }

        #endregion


        #region ExecuteDataSet



        public DataSet ExecuteDataSet(string commandText, bool isProcedure)
        {
            OpenConnection();
            DataSet ds = _database.ExecuteDataSet(commandText, isProcedure);
            CloseConnection();
            return ds;
        }

        public DataSet ExecuteDataSet(string commandText, bool isProcedure, IDataParameter parameter)
        {
            OpenConnection();
            DataSet ds = _database.ExecuteDataSet(commandText, isProcedure, parameter);
            CloseConnection();
            return ds;
        }

        public DataSet ExecuteDataSet(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            OpenConnection();
            DataSet ds = _database.ExecuteDataSet(commandText, isProcedure, parameters);
            CloseConnection();
            return ds;
        }


        #endregion


        #region ExecuteDataTable

        public DataTable ExecuteDataTable(string commandText, bool isProcedure)
        {
            OpenConnection();
            DataTable dt = _database.ExecuteDataTable(commandText, isProcedure);
            CloseConnection();
            return dt;
        }

        public DataTable ExecuteDataTable(string commandText, bool isProcedure, IDataParameter parameter)
        {
            OpenConnection();
            DataTable dt = _database.ExecuteDataTable(commandText, isProcedure, parameter);
            CloseConnection();
            return dt;
        }

        public DataTable ExecuteDataTable(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            OpenConnection();
            DataTable dt = _database.ExecuteDataTable(commandText, isProcedure, parameters);
            CloseConnection();
            return dt;
        }

    
        #endregion

        public override string ToString()
        {
            return _database.Command.CommandText;
        }

    }
}
