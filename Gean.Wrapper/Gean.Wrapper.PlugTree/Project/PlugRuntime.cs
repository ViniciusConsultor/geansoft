using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Gean.Wrapper.PlugTree.Components;
using Gean.Wrapper.PlugTree.Exceptions;
using Gean.Wrapper.PlugTree.Service;


namespace Gean.Wrapper.PlugTree
{
    public sealed class PlugRuntime
    {
        public PlugRuntime(string assemblyName)
        {
            this.AssemblyName = assemblyName;
            this.DefinedBuilders = new BuilderCollection();
            this.DefinedConditionEvaluators = new ConditionEvaluatorCollection();
        }

        public string AssemblyName { get; private set; }

        /// <summary>
        /// 获取当前的Runtime携带的已载入的程序集
        /// </summary>
        /// <value>The loaded assembly.</value>
        public Assembly LoadedAssembly
        {
            get
            {
                this.Load(); // load the assembly, if not already done
                return _LoadedAssembly;
            }
        }
        private Assembly _LoadedAssembly = null;

        public Plug OwnerPlug { get; private set; }
        public Properties Properties { get; private set; }
        public BuilderCollection DefinedBuilders { get; private set; }
        public ConditionEvaluatorCollection DefinedConditionEvaluators { get; private set; }

        public bool IsActive
        {
            get
            {
                if (_Conditions != null)
                {
                    _IsActive = Condition.GetFailedAction(_Conditions, this) == ConditionFailedAction.None;
                    _Conditions = null;
                }
                return _IsActive;
            }
        }
        private bool _IsActive = true;

        private ICondition[] _Conditions;
        /// <summary>
        /// 程序是否载入，即this.Load()方法是否被执行过
        /// </summary>
        private bool _IsAssemblyLoaded;

        /// <summary>
        /// 载入当前的Runtime携带的程序集
        /// </summary>
        public void Load()
        {
            if (this._IsAssemblyLoaded)
            {
                return;
            }
            this._IsAssemblyLoaded = true;
            try
            {
                if (this.AssemblyName[0] == ':')//以冒号起始的话，该程序集名称在XML文件中应是程序集的FullPath
                {
                    this._LoadedAssembly = Assembly.Load(this.AssemblyName.Substring(1));
                }
                else if (this.AssemblyName[0] == '$')
                {
                    int pos = this.AssemblyName.IndexOf('/');
                    if (pos < 0)
                    {
                        throw new ApplicationException("Expected '/' in path beginning with '$'!");
                    }
                    //<Import assembly="$ICSharpCode.FormsDesigner/FormsDesigner.dll"/>
                    string referencedPlug = this.AssemblyName.Substring(1, pos - 1);
                    foreach (Plug plug in PlugManager.PlugCollection)
                    {
                        if (plug.Enabled && plug.PlugManifest.Identities.ContainsKey(referencedPlug))
                        {
                            string assemblyFile = Path.Combine(Path.GetDirectoryName(plug.FileName),
                                                               this.AssemblyName.Substring(pos + 1));
                            this._LoadedAssembly = Assembly.LoadFrom(assemblyFile);
                            break;
                        }
                    }
                    if (this._LoadedAssembly == null)
                    {
                        throw new FileNotFoundException("Could not find referenced Plug " + referencedPlug);
                    }
                }
                else
                {
                    string dir = Path.GetDirectoryName(this.OwnerPlug.FileName);
                    string filename = Path.Combine(dir, this.AssemblyName + ".dll");
                    this._LoadedAssembly = Assembly.LoadFrom(filename);
                }
            }
            catch (FileNotFoundException)
            {
                //MessageService.ShowError("The addin '" + this.AssemblyName + "' could not be loaded:\n" + ex.ToString());
            }
            catch (FileLoadException)
            {
                //MessageService.ShowError("The addin '" + this.AssemblyName + "' could not be loaded:\n" + ex.ToString());
            }
        }

        public object CreateInstance(string instance)
        {
            if (IsActive)
            {
                Assembly asm = this.LoadedAssembly;
                if (asm == null)
                    return null;
                return asm.CreateInstance(instance);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从Runtime节点的XmlElement中解析出PlugRuntime对象集合
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="plug">The plug.</param>
        /// <returns></returns>
        public static void Parse(XmlElement element, Plug plug)
        {
            string name = string.Empty;
            if (plug != null)
            {
                name = (string)plug.Properties["name"];
            }
            foreach (XmlNode node in element.ChildNodes) // 遍历Runtime的所有子节点
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                Stack<ICondition> conditionStack = new Stack<ICondition>();
                PlugRuntime runtime = new PlugRuntime(name);
                runtime.OwnerPlug = plug;
                runtime.Properties = Properties.Parse(element);

                XmlElement ele = (XmlElement)node;
                Properties ppties = Properties.Parse(ele);
                switch (ele.LocalName.ToLowerInvariant())
                {
                    case "import":
                        plug.PlugRuntimes.Add(PlugRuntime.Parse(ele, plug, conditionStack));
                        break;
                    default:
                        throw new PlugParseException("Unknown node in runtime section :" + ele.LocalName);
                }
            }
        }

        /// <summary>
        /// 从Import节点的XmlElement中解析PlugRuntime对象
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="plug">The plug.</param>
        /// <param name="conditionStack">The condition stack.</param>
        /// <returns></returns>
        private static PlugRuntime Parse(XmlElement element, Plug plug, Stack<ICondition> conditionStack)
        {
            if (element.Attributes.Count != 1)
            {
                throw new PlugParseException("Import node requires ONE attribute.");
            }
            // 取出Import节点中的程序集的名称
            string assemblyName = string.Empty;
            foreach (XmlAttribute attribute in element.Attributes)
            {
                if (attribute.LocalName.ToLowerInvariant() == "assembly")
                {
                    assemblyName = attribute.Value;
                    break;
                }
                throw new PlugParseException("Import node requires ONE attribute of assembly.");
            }
            // 根据程序集的名称创建一个PlugRuntime实例
            PlugRuntime runtime = new PlugRuntime(assemblyName);
            runtime.OwnerPlug = plug;
            runtime.Properties = Properties.Parse(element);
            if (conditionStack.Count > 0)
            {
                runtime._Conditions = conditionStack.ToArray();
            }
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                switch (node.LocalName.ToLowerInvariant())
                {
                    case "builder":
                        if (node.Attributes.Count <= 0)
                        {
                            throw new PlugParseException("Builder nodes must be NOT empty!");
                        }
                        runtime.DefinedBuilders.Add(node.Attributes["name"].Value, new LazyLoadBuilder(plug, Properties.Parse(node.Attributes)));
                        break;
                    case "conditionevaluator":
                        if (node.Attributes.Count <= 0)
                        {
                            throw new PlugParseException("ConditionEvaluator nodes must be NOT empty!");
                        }
                        runtime.DefinedConditionEvaluators.Add(node.Attributes["name"].Value, new LazyConditionEvaluator(plug, Properties.Parse(node.Attributes)));
                        break;
                    default:
                        throw new PlugParseException("Unknown node in Import section: " + node.LocalName);
                }
            }
            return runtime;
        }

    }
}
