using System;
using System.Collections.Generic;
using System.Text;
using Runner.DAL;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace Runner.DAL.Factory
{
    /// <summary>
    /// 本工厂的工作目标是提供各实体类型的Dal(数据操作层工作类型)。该类型需在首次使用前调用<see cref="Initialize"/>初始化参数，初始化参数时传递的程序集名将决定采用何种数据的存储方式。
    /// 在这里我们约定，程序集名称由 [ A.Dal.B ] 组成，其中A：是我们项目的命名空间，B：说明数据的存储方式，如SqlServer等。
    /// 在这里我们约定，一个实体类型的数据操作层工作类型的名称由  [ A.Dal.B.(EntityName)DataAcess ] 组成。
    /// 有了以上两点约定，本框架下的数据存储基本可以做到无关，可替换。
    /// </summary>
    public static class DalFactory
    {
        /// <summary>
        /// 类型的程序集所在路径
        /// </summary>
        private static string _AssemblyPath; //例如：Runner.DAL.SqlServer
        /// <summary>
        /// 类型的程序集名称,通常是命名空间名称
        /// </summary>
        private static string _AssemblyName; //例如：Runner.DAL.SqlServer

        /// <summary>
        /// 初始化(数据操作层工作类型)类型工厂
        /// </summary>
        /// <param name="assemblyPath">类型的程序集所在路径</param>
        /// <param name="assemblyName">类型的程序集名称,通常是命名空间名称</param>
        public static void Initialize(string assemblyPath, string assemblyName)
        {
            _AssemblyPath = assemblyPath;
            _AssemblyName = assemblyName;
        }

        /// <summary>
        /// Gets the employee data acess.
        /// </summary>
        /// <value>The employee data acess.</value>
        public static IEmployeeDataAcess EmployeeDataAcess
        {
            get
            {
                string path = Path.Combine(_AssemblyPath, _AssemblyName + ".dll");
                Debug.Assert(File.Exists(path), path);
                string classname = GetClassName("Employee");
                Assembly ass = Assembly.LoadFile(path);
                IEmployeeDataAcess a = (IEmployeeDataAcess)(ass.CreateInstance(classname, true));
                return a;
            }
        }

        /// <summary>
        /// 根据实体名构造一个类型的全名
        /// </summary>
        /// <param name="tablename">指定的实体名.</param>
        /// <returns></returns>
        private static string GetClassName(string entityName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_AssemblyName).Append(".").Append(entityName).Append(DATAACESS).ToString();
            return sb.ToString();
        }

        /// <summary>
        /// 数据操作层工作类型名的后缀
        /// </summary>
        private const string DATAACESS = "DataAcess";

    }
}
