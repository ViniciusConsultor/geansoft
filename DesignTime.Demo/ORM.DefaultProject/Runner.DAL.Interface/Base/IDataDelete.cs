using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Runner.DAL
{
    /// <summary>
    /// 数据的Delete功能接口。数据库或者持久层的基本操作功能。CRUD是指在做计算处理时的增加(Create)、查询(Retrieve)（重新得到数据）、更新(Update)和删除(Delete)几个单词的首字母简写。
    /// In computing, CRUD is an acronym for create, retrieve, update, and delete. It is used to refer to the basic functions of a database or persistence layer in a software system.
    /// </summary>
    public interface IDataDelete
    {
        /// <summary>
        /// 删除指定ID的记录。
        /// </summary>
        /// <param name="entity">指定的ID.</param>
        /// <returns></returns>
        bool Delete(object id);

        /// <summary>
        /// 删除指定条件集合的记录
        /// </summary>
        /// <param name="pairs">指定的条件集合.</param>
        /// <returns></returns>
        bool Delete(IDictionary pairs);

        /// <summary>
        /// 删除指定的条件键值的记录（单一条件删除）
        /// </summary>
        /// <param name="key">指定的条件.</param>
        /// <param name="value">指定的条件的值.</param>
        /// <returns></returns>
        bool Delete(string key, object value);

    }
}
