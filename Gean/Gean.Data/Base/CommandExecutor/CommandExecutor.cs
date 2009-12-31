using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Gean.Data.Resources;
using Gean.Data.Exceptions;

namespace Gean.Data
{
    /// <summary>
    /// Executes sql statement or stored procedure
    /// </summary>
    internal class CommandExecutor : ITableCommandExecutor
    {
        #region Properties

        public IDatabase Database { get; private set; }

        public bool IsInTransaction { get; private set; }

        #endregion

        public CommandExecutor(IDatabase database)
        {
            if (database == null)
            {
                throw new CommandException(Messages.InvalidArguments + "IDatabase database");
            }
            this.Database = database;
        }

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
            if (string.IsNullOrEmpty(Database.ConnectionString))
            {
                throw new CommandException(Messages.ConnectionStringMissing);
            }

            IDbTransaction transaction = null;
            if (Database.ConnectionState == ConnectionState.Closed)
            {
                bool r = Database.Open();
                if (r)
                {
                     transaction= Database.BeginTransaction(activeTime);
                     IsInTransaction = (transaction != null);
                }
                else
                {
                    IsInTransaction = false;
                }
            }
            else if (Database.ConnectionState == ConnectionState.Open && !IsInTransaction)
            {
                transaction = Database.BeginTransaction(activeTime);
                IsInTransaction = (transaction != null);
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
            if (string.IsNullOrEmpty(Database.ConnectionString))
            {
                throw new CommandException(Messages.ConnectionStringMissing);
            }

            IDbTransaction transaction = null;
            if (Database.ConnectionState == ConnectionState.Closed)
            {
                bool r = Database.Open();
                if (r)
                {
                    transaction = Database.BeginTransaction(isolationLevel,activeTime);
                    IsInTransaction = (transaction != null);
                }
                else
                {
                    IsInTransaction = false;
                }
            }
            else if (Database.ConnectionState == ConnectionState.Open && !IsInTransaction)
            {
                transaction = Database.BeginTransaction(isolationLevel, activeTime);
                IsInTransaction = (transaction != null);
            }
            return transaction;

        }

        /// <summary>
        /// Commits a transaction.
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            if (Database.ConnectionState == ConnectionState.Open)
            {
                bool r = Database.Commit();
                if (r)
                {
                    IsInTransaction = false;
                    return Database.Close();
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
            if (Database.ConnectionState == ConnectionState.Open)
            {
                bool r = Database.Rollback();
                if (r)
                {
                    IsInTransaction = false;
                    return Database.Close();
                }
            }
            return false;
        }

        /// <summary>
        /// Closes the connection with database.
        /// </summary>
        private void CloseConnection()
        {
            if (!IsInTransaction && this.Database.ConnectionState == ConnectionState.Open)
            {
                this.Database.Close();
            }
        }

        private void OpenConnection()
        {
            if (string.IsNullOrEmpty(Database.ConnectionString))
            {
                throw new ConnectionException(Messages.ConnectionStringMissing, Database.ConnectionString);
            }
           
            if (this.Database.ConnectionState == ConnectionState.Closed)
            {
                this.Database.Open();
            }
        }

        #endregion  //Transaction

        #region ExecuteReader



        public IDataReader ExecuteReader(string commandText, bool isProcedure)
        {
            OpenConnection();
            IDataReader reader = Database.ExecuteReader(commandText, isProcedure);
            //CloseConnection();
            return reader;
        }

        public IDataReader ExecuteReader(string commandText, bool isProcedure, IDataParameter parameter)
        {
            OpenConnection();
            IDataReader reader = Database.ExecuteReader(commandText, isProcedure, parameter);
            //CloseConnection();
            return reader;
        }

        public IDataReader ExecuteReader(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            OpenConnection();
            IDataReader reader = Database.ExecuteReader(commandText, isProcedure, parameters);
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
            int count = Database.ExecuteNonQuery(commandText, isProcedure);
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
            int count = Database.ExecuteNonQuery(commandText, isProcedure, parameter);
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
            int count = Database.ExecuteNonQuery(commandText, isProcedure, parameters);
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
            object obj = Database.ExecuteScalar(commandText, isProcedure);
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
            object obj = Database.ExecuteScalar(commandText, isProcedure, parameter);
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
            object obj = Database.ExecuteScalar(commandText, isProcedure, parameters);
            CloseConnection();
            return obj;
        }

        #endregion

        #region ExecuteDataSet



        public DataSet ExecuteDataSet(string commandText, bool isProcedure)
        {
            OpenConnection();
            DataSet ds = Database.ExecuteDataSet(commandText, isProcedure);
            CloseConnection();
            return ds;
        }

        public DataSet ExecuteDataSet(string commandText, bool isProcedure, IDataParameter parameter)
        {
            OpenConnection();
            DataSet ds = Database.ExecuteDataSet(commandText, isProcedure, parameter);
            CloseConnection();
            return ds;
        }

        public DataSet ExecuteDataSet(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            OpenConnection();
            DataSet ds = Database.ExecuteDataSet(commandText, isProcedure, parameters);
            CloseConnection();
            return ds;
        }


        #endregion

        #region ExecuteDataTable

        public DataTable ExecuteDataTable(string commandText, bool isProcedure)
        {
            OpenConnection();
            DataTable dt = Database.ExecuteDataTable(commandText, isProcedure);
            CloseConnection();
            return dt;
        }

        public DataTable ExecuteDataTable(string commandText, bool isProcedure, IDataParameter parameter)
        {
            OpenConnection();
            DataTable dt = Database.ExecuteDataTable(commandText, isProcedure, parameter);
            CloseConnection();
            return dt;
        }

        public DataTable ExecuteDataTable(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            OpenConnection();
            DataTable dt = Database.ExecuteDataTable(commandText, isProcedure, parameters);
            CloseConnection();
            return dt;
        }

    
        #endregion

        public override string ToString()
        {
            return Database.Command.CommandText;
        }
    }
}
