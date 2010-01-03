using System;
using System.Data;
using System.Configuration;
using System.Threading;
using System.Diagnostics;
using System.Collections;

namespace Gean.Data
{
    /// <summary>
    /// 事务管理器。<see cref="TransactionManager"/>用于使得<see cref="BaseEntity"/>启动事务。因为<see cref="TransactionManager"/>使用ADO.NET事务，所以没有分布式事务。如果有两个或两个以上<see cref="BaseEntity"/>的需求被保存为一个事务，只需要使用<see cref="TransactionManager"/>。当然，<see cref="BaseEntity.Save()"/>方法的核心就是一个事务。
    /// 
    /// 从MyGeneration.dOOdads移植。
    /// 2010-01-02 1:15:23
    /// </summary>
    ///	<example>
    /// C#
    /// <code>
    /// TransactionManager tx = TransactionManager.ThreadTransactionManager();
    /// 
    /// try
    /// {
    /// 	tx.BeginTransaction();
    /// 	emps.Save();
    /// 	prds.Save();
    /// 	tx.CommitTransaction();
    /// }
    /// catch(Exception ex)
    /// {
    /// 	tx.RollbackTransaction();
    /// 	tx.ThreadTransactionManagerReset();
    /// }
    /// </code>
    /// </example>
    public class TransactionManager
    {
        /// <summary>
        /// 私有化构造函数。使用<see cref="ThreadTransactionManager"/>方法返回实例。
        /// </summary>
        protected TransactionManager()
        {

        }

        /// <summary>
        /// Returns the number of outstanding calls to <see cref="BeginTransaction"/> without subsequent calls to 
        /// <see cref="CommitTransaction"/>
        /// </summary>
        public int NestingCount
        {
            get
            {
                return this.txCount;
            }
        }

        /// <summary>
        /// True if <see cref="RollbackTransaction"/> has been called on this thread. 
        /// </summary>
        public bool HasBeenRolledBack
        {
            get
            {
                return hasRolledBack;
            }
        }

        /// <summary>
        /// BeginTransaction should always be a followed by a call to CommitTransaction if all goes well, or
        /// RollbackTransaction if problems are detected.  BeginTransaction() can be nested any number of times
        /// as long as each call is unwound with a call to CommitTransaction().
        /// </summary>
        public void BeginTransaction()
        {
            if (hasRolledBack) throw new Exception("Transaction Rolledback");

            txCount = txCount + 1;
        }

        /// <summary>
        /// The final call to CommitTransaction commits the transaction to the database, BeginTransaction and
        /// CommitTransaction calls can be nested, <see cref="BeginTransaction"/>
        /// </summary>
        public void CommitTransaction()
        {
            if (hasRolledBack) throw new Exception("Transaction Rolledback");

            txCount = txCount - 1;

            if (txCount == 0)
            {
                foreach (Transaction tx in this.transactions.Values)
                {
                    tx.sqlTx.Commit();
                    tx.Dispose();
                }

                this.transactions.Clear();

                if (this._objectsInTransaction != null)
                {
                    try
                    {
                        foreach (BaseEntity entity in this._objectsInTransaction)
                        {
                            entity.AcceptChanges();
                        }
                    }
                    catch { }

                    this._objectsInTransaction = null;
                }
            }
        }

        /// <summary>
        /// RollbackTransaction dooms the transaction regardless of nested calls to BeginTransaction. Once this method is called
        /// nothing can be done to commit the transaction.  To reset the thread state a call to <see cref="ThreadTransactionManagerReset"/> must be made.
        /// You must call 
        /// </summary>
        public void RollbackTransaction()
        {
            if (false == hasRolledBack && txCount > 0)
            {
                foreach (Transaction tx in this.transactions.Values)
                {
                    tx.sqlTx.Rollback();
                    tx.Dispose();
                }

                this.transactions.Clear();
                this.txCount = 0;
                this._objectsInTransaction = null;
            }
        }

        /// <summary>
        /// Enlist by the dOOdads architecture when a IDbCommand (SqlCommand is an IDbCommand). The command may or may not be enrolled 
        /// in a transaction depending on whether or not BeginTransaction has been called. Each call to Enlist must be followed by a
        /// call to <see cref="DeEnlist"/>.
        /// </summary>
        /// <param name="cmd">Your SqlCommand, OleDbCommand, etc ...</param>
        /// <param name="entity">Your business entity, in C# use 'this', VB.NET use 'Me'.</param>
        /// <example>
        /// C#
        /// <code>
        /// txMgr.Enlist(cmd, this);
        /// cmd.ExecuteNonQuery();
        /// txMgr.DeEnlist(cmd, this);
        /// </code>
        /// VB.NET
        /// <code>
        /// txMgr.Enlist(cmd, Me)
        /// cmd.ExecuteNonQuery()
        /// txMgr.DeEnlist(cmd, Me)
        /// </code>
        /// </example>
        public void Enlist(IDbCommand cmd, BaseEntity entity)
        {
            if (txCount == 0 || entity._notRecommendedConnection != null)
            {
                // NotRecommendedConnections never play in dOOdad transactions
                cmd.Connection = CreateSqlConnection(entity);
            }
            else
            {
                string connStr = entity._config;
                if (entity._raw != "") connStr = entity._raw;

                Transaction tx = this.transactions[connStr] as Transaction;

                if (tx == null)
                {
                    IDbConnection sqlConn = CreateSqlConnection(entity);

                    tx = new Transaction();
                    tx.sqlConnection = sqlConn;

                    if (_isolationLevel != IsolationLevel.Unspecified)
                    {
                        tx.sqlTx = sqlConn.BeginTransaction(_isolationLevel);
                    }
                    else
                    {
                        tx.sqlTx = sqlConn.BeginTransaction();
                    }
                    this.transactions[connStr] = tx;
                }
                cmd.Connection = tx.sqlConnection;
                cmd.Transaction = tx.sqlTx;
            }
        }

