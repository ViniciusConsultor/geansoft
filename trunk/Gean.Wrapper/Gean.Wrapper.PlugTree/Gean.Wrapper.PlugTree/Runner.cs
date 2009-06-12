using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    public class Runner
    {
        public Runner(string path)
        {
            this.SearchRunType(path);
        }

        private void SearchRunType(string path)
        {
            this.OwnerAssembly = Assembly.LoadFile(path);
            Type[] ts = this.OwnerAssembly.GetTypes();
            foreach (Type t in ts)
            {
                if (typeof(IRun).IsAssignableFrom(t))
                {
                    this.Type = t;
                    break;
                }
            }
        }

        public IRun GetRunObject()
        {
            return (IRun)Activator.CreateInstance(this.Type);
        }

        public Type Type { get; set; }
        public Assembly OwnerAssembly { get; set; }
    }
}
