using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Gean.Data
{
    /// <summary>
    /// 数据库参数集合类
    /// </summary>
    [Serializable]
    public class DbParamCollection : IEnumerable<DbParam>
    {
        private List<DbParam> _params = new List<DbParam>();

        /// <summary>
        /// 添加数据库参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="type">参数的类型</param>
        /// <param name="value">参数的值</param>
        public void AddParam(string paramName, DbType type, object value)
        {
            //创建参数对象
            DbParam param = new DbParam(type, paramName, value);

            //添加到集合
            _params.Add(param);
        }

        public int IndexOf(DbParam item)
        {
            return _params.IndexOf(item);
        }

        public DbParam this[int index]
        {
            get { return _params[index]; }
        }

        public void Add(DbParam item)
        {
            _params.Add(item);
        }

        public bool Contains(DbParam item)
        {
           return _params.Contains(item);
        }

        public void RemoveAt(int index)
        {
            _params.RemoveAt(index);
        }

        public bool Remove(DbParam item)
        {
            return _params.Remove(item);
        }

        public void Clear()
        {
            _params.Clear();
        }

        #region IEnumerable<DbParam> 成员

        public IEnumerator<DbParam> GetEnumerator()
        {
            return _params.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _params.GetEnumerator();
        }

        #endregion
    }
}
