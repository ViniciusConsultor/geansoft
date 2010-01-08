using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Gean.Data
{
    /// <summary>
    /// GROUP BY 子句生成器。将记录与指定字段中的相等值组合成单一记录。如果使 SQL 合计函数，例如Sum或Count，蕴含于 SELECT 语句中，会创建一个各记录的总计值。
    /// </summary>
    public class SQLGroupByTextBuilder : ISQLTextBuilder
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
