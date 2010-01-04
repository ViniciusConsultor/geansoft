using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;

namespace Gean.Data.DAL
{
    /// <summary>
    /// IDatabase 的摘要说明。
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        IDbConnection Connection { get; }

        /// <summary>
        /// 数据库的操作对象
        /// </summary>
        IAdoHelper AdoHelper { get; }
    }
}
