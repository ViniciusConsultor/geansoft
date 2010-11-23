using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using NLog;
using System.Reflection;
using System.Collections.Specialized;
using Gean.Option.Interfaces;
using Gean.Option.Common;

namespace Gean.Option
{
    /// <summary>
    /// 选项服务管理器
    /// </summary>
    public sealed class OptionService : IService<OptionXmlFile>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 选项定义文件的后缀名
        /// </summary>
        public static readonly string OPTION_SUFFIX_NAME = ".option";
        /// <summary>
        /// 单建实例静态属性的属性名
        /// </summary>
        public static readonly string INSTANCE_NAME = "ME";
        /// <summary>
        /// 核心载入选项内容的方法名
        /// </summary>
        public static readonly string METHOD_NAME = "Initializes";
        /// <summary>
        /// 应用程序的启动路径
        /// </summary>
        /// <value>应用程序的启动路径</value>
        public static string ApplicationStartPath { get { return AppDomain.CurrentDomain.SetupInformation.ApplicationBase; } }

        #region 单件实例

        private OptionService() { }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static OptionService ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton()
            {
                Instance = new OptionService();
                Instance.XmlFileMap = new Dictionary<string, OptionXmlFile>();
            }
            internal static readonly OptionService Instance = null;
        }

        #endregion

        private OptionXmlFile[] _SettingDocuments = null;

        private OptionMap _OptionMap = new OptionMap();

        /// <summary>
        /// Gets or sets the XML file map.它的Key是Option类的完全名，Value是该选项数据所在的XML文件。
        /// </summary>
        /// <value>The XML file map.</value>
        public Dictionary<string, OptionXmlFile> XmlFileMap { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="args">传入参数是所有选项的Xml文件的封装类型(OptionXmlFile).</param>
        /// <returns></returns>
        public bool Initializes(params OptionXmlFile[] args)
        {
            if (args == null || args.Length <= 0)
            {
                logger.Error("当Option服务管理器初始化时,传入参数有误。传入的Option的Xml文件数量为零或为空。");
                return false;
            }
            _SettingDocuments = new OptionXmlFile[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                if (!(args[i] is OptionXmlFile))
                {
                    logger.Warn("当Option服务管理器初始化时,传入的对象不是Option的XML，无法解析。" + args[i]);
                }
                _SettingDocuments[i] = (OptionXmlFile)args[i];
            }
            Dictionary<String, XmlElement> parmsMap = new Dictionary<string, XmlElement>();
            foreach (var document in _SettingDocuments)
            {
                XmlElement[] eles = GetElementByClassMap(document);
                foreach (var ele in eles)
                {
                    string klass = ele.GetAttribute("class");
                    parmsMap.Add(klass, ele);
                    this.XmlFileMap.Add(klass, document);
                }
            }
            logger.Info(string.Format("从Option的配置Xml文件中找到 {0} 个Option配置节点", parmsMap.Count));

            Type[] optionArray = GetOptionClassByDirectory(ApplicationStartPath);
            logger.Info(string.Format("搜索所有的程序集，并找到 {0} 个IOption的实现类型。", optionArray.Length));

            foreach (Type klass in optionArray)
            {
                try
                {
                    if (klass.IsAbstract)
                        continue;
                    IOption option = (IOption)klass.GetProperty(INSTANCE_NAME).GetValue(null, null);
                    MethodInfo method = klass.GetMethod(METHOD_NAME, BindingFlags.NonPublic | BindingFlags.Instance);
                    method.Invoke(option, new object[] { parmsMap[klass.FullName] });

                    logger.Info(string.Format("程序的 {0} 选项类型初始化成功。", klass.Name));
                    _OptionMap.Add(klass.Name, option);
                }
                catch (Exception e)
                {
                    logger.Error(string.Format("{0} 选项初始化异常。", klass.Name), e);
                }
            }


            return _OptionMap.Count > 0;
        }

        /// <summary>
        /// 重新启动服务
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns></returns>
        public bool ReStart(params OptionXmlFile[] args)
        {
            _OptionMap.Clear();
            try
            {
                if (args == null || args.Length <= 0)
                    this.Initializes(_SettingDocuments);
                else
                    this.Initializes(args);
            }
            catch (Exception e)
            {
                logger.Error("Option服务管理器重新启动时发生异常。异常信息:" + e.Message, e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            return true;
        }

        /// <summary>
        /// 终止服务
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            return true;
        }

        /// <summary>
        /// 从指定的Document中的约定的节点遍历获得所有定义的“设置”的类的全名
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns></returns>
        private static XmlElement[] GetElementByClassMap(OptionXmlFile document)
        {
            List<XmlElement> elements = new List<XmlElement>();
            XmlNodeList nodelist = document.DocumentElement.SelectNodes("option[@class]");
            foreach (var node in nodelist)
            {
                if (!(node is XmlElement))
                    continue;
                XmlElement element = (XmlElement)node;
                elements.Add(element);
            }
            return elements.ToArray();
        }

        /// <summary>
        /// 从目录中找到所有的.Net程序集，并遍历所有程序集找到所有实现了IOption接口的类型
        /// </summary>
        /// <param name="appStartPath">The app start path.</param>
        /// <returns></returns>
        private static Type[] GetOptionClassByDirectory(string appStartPath)
        {
            List<Type> typeList = new List<Type>();
            Assembly[] assArray = UtilityFile.SearchAssemblyByDirectory(appStartPath);
            foreach (Assembly ass in assArray)
            {
                Type[] types = ass.GetTypes();
                foreach (Type type in types)
                {
                    if (UtilityType.ContainsInterface(type, typeof(IOption)))
                        typeList.Add(type);
                }
            }
            return typeList.ToArray();
        }

        /// <summary>
        /// 默认获取应用程序所在的目录及子目录下的所有选项数据文件的文件路径
        /// </summary>
        /// <returns></returns>
        public static OptionXmlFile[] GetOptionFiles()
        {
            StringCollection sc = UtilityFile.SearchDirectory(OptionService.ApplicationStartPath, "*.option");
            List<OptionXmlFile> fileList = new List<OptionXmlFile>();
            foreach (var filePath in sc)
            {
                OptionXmlFile doc = new OptionXmlFile(filePath);
                fileList.Add(doc);
            }
            return fileList.ToArray();
        }
    }
}