using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Data
{
    public class SQLOrderByTextBuilder : ISQLTextBuilder
    {
        #region ISQLTextBuilder 成员

        public System.Data.Common.DbParameter[] GetDbParameters()
        {
            throw new NotImplementedException();
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
