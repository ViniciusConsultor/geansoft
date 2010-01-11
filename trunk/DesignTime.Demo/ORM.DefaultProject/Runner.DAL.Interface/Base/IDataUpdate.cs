using System;
using System.Collections.Generic;
using System.Text;
using Gean.Data;

namespace Runner.DAL
{
    /// <summary>
    /// 数据的Update功能接口。数据库或者持久层的基本操作功能。CRUD是指在做计算处理时的增加(Create)、查询(Retrieve)（重新得到数据）、更新(Update)和删除(Delete)几个单词的首字母简写。
    /// In computing, CRUD is an acronym for create, retrieve, update, and delete. It is used to refer to the basic functions of a database or persistence layer in a software system.
    /// </summary>
    public interface IDataUpdate
    {
        /// <summary>
        /// 修改指定的实体。
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        bool Update(IEntity entity);
    }
}
