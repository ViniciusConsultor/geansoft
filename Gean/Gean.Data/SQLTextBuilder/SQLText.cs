using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Gean.Data
{
    /// <summary>
    /// 定义了一些SQL的关键字只读字段，和一些相关的枚举。
    /// </summary>
    public class SQLText
    {


        /// <summary>
        /// [readonly] 简单防止SQL注入：字段名中只能含有数字与大小写字母
        /// </summary>
        internal static readonly Regex RegexParamName = new Regex(@"\?[_a-zA-Z0-9]+", RegexOptions.Compiled);

        /// <summary>
        /// [readonly] 逻辑操作运算符
        /// </summary>
        internal static readonly string[] LogicOperationFlags = new string[] { "AND", "OR" };

        /// <summary>
        /// [readonly] 比较操作运算符
        /// </summary>
        internal static readonly string[] CompareOperationFlags = new string[] { ">", "<", "<=", ">=", "=", "<>", "LIKE", "NOT LIKE", "IN" };

        /// <summary>
        /// [readonly] 谓词集合
        /// </summary>
        internal static readonly string[] PredicateFlags = new string[] { "ALL", "DISTINCT", "DISTINCTROW", "TOP" };

        /// <summary>
        /// [readonly] 语句的动词集合，包括：SELECT,INSERT,UPDATE,DELETE
        /// </summary>
        internal static readonly string[] Actions = new string[] { "SELECT", "INSERT", "UPDATE", "DELETE" };

        /// <summary>
        /// [readonly] 连词：FORM
        /// </summary>
        internal static readonly string FROM = "FROM";

        /// <summary>
        /// [readonly] 连词：WHERE
        /// </summary>
        internal static readonly string WHERE = "WHERE";

        /// <summary>
        /// [readonly] 设置别名的关键字
        /// </summary>
        internal static readonly string AS = "AS";

        /// <summary>
        /// 语句的动作集合枚举。包括：SELECT,INSERT,UPDATE,DELETE
        /// </summary>
        public enum Action : int
        {
            /// <summary>
            /// Select
            /// </summary>
            Select = 0,
            /// <summary>
            /// Insert
            /// </summary>
            Insert = 1,
            /// <summary>
            /// Update
            /// </summary>
            Update = 2,
            /// <summary>
            /// DELETE
            /// </summary>
            Delete = 3,
        }

        /// <summary>
        /// SQL语句中谓词的枚举。ALL, DISTINCT, DISTINCTROW, 或TOP。您可用谓词来限制返回的记录数量。如果没有指定谓词，则默认值为 ALL。
        /// </summary>
        public enum Predicate : int
        {
            /// <summary>
            /// 如果不包含任何一个谓词，则取此值。选取所有满足 SQL 语句的所有记录。
            /// </summary>
            All = 0,
            /// <summary>
            /// 省略选择字段中包含重复数据的记录。为了让查询结果包含它们，必须使 SELECT 语句中列举的每个字段值是唯一的。
            /// </summary>
            Distinct = 1,
            /// <summary>
            /// 省略基于整个重复记录的数据，而不只是基于重复字段的数据。例如，可在客户ID字段上创建一个联结客户表及订单表的查询。客户表并未复制一份 CustomerID 字段，但是订单表必须如此做，因为每一客户能有许多订单。
            /// </summary>
            Distinctrow = 2,
            /// <summary>
            /// 返回特定数目的记录，且这些记录将落在由 ORDER BY 子句指定的前面或后面的范围中。假设您想要 1994 年班级里的前 25 个学生名字： 
            /// SELECT TOP 25
            /// FirstName, LastName
            /// FROM Students
            /// WHERE GraduationYear = 1994
            /// ORDER BY GradePointAverage DESC;
            /// 如果您没有包含 ORDER BY 子句，则查询将由学生表返回 25 个记录的任意集合，且该表满足 WHERE 子句。
            /// TOP 谓词不在相同值间作选择。在前一示例中，如果第 25 及第 26 的最高平均分数相同，则查询将返回 26 个记录。
            /// 也可用 PERCENT 保留字返回特定记录的百分比，且这些记录将落在由 ORDER BY 子句指定的前面或后面范围中。假设用班级后面 10% 的学生代替前 25 个学生，：
            /// SELECT TOP 10 PERCENT
            ///    FirstName, LastName
            /// FROM Students
            /// WHERE GraduationYear = 1994
            /// ORDER BY GradePointAverage ASC;
            /// ASC 谓词指定返回后面的值。遵循 TOP的值一定是无符号 Integer.
            /// 查询是否可更新，这不会受到 TOP 的影响。
            /// </summary>
            Top = 3,
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
            /// 符号：&gt;
            /// </summary>
            MoreThan = 0,
            /// <summary>
            /// 符号：&lt;
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
}
