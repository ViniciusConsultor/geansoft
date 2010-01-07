using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Gean.Data.Exceptions;
using Gean.Data.Resources;
using System.Collections.Specialized;
using System.Collections;

namespace Gean.Data
{
    /// <summary>
    /// 字段(列名)集合生成器。字段的名称，包含了要获取的数据。如果数据包含多个字段，则按列举顺序依次获取它们。
    /// </summary>
    public class SQLFieldTextBuilder : ISQLTextBuilder
    {
        /// <summary>
        /// 设置别名的关键字
        /// </summary>
        static readonly string AS = "AS";

        /// <summary>
        /// 列名与别名对应的集合
        /// </summary>
        protected Dictionary<string, string> _fieldList = new Dictionary<string, string>();

        /// <summary>
        /// 用指定的列名集合与指定的别名集合来设置列名与别名集合
        /// </summary>
        /// <param name="fields">指定的列名集合。</param>
        /// <param name="aliass">指定的别名集合。</param>
        public void Set(string[] fields, string[] aliass)
        {
            if (fields.Length != aliass.Length)
            {
                throw new SQLTextBuilderException(Messages.SQLTextBuilderError + " : fields.Length != aliass.Length");
            }
            for (int i = 0; i < fields.Length; i++)
            {
                _fieldList.Add(fields[i], aliass[i]);
            }
        }

        /// <summary>
        /// 用指定的列名与别名对集合来设置。
        /// </summary>
        /// <param name="pairs">The pairs.</param>
        public void Set(params Pair<string, string>[] pairs)
        {
            foreach (var item in pairs)
            {
                _fieldList.Add(item.First, item.Second);
            }
        }

        /// <summary>
        /// 用指定的列名集合设置。
        /// </summary>
        /// <param name="fields">指定的列名集合。</param>
        public void Set(IList<string> fields)
        {
            foreach (var item in fields)
            {
                _fieldList.Add(item, string.Empty);
            }
        }

        /// <summary>
        /// 用指定的列名集合设置。
        /// </summary>
        /// <param name="fields">指定的列名集合。</param>
        public void Set(params string[] fields)
        {
            foreach (var item in fields)
            {
                _fieldList.Add(item, string.Empty);
            }
        }

        #region ISQLTextBuilder 成员

        /// <summary>
        /// 获取该语句(子句)的所有参数
        /// </summary>
        /// <returns></returns>
        public DbParameter[] GetDbParameters()
        {
            return null;
        }

        /// <summary>
        /// 生成Ado.net的模板化SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        public string ToSqlTempletText()
        {
            return this.ToSqlTempletText();
        }

        /// <summary>
        /// 生成相应的SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        public string ToSqlText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in _fieldList)
            {
                sb.Append(pair.Key);
                if (!string.IsNullOrEmpty((string)pair.Value))
                {
                    sb.Append(' ').Append(AS).Append(" \"").Append(pair.Value).Append("\"");
                }
                sb.Append(", ");
            }
            return sb.ToString().TrimEnd(new char[] { ',', ' ' }); ;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ToSqlTempletText();
        }
    }
}
