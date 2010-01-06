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
    public interface IDataAcess<T> where T : IEntity
    {
        IAcessHelper AcessHelper { get; }
    }
}
