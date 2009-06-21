using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    public class Runner
    {
        internal Runner() { }

        ///// <summary>
        ///// 根据类型的全名获得已实例的IRun接口类型。
        ///// </summary>
        ///// <param name="classname">类型的全名</param>
        ///// <returns>一个已实例的IRun接口类型</returns>
        //public IRun GetIRunObject(string classname)
        //{
        //    IRun type = PlugTree.Runners[classname];
        //    return (IRun)Activator.CreateInstance(type.GetType());
        //}

        internal static IRun Load(Assembly assembly, string classname)
        {
            Type type = assembly.GetType(classname, true, false);
            if (typeof(IRun).IsAssignableFrom(type))
            {
                return (IRun)type;
            }
            else
            {
                return null;
            }
        }
    }
}
