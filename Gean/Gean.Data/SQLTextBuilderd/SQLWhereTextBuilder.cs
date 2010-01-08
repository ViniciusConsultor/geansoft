using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using Gean.Data.Exceptions;
using Gean.Data.Resources;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.Collections.Specialized;

namespace Gean.Data
{
    /// <summary>
    /// WHERE 子句生成器。从 FROM 子句中的列表中指定哪些记录受到 SELECT, UPDATE, 或 DELETE 语句的影响。
    /// </summary>
    public class SQLWhereTextBuilder : ISQLTextBuilder
    {
        /// <summary>
        /// Gets or sets 比较运算符。
        /// </summary>
        /// <value>The compare operation flag.</value>
        protected string CompareOperationFlag { get; set; }
        /// <summary>
        /// Gets or sets 一个参数名。
        /// </summary>
        /// <value>The name of the parameter.</value>
        protected string ParameterName { get; set; }
        /// <summary>
        /// Gets or sets 当前条件的参数的模板字符串。不设置就是当前条件的参数名加上"@"前缀。
        /// </summary>
        /// <value>The name of the template SQL text.</value>
        protected string TemplateSqlTextName { get; set; }
        /// <summary>
        /// Gets or sets 当前条件的参数类型。
        /// </summary>
        /// <value>The type of the db.</value>
        protected DbType DbType { get; set; }
        /// <summary>
        /// Gets or sets 当前条件的参数的值。
        /// </summary>
        /// <value>The value.</value>
        protected object Value { get; set; }

        /// <summary>
        /// Gets or sets 本类型是否有效。即当设置生成器成功后，可以正确生成SQL子句。
        /// </summary>
        /// <value>
        /// 	<c>true</c> 正确设置，可以生成SQL子句; otherwise, <c>false</c>.
        /// </value>
        public bool IsEffective { get { return !string.IsNullOrEmpty(ParameterName); } }

        /// <summary>
        /// 逻辑运算集合。
        /// </summary>
        protected ArrayList Operaters = new ArrayList();
        /// <summary>
        /// 条件集合。
        /// </summary>
        protected ArrayList Conditions = new ArrayList();

        /// <summary>
        /// 设置当前条件的各个属性。
        /// </summary>
        /// <param name="name">当前条件的参数名。</param>
        /// <param name="flag">当前条件的比较运算符。当使用In运算符时，参数的值请使用<see cref="InTextBuilder"/>类型定义，参数类型请使用String。</param>
        /// <param name="dbType">当前条件的参数类型。</param>
        /// <param name="value">当前条件的参数的值。</param>
        public void Set(string name, SQLText.CompareOperation flag, DbType dbType, object value)
        {
            this.Set(name, flag, dbType, value, name);
        }
        /// <summary>
        /// 设置当前条件的各个属性。
        /// </summary>
        /// <param name="name">当前条件的参数名。</param>
        /// <param name="flag">当前条件的比较运算符。当使用In运算符时，参数的值请使用<see cref="InTextBuilder"/>类型定义，参数类型请使用String。</param>
        /// <param name="dbType">当前条件的参数类型。</param>
        /// <param name="value">当前条件的参数的值。</param>
        /// <param name="tmpltName">当前条件的参数的模板字符串。不设置就是当前条件的参数名加上"@"前缀。</param>
        public void Set(string name, SQLText.CompareOperation flag, DbType dbType, object value, string tmpltName)
        {
            if (SQLText.RegexParamName.IsMatch(name))//简单防止SQL注入
            {
                return;
            }
            if (flag == SQLText.CompareOperation.In)//当使用In子句时，Value必需是SQLTextBuilder.InTextBuilder类型。
            {
                if (!(value is SQLInTextBuilder))
                {
                    throw new SQLTextBuilderException(Messages.SQLTextBuilderError_In);
                }
            }
            this.CompareOperationFlag = SQLText.CompareOperationFlags[(int)flag];
            this.ParameterName = name;
            this.DbType = dbType;
            this.Value = value;
            this.TemplateSqlTextName = tmpltName;
        }

