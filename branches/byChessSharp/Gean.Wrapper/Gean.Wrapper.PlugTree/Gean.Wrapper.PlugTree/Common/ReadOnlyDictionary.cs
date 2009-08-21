using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 一个只针对本程序集可写的Dictionary，程序集以外是只读的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReadOnlyDictionary<T> : IEnumerable<KeyValuePair<string, T>>
    {
        private Dictionary<string, T> _Dictionary { get; set; }

        internal ReadOnlyDictionary()
        {
            this._Dictionary = new Dictionary<string, T>();
        }

        #region IEnumerable<KeyValuePair<string,T>> 成员

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return this._Dictionary.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._Dictionary.GetEnumerator();
        }

        #endregion

        #region internal 成员

        internal void Add(string key, T value)
        {
            this._Dictionary.Add(key, value);
        }

        internal void Add(KeyValuePair<string, T> item)
        {
            this._Dictionary.Add(item.Key, item.Value);
        }

        internal bool Remove(string key)
        {
            return this._Dictionary.Remove(key);
        }

        internal bool Remove(KeyValuePair<string, T> item)
        {
            if (this.Contains(item))
            {
                return this._Dictionary.Remove(item.Key);
            }
            return false;
        }

        internal void Clear()
        {
            this._Dictionary.Clear();
        }

        public T this[string key]
        {
            get
            {
                return this._Dictionary[key];
            }
            internal set
            {
                this._Dictionary[key] = value;
            }
        }

        #endregion

        #region public 成员

        public bool ContainsKey(string key)
        {
            return this._Dictionary.ContainsKey(key);
        }

        public bool ContainsValue(T value)
        {
            return this._Dictionary.ContainsValue(value);
        }

        public ICollection<string> Keys
        {
            get { return this._Dictionary.Keys; }
        }

        public ICollection<T> Values
        {
            get { return this._Dictionary.Values; }
        }

        public bool TryGetValue(string key, out T value)
        {
            return this._Dictionary.TryGetValue(key, out value);
        }

        public bool Contains(KeyValuePair<string, T> item)
        {
            T t;
            if (this._Dictionary.TryGetValue(item.Key, out t))
            {
                if (t.Equals(item.Value))
                {
                    return true;
                }
            }
            return false;
        }

        public int Count
        {
            get { return this._Dictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        #endregion
        
    }
}
