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
    /// </summary>
    public class PlugTree
    {
        const string _PLUG_FILE_EXPAND_NAME = "*.gplug";

        StringCollection _PlugFiles = new StringCollection();
        StringCollection _DisabledPlugs = new StringCollection();
        XmlSchema _Schema;

        public RunnerCollection Runners { get; private set; }
        public ConditionCollection Conditions { get; private set; }
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
        public static void Initialization(string plugDirectory)
        {
            if (string.IsNullOrEmpty(plugDirectory))
            {
                throw new ArgumentNullException("Gean: Plug Directory is empty!");
            }
            if (_PlugTree == null)//单建实例
            {
                _PlugTree = new PlugTree();
            }
            _PlugTree._PlugFiles = UtilityFile.SearchDirectory(plugDirectory, _PLUG_FILE_EXPAND_NAME, true, true);

            foreach (string file in _PlugTree._PlugFiles)
            {
                if (!XmlHelper.VerifyXmlFile(file, _PlugTree._Schema))
                {
                    continue;
                }
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                _PlugTree.ScanRunTimeNode(doc, file);
                _PlugTree.ScanPlugPath(doc);
            }
        }

        /// <summary>
        /// 扫描一个给定的Plug的Xml文件中定义的实现了IRun接口的类型的描述
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
                    case "Producer":
                        if (Producers.ContainsKey(classname))
                        {
                            break;
                        }
                        Producers.Add(classname, UtilityType.Load(assembly, classname, typeof(IProducer)));
                        break;
                    case "ConditionEvaluator":
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
                PlugPath.Install(element, DocumentPath);
            }
        }
    }
}
