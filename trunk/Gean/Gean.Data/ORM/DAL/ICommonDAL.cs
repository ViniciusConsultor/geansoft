using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Data.DAL
{
    /// <summary>
    /// 数据访问层(DAL)的一些基本方法的接口
    /// </summary>
    public interface ICommonDAL
    {
        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="recordTable">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistKey(Hashtable recordTable);

        /// <summary>
        /// 查询数据库,检查是否存在指定键值的对象
        /// </summary>
        /// <param name="fieldName">指定的属性名</param>
        /// <param name="key">指定的值</param>
        /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
        bool IsExistKey(string fieldName, object key);

        /// <summary>
        /// 获取数据库中该对象的最大ID值
        /// </summary>
        /// <returns>最大ID值</returns>
        int GetMaxID();

        /// <summary>
        /// 根据指定对象的ID,从数据库中删除指定对象(用于整型主键)
        /// </summary>
        /// <param name="key">指定对象的ID</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool Delete(int key);

        /// <summary>
        /// 根据条件,从数据库中删除指定对象
        /// </summary>
        /// <param name="condition">删除记录的条件语句</param>
        /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
        bool Delete(string condition);
    }
}
