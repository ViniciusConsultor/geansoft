﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using Gean.Data.Exceptions;
using Gean.Data.Resources;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Gean.Data
{
    /// <summary>
    /// WHERE 子句生成器。从 FROM 子句中的列表中指定哪些记录受到 SELECT, UPDATE, 或 DELETE 语句的影响。
    /// </summary>
    public class SQLWhereTextBuilder : ISQLTextBuilder
    {
        /// <summary>
        /// 简单防止SQL注入：字段名中只能含有数字与大小写字母
        /// </summary>
        static Regex _RegexParamName = new Regex(@"\?[_a-zA-Z0-9]+", RegexOptions.Compiled);
        /// <summary>
        /// 逻辑操作运算符
        /// </summary>
        static readonly string[] _LogicOperationFlags = new string[] { "AND", "OR" };
        /// <summary>
        /// 比较操作运算符
        /// </summary>
        static readonly string[] _CompareOperationFlags = new string[] 
        { ">", "<", "<=", ">=", "=", "<>", "LIKE", "NOT LIKE", "IN" };

        protected string CompareOperationFlag { get; set; }
        protected string ParameterName { get; set; }
        protected string TemplateSqlTextName { get; set; }
        protected DbType DbType { get; set; }
        protected object Value { get; set; }

        protected ArrayList Operaters = new ArrayList();
        protected ArrayList Conditions = new ArrayList();

        /// <summary>
        /// 设置当前条件的各个属性。
        /// </summary>
        /// <param name="name">当前条件的参数名。</param>
        /// <param name="flag">当前条件的比较运算符。</param>
        /// <param name="dbType">当前条件的参数类型。</param>
        /// <param name="value">当前条件的参数的值。</param>
        public void Set(string name, CompareOperation flag, DbType dbType, object value)
        {
            this.Set(name, flag, dbType, value, name);
        }
        /// <summary>
        /// 设置当前条件的各个属性。
        /// </summary>
        /// <param name="name">当前条件的参数名。</param>
        /// <param name="flag">当前条件的比较运算符。</param>
        /// <param name="dbType">当前条件的参数类型。</param>
        /// <param name="value">当前条件的参数的值。</param>
        /// <param name="tmpltName">当前条件的参数的模板字符串。不设置就是当前条件的参数名加上"@"前缀。</param>
        public void Set(string name, CompareOperation flag, DbType dbType, object value, string tmpltName)
        {
            if (_RegexParamName.IsMatch(name))//简单防止SQL注入
            {
                return;
            }
            this.CompareOperationFlag = _CompareOperationFlags[(int)flag];
            this.ParameterName = name;
            this.DbType = dbType;
            this.Value = value;
            this.TemplateSqlTextName = tmpltName;
        }

        public string ToSqlText()
        {
            string[] operaterArray = (string[])Operaters.ToArray("".GetType());
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
                            sb.Append("'" + dt.ToString("yyyy-MM-dd") + "'");
                            break;
                        }
                    case DbType.DateTime:
                    case DbType.DateTime2:
                    case DbType.DateTimeOffset:
                        {
                            DateTime dt = (DateTime)Value;
                            sb.Append("'" + dt.ToString("yyyy-MM-dd hh:mm:ss.fff") + "'");
                            break;
                        }
                    case DbType.Time:
                        {
                            DateTime dt = (DateTime)Value;
                            sb.Append("'" + dt.ToString("hh:mm:ss.fff") + "'");
                            break;
                        }
                    case DbType.AnsiString:
                    case DbType.AnsiStringFixedLength:
                    case DbType.String:
                    case DbType.StringFixedLength:
                        {
                            string tmp = (string)Value;
                            sb.Append("'" + tmp.Replace("'", "\"") + "'");
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
                            sb.Append("'" + tmp.Replace("'", "\"") + "'");
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
                for (int i = 0; i < operaterArray.Length; i++)
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

        public void Add(LogicOperation logicOper, SQLWhereTextBuilder txtBuilder)
        {
            Operaters.Add(_LogicOperationFlags[(int)logicOper]);
            Conditions.Add(txtBuilder); 
        }
    }

    /// <summary>
    /// 逻辑操作运算符
    /// </summary>
    public enum LogicOperation : int
    {
        /// <summary>
        /// 逻辑操作符：And
        /// </summary>
        And = 0,
        /// <summary>
        /// 逻辑操作符：Or
        /// </summary>
        Or = 1
    }
    /// <summary>
    /// 比较操作运算符："&lt;", "&gt;", "&gt;=", "&lt;=", "=", "&lt;&gt;", "like", "not like", "in"
    /// </summary>
    public enum CompareOperation : int
    {
        /// <summary>
        /// 符号：&lt;
        /// </summary>
        MoreThan = 0,
        /// <summary>
        /// 符号：&gt;
        /// </summary>
        LessThan = 1,
        /// <summary>
        /// 符号：&gt;=
        /// </summary>
        NotMoreThan = 2,
        /// <summary>
        /// 符号：&lt;=
        /// </summary>
        NotLessThan = 3,
        /// <summary>
        /// 符号：=
        /// </summary>
        Equal = 4,
        /// <summary>
        /// 符号：&lt;&gt;
        /// </summary>
        NotEqual = 5,
        /// <summary>
        /// 符号：like
        /// </summary>
        Like = 6,
        /// <summary>
        /// 符号：not like
        /// </summary>
        NotLike = 7,
        /// <summary>
        /// 符号：in
        /// </summary>
        In = 8
    }


}
