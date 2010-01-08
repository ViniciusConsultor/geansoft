using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Gean.Data
{
    /// <summary>
    /// SQL语句文本构造器。
    /// </summary>
    public class SQLTextBuilder : ISQLTextBuilder
    {

        /// <summary>
        /// 构造函数。Initializes a new instance of the <see cref="SQLTextBuilder"/> class.
        /// </summary>
        public SQLTextBuilder()
        {
            this.Predicate = SQLText.Predicate.All;
            this.Field = new SQLFieldTextBuilder();
            this.From = new SQLFromTextBuilder();
            this.Where = new SQLWhereTextBuilder();
        }

        /// <summary>
        /// Gets or sets 一个动作.
        /// </summary>
        /// <value>The action.</value>
        public SQLText.Action Action { get; set; }

        /// <summary>
        /// Gets or sets 一个指定的谓词，默认是<see cref="Predicate.All"/>
        /// </summary>
        /// <value>The predicate.</value>
        public SQLText.Predicate Predicate { get; set; }

        /// <summary>
        /// 字段列表。
        /// </summary>
        /// <value>The field.</value>
        public SQLFieldTextBuilder Field { get; set; }

        /// <summary>
        /// FROM连接子句
        /// </summary>
        /// <value>From.</value>
        public SQLFromTextBuilder From { get; set; }

        /// <summary>
        /// Gets or sets 一个Where子句.
        /// </summary>
        /// <value>The where.</value>
        public SQLWhereTextBuilder Where { get; set; }

        #region ISQLTextBuilder 成员

        /// <summary>
        /// 获取该语句(子句)的所有参数
        /// </summary>
        /// <returns></returns>
        public DbParameter[] GetDbParameters()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成Ado.net的模板化SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        public string ToSqlTempletText()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// [不推荐使用，一般情况下应使用<see cref="ToSqlTempletText"/>，以生成<see cref="DbCommand"/>，并以参数设置方式生成该命令。目的：防止SQL注入。]生成相应的SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        public string ToSqlText()
        {
            StringBuilder sb = new StringBuilder();
            switch (Action)
            {
                case SQLText.Action.Select:
                    SelectTextBuilder(sb);
                    break;
                case SQLText.Action.Insert:
                    break;
                case SQLText.Action.Update:
                    break;
                case SQLText.Action.Delete:
                    break;
            }
            return sb.ToString().Trim();
        }

        private void SelectTextBuilder(StringBuilder sb)
        {
            sb.Append(SQLText.Actions[(int)this.Action]).Append(' ').Append(this.Field.ToSqlText()).Append(' ');
            sb.Append(SQLText.FROM).Append(' ').Append(this.From.ToSqlText()).Append(' ');
            if (this.Where.IsEffective)
            {
                sb.Append(SQLText.WHERE).Append(' ').Append(this.Where.ToSqlText());
            }
        }

        #endregion

        public override string ToString()
        {
            return this.ToSqlText();
        }
    }
}
