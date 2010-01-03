//==============================================================================
// MyGeneration.dOOdads
//
// SQLiteEntity.cs
// Version 5.0
// Updated - 09/15/2005
//------------------------------------------------------------------------------
// Copyright 2004, 2005 by MyGeneration Software.
// All Rights Reserved.
//
// Permission to use, copy, modify, and distribute this software and its 
// documentation for any purpose and without fee is hereby granted, 
// provided that the above copyright notice appear in all copies and that 
// both that copyright notice and this permission notice appear in 
// supporting documentation. 
//
// MYGENERATION SOFTWARE DISCLAIMS ALL WARRANTIES WITH REGARD TO THIS 
// SOFTWARE, INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY 
// AND FITNESS, IN NO EVENT SHALL MYGENERATION SOFTWARE BE LIABLE FOR ANY 
// SPECIAL, INDIRECT OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES 
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, 
// WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER 
// TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE 
// OR PERFORMANCE OF THIS SOFTWARE. 
//==============================================================================

using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using Gean.Data;

namespace Gean.Data.SQLite
{
	/// <summary>
	/// VistaDBEntity is the VistaDB implementation of BusinessEntity
	/// </summary>
    public class SQLiteEntity : BaseEntity
    {
        public SQLiteEntity()
        {

        }

        public override DynamicQuery CreateDynamicQuery(BaseEntity entity)
        {
            return new SQLiteDynamicQuery(entity);
        }

        public override IDataParameter CreateIDataParameter(string name, object value)
        {
            SQLiteParameter p = new SQLiteParameter();
            p.ParameterName = name;
            p.Value = value;
            return p;
        }

        public override IDataParameter CreateIDataParameter()
        {
            return new SQLiteParameter();
        }

        public override IDbCommand CreateIDbCommand()
        {
            return new SQLiteCommand();
        }

        public override IDbDataAdapter CreateIDbDataAdapter()
        {
            return new SQLiteDataAdapter();
        }

        public override IDbConnection CreateIDbConnection()
        {
            return new SQLiteConnection();
        }

        public override DbDataAdapter ConvertIDbDataAdapter(IDbDataAdapter dataAdapter)
        {
            return (dataAdapter as SQLiteDataAdapter) as DbDataAdapter;
        }

        #region LastIdentity Logic

        // Overloaded in the generated class
        public virtual string GetAutoKeyColumns()
        {
            return "";
        }

        override protected void HookupRowUpdateEvents(DbDataAdapter adapter)
        {
            // We only bother hooking up the event if we have an AutoKey
            if (this.GetAutoKeyColumns().Length > 0)
            {
                SQLiteDataAdapter da = adapter as SQLiteDataAdapter;
                da.RowUpdated += new EventHandler<RowUpdatedEventArgs>(OnRowUpdated);
            }
        }

        // If it's an Insert we fetch the @@Identity value and stuff it in the proper column
        protected void OnRowUpdated(object sender, RowUpdatedEventArgs e)
        {
            try
            {
                if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
                {
                    TransactionManager txMgr = TransactionManager.ThreadTransactionManager();

                    string[] identityCols = this.GetAutoKeyColumns().Split(';');

                    SQLiteCommand cmd = new SQLiteCommand();

                    foreach (string col in identityCols)
                    {
                        cmd.CommandText = "SELECT last_insert_rowid()";

                        // We make sure we enlist in the ongoing transaction, otherwise, we 
                        // would most likely deadlock
                        txMgr.Enlist(cmd, this);
                        object o = cmd.ExecuteScalar(); // Get the Identity Value
                        txMgr.DeEnlist(cmd, this);

                        if (o != null)
                        {
                            e.Row[col] = o;
                        }
                    }

                    e.Row.AcceptChanges();
                }
            }
            catch { }
        }

        #endregion
    }

}
