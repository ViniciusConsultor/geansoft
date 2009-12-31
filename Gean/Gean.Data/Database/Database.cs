using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Globalization;
using System.Reflection;
using System.Text;
using Gean.Data.Resources;

namespace Gean.Data
{
    /// <summary>
    /// Base class .
    /// Last updated : 2008.01.02
    /// </summary>
    /// 
    internal abstract class Database : System.MarshalByRefObject, IDatabase, IDisposable
    {

        private IDbConnection _connection;
        private IDbDataAdapter _dataAdapter;
        private IDbCommand _command;
        private IDbTransaction _transaction;

        private string _connectionString = "";

        //private DataSet m_DataSet;
        //private DataTable m_DataTable;

        private DatabaseType _databaseType = DatabaseType.SqlServer;


        #region "Properties"

        #region protected

        public ConnectionState ConnectionState
        {
            get
            {
                return Connection.State;
            }
        }

        public IDbConnection Connection
        {
            get
            {
                //if (_connection == null)
                //{
                //    CreateDatabaseObjects();
                //}
                return _connection;
            }
            internal set
            {
                _connection = value;
            }
        }

        public IDbDataAdapter DataAdapter
        {
            get
            {
                //if (_dataAdapter == null)
                //{
                //    CreateDatabaseObjects();
                //}
                return _dataAdapter;
            }
            internal set
            {
                _dataAdapter = value;
            }
        }

        public IDbCommand Command
        {
            get
            {
                //if (_command == null)
                //{
                //    CreateDatabaseObjects();
                //}
                return _command;
            }
            internal set
            {
                _command = value;
            }
        }

        public IDbTransaction Transaction
        {
            get
            {
                return _transaction;
            }
            //set
            //{
            //    _transaction = value;
            //}
        }

