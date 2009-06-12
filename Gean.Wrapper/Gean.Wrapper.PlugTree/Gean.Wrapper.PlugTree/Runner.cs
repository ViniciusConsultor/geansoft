using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections.Specialized;

namespace Gean.Wrapper.PlugTree
{
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
            List<Type> typelist = new List<Type>();
            this.OwnerAssembly = Assembly.LoadFile(path);
            foreach (string classname in this._ClassNameList)
            {
                Type type;
                try
                {
                    type = this.OwnerAssembly.GetType(classname, true, false);
                }
                catch (Exception)
                {
                    //TODO:返回找不到类的异常
                    throw;
                }
                if (typeof(IRun).IsAssignableFrom(type))
                {
                    this.Types.Add(classname, type);
                }
                else
                {
                    //TODO:返回找到的类没有继承IRun接口的异常
                }
            }//foreach
        }

        public IRun GetRunObject(string classname)
        {
            Type type = this.Types[classname];
            return (IRun)Activator.CreateInstance(type);
        }

    }
}
