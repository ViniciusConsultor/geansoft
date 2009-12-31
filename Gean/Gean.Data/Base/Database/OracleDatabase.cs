using System;
using System.Data;
using System.Data.OracleClient;
using System.Reflection;

namespace Gean.Data
{
	/// <summary>
	///
	/// </summary>
    internal class OracleDatabase : Database
	{
        public OracleDatabase()
		{
            CreateDatabaseObjects();
            StartupDatabaseObjects();
		}

        private void CreateDatabaseObjects()
        {
            base.Connection = new  OracleConnection();
            base.Command = new OracleCommand();
            base.DataAdapter = new OracleDataAdapter();
            base.DatabaseType = DatabaseType.Oracle;
        }

        private void StartupDatabaseObjects()
        {
            base.Command.Connection = base.Connection;
            base.DataAdapter.SelectCommand = base.Command;
        }


        public override void ClearAllPools()
        {
            OracleConnection.ClearAllPools();
        }

        public override void ClearPool()
        {
            OracleConnection.ClearPool(base.Connection as OracleConnection);
        }
    }
}
