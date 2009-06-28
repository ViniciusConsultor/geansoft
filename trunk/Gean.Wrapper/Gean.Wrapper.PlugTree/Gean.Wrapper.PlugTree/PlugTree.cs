using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using Gean.Xml;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 本程序集的核心类型。该类型描述一个应用的插件树，以及可能用到的Runner，Producer，Condition的集合。
    /// 本集合只能从Initialization方法单建一个实例。
    /// Gean: 2009-06-27 22:01:59
    /// </summary>
    public class PlugTree
    {
        const string _PLUG_FILE_EXPAND_NAME = "*.gplug";

        StringCollection _PlugFiles = new StringCollection();
        StringCollection _DisabledPlugs = new StringCollection();
        /// <summary>
        /// 描述Plug的Xml文件的Schema文件
        /// </summary>
        XmlSchema _Schema;

        /// <summary>
        /// 命令器集合
        /// </summary>
        public RunnerCollection Runners { get; private set; }
        /// <summary>
        /// 条件求值器集合
        /// </summary>
        public ConditionCollection Conditions { get; private set; }
        /// <summary>
        /// 对象生成器集合
        /// </summary>
        public ProducerCollection Producers { get; private set; }
        /// <summary>
        /// 获取PlugTree的根路径
        /// </summary>
        public PlugPath DocumentPath { get; private set; }

        /// <summary>
        /// 私有构造函数，单建实例
        /// </summary>
        private PlugTree()
        {
            this.DocumentPath = new PlugPath("Root");
            this.Runners = new RunnerCollection();
            this.Producers = new ProducerCollection();
            this.Conditions = new ConditionCollection();
            this._Schema = new XmlSchema();
        }

        private static PlugTree _PlugTree = null;
        /// <summary>
        /// PlugTree类型的初始化方法，在该方法中单建一个PlugTree实例。
        /// 在该方法中实现了PlugTree的：
        /// 1.从指定的目录扫描出所有的Plug文件
        /// 2.从指定的配置文件中定义出失效的Plug集合
        /// 3.从所有的Plug文件的“Runtime/Import”节点中扫描出所有的“对象生成器”
        /// 4.从所有的Plug文件的“Runtime/Import”节点中扫描出所有的“条件求值器”
        /// 5.安装PlugPath树结构
        /// </summary>
        /// <param name="plugDirectory"></param>
        public static PlugTree Initialization(string plugDirectory)
        {
            if (string.IsNullOrEmpty(plugDirectory))
            {
                throw new ArgumentNullException("Gean: Plug Directory is empty!");
            }
            if (_PlugTree == null)//单建实例
            {
                _PlugTree = new PlugTree();
            }
            // 1.从指定的目录扫描出所有的Plug文件
            _PlugTree._PlugFiles = UtilityFile.SearchDirectory(plugDirectory, _PLUG_FILE_EXPAND_NAME, true, true);
            // 2.从指定的配置文件中定义出失效的Plug集合
            _PlugTree._DisabledPlugs = LoadDisabledPlugs();

            foreach (string file in _PlugTree._PlugFiles)
            {
                if (!XmlHelper.VerifyXmlFile(file, _PlugTree._Schema))//校验Plug文件是否是有效的Plug定义文件
                {
                    continue;
                }
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                // 3.从所有的Plug文件的“Runtime/Import”节点中扫描出所有的“对象生成器”
                // 4.从所有的Plug文件的“Runtime/Import”节点中扫描出所有的“条件求值器”
                _PlugTree.ScanRunTimeNode(doc, file);
                // 5.安装PlugPath树结构
                _PlugTree.ScanPlugPath(doc);
            }
            return _PlugTree;
        }

        /// <summary>
        /// 从一个配置文件载入用户确认不生效的Plug
        /// </summary>
        /// <returns></returns>
        private static StringCollection LoadDisabledPlugs()
        {
            return new StringCollection();
        }

        /// <summary>
        /// 扫描一个Plug文件的“Runtime/Import”节点
        /// 3.从一个Plug文件的“Runtime/Import”节点中扫描出所有的“对象生成器”
        /// 4.从一个Plug文件的“Runtime/Import”节点中扫描出所有的“条件求值器”
        /// </summary>
        /// <param name="doc">一个给定的Plug的Xml文件</param>
        /// <param name="docPath">一个给定的Plug的Xml文件的文件路径</param>
        /// <example>
        /// ＜Runtime>
        ///     ＜Import assembly="Gean.Wrapper.PlugTree.DemoProject1.dll">
        ///         ＜Producer class="Gean.Wrapper.PlugTree.DemoProject1.SacramentoProducer"/>
        ///         ＜Producer class="Gean.Wrapper.PlugTree.DemoProject1.ThunderProducer"/>
        ///         ＜ConditionEvaluator class="Gean.Wrapper.PlugTree.DemoProject1.AngelesConditionEvaluator"/>
        ///         ＜ConditionEvaluator class="Gean.Wrapper.PlugTree.DemoProject1.ClippersConditionEvaluator"/>
        ///     ＜/Import>
        /// ＜/Runtime>
        /// </example>
        private void ScanRunTimeNode(XmlDocument doc, string docPath)
        {
            XmlElement element = ((XmlElement)doc.DocumentElement.SelectSingleNode("Runtime/Import"));

            string assName = element.GetAttribute("assembly");//程序集的名称
            if (string.IsNullOrEmpty(assName))
            {
                throw new PlugTreeException("Gean: Assembly name is Error!");
            }
            if (!assName.ToLowerInvariant().EndsWith(".dll"))
            {
                assName = assName + ".dll";
            }

            //程序集所在路径
            string filepath = Path.Combine(UtilityFile.GetDirectoryByFilepath(docPath), assName);
            Debug.Assert(File.Exists(filepath), "Gean: File not found.");
            Assembly assembly = Assembly.LoadFile(filepath);

            XmlNodeList nodelist = element.ChildNodes;
            foreach (XmlNode node in nodelist)//在文件里扫描并生成所有类型(Type)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }

                //程序集中类型的名称
                string classname = node.Attributes["class"].Value;
                switch (node.LocalName)
                {
                    case "Producer"://扫描出所有的“对象生成器”
                        if (Producers.ContainsKey(classname))
                        {
                            break;
                        }
                        Producers.Add(classname, UtilityType.Load(assembly, classname, typeof(IProducer)));
                        break;
                    case "ConditionEvaluator"://扫描出所有的“条件求值器”
                        if (Conditions.ContainsKey(classname))
                        {
                            break;
                        }
                        Conditions.Add(classname, UtilityType.Load(assembly, classname, typeof(ICondition)));
                        break;
                    default:
                        Debug.Fail("\"/Runtime/Import/\"有未知的子节点");
                        break;
                }
            }
        }

        /// <summary>
        /// 从给定的一个XML文件中扫描所有的PlugPath
        /// </summary>
        /// <param name="doc">一个Plug的XML文件</param>
        /// <param name="docPath"></param>
        private void ScanPlugPath(XmlDocument doc)
        {
            XmlNodeList nodelist = doc.DocumentElement.SelectNodes("Path");
            foreach (XmlNode node in nodelist)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement element = (XmlElement)node;
                PlugPath.Install(element, DocumentPath, this._DisabledPlugs);
            }
        }
    }
}
