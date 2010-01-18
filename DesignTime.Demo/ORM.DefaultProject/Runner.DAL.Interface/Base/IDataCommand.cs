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
    /// 数据库保存持久的相关动作。数据访问层(DAL)数据的新增、修改、删除等三类型动作（注意：已与查询分离），当一个Command进来时，从仓储Repository加载一个聚合aggregate对象群，然后执行其方法和行为。这样，会激发聚合对象群产生一个事件，这个事件可以分发给仓储Repository，或者分发给Event Bus事件总线。事件总线将再次激活所有监听本事件的处理者。当然一些处理者会执行其他聚合对象群的操作，包括数据库的更新。
    /// 由于事件驱动了领域模型的状态改变，如果你记录这些事件 audit ，将可以将一些用户操作进行回放，从而找到重要状态改变的轨迹，而不是单纯只能依靠数据表字段显示当前状态，至于这些当前状态怎么来的，你无法得知。当你从数据库中获得聚合体时，可以将相关的事件也取出来，这些叫Event Sourcing，事件源虽然没有何时何地发生，但是可以清楚说明用户操作的意图。
    /// </summary>
    public interface IDataCommand : IDataCreate, IDataUpdate, IDataDelete
    {
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

    }
}
