using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Gean.Data
{
    /// <summary>
    /// 数据访问层(DAL)的一些基本方法的接口
    /// </summary>
    public interface IDataAcess<T> : IDataCRUD where T : IEntity
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
        /// Gets 一个更新命令文本。
        /// </summary>
        /// <value>The update command text.</value>
        String UpdateCommandText { get; }

        /// <summary>
        /// Gets 一个插入命令文本。
        /// </summary>
        /// <value>The insert command text.</value>
        String InsertCommandText { get; }

        /// <summary>
        /// Gets 一个删除命令文本。
        /// </summary>
        /// <value>The delete command text.</value>
        String DeleteCommandText { get; }

        /// <summary>
        /// Gets 一组 SQL 命令和一个数据库连接的封装类型<see cref="DataAdapter"/>。
        /// </summary>
        /// <value>The data adapter.</value>
        DataAdapter DataAdapter { get; }
        
    }
}
