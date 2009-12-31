using System;
using System.Data;
using System.Collections.Generic;

namespace Gean.Data
{
    /// <summary>
    /// The interface of query with database tables or views.
    /// </summary>
    public interface IViewCommandExecutor
    {
        /// <summary>
        /// The interface of database.
        /// </summary>
        IDatabase Database
        {
            get;
        }

        /// <summary>
        /// Creates an IDataParameter.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <returns></returns>
        //IDataParameter CreateParameter(string parameterName, object parameterValue);

        /// <summary>
        /// Creates an array of IDataParameter.
        /// </summary>
        /// <param name="parameterNames"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        //List<IDataParameter> CreateParameters(string[] parameterNames, object[] parameterValues);

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
        /// <param name="parameters"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string commandText, bool isProcedure, IList<IDataParameter> parameters);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns a DataSet.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(string commandText, bool isProcedure, IDataParameter parameter);

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
        /// Executes a procedure or sql statement ,and returns a DataTable.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        DataTable ExecuteDataTable(string commandText, bool isProcedure);

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
        /// <param name="parameters"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string commandText, bool isProcedure, IList<IDataParameter> parameters);

        /// <summary>
        /// Executes a procedure or sql statement ,and builds an IDataReader.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string commandText, bool isProcedure, IDataParameter parameter);

        /// <summary>
        /// Executes a procedure or sql statement ,and returns the first column of the first row of the resultset.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, bool isProcedure);

        /// <summary>
        ///  Executes a procedure or sql statement ,and returns the first column of the first row of the resultset.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, bool isProcedure, IDataParameter parameter);

        /// <summary>
        ///  Executes a procedure or sql statement ,and returns the first column of the first row of the resultset.
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="isProcedure"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        object ExecuteScalar(string commandText, bool isProcedure, IList<IDataParameter> parameters);
        
        /// <summary>
        /// A procedure or sql statement.
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
