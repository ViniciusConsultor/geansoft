using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Gean.Data
{
    public class SQLFromTextBuilder : ISQLTextBuilder
    {
        public SQLFromTextBuilder()
        {
        }

        public void Set(string tableName)
        {
            this.TableName = tableName;
        }

        public string TableName { get; set; }

        #region ISQLTextBuilder 成员

        public DbParameter[] GetDbParameters()
        {
            return null;
        }

        public string ToSqlTempletText()
        {
            return this.TableName;
        }

        public string ToSqlText()
        {
            return this.TableName;
        }

        #endregion
    }
}
