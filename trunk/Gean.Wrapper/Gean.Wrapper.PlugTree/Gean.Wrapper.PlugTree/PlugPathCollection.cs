using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.PlugTree
{
    public sealed class PlugPathCollection : IEnumerable<PlugPath>, IEnumerable
    {
        private List<PlugPath> _PlugPaths = new List<PlugPath>();

        internal PlugPathCollection()
        {

        }

        public PlugPath this[int index]
        {
            get { return this._PlugPaths[index]; }
            internal set { this._PlugPaths[index] = value; }
        }
        /// <summary>
        /// 尝试用一个PlugPath的名字(PlugPath.Name)来获取该PlugPath。
        /// 如果该PlugPath存在则返回true，并out该PlugPath。不存在返回false。
        /// </summary>
        public bool TryGetValue(string name, out PlugPath plagpath)
        {
            plagpath = null;
            foreach (PlugPath path in _PlugPaths)
            {
                if (path.Name.Equals(name))
                {
                    plagpath = path;
                    return true;
                }
            }
            return false;
        }

        public int IndexOf(PlugPath item)
        {
            return this._PlugPaths.IndexOf(item);
        }

        /// <summary>
        /// 通过一个PlugPath的名字(PlugPath.Name)来查询该PlugPath是否存在。
        /// </summary>
        public bool Contains(string name)
        {
            foreach (PlugPath path in _PlugPaths)
            {
                if (path.Name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains(PlugPath item)
        {
            return this._PlugPaths.Contains(item);
        }

        public int Count
        {
            get { return this._PlugPaths.Count; }
        }

        internal void Insert(int index, PlugPath item)
        {
            this._PlugPaths.Insert(index, item);
        }

        internal void Add(PlugPath item)
        {
            this._PlugPaths.Add(item);
        }

        internal void AddRange(IEnumerable<PlugPath> plugs)
        {
            this._PlugPaths.AddRange(plugs);
        }

        internal void Clear()
        {
            this._PlugPaths.Clear();
        }

        internal bool Remove(PlugPath item)
        {
            return this._PlugPaths.Remove(item);
        }

        internal void RemoveAt(int index)
        {
            this._PlugPaths.RemoveAt(index);
        }

        internal void RemoveAll()
        {
            while (this._PlugPaths.Count <= 0)
            {
                this._PlugPaths.RemoveAt(0);
            }
        }

        #region IEnumerable<PlugPath> 成员

        public IEnumerator<PlugPath> GetEnumerator()
        {
            return this._PlugPaths.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._PlugPaths.GetEnumerator();
        }

        #endregion
    }
}
