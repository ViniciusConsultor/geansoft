using System.IO;
using System.Xml;

namespace Gean.Wrapper.PlugTree
{
    public static class StartupService
    {
        private static readonly string PLUG_FILE_EXPAND_NAME = "*.gplug";
        private static bool _AlreadyInitializes = false;
        private static string _ApplicationPath;

        public static Runners Runners
        {
            get { return _Runners; }
        }
        private static Runners _Runners = null;

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

        public static void Initializes(string applicationName, string path)
        {
            if (_AlreadyInitializes)
            {
                return;
            }
            _ApplicationPath = path;
            if (_PlugPath == null)
            {
                _PlugPath = new PlugPath(applicationName);
                _PlugPath.IsRoot = true;
            }
            if (_Runners == null)
            {
                _Runners = new Runners();
            }
            if (_PlugFiles == null)
            {
                _PlugFiles = Directory.GetFiles(path, PLUG_FILE_EXPAND_NAME, SearchOption.TopDirectoryOnly);
            }
            _AlreadyInitializes = true;
            ScanPlugFiles();
        }

        private static void ScanPlugFiles()
        {
            foreach (string pathstr in _PlugFiles)//第一步，扫描指定目录下的所有Plug文件
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(pathstr);
                ScanRunner(doc);
                ScanPath(doc);
            }
        }

        private static void ScanPath(XmlDocument doc)
        {
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
            XmlElement element = ((XmlElement)doc.DocumentElement.SelectSingleNode("Definiens"));
            string assName = element.SelectSingleNode("Assembly").InnerText;
            XmlNodeList nodelist = element.SelectSingleNode("Class").ChildNodes;

            foreach (XmlNode node in nodelist)//在文件里扫描所有类型
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                string classname = node.InnerText;
                string filepath = Path.Combine(_ApplicationPath, assName);

                _Runners.Add(classname, Runners.SearchRunType(filepath, classname));
            }
        }
    }
}
