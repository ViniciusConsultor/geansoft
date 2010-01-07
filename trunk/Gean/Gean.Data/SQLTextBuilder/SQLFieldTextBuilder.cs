using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Gean.Data
{
    public class SQLFieldTextBuilder : ISQLTextBuilder
    {
        #region ISQLTextBuilder 成员

        public DbParameter[] GetDbParameters()
        {
            return null;
        }

        public string ToSqlTempletText()
        {
            throw new NotImplementedException();
        }

        public string ToSqlText()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
