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
        /// 生成相应的SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        string ToSqlTempletText();
        /// <summary>
        /// 生成Ado.net的模板化SQL语句(子句)
        /// </summary>
        /// <returns></returns>
        string ToSqlText();
    }
}