        #endregion

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                this._connectionString = value;
                if (this.Connection != null)
                {
                    this.Connection.ConnectionString = this._connectionString;
                }
            }
        }

        public IDataParameterCollection Parameters
        {
            get
            {
                return this.Command.Parameters;
            }
        }

        public DatabaseType DatabaseType
        {
            get
            {
                return _databaseType;
            }
            internal set
            {
                _databaseType = value;
            }
        }

        #endregion

        internal protected Database()
        {
        }

        /// <summary>
        /// Derive class must implement this method.
        /// </summary>
        //internal protected abstract void CreateDatabaseObjects();//Factory Method

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._transaction != null)
                {
                    this._transaction.Dispose();
                }

                if (this.Command != null)
                {
                    this.Command.Dispose();
                }

                if (this.Connection != null)
                {
                    this.Connection.Dispose();
                }
            }
        }

        #region Connection

        public bool Close()
        {
            try
            {
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Dispose();
                this.ClearAllPools();
                throw new ConnectionException(MsgResource.CloseDatabaseFailed + ex.Message, ex);
            }
        }


        public bool Open()
        {
            if (string.IsNullOrEmpty(this._connectionString))
            {
                throw new ConnectionException(MsgResource.ConnectionStringMissing);
            }

            try
            {
                this.Connection.ConnectionString = this._connectionString;
                if (this.Connection.State != ConnectionState.Closed)
                {
                    this.Connection.Close();
                }
                this.Connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                this.Dispose();
                this.ClearAllPools();
                throw new ConnectionException(MsgResource.OpenDatabaseFailed + ex.Message, ex);
            }
        }


        public bool Open(string connectionString)
        {
            if (string.IsNullOrEmpty(this._connectionString))
            {
                return false;
            }
            this._connectionString = connectionString;
            return this.Open();
        }

        public bool ChangeDatabase(string databaseName)
        {
            try
            {
                this.Connection.ChangeDatabase(databaseName);
                return true;
            }
            catch (Exception ex)
            {
                this.Dispose();
                this.ClearAllPools();
                throw new ConnectionException(MsgResource.ChangeDatabaseFailed, ex);
            }
        }

        public abstract void ClearAllPools();
        public abstract void ClearPool();

        #endregion

        #region Transaction

        /// <summary>
        /// Begins a database transaction with 60s active time.
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            try
            {
                this._transaction = this.Connection.BeginTransaction();
                return this._transaction;
            }
            catch (Exception ex)
            {
                this._transaction = null;
                throw new GatewayException(MsgResource.BeginTransactionFailed + ex.Message, ex);
            }
        }

        /// <summary>
        /// Begins a database transaction with specified active time.
        /// </summary>
        /// <param name="activeTime"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(double activeTime)
        {
            this.BeginTransaction();
            if (this._transaction != null)
            {
                TransactionCleaner transactionCleaner = new TransactionCleaner(this._transaction, activeTime);
            }
            return this._transaction;
        }


        /// <summary>
        /// Begins a database transaction with specified IsolationLevel value.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            try
            {
                this._transaction = this.Connection.BeginTransaction(isolationLevel);
                return this._transaction;
            }
            catch (Exception ex)
            {
                this._transaction = null;
                throw new GatewayException(MsgResource.BeginTransactionFailed + ex.Message, ex);
            }
        }

        /// <summary>
        /// Begins a database transaction with specified IsolationLevel value and active time.
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <param name="activeTime"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel, double activeTime)
        {
            this.BeginTransaction(isolationLevel);
            if (this._transaction != null)
            {
                TransactionCleaner transactionCleaner = new TransactionCleaner(this._transaction, activeTime);
            }
            return this._transaction;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            try
            {
                if (this._transaction != null)
                {
                    this._transaction.Commit();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new GatewayException(MsgResource.CommitTransactionFailed + ex.Message, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Rollback()
        {
            try
            {
                if (this._transaction != null)
                {
                    this._transaction.Rollback();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new GatewayException(MsgResource.RollbackTransactionFailed + ex.Message, ex);
            }
        }

        private void OnException(DbException ex)
        {
            this.Rollback();
            this.Close();
            this.Dispose();
        }

        #endregion

        //public IDataParameter CreateParameter(string parameterName, object parameterValue)
        //{
        //    return ParameterBuilder.BuildParameter(this._databaseType, parameterName, parameterValue);
        //}

        //public List<IDataParameter> CreateParameters(string[] parameterNames, object[] parameterValues)
        //{
        //    return ParameterBuilder.BuildParameters(this._databaseType, parameterNames, parameterValues);
        //}

        public ITableCommandExecutor CreateTableCommandExecutor()
        {
            return new CommandExecutor(this);
        }

        public IViewCommandExecutor CreateViewCommandExecutor()
        {
            return new CommandExecutor(this);
        }

        #region CommandParameter

        private void ResetCommandType(bool isStoredProcedure)
        {
            if (isStoredProcedure)
            {
                this.Command.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                this.Command.CommandType = CommandType.Text;
            }
        }

        private void ResetCommandParameters()
        {
            this.Command.Parameters.Clear();
        }
        /// <summary>
        /// Clears the previous parameters of command ,and reassigns parameters .
        /// </summary>
        /// <param name="parameter"></param>
        private void ResetCommandParameters(IDataParameter parameter)
        {
            this.Command.Parameters.Clear();
            this.Command.Parameters.Add(parameter);
        }

        private void ResetCommandParameters(IList<IDataParameter> parameters)
        {
            this.Command.Parameters.Clear();
            foreach (IDataParameter parameter in parameters)
            {
                this.Command.Parameters.Add(parameter);
            }
        }

        private void ResetCommandParameters(IDataParameterCollection parameters)
        {
            this.Command.Parameters.Clear();
            foreach (IDataParameter parameter in parameters)
            {
                this.Command.Parameters.Add(parameter);
            }
        }


        #endregion

        #region ExecuteReader

        private IDataReader ExecuteReader(string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new CommandException(MsgResource.CommandTextMissing);
            }
            try
            {
                this.Command.Transaction = this._transaction;
                this.Command.CommandText = commandText;
                return Command.ExecuteReader();
            }
            catch (DbException ex)
            {
                OnException(ex);
                throw new CommandException(commandText, MsgResource.ExecuteReaderFailed + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new CommandException(commandText, MsgResource.ExecuteReaderFailed + ex.Message, ex);
            }

        }

        public IDataReader ExecuteReader(string commandText, bool isProcedure)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters();
            return ExecuteReader(commandText);

        }

        public IDataReader ExecuteReader(string commandText, bool isProcedure, IDataParameter parameter)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameter);
            return ExecuteReader(commandText);

        }

        public IDataReader ExecuteReader(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameters);
            return ExecuteReader(commandText);

        }

        public IDataReader ExecuteReader(string commandText, bool isProcedure, IDataParameterCollection parameters)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameters);
            return ExecuteReader(commandText);

        }

        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// Executes a SQL statement or commandText ,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <returns>The number of rows affected.</returns>
        private int ExecuteNonQuery(string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new CommandException(MsgResource.CommandTextMissing);
            }
            int rowCount = 0;
            try
            {
                this.Command.Transaction = this._transaction;
                this.Command.CommandText = commandText;
                rowCount = Command.ExecuteNonQuery();
                return rowCount;
            }
            catch (DbException ex)
            {
                OnException(ex);
                throw new CommandException(commandText, MsgResource.ExecuteNonQueryFailed + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new CommandException(commandText, MsgResource.ExecuteNonQueryFailed + ex.Message, ex);
            }

        }

        /// <summary>
        /// Executes a SQL statement or procedure,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteNonQuery(string commandText, bool isProcedure)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters();
            return ExecuteNonQuery(commandText);

        }

        /// <summary>
        /// Executes a SQL statement or procedure,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, bool isProcedure, IDataParameter parameter)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameter);
            return ExecuteNonQuery(commandText);

        }

        /// <summary>
        ///Executes a SQL statement or procedure,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameters);
            return ExecuteNonQuery(commandText);

        }

        /// <summary>
        /// Executes a SQL statement or procedure,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, bool isProcedure, IDataParameterCollection parameters)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameters);
            return ExecuteNonQuery(commandText);

        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// Executes the query ,and returns the first column of the first row .
        /// </summary>
        /// <param name="commandText">a SQL statement or procedure</param>
        /// <returns></returns>
        private object ExecuteScalar(string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new CommandException(MsgResource.CommandTextMissing);
            }
            try
            {
                Command.Transaction = this._transaction;
                Command.CommandText = commandText;
                return Command.ExecuteScalar();
            }
            catch (DbException ex)
            {
                OnException(ex);
                throw new CommandException(commandText, MsgResource.ExecuteScalarFailed + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new CommandException(commandText, MsgResource.ExecuteScalarFailed + ex.Message, ex);
            }
        }

        /// <summary>
        /// Executes the query,and returns the first column of the first row.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, bool isProcedure)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters();
            return ExecuteScalar(commandText);
        }

        /// <summary>
        ///  Executes the query,and returns the first column of the first row.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, bool isProcedure, IDataParameter parameter)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameter);
            return ExecuteNonQuery(commandText);
        }

        /// <summary>
        ///  Executes the query,and returns the first column of the first row.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameters);
            return ExecuteScalar(commandText);
        }

        /// <summary>
        ///  Executes the query,and returns the first column of the first row.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, bool isProcedure, IDataParameterCollection parameters)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameters);
            return ExecuteScalar(commandText);
        }

        #endregion

        #region ExecuteDataSet


        private DataSet ExecuteDataSet(string commandText)
        {
            if (string.IsNullOrEmpty(commandText))
            {
                throw new CommandException(MsgResource.CommandTextMissing);
            }
            try
            {
                this.Command.CommandText = commandText;
                DataSet dataSet = new DataSet("NewDataSet");
                dataSet.Locale = CultureInfo.InvariantCulture;
                this.DataAdapter.Fill(dataSet);
                return dataSet;
            }
            catch (DbException ex)
            {
                OnException(ex);
                throw new CommandException(commandText, MsgResource.GetDataSetFailed + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new CommandException(commandText, MsgResource.GetDataSetFailed + ex.Message, ex);
            }
        }

        public DataSet ExecuteDataSet(string commandText, bool isProcedure)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters();
            return ExecuteDataSet(commandText);
        }

        public DataSet ExecuteDataSet(string commandText, bool isProcedure, IDataParameter parameter)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameter);
            return ExecuteDataSet(commandText);

        }

        public DataSet ExecuteDataSet(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameters);
            return ExecuteDataSet(commandText);
        }

        public DataSet ExecuteDataSet(string commandText, bool isProcedure, IDataParameterCollection parameters)
        {
            ResetCommandType(isProcedure);
            ResetCommandParameters(parameters);
            return ExecuteDataSet(commandText);

        }


        #endregion

        #region ExecuteDataTable

        private DataTable ExecuteDataTable(string commandText)
        {
            DataSet ds = ExecuteDataSet(commandText);
            return ds.Tables.Count > 0 ? ds.Tables[0].Copy() : new DataTable("NewDataTable");
        }


        public DataTable ExecuteDataTable(string commandText, bool isProcedure)
        {
            DataSet ds = ExecuteDataSet(commandText, isProcedure);
            return ds.Tables.Count > 0 ? ds.Tables[0].Copy() : new DataTable("NewDataTable");
        }

        public DataTable ExecuteDataTable(string commandText, bool isProcedure, IDataParameter parameter)
        {
            DataSet ds = ExecuteDataSet(commandText, isProcedure, parameter);
            return ds.Tables.Count > 0 ? ds.Tables[0].Copy() : new DataTable("NewDataTable");
        }

        public DataTable ExecuteDataTable(string commandText, bool isProcedure, IList<IDataParameter> parameters)
        {
            DataSet ds = ExecuteDataSet(commandText, isProcedure, parameters);
            return ds.Tables.Count > 0 ? ds.Tables[0].Copy() : new DataTable("NewDataTable");
        }

        public DataTable ExecuteDataTable(string commandText, bool isProcedure, IDataParameterCollection parameters)
        {
            DataSet ds = ExecuteDataSet(commandText, isProcedure, parameters);
            return ds.Tables.Count > 0 ? ds.Tables[0].Copy() : new DataTable("NewDataTable");
        }

        #endregion

        public override string ToString()
        {
            return this._connectionString;
        }

    }

    /// <summary>
    /// Represents the exception that raises when executing a command.
    /// </summary>
    [Serializable]
    public class GatewayException : ApplicationException
    {
        private object _customSource;
        private string _message;

        public object CustomSource
        {
            get
            {
                return _customSource;
            }
        }

        public string Text
        {
            get
            {
                return _message;
            }
        }

        public GatewayException()
            : base()
        {
        }

        public GatewayException(string message)
            : base(message)
        {
            _message = message;
        }

        public GatewayException(string message, object customSource)
            : base(message)
        {
            _customSource = customSource;
            _message = message;
        }

        public GatewayException(string message, object customSource, Exception ex)
            : base(message, ex)
        {
            _customSource = customSource;
            _message = message;
        }

        public GatewayException(string message, Exception ex)
            : base(message, ex)
        {
            _message = message;
        }

        //protected GatewayException(SerializationInfo info, StreamingContext context)
        //    : base(info, context)
        //{
        //    if (info == null)
        //    {
        //        throw new ArgumentNullException("info");
        //    }
        //    _message = info.GetString("Text");
        //}

        //protected virtual void GetObjectData(SerializationInfo info, StreamingContext context) 
        //{
        //    info.AddValue("Text", _message); 
        //}

        //[SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        //void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context) 
        //{
        //    if (info == null)
        //    {
        //        throw new ArgumentNullException("info");
        //    }
        //    GetObjectData(info, context); 
        //}

        public override string ToString()
        {
            return this.Message;
        }

    }
}
