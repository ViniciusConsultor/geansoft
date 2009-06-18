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
    public sealed class RunnerCollection : OutList<Type>
    {
        private Dictionary<string, Type> Types { get; set; }

        internal RunnerCollection()
        {
            this.Types = new Dictionary<string, Type>();
        }

        internal void Add(string classname, Type type)
        {
            this.Types.Add(classname, type);
        }

        internal static Type SearchRunType(string assemblyPath, string classname)
        {
            Debug.Assert(File.Exists(assemblyPath), "Gean: File not found.");
            List<Type> typelist = new List<Type>();
            Assembly assembly = Assembly.LoadFile(assemblyPath);
            Type type = null;
            try
            {
                type = assembly.GetType(classname, true, false);
            }
            catch
            {
                throw;
            }
            if (typeof(IRun).IsAssignableFrom(type))
            {
                return type;
            }
            return null;
        }

        internal Type GetIRunType(string classname)
        {
            return this.Types[classname];
        }

        /// <summary>
        /// 根据类型的全名获得已实例的IRun接口类型。
        /// </summary>
        /// <param name="classname">类型的全名</param>
        /// <returns>一个已实例的IRun接口类型</returns>
        public IRun GetIRunObject(string classname)
        {
            Type type = this.GetIRunType(classname);
            return (IRun)Activator.CreateInstance(type);
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return this.Types.GetEnumerator();
        }

        #endregion
    }
}
