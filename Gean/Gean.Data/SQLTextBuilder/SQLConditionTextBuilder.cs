using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using Gean.Data.Exceptions;
using Gean.Data.Resources;

namespace Gean.Data
{
    public class SQLConditionTextBuilder
    {
        /// <summary>
        /// 逻辑操作运算符
        /// </summary>
        static readonly string[] LogicOperationFlags = new string[] { "AND", "OR" };
        /// <summary>
        /// 比较操作运算符
        /// </summary>
        static readonly string[] CompareOperationFlags = new string[] 
        { ">", "<", "<=", ">=", "=", "<>", "LIKE", "NOT LIKE", "IN" };

        protected string CompareOperationFlag { get; set; }
        protected string ParameterName { get; set; }
        protected string TemplateSqlTextName { get; set; }
        protected DbType DbType { get; set; }
        protected object Value { get; set; }

        protected ArrayList Operaters = new ArrayList();
        protected ArrayList Conditions = new ArrayList();

        public void Set(CompareOperation flag, DbType dbType, string name, object value)
        {
            this.Set(flag, dbType, name, value, name);
        }
        public void Set(CompareOperation flag, DbType dbType, string name, object value, string tmpltName)
        {
            this.CompareOperationFlag = CompareOperationFlags[(int)flag];
            this.ParameterName = name;
            this.DbType = dbType;
            this.Value = value;
            this.TemplateSqlTextName = tmpltName;
        }

        public string ToSqlText()
        {
            string[] operaterArray = (string[])Operaters.ToArray("".GetType());
            SQLConditionTextBuilder[] builderArray = (SQLConditionTextBuilder[])Conditions.ToArray((new SQLConditionTextBuilder()).GetType());

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
            SQLConditionTextBuilder[] builderArray = (SQLConditionTextBuilder[])Conditions.ToArray((new SQLConditionTextBuilder()).GetType());

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

        public SqlParameter[] GetSqlParameters()
        {
            ArrayList array = new ArrayList();
            if (ParameterName != null && Value != null)
            {
                array.Add(new SqlParameter("@" + TemplateSqlTextName, Value));
            }
            SQLConditionTextBuilder[] builderArray = (SQLConditionTextBuilder[])Conditions.ToArray((new SQLConditionTextBuilder()).GetType());

            for (int i = 0; i < builderArray.Length; i++)
            {
                SqlParameter[] sps = builderArray[i].GetSqlParameters();
                for (int j = 0; j < sps.Length; j++)
                {
                    array.Add(sps[j]);
                }
            }
            return (SqlParameter[])array.ToArray(new SqlParameter("", "").GetType());
        }

        public void AddCondition(LogicOperation logicOper, SQLConditionTextBuilder txtBuilder)
        {
            Operaters.Add(LogicOperationFlags[(int)logicOper]);
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