        /// <summary>
        /// [不推荐使用，一般情况下应使用<see cref="ToSqlTempletText"/>，以生成<see cref="DbCommand"/>，并以参数设置方式生成该命令。目的：防止SQL注入。]生成相应的SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        public string ToSqlText()
        {
            string[] operaterArray = (string[])Operaters.ToArray(typeof(String));
            SQLWhereTextBuilder[] builderArray = (SQLWhereTextBuilder[])Conditions.ToArray((new SQLWhereTextBuilder()).GetType());

            StringBuilder sb = new StringBuilder();

            int count = 0;
            if (ParameterName != null && Value != null)
            {
                sb.Append(ParameterName);
                sb.Append(" ");
                sb.Append(CompareOperationFlag);
                sb.Append(" ");
                switch (DbType)
                {
                    #region case
                    case DbType.Binary:
                        throw new DbTypeNotSupportException("DbType.Binary: " + Messages.DbTypeNotSupport);
                    case DbType.Date:
                        {
                            DateTime dt = (DateTime)Value;
                            sb.Append("'").Append(dt.ToString("yyyy-MM-dd")).Append("'");
                            break;
                        }
                    case DbType.DateTime:
                    case DbType.DateTime2:
                    case DbType.DateTimeOffset:
                        {
                            DateTime dt = (DateTime)Value;
                            sb.Append("'").Append(dt.ToString("yyyy-MM-dd hh:mm:ss.fff")).Append("'");
                            break;
                        }
                    case DbType.Time:
                        {
                            DateTime dt = (DateTime)Value;
                            sb.Append("'").Append(dt.ToString("hh:mm:ss.fff")).Append("'");
                            break;
                        }
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                    case DbType.StringFixedLength:
                        {
                            string tmp = "";
                            if (Value is SQLInTextBuilder)
                            {
                                tmp = ((SQLInTextBuilder)Value).ToSqlText();
                                sb.Append(tmp);
                            }
                            else
                            {
                                tmp = (string)Value;
                                sb.Append("'").Append(tmp).Append("'");
                            }
                            break;
                        }
                    case DbType.Currency:
                    case DbType.Single:
                    case DbType.Decimal:
                    case DbType.Double:
                    case DbType.Int16:
                    case DbType.Int32:
                    case DbType.Int64:
                    case DbType.UInt16:
                    case DbType.UInt32:
                    case DbType.UInt64:
                    case DbType.VarNumeric:
                    case DbType.Boolean:
                        {
                            sb.Append(Value);
                            break;
                        }
                    case DbType.Byte:
                    case DbType.Guid:
                    case DbType.Xml:
                    case DbType.Object:
                    case DbType.SByte:
                    default:
                        {
                            string tmp = Value.ToString();
                            sb.Append("'").Append(tmp).Append("'");
                            break;
                        }
                    #endregion
                }
                count++;
            }
            if (operaterArray.Length > 0)
            {
                for (int i = 0; i < operaterArray.Length; i++)
                {
                    if (builderArray[i].ToSqlTempletText() == "")
                        continue;
                    count++;
                    if ((ParameterName != null && Value != null) || count > 1)
                    {
                        sb.Append(" ");
                        sb.Append(operaterArray[i]);
                        sb.Append(" ");
                    }
                    sb.Append(builderArray[i].ToSqlText());
                }
            }
            if (count > 1)
            { 
                sb.Insert(0, '(');
                sb.Append(')');
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成Ado.net的模板化SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        public string ToSqlTempletText()
        {
            string[] operaterArray = (string[])Operaters.ToArray("".GetType());
            SQLWhereTextBuilder[] builderArray = (SQLWhereTextBuilder[])Conditions.ToArray((new SQLWhereTextBuilder()).GetType());

            StringBuilder outStr = new StringBuilder();

            int count = 0;
            if (ParameterName != null && Value != null)
            {
                outStr.Append(ParameterName);
                outStr.Append(" ");
                outStr.Append(CompareOperationFlag);
                outStr.Append(" @");
                outStr.Append(TemplateSqlTextName);
                count++;
            }

            if (operaterArray.Length > 0)
            {
                for (int i = 0; i < operaterArray.Length; i++)//进入递归生成
                {
                    if (builderArray[i].ToSqlTempletText() == "")
                        continue;
                    count++;
                    if ((ParameterName != null && Value != null) || count > 1)
                    {
                        outStr.Append(" ");
                        outStr.Append(operaterArray[i]);
                        outStr.Append(" ");
                    }
                    outStr.Append(builderArray[i].ToSqlTempletText());
                }
            }
            if (count > 1)
            {
                outStr.Insert(0, '(');
                outStr.Append(')');
            }
            return outStr.ToString();
        }

        /// <summary>
        /// 获取该语句(子句)的所有参数
        /// </summary>
        /// <returns></returns>
        public DbParameter[] GetDbParameters()
        {
            ArrayList array = new ArrayList();
            if (ParameterName != null && Value != null)
            {
                array.Add(new SqlParameter("@" + TemplateSqlTextName, Value));
            }
            SQLWhereTextBuilder[] builderArray = (SQLWhereTextBuilder[])Conditions.ToArray((new SQLWhereTextBuilder()).GetType());

            for (int i = 0; i < builderArray.Length; i++)
            {
                DbParameter[] sps = builderArray[i].GetDbParameters();
                for (int j = 0; j < sps.Length; j++)
                {
                    array.Add(sps[j]);
                }
            }
            return (DbParameter[])array.ToArray(typeof(DbParameter));
        }

        /// <summary>
        /// 通过一个逻辑运算符(<see cref="LogicOperation"/>)给本类型连接一个逻辑条件。
        /// </summary>
        /// <param name="logicOper">一个逻辑运算符。</param>
        /// <param name="txtBuilder">一个逻辑条件。</param>
        public void Add(SQLText.LogicOperation logicOper, SQLWhereTextBuilder txtBuilder)
        {
            Operaters.Add(SQLText.LogicOperationFlags[(int)logicOper]);
            Conditions.Add(txtBuilder); 
        }

    }
}
