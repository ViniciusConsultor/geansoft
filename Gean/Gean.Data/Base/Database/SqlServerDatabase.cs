using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Gean.Data
{
	/// <summary>
	/// 
	/// </summary>
    internal class SqlServerDatabase : Database
	{
        public SqlServerDatabase()
		{
            CreateDatabaseObjects();
            StartupDatabaseObjects();
		}

        private void CreateDatabaseObjects()
        {
            base.Connection = new SqlConnection();
            base.Command = new SqlCommand();
            base.DataAdapter = new SqlDataAdapter();
            base.DatabaseType = DatabaseType.SqlServer;
        }

        private void StartupDatabaseObjects()
        {
            base.Command.Connection = base.Connection;
            base.DataAdapter.SelectCommand = base.Command;
        }

        public override void ClearAllPools()
        {
            SqlConnection.ClearAllPools();
        }

        public override void ClearPool()
        {
            SqlConnection.ClearPool(base.Connection as SqlConnection);
        }
    }
}