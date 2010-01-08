using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Gean.Data
{
    /// <summary>
    /// HAVING 子句生成器。在SELECT语句中指定，显示哪些已用GROUP BY子句分组的记录。在GROUP BY组合了记录后，HAVING会显示GROUP BY子句分组的任何符合HAVING子句的记录。
    /// </summary>
    public class SQLHavingTextBuilder : ISQLTextBuilder
    {
        #region ISQLTextBuilder 成员

        public DbParameter[] GetDbParameters()
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
