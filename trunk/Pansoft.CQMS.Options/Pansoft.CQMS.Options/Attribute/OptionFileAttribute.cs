using System;
using System.Reflection;
using Gean;
using System.IO;

namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// ѡ���ļ�����
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
        /// ���캯��
        /// </summary>
        /// <param name="configFile">ָ��ѡ���ļ���ַ</param>
        public OptionFileAttribute(string optionFile)
        {
            string appPath;
            this.OptionFile = new FileInfo(optionFile);
        }

        public FileInfo OptionFile { get; private set; }

        /// <summary>
        /// �ӳ����л��Ԫ����
        /// </summary>
        /// <param name="assemblies">���򼯣����Ϊnull����ӵ�ǰӦ�ó������л�ȡ����������г���</param>
        /// <returns>�ҵ���Ԫ���Ե�����</returns>
        public static OptionFileAttribute[] GetOptionFileAttributeFromAssembly(Assembly[] assemblies)
        {
            return UtilityType.GetAttributeFromAssembly<OptionFileAttribute>(assemblies);
        }
    }
}