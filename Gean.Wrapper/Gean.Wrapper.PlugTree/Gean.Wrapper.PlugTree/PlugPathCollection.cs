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

        public int IndexOf(PlugPath item)
        {
            return this._PlugPaths.IndexOf(item);
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (PlugPath item in this._PlugPaths)
            {
                sb.Append(item.Name);
            }
            return sb.ToString();
        }
    }
}
