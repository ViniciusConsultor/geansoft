using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Data
{
    public class SQLTextBuilder
    {
        static readonly string[] PredicateFlags = new string[] { "ALL", "DISTINCT", "DISTINCTROW", "TOP" };

        public SQLTextBuilder()
        {
            this.Predicate = Predicate.All;
        }

        public Predicate Predicate { get; set; }
        public SQLWhereTextBuilder Where { get; set; }
    }

    /// <summary>
    /// 说明SQL语句中谓词的枚举。ALL, DISTINCT, DISTINCTROW, 或TOP。您可用谓词来限制返回的记录数量。如果没有指定谓词，则默认值为 ALL。
    /// </summary>
    public enum Predicate : int
    {
        /// <summary>
        /// 如果不包含任何一个谓词，则取此值。选取所有满足 SQL 语句的所有记录。
        /// </summary>
        All,
        /// <summary>
        /// 省略选择字段中包含重复数据的记录。为了让查询结果包含它们，必须使 SELECT 语句中列举的每个字段值是唯一的。
        /// </summary>
        Distinct,
        /// <summary>
        /// 省略基于整个重复记录的数据，而不只是基于重复字段的数据。例如，可在客户ID字段上创建一个联结客户表及订单表的查询。客户表并未复制一份 CustomerID 字段，但是订单表必须如此做，因为每一客户能有许多订单。
        /// </summary>
        Distinctrow,
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
        Top
    }
}
