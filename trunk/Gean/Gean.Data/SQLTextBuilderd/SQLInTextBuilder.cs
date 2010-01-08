using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Data.Common;

namespace Gean.Data
{
    /// <summary>
    /// Where... In...子句生成器。IN 操作符允许我们在 WHERE 子句中规定多个值。
    /// </summary>
    public class SQLInTextBuilder : StringCollection, ISQLTextBuilder
    {
        public SQLInTextBuilder()
        {
            //在这里添加构造函数逻辑。
        }

        public void Set(params string[] values)
        {
            this.AddRange(values);
        }

        public void Set(SQLTextBuilder builder)
        {
            this.Builder = builder;
        }

        public SQLTextBuilder Builder { get; set; }

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
            StringBuilder sb = new StringBuilder();
            if (Builder != null)
            {
                sb.Append('(').Append(Builder.ToSqlText()).Append(')');
            }
            else
            {
                sb.Append('(');
                foreach (var item in this)
                {
                    sb.Append('\'').Append(item).Append('\'').Append(',');
                }
                sb.Remove(sb.Length - 1, 1).Append(')');
            }
            return sb.ToString();
        }

        #endregion
    }
}
