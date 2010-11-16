using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Diagnostics;

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
            Type type;
            try
            {
                type = assembly.GetType(classname, true, false);
            }
            catch
            {
                throw;
            }
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

        /// <summary>
        /// 从类型名称中创建类型
        /// </summary>
        /// <param name="assembly">类型所在程序集.</param>
        /// <param name="typeName">类型名</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <returns>Type</returns>
        public static Type CreateType(Assembly assembly, string typeName, bool throwOnError)
        {
            return assembly.GetType(typeName, throwOnError, false);
        }

        /// <summary>
        /// 从类型中创建此类型的实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameterTypes">创建实例所需参数的类型列表</param>
        /// <param name="parameterValues">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Type type, Type expectedType, bool throwOnError, Type[] parameterTypes, object[] parameterValues)
        {
            if (expectedType != null && !expectedType.IsAssignableFrom(type))
            {
                if (throwOnError)
                {
                    throw new Exception(string.Format("将要创建的类型：{0}，不是期望的类型：{1}", type.FullName, expectedType.FullName));
                }
                return null;
            }
            if (parameterTypes != null && parameterValues != null && parameterTypes.Length != parameterValues.Length)
            {
                if (throwOnError)
                {
                    throw new Exception("构造函数参数类型数量和参数数量不一致");
                }
            }
            object createdObject = null;
            if (parameterTypes == null)
            {
                parameterTypes = new Type[] { };
            }
            ConstructorInfo constructor = type.GetConstructor(parameterTypes);
            if (constructor == null)
            {
                try
                {
                    createdObject = Activator.CreateInstance(type, BindingFlags.CreateInstance | (BindingFlags.NonPublic | (BindingFlags.Public | BindingFlags.Instance)), null, parameterValues, null);
                }
                catch (Exception e)
                {
                    if (throwOnError)
                    {
                        throw new Exception("即将创建的类型不支持指定的构造函数：" + e.Message, e);
                    }
                }
            }
            else
            {
                try
                {
                    createdObject = constructor.Invoke(parameterValues);
                }
                catch (Exception e)
                {
                    throw new Exception("对象创建失败：" + e.Message, e);
                }
            }
            return createdObject;
        }

        /// <summary>
        /// 从类型中创建此类型的实例（本方法不支持参数可为Null的构造函数）
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameters">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Type type, Type expectedType, bool throwOnError, params object[] parameters)
        {
            int paramNum = 0;
            if (parameters != null)
            {
                paramNum = parameters.Length;
            }
            Type[] paramTypes = new Type[paramNum];
            object[] paramValues = new object[paramNum];
            for (int i = 0; i < paramNum; i++)
            {
                if (parameters[i] == null)
                {
                    if (throwOnError)
                    {
                        throw new Exception("不支持参数可为Null的构造函数，请使用本方法的另外重载版本");
                    }
                    else
                    {
                        return null;
                    }
                }
                paramTypes[i] = parameters[i].GetType();
                paramValues[i] = parameters[i];
            }
            return CreateObject(type, expectedType, throwOnError, paramTypes, paramValues);
        }

        /// <summary>
        /// 从类型名中创建此类型的实例
        /// </summary>
        /// <param name="assembly">类型所在程序集.</param>
        /// <param name="typeName">类型名</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameters">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Assembly assembly, string typeName, Type expectedType, bool throwOnError, params object[] parameters)
        {
            Type type = CreateType(assembly, typeName, throwOnError);
            return CreateObject(type, expectedType, throwOnError, parameters);
        }

        /// <summary>
        /// 从类型名中创建此类型的实例
        /// </summary>
        /// <param name="assembly">类型所在程序集.</param>
        /// <param name="typeName">类型名</param>
        /// <param name="expectedType">期望的类型</param>
        /// <param name="throwOnError">失败时是否抛出异常</param>
        /// <param name="parameterTypes">创建实例所需参数的类型列表</param>
        /// <param name="parameterValues">创建实例所需的参数值列表</param>
        /// <returns>类型实例</returns>
        public static object CreateObject(Assembly assembly, string typeName, Type expectedType, bool throwOnError, Type[] parameterTypes, object[] parameterValues)
        {
            Type type = CreateType(assembly, typeName, throwOnError);
            return CreateObject(type, expectedType, throwOnError, parameterTypes, parameterValues);
        }

        /// <summary>
        /// 在当前应用程序域中查找指定的类型
        /// </summary>
        /// <param name="typeName">类型全名（包括命名空间）</param>
        /// <returns>找到则返回指定的类型，否则返回空</returns>
        public static Type FindType(string typeName)
        {
            Type type = null;
            List<string> files = new List<string>();
            Assembly[] currAsseArray = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in currAsseArray)
            {
                type = assembly.GetType(typeName, false);
                if (type != null)
                {
                    break;
                }
                else if (!assembly.GlobalAssemblyCache)
                {
                    files.Add(assembly.ManifestModule.ScopeName.ToLower());
                }
            }
            if (type == null)
            {
                string[] fileNames = Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll", SearchOption.TopDirectoryOnly);
                foreach (string file in fileNames)
                {
                    string fileName = Path.GetFileName(file);
                    if (!files.Contains(fileName.ToLower()))
                    {
                        string assemblyName = Path.GetFileNameWithoutExtension(fileName);
                        string typeFullName = typeName + ", " + assemblyName;
                        type = Type.GetType(typeFullName, false);
                        if (type != null)
                        {
                            break;
                        }
                    }
                }
            }
            return type;
        }

        /// <summary>
        /// 从程序集中获得元属性
        /// </summary>
        /// <param name="assemblies">程序集，如果为null，则从当前应用程序域中获取所载入的所有程序集</param>
        /// <returns>找到的元属性的数组</returns>
        public static T[] GetAttributeFromAssembly<T>(Assembly[] assemblies) where T : Attribute
        {
            List<T> list = new List<T>();
            T[] attributes = null;
            if (assemblies == null)
            {
                assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }
            foreach (Assembly assembly in assemblies)
            {
                attributes = (T[])assembly.GetCustomAttributes(typeof(T), false);
                if (attributes != null && attributes.Length > 0)
                {
                    list.AddRange(attributes);
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// 从运行时的堆栈中获取元属性
        /// </summary>
        /// <param name="includeAll">是否包含堆栈上所有的元属性</param>
        /// <typeparam name="T">元属性类型</typeparam>
        /// <returns>找到的元属性的数组</returns>
        public static T[] GetAttributeFromRuntimeStack<T>(bool includeAll) where T : Attribute
        {
            var list = new List<T>();
            var t = new StackTrace();
            for (var i = 0; i < t.FrameCount; i++)
            {
                var f = t.GetFrame(i);
                var m = (MethodInfo)f.GetMethod();
                var a = Attribute.GetCustomAttributes(m, typeof(T)) as T[];
                if (a != null && a.Length > 0)
                {
                    list.AddRange(a);
                    if (!includeAll)
                    {
                        break;
                    }
                }
            }
            return list.ToArray();
        }

        /// <summary>
        /// Determines whether the specified target type contains interface.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="implType">Type of the impl.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target type contains interface; otherwise, <c>false</c>.
        /// </returns>
        public static bool ContainsInterface(Type targetType, Type implType)
        {
            Type[] interfaces = targetType.GetInterfaces();
            foreach (var item in interfaces)
            {
                if (item == implType)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
