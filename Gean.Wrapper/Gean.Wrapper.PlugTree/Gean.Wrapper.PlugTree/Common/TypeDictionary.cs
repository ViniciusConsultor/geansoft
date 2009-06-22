using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    public abstract class TypeDictionary : ReadOnlyDictionary<Type>
    {
        public static Type Load(Assembly assembly, string classname, Type assignableFromType)
        {
            Type type = assembly.GetType(classname, true, false);
            if (assignableFromType.IsAssignableFrom(type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }

    }
}
