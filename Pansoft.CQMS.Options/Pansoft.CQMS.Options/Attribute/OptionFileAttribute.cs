using System;
using System.Reflection;
using Gean;
using System.IO;

namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// 选项文件属性
    /// </summary>
    /// <remarks>
    ///		<code>
    ///			[assembly: OptionFile(OptionFile="option\my.option")]
    ///		</code>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class OptionFileAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configFile">指定选项文件地址</param>
        public OptionFileAttribute(string optionFile)
        {
            string appPath;
            this.OptionFile = new FileInfo(optionFile);
        }

        public FileInfo OptionFile { get; private set; }

        /// <summary>
        /// 从程序集中获得元属性
        /// </summary>
        /// <param name="assemblies">程序集，如果为null，则从当前应用程序域中获取所载入的所有程序集</param>
        /// <returns>找到的元属性的数组</returns>
        public static OptionFileAttribute[] GetOptionFileAttributeFromAssembly(Assembly[] assemblies)
        {
            return UtilityType.GetAttributeFromAssembly<OptionFileAttribute>(assemblies);
        }
    }
}