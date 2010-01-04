using System;
using System.Data;
using System.Data.OleDb;
using System.Reflection;

namespace Gean.Data
{
    /// <summary>
    ///
    /// </summary>
    internal class OleDbDatabase : Database
    {
        public OleDbDatabase()
        {
            CreateDatabaseObjects();
            StartupDatabaseObjects();
        }

        private void CreateDatabaseObjects()
        {
            base.Connection = new OleDbConnection();
            base.Command = new OleDbCommand();
            base.DataAdapter = new OleDbDataAdapter();
            base.DatabaseType = DatabaseType.Oracle;
        }

        private void StartupDatabaseObjects()
        {
            base.Command.Connection = base.Connection;
            base.DataAdapter.SelectCommand = base.Command;
        }


        public override void ClearAllPools()
        {
            OleDbConnection.ReleaseObjectPool();
        }

        public override void ClearPool()
        {
            OleDbConnection.ReleaseObjectPool();
        }
    }
}
