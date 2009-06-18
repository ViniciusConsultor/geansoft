using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 从所有Plug文件中解析出实现了IRun接口的类型，以键值对的模式存储在一个集合里。
    /// 键是类的全名，值为System.Type。
    /// 通过GetIRunObject方法可以直接获得已实例的IRun接口类型。
    /// </summary>
    public sealed class RunnerCollection : IEnumerable<KeyValuePair<string, IRun>>, IEnumerable
    {
        private Dictionary<string, IRun> _IRuns { get; set; }

        internal RunnerCollection()
        {
            this._IRuns = new Dictionary<string, IRun>();
        }

        internal static IRun SearchRunType(string assemblyPath, string classname)
        {
            Debug.Assert(File.Exists(assemblyPath), "Gean: File not found.");

            Type type = Assembly.LoadFile(assemblyPath).GetType(classname, true, false);
            if (typeof(IRun).IsAssignableFrom(type))
            {
                return (IRun)type;
            }
            else
            {
                return null;
            }
        }

        internal IRun GetIRunType(string classname)
        {
            return this._IRuns[classname];
        }

        /// <summary>
        /// 根据类型的全名获得已实例的IRun接口类型。
        /// </summary>
        /// <param name="classname">类型的全名</param>
        /// <returns>一个已实例的IRun接口类型</returns>
        public IRun GetIRunObject(string classname)
        {
            IRun type = this.GetIRunType(classname);
            return (IRun)Activator.CreateInstance(type.GetType());
        }

        public bool ContainsKey(string key)
        {
            return this._IRuns.ContainsKey(key);
        }

        public bool ContainValue(IRun item)
        {
            return this._IRuns.ContainsValue(item);
        }

        public bool TryGetValue(string key, out IRun value)
        {
            return this._IRuns.TryGetValue(key, out value);
        }

        public ICollection<string> Keys
        {
            get { return this._IRuns.Keys; }
        }

        public ICollection<IRun> Values
        {
            get { return this._IRuns.Values; }
        }

        public int Count
        {
            get { return this._IRuns.Count; }
        }

        public IRun this[string key]
        {
            get
            {
                return this._IRuns[key];
            }
            internal set
            {
                this._IRuns[key]= value;
            }
        }

        internal void Add(string key, IRun value)
        {
            this._IRuns.Add(key, value);
        }

        internal void Clear()
        {
            this._IRuns.Clear();
        }

        internal bool Remove(string key)
        {
            return this._IRuns.Remove(key);
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return this._IRuns.GetEnumerator();
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,IRun>> 成员

        IEnumerator<KeyValuePair<string, IRun>> IEnumerable<KeyValuePair<string, IRun>>.GetEnumerator()
        {
            return this._IRuns.GetEnumerator();
        }

        #endregion
    }
}
