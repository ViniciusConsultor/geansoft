using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Gean
{
    public class UtilityType
    {
        /// <summary>
        /// 从程序集中获取程序集实例中具有指定名称的 System.Type 对象。
        /// 当输入assignableFromType时判断该Type是否是从assignableFromType继承。
        /// assignableFromType可以为Null，为Null时不作判断。
        /// </summary>
        /// <param name="assembly">指定的程序集</param>
        /// <param name="classname">类型的全名</param>
        /// <returns>如找到返回该类型，未找到返Null</returns>
        public static Type Load(Assembly assembly, string classname)
        {
            return UtilityType.Load(assembly, classname, null);
        }

        /// <summary>
        /// 从程序集中获取程序集实例中具有指定名称的 System.Type 对象。
        /// 当输入assignableFromType时判断该Type是否是从assignableFromType继承。
        /// assignableFromType可以为Null，为Null时不作判断。
        /// </summary>
        /// <param name="assembly">指定的程序集</param>
        /// <param name="classname">类型的全名</param>
        /// <param name="assignableFromType">被继承的类型</param>
        /// <returns>如找到返回该类型，未找到返Null</returns>
        public static Type Load(Assembly assembly, string classname, Type assignableFromType)
        {
            Type type = assembly.GetType(classname, true, false);
            if (assignableFromType == null)
            {
                return type;
            }
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
