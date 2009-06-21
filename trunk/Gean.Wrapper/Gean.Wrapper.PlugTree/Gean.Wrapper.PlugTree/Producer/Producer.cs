using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    public class Producer
    {
        public static IProducer Load(Assembly assembly, string classname)
        {
            Type type = assembly.GetType(classname, true, false);
            if (typeof(IProducer).IsAssignableFrom(type))
            {
                return (IProducer)type;
            }
            else
            {
                return null;
            }
        }
    }
}
