using System;
using System.Reflection;
using Gean;
using System.IO;

namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// ѡ���ļ�����
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class OptionFileAttribute : Attribute
    {
        private static string StartPath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="configFile">ָ��ѡ���ļ���ַ</param>
        public OptionFileAttribute(string optionFile)
        {
            string optionFilePath = Path.Combine(StartPath, optionFile);
            if (!File.Exists(optionFilePath))
            {
                OptionFile.Create(optionFilePath);
            }
            this.OptionFileInfo = new FileInfo(optionFilePath);
        }

        public FileInfo OptionFileInfo { get; private set; }

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