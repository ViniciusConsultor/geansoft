using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;
using Gean.Data;

namespace Runner.DAL
{
    /// <summary>
    /// 数据访问层(DAL)的一些基本方法的接口
    /// </summary>
    public interface IDataAcess<T> : IDataCreate, IDataQuery, IDataUpdate, IDataDelete where T : IEntity
    {
        /// <summary>
        /// Gets 一个数据库访问助手类
        /// </summary>
        /// <value>The acess helper.</value>
        IAcessHelper AcessHelper { get; }

        /// <summary>
        /// 实体对应的表名
        /// </summary>
        string TableName { get; }

        /// <summary>
        /// 实体对应的表中的主键字段名
        /// </summary>
        string PrimaryKey { get; }

        /// <summary>
        /// 实体对应的表中的主键字段数据类型。在数据库设计中应当注意，仅采用int,string两种类型。
        /// </summary>
        /// <value>The type of the primary key.</value>
        Type PrimaryKeyType { get; }

        /// <summary>
        /// Gets 一组 SQL 命令和一个数据库连接的封装类型<see cref="DataAdapter"/>。
        /// </summary>
        /// <value>The data adapter.</value>
        DataAdapter DataAdapter { get; }

        T DataReaderToEntity(IDataReader dr);

        ICollection<T> GetAll();

        T FindById(object id);
    }
}
