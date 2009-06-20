using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace Gean.Wrapper.PlugTree
{
    public static class PlugTree
    {
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


        internal static void Load(List<string> plugFiles, List<string> disabledPlugs)
        {
            foreach (string file in plugFiles)
            {
                if (!VerifyXmlFile(file))
                {
                    continue;
                }
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                PlugTree.ScanRunner(doc, file);
                PlugTree.ScanPlugPath(doc, file);
            }
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
        ///         ＜Runner class="Gean.Wrapper.PlugTree.DemoProject1.ActiveContentExtension"/>
        ///         ＜Runner class="Gean.Wrapper.PlugTree.DemoProject1.ActiveViewContentUntitled"/>
        ///         ＜Runner class="Gean.Wrapper.PlugTree.DemoProject1.CustomTool"/>
        ///         ＜Runner class="Gean.Wrapper.PlugTree.DemoProject1.CustomProperty"/>
        ///         ＜Runner class="Gean.Wrapper.PlugTree.DemoProject1.DialogPanel"/>
        ///     ＜/Import>
        /// ＜/Runtime>
        /// </example>
        internal static void ScanRunner(XmlDocument doc, string docPath)
        {
            XmlElement element = ((XmlElement)doc.DocumentElement.SelectSingleNode("Runtime/Import"));

            string assName = element.GetAttribute("assembly");//程序集的名称
            if (string.IsNullOrEmpty(assName))
            {
                throw new PlugTreeException("Gean: Assembly name is Error!");
            }
            XmlNodeList nodelist = element.ChildNodes;

            foreach (XmlNode node in nodelist)//在文件里扫描所有类型
            {
                if (node.NodeType != XmlNodeType.Element && node.LocalName.Equals("Runner"))
                {
                    continue;
                }
                //程序集所在路径
                string filepath = Path.Combine(PlugTree.GetDirectoryByFilepath(docPath), assName);
                //程序集中类型的名称
                string classname = node.Attributes["class"].Value;

                _Runners.Add(classname, RunnerCollection.SearchRunType(filepath, classname));
            }
        }

        /// <summary>
        /// 从给定的一个XML文件中扫描所有的PlugPath
        /// </summary>
        /// <param name="doc">一个Plug的XML文件</param>
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

        /// <summary>
        /// 从一个文件的全路径字符串中获取一个目录路径
        /// </summary>
        /// <param name="filepath">文件的完整路径</param>
        /// <returns>一个代表目录的路径</returns>
        internal static string GetDirectoryByFilepath(string filepath)
        {
            return filepath.Substring(0, filepath.LastIndexOf('\\') + 1);
        }

        internal static string[] SearchDirectory
            (string directory, string filemask, bool searchSubdirectories, bool ignoreHidden)
        {
            List<string> collection = new List<string>();

            // 当8.3型的文件名时，"*.xpt" 即 "Template.xpt~" 的处理
            bool isExtMatch = Regex.IsMatch(filemask, @"^\*\..{3}$");
            string ext = null;
            string[] files = Directory.GetFiles(directory, filemask);
            if (isExtMatch) ext = filemask.Remove(0, 1);

            foreach (string file in files)
            {
                if (ignoreHidden && (File.GetAttributes(file) & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    continue;
                }
                if (isExtMatch && Path.GetExtension(file) != ext) continue;

                collection.Add(file);
            }

            if (searchSubdirectories)
            {
                string[] dirs = Directory.GetDirectories(directory);
                foreach (string dir in dirs)
                {
                    if (ignoreHidden && (File.GetAttributes(dir) & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue;
                    }
                    collection.AddRange(SearchDirectory(dir, filemask, searchSubdirectories, ignoreHidden));
                }
            }
            return collection.ToArray();
        }

    }
}
