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
        internal static IProducer GetProducer(string name)
        {
            Type type = PlugTree.Producers[name];
            return (IProducer)Activator.CreateInstance(type);
        }
    }
}
