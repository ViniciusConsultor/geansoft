using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gean.LinqToData
{
    /// <summary>
    /// 数据库接口
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// 取得某一个实体的数据访问
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>该实体的数据访问</returns>
        IEntityDataAccess<T> GetDataAccess<T>() where T : class;

        /// <summary>
        /// 提交数据库变更
        /// </summary>
        void Submit();
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="command">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int Execute(string command, params object[] parameters);

        #region Users
        /// <summary>
        /// 取得用户数据访问
        /// </summary>
        IEntityDataAccess<User> Users { get; }
        /// <summary>
        /// 取得用户角色数据访问
        /// </summary>
        IEntityDataAccess<UserRole> UserRoles { get; }
        #endregion
    }
}
