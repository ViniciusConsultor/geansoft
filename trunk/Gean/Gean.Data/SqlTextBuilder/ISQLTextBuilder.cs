using System;
using System.Data.Common;
namespace Gean.Data
{
    /// <summary>
    /// Sql语句生成器接口
    /// </summary>
    public interface ISQLTextBuilder
    {
        /// <summary>
        /// 获取该语句(子句)的所有参数
        /// </summary>
        /// <returns></returns>
        DbParameter[] GetDbParameters();
        /// <summary>
        /// 生成Ado.net的模板化SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        string ToSqlTempletText();
        /// <summary>
        /// [不推荐使用，一般情况下应使用<see cref="ToSqlTempletText"/>，以生成<see cref="DbCommand"/>，并以参数设置方式生成该命令。目的：防止SQL注入。]生成相应的SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        string ToSqlText();
    }
}
