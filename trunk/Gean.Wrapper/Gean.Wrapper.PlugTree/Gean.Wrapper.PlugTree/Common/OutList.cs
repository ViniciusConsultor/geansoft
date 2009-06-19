using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 一个抽象集合类，他封装成只在本程序集里可以读写，而在其他程序集中仅能读取的集合类。已实现枚举。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class OutList<T> : IEnumerable<T>
    {
        protected List<T> _List = new List<T>();

        /// <summary>
        /// 如果泛型T具有Name的属性，那么可以根据Name的值进行尝试Get相应的值
        /// </summary>
        public bool TryGetValue(string name, out T item)
        {
            item = default(T);
            PropertyInfo pinfo = typeof(T).GetProperty("Name");
            if (pinfo == null)
            {
                return false;
            }
            foreach (T t in _List)
            {
                string str = (string)pinfo.GetValue(t, null);
                if (str.Equals(name))
                {
                    item = t;
                    return true;
                }
            }
            return false;
        }

        public bool Contains(string name)
        {
            PropertyInfo pinfo = typeof(T).GetProperty("Name");
            if (pinfo == null)
            {
                return false;
            }

            foreach (T item in _List)
            {
                string str = (string)pinfo.GetValue(item, null);
                if (str.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        public T this[int index]
        {
            get { return this._List[index]; }
            internal set { this._List[index] = value; }
        }

        public int IndexOf(T item)
        {
            return this._List.IndexOf(item);
        }

        public bool Contains(T item)
        {
            return this._List.Contains(item);
        }

        public int Count
        {
            get { return this._List.Count; }
        }

        public T[] ToArrary()
        {
            return this._List.ToArray();
        }

        internal protected void Insert(int index, T item)
        {
            this._List.Insert(index, item);
        }

        internal protected void Add(T item)
        {
            this._List.Add(item);
        }

        internal protected void AddRange(IEnumerable<T> plugs)
        {
            this._List.AddRange(plugs);
        }

        internal protected void Clear()
        {
            this._List.Clear();
        }

        internal protected bool Remove(T item)
        {
            return this._List.Remove(item);
        }

        internal protected void RemoveAt(int index)
        {
            this._List.RemoveAt(index);
        }

        internal protected void RemoveAll()
        {
            while (this._List.Count <= 0)
            {
                this._List.RemoveAt(0);
            }
        }

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
        {
            return this._List.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._List.GetEnumerator();
        }

        #endregion
    }
}
