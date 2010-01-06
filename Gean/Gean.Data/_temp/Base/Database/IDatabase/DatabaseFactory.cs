using System;
using System.Collections.Generic;
using System.Text;
using Gean.Data.Exceptions;
using Gean.Data.Resources;

namespace Gean.Data
{
    /// <summary>
    ///  Database factory to create instance of database interfaces.
    /// </summary>
    public static class DatabaseFactory
    {
        /// <summary>
        ///Creates instance of database interfaces to access SqlServer.
        /// </summary>
        /// <returns></returns>
        public static IDatabase CreateSqlServerDatabase()
        {
            return new SqlServerDatabase();
        }

        /// <summary>
        /// Creates instance of database interfaces to access SqlServer.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDatabase CreateSqlServerDatabase(string connectionString)
        {
            IDatabase database = new SqlServerDatabase();
            database.ConnectionString = connectionString;
            return database;
        }

        /// <summary>
        /// Creates instance of database interfaces to access Oracle.
        /// </summary>
        /// <returns></returns>
        public static IDatabase CreateOracleDatabase()
        {
            return new OracleDatabase();
        }

        /// <summary>
        /// Creates instance of database interfaces to access Oracle.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDatabase CreateOracleDatabase(string connectionString)
        {
            IDatabase database = new OracleDatabase();
            database.ConnectionString = connectionString;
            return database;
        }

        /// <summary>
        /// Creates instance of database interfaces to ole database.
        /// </summary>
        /// <returns></returns>
        public static IDatabase CreateOleDbDatabase()
        {
            IDatabase database = new OleDbDatabase();
            return database;
        }

        /// <summary>
        /// Creates instance of database interfaces to ole database.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDatabase CreateOleDbDatabase(string connectionString)
        {
            IDatabase database = new OleDbDatabase();
            database.ConnectionString = connectionString;
            return database;
        }

        /// <summary>
        /// Creates instance of database.
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static IDatabase CreateInstance(DatabaseType databaseType)
        {
            IDatabase database = null;
            switch (databaseType)
            {
                case DatabaseType.SqlServer: database = DatabaseFactory.CreateSqlServerDatabase();
                    break;
                case DatabaseType.Oracle: database = DatabaseFactory.CreateOracleDatabase();
                    break;
                case DatabaseType.OleDb: database = DatabaseFactory.CreateOleDbDatabase();
                    break;
                case DatabaseType.SQLite:
                case DatabaseType.MySql:
                default:
                    throw new GeanDataException(Messages.InvalidDatabaseType);
            }
            return database;
        }

    }
}
