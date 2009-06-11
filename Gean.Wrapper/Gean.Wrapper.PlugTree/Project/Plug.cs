using System.Diagnostics;
using System.IO;
using System.Xml;

using Gean.Wrapper.PlugTree.Exceptions;

namespace Gean.Wrapper.PlugTree
{
    public class Plug
    {
        internal Plug()
        {
            this.PlugRuntimes = new PlugRuntimeCollention();
        }

        public Properties Properties { get; internal set; }
        public PlugManifest PlugManifest { get; internal set; }
        public PlugRuntimeCollention PlugRuntimes { get; internal set; }
        public PlugPath PlugPath { get; internal set; }

        public string CustomErrorMessage { get; set; }
        public string FileName { get; set; }
        public PlugAction Action { get; set; }
        public bool Enabled
        {
            get { return this._Enabled; }
            set
            {
                this._Enabled = value;
                this.Action = value ? PlugAction.Enable : PlugAction.Disable;
            }
        }
        private bool _Enabled;

        public object CreateObject(string className)
        {
            this.LoadDependencies();
            foreach (PlugRuntime runtime in this.PlugRuntimes)
            {
                object o = runtime.CreateInstance(className);
                if (o != null)
                {
                    return o;
                }
            }
            return null;
        }

        private void LoadDependencies()
        {
            if (!this._DependenciesLoaded)
            {
                this._DependenciesLoaded = true;
                foreach (var verEx in this.PlugManifest.Dependencies)
                {
                    if (verEx.Value.RequirePreload)
                    {
                        bool found = false;
                        foreach (Plug plug in PlugManager.PlugCollection)
                        {
                            if (plug.PlugManifest.Identities.ContainsKey(verEx.Key))
                            {
                                found = true;
                                plug.LoadRuntimeAssemblies();
                            }
                        }
                        if (!found)
                        {
                            throw new PlugParseException("Cannot load run-time dependency for " + verEx.ToString());
                        }
                    }
                }
            }
        }

        public void LoadRuntimeAssemblies()
        {
            this.LoadDependencies();
            foreach (PlugRuntime runtime in this.PlugRuntimes)
            {
                runtime.Load();
            }
        }

        public override string ToString()
        {
            return "[PlugFile: " + this.FileName + "]";
        }

        /// <summary>
        /// 从Plug的Xml文件中解析Plug
        /// </summary>
        /// <param name="fullFile">Plug的Xml文件</param>
        /// <param name="plug">The plug.</param>
        /// <returns></returns>
        public static bool TryParse(string fullFile, out Plug plug)
        {
            plug = null;

            /***************************************************/
            if (!File.Exists(fullFile))
            {
                return false;
            }

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(fullFile);
            }
            catch (XmlException ex)
            {
                Debug.Fail(ex.Message);
                return false;
            }

            if (doc.DocumentElement.LocalName.ToLowerInvariant() != "plug")
            {
                return false;// 当Plug的Xml文件的根节点的节点名的小写状态不是plug，放弃解析该文件。
            }
            plug = new Plug();
            plug.FileName = fullFile;
            plug.Properties = Properties.Parse(doc.DocumentElement.Attributes);

            foreach (XmlNode item in doc.DocumentElement.ChildNodes)
            {
                bool hasIdentity = false;
                bool hasRuntime = false;
                if (item.NodeType != XmlNodeType.Element)
                {
                    break;
                }
                XmlElement element = (XmlElement)item;
                switch (element.LocalName.ToLowerInvariant())
                {
                    case "manifest":
                        if (hasIdentity)
                            throw new PlugParseException("\"Plug签名\"节点(LocalName: Identity)只能有一个节点");
                        PlugManifest.Parse(element, plug);
                        hasIdentity = true;
                        break;
                    case "runtime":
                        if (hasRuntime)
                            throw new PlugParseException("\"Plug运行时\"节点(LocalName: Runtime)只能有一个节点");
                        PlugRuntime.Parse(element, plug);
                        hasRuntime = true;
                        break;
                    case "path":
                        PlugPath.Setup(element, PlugManager.PlugPath, plug);
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        private bool _DependenciesLoaded;
    }

}
