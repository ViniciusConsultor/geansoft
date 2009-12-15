using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Reflection;

namespace Gean.LinqToData
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class Database : DataContext, IDatabase
    {
        #region Singelton Pattern

        private const string MAPPING_SOURCE_RESOURCE_NAME = "Srims.DataAccess.MappingSource.Xml";
        private static MappingSource _mappingSource;
        private static object _locker = new object();

        private Database(string connectionString, MappingSource mappingSource)
            : base(connectionString, mappingSource)
        {
        }

        private static MappingSource GetMappingSource()
        {
            if (_mappingSource == null)
            {
                lock (_locker)
                {
                    if (_mappingSource == null)
                    {
                        Stream mapingSourceStream = Assembly.GetAssembly(typeof(Database)).GetManifestResourceStream(MAPPING_SOURCE_RESOURCE_NAME);
                        if (mapingSourceStream == null)
                        {
                            throw new System.IO.InvalidDataException
                                ("映射文件不存在！您确定已经将其编译属性设置为\"嵌入资源(Embedded Resource)\"?");
                        }
                        _mappingSource = XmlMappingSource.FromStream(mapingSourceStream);
                    }
                }
            }
            return _mappingSource;
        }

        #endregion

        /// <summary>
        /// 构造新的数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>数据库实例</returns>
        public static Database New(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException("connectionString");

            return new Database(connectionString, GetMappingSource());
        }

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="command">Sql命令</param>
        /// <param name="parameters">参数</param>
        /// <returns>受影响的条数</returns>
        public int Execute(string command, params object[] parameters)
        {
            return base.ExecuteCommand(command, parameters);
        }

        #region Users
        /// <summary>
        /// 取得用户数据访问
        /// </summary>
        public IEntityDataAccess<User> Users
        {
            get { return new EntityDataAccessAdapter<User>(this); }
        }
        /// <summary>
        /// 取得用户角色数据访问
        /// </summary>
        public IEntityDataAccess<UserRole> UserRoles
        {
            get { return new EntityDataAccessAdapter<UserRole>(this); }
        }

        #endregion

        #region IDatabase Members

        /// <summary>
        /// 取得某一个实体的数据访问
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <returns>该实体的数据访问</returns>
        public IEntityDataAccess<T> GetDataAccess<T>() where T : class
        {
            return new EntityDataAccessAdapter<T>(this);
        }

        /// <summary>
        /// 提交数据库变更
        /// </summary>
        public void Submit()
        {
            base.SubmitChanges();
        }

        #endregion
    }

    public class User { }
    public class UserRole { }
}
