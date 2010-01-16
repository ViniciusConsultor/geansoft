using Gean.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System;

namespace Runner.DAL
{
    /// <summary>
    /// 数据的Query功能接口。数据库或者持久层的基本操作功能。CRUD是指在做计算处理时的增加(Create)、查询(Retrieve)（重新得到数据）、更新(Update)和删除(Delete)几个单词的首字母简写。
    /// In computing, CRUD is an acronym for create, retrieve, update, and delete. It is used to refer to the basic functions of a database or persistence layer in a software system.
    /// </summary>
    public interface IDataQuery
    {
        /// <summary>
        /// 查询指定的ID(主键)的目标记录是否存在
        /// </summary>
        /// <param name="id">指定的ID</param>
        /// <returns>目标记录存在，返回<c>True</c>,否则返回<c>False</c></returns>
        bool Exists(object id);

        /// <summary>
        /// 根据指定的条件集合查询目标记录是否存在
        /// </summary>
        /// <param name="pairs">指定的条件集合.</param>
        /// <returns>目标记录存在，返回<c>True</c>,否则返回<c>False</c></returns>
        bool Query(IDictionary pairs);

        /// <summary>
        /// 根据指定的条件键值查询（单一条件查询）
        /// </summary>
        /// <param name="key">指定的条件.</param>
        /// <param name="value">指定的条件的值.</param>
        /// <returns>目标记录存在，返回<c>True</c>,否则返回<c>False</c></returns>
        bool Query(string key, object value);

    }

}
