using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.Specialized;
using System.IO;
using System.Diagnostics;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 从Plug文件中解析出实现了IRun接口的类型，并对他进行封装
    /// </summary>
    public class Runner
    {
        private string[] _ClassNameList;

        public Dictionary<string,Type> Types { get; set; }
        public Assembly OwnerAssembly { get; set; }

        public Runner(string assemblyFilePath, string[] classNames)
        {
            this.Types = new Dictionary<string, Type>();
            this._ClassNameList = classNames;
            this.SearchRunType(assemblyFilePath);
        }

        private void SearchRunType(string path)
        {
            Debug.Assert(File.Exists(path), "Gean: File not found.");
            List<Type> typelist = new List<Type>();
            this.OwnerAssembly = Assembly.LoadFile(path);
            foreach (string classname in this._ClassNameList)
            {
                Type type = this.OwnerAssembly.GetType(classname, true, false);
                if (typeof(IRun).IsAssignableFrom(type))
                {
                    this.Types.Add(classname, type);
                }
            }//foreach
        }

        public IRun GetRunObject()
        {
            string str = string.Empty;
            foreach (string s in this.Types.Keys)
            {
                str = s;
            }
            return this.GetRunObject(str);
        }
        public IRun GetRunObject(string classname)
        {
            Type type = this.Types[classname];
            return (IRun)Activator.CreateInstance(type);
        }

    }
}
