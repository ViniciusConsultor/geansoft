using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Specialized;

namespace Gean.Wrapper.PlugTree
{
    public static class PlugTree
    {
        static readonly string _PLUG_FILE_EXPAND_NAME = "*.gplug";

        static bool IsInitializationed { get; set; }

        static StringCollection _PlugFiles = new StringCollection();
        static StringCollection _DisabledPlugs = new StringCollection();

        static RunnerCollection _Runners = new RunnerCollection();

        static ConditionCollection _Conditions = new ConditionCollection();

        static ProducerCollection _Producers = new ProducerCollection();

        static PlugPath _RootPath = new PlugPath("Root");


        public static RunnerCollection Runners
        {
            get { return _Runners; }
        }

        public static ConditionCollection Conditions
        {
            get { return _Conditions; }
        }

        public static ProducerCollection Producers
        {
            get { return PlugTree._Producers; }
        }

        /// <summary>
        /// 获取PlugTree的根路径
        /// </summary>
        public static PlugPath DocumentPath
        {
            get { return _RootPath; }
        }


        public static void Initialization(string plugDirectory)
        {
            if (string.IsNullOrEmpty(plugDirectory))
                throw new ArgumentNullException("Plug Directory is empty!");
            _PlugFiles = UtilityFile.SearchDirectory(plugDirectory, _PLUG_FILE_EXPAND_NAME, true, true);

            foreach (string file in _PlugFiles)
            {
                if (!VerifyXmlFile(file))
                {
                    continue;
                }
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                PlugTree.ScanRunTimeNode(doc, file);
                PlugTree.ScanPlugPath(doc, file);
            }
            PlugTree.IsInitializationed = true;
        }

        internal static bool VerifyXmlFile(string pathstr)
        {
            return true;
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
        internal static void ScanRunTimeNode(XmlDocument doc, string docPath)
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
                        if (_Producers.ContainsKey(classname))
                        {
                            break;
                        }
                        _Producers.Add(classname, UtilityType.Load(assembly, classname, typeof(IProducer)));
                        break;
                    case "ConditionEvaluator":
                        if (_Conditions.ContainsKey(classname))
                        {
                            break;
                        }
                        _Conditions.Add(classname, UtilityType.Load(assembly, classname, typeof(ICondition)));
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
        /// 
        internal static void ScanPlugPath(XmlDocument doc, string docPath)
        {
            if (_RootPath == null)
            {
                _RootPath = new PlugPath("Application");
                _RootPath.IsRoot = true;
            }

            XmlNodeList nodelist = doc.DocumentElement.SelectNodes("Path");
            foreach (XmlNode node in nodelist)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement element = (XmlElement)node;
                PlugPath.Install(element, _RootPath);
            }
        }



    }
}
