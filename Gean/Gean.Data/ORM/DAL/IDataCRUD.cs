﻿using System;
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
        bool Create(IEntity entity);
        bool Retrieve(IEntity entity);
        bool Update(IEntity entity);
        bool Delete(IEntity entity);
    }
}
