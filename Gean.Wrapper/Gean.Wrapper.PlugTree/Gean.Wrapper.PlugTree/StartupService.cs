﻿using System.IO;
using System.Xml;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 
    /// </summary>
    public static class StartupService
    {
        private static readonly string PLUG_FILE_EXPAND_NAME = "*.gplug";
        private static bool _AlreadyInitializes = false;
        private static string _ApplicationPath;

        public static RunnerCollection Runners
        {
            get { return _Runners; }
        }
        private static RunnerCollection _Runners = null;

        public static PlugPath PlugPath
        {
            get { return _PlugPath; }
        }
        private static PlugPath _PlugPath = null;

        public static string[] PlugFiles
        {
            get { return _PlugFiles; }
        }
        private static string[] _PlugFiles = null;

        /// <summary>
        /// PlugTree的初始化
        /// </summary>
        /// <param name="applicationName">Name of the application.</param>
        /// <param name="startupPath">The startup path.</param>
        public static void Initializes(string applicationName, string startupPath)
        {
            if (_AlreadyInitializes)
            {
                return;
            }
            _ApplicationPath = startupPath;
            if (_PlugPath == null)
            {
                _PlugPath = new PlugPath(applicationName);
                _PlugPath.IsRoot = true;
            }
            if (_Runners == null)
            {
                _Runners = new RunnerCollection();
            }
            if (_PlugFiles == null)
            {
                _PlugFiles = Directory.GetFiles(startupPath, PLUG_FILE_EXPAND_NAME, SearchOption.AllDirectories);
            }

            //本静态初始类型中初始化完成，开始扫描文件。
            StartupService.StartScanPlugFiles();
            
            _AlreadyInitializes = true;
        }

        /// <summary>
        /// 插件树架构工作的第一步：扫描所有Plug文件
        /// </summary>
        private static void StartScanPlugFiles()
        {
            foreach (string pathstr in _PlugFiles)//第一步，扫描指定目录下的所有Plug文件
            {
                if (StartupService.VerifyXmlFile(pathstr))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(pathstr);

                    //扫描该文件中所有Runner
                    ScanRunner(doc);
                    //扫描该文件中所有的PlugPath
                    ScanPlugPath(doc);
                }
                else
                {
                    //TODO:通知用户该Plug未能载入
                }
            }
        }

        private static bool VerifyXmlFile(string pathstr)
        {
            return true;
        }

        /// <summary>
        /// 从给定的一个XML文件中扫描所有的PlugPath
        /// </summary>
        /// <param name="doc">一个Plug的XML文件</param>
        private static void ScanPlugPath(XmlDocument doc)
        {
            if (_PlugPath == null)
            {
                _PlugPath = new PlugPath("Application");
                _PlugPath.IsRoot = true;
            }

            XmlNodeList nodelist = doc.DocumentElement.SelectNodes("Path");
            foreach (XmlNode node in nodelist)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement element = (XmlElement)node;
                PlugPath.Install(element, _PlugPath);
            }
        }

        private static void ScanRunner(XmlDocument doc)
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
                string filepath = Path.Combine(_ApplicationPath, assName);
                //程序集中类型的名称
                string classname = node.Attributes["class"].Value;

                _Runners.Add(classname, RunnerCollection.SearchRunType(filepath, classname));
            }
        }
    }
}
