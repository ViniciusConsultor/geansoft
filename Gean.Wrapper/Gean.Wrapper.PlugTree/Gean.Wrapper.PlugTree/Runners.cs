﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 从Plug文件中解析出实现了IRun接口的类型，并对他进行封装
    /// </summary>
    public sealed class Runners : IEnumerable
    {
        private Dictionary<string, Type> Types { get; set; }

        internal Runners()
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
            catch (TypeLoadException)//程序集中未有该类型
            {
                //TODO:应处理为向用户提示
            }
            if (typeof(IRun).IsAssignableFrom(type))
            {
                return type;
            }
            return null;
        }

        public Type GetIRunType(string classname)
        {
            return this.Types[classname];
        }

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
