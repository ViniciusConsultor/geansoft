using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Data
{
    /// <summary>
    /// 数据库或者持久层的基本操作功能。CRUD是指在做计算处理时的增加(Create)、查询(Retrieve)（重新得到数据）、更新(Update)和删除(Delete)几个单词的首字母简写。
　　/// In computing, CRUD is an acronym for create, retrieve, update, and delete. It is used to refer to the basic functions of a database or persistence layer in a software system.
    /// </summary>
    public interface IDataCRUD
    {
        /// <summary>
        /// 增加指定的实体到需要的位置。
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Create(IEntity entity);
        /// <summary>
        /// 查询指定的实体。
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Retrieve(IEntity entity);
        /// <summary>
        /// 修改指定的实体。
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Update(IEntity entity);
        /// <summary>
        /// 删除指定的实体。
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Delete(IEntity entity);
    }
}
