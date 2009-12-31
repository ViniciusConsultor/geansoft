using System;
using System.Data;
using System.Collections.Generic;

namespace Gean.Data
{
    /// <summary>
    /// The interface of transation with database tables.
    /// </summary>
    public interface ITableCommandExecutor : IViewCommandExecutor
    {
        /// <summary>
        /// Indicats whether there is a database transaction.
        /// </summary>
        bool IsInTransaction
        {
            get;
        }

        /// <summary>
        /// Begins a database transaction with 60s active time.
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
        /// <returns></returns>
        int ExecuteNonQuery(string commandText, bool isProcedure);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns the number of rows affected.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string commandText, bool isProcedure, IList<IDataParameter> parameters);
    }
}