        /// <summary>
        /// Each call to Enlist must be followed eventually by a call to DeEnlist.  
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="entity"></param>
        /// <example>
        /// C#
        /// <code>
        /// txMgr.Enlist(cmd, this);
        /// cmd.ExecuteNonQuery();
        /// txMgr.DeEnlist(cmd, this); 
        /// </code>
        /// VB.NET
        /// <code>>
        /// txMgr.Enlist(cmd, Me)
        /// cmd.ExecuteNonQuery()
        /// txMgr.DeEnlist(cmd, Me)
        /// </code>
        /// </example>
        public void DeEnlist(IDbCommand cmd, BaseEntity entity)
        {
            if (entity._notRecommendedConnection != null)
            {
                // NotRecommendedConnection never play in dOOdad transactions
                cmd.Connection = null;
            }
            else
            {
                if (txCount == 0)
                {
                    cmd.Connection.Dispose();
                }
            }
        }

        /// <summary>
        /// Called internally by BaseEntity
        /// </summary>
        /// <param name="entity"></param>
        internal void AddBaseEntity(BaseEntity entity)
        {
            if (_objectsInTransaction == null)
            {
                _objectsInTransaction = new ArrayList();
            }

            _objectsInTransaction.Add(entity);
        }

        private IDbConnection CreateSqlConnection(BaseEntity entity)
        {
            IDbConnection cn;

            if (entity._notRecommendedConnection != null)
            {
                // This is assumed to be open
                cn = entity._notRecommendedConnection;
            }
            else
            {
                cn = entity.CreateIDbConnection();

                if (entity._raw != "")
                    cn.ConnectionString = entity._raw;
                else
#if(VS2005)
					cn.ConnectionString = ConfigurationManager.AppSettings[entity._config];
#else
                    cn.ConnectionString = ConfigurationSettings.AppSettings[entity._config];
#endif

                cn.Open();
            }

            return cn;
        }

        private Hashtable transactions = new Hashtable();
        private int txCount = 0;
        private bool hasRolledBack = false;

        /// <summary>
        /// Used to control AcceptChanges()
        /// </summary>
        internal ArrayList _objectsInTransaction = null;

        #region "static"
        /// <summary>
        /// This static method is how you obtain a reference to the TransactionManager. You cannot call "new" on TransactionManager.
        /// If a TransactionManager doesn't exist on the current thread, one is created and returned to you.
        /// </summary>
        /// <returns>The one and only TransactionManager for this thread.</returns>
        public static TransactionManager ThreadTransactionManager()
        {
            TransactionManager txMgr = null;

            object obj = Thread.GetData(_txMgrSlot);

            if (obj != null)
            {
                txMgr = (TransactionManager)obj;
            }
            else
            {
                txMgr = new TransactionManager();
                Thread.SetData(_txMgrSlot, txMgr);
            }

            return txMgr;
        }

        /// <summary>
        /// This must be called after RollbackTransaction or no futher database activity will happen successfully on the current thread.
        /// </summary>
        public static void ThreadTransactionManagerReset()
        {
            TransactionManager txMgr = TransactionManager.ThreadTransactionManager();

            try
            {
                if (txMgr.txCount > 0 && txMgr.hasRolledBack == false)
                {
                    txMgr.RollbackTransaction();
                }
            }
            catch { }

            Thread.SetData(_txMgrSlot, null);
        }

        /// <summary>
        /// This is the Transaction's strength. The default is "IsolationLevel.Unspecified, the strongest is "IsolationLevel.Serializable" which is what
        /// is recommended for serious enterprize level projects.
        /// </summary>
        public static IsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
            set { _isolationLevel = value; }
        }
        private static IsolationLevel _isolationLevel = IsolationLevel.Unspecified;

        private static LocalDataStoreSlot _txMgrSlot = Thread.AllocateDataSlot();
        #endregion

        #region class Transaction
        /// <summary>
        /// 可能出现在同一时间出现多事务时。有一个连接字符串。
        /// </summary>
        private class Transaction : IDisposable
        {
            public IDbTransaction sqlTx = null;
            public IDbConnection sqlConnection = null;

            #region IDisposable Members
            public void Dispose()
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                }

                if (sqlTx != null)
                {
                    sqlTx.Dispose();
                }
            }
            #endregion
        }
        #endregion
    }
}
