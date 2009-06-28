using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 一个Plug对象，一般以Xml节点保存在一个Plug文件的＜Path＞节点下，一个＜Path＞节点下
    /// 可能保存多个该对象，在这个＜Path＞节点下的所有Plug节点会被一一解析，
    /// 并保存入PlugCollection集合中，并绑定到PlugPath对象的PlugItems属性中。
    /// <para>
    /// ＜Path＞
    ///       ＜MenuItem  id           = "File"
	///     	    label        = "${res:XML.MainMenu.FileMenu.New.File}"
	///     	    icon         = "Icons.16x16.NewDocumentIcon"
	///	            shortcut     = "Control|N"
    ///	            class        = "ICSharpCode.SharpDevelop.Commands.CreateNewFile"/>
    ///	＜/Path＞
    /// </para>
    /// </summary>
    public class Plug
    {
        /// <summary>
        /// 私有化构造函数，只能通过Parse静态方法解析得到该对象
        /// </summary>
        private Plug() { }

        /// <summary>
        /// 该Plug的名字，一般就是描述该Plug的XmlElement的LocalName
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 该Plug是否可用,True可用，False不可用
        /// </summary>
        public bool Enabled { get { return _Enabled; } internal set { this._Enabled = value; } }
        private bool _Enabled = true;

        /// <summary>
        /// 描述该Plug的数据集合
        /// </summary>
        public Definer Definers { get; private set; }

        /// <summary>
        /// 将一个描述Plug的XmlElement节点中解析成一个Plug
        /// </summary>
        /// <param name="element">一个描述Plug的XmlElement节点</param>
        internal static Plug Parse(XmlElement element)
        {
            Plug plug = new Plug();
            plug.Name = element.LocalName;
            plug.Definers = Definer.ReadFromAttributes(element.Attributes);
            return plug;
        }

        public static ConditionFalseAction GetFailedAction(object caller)
        {
            throw new NotImplementedException();
        }

        //Definers definers = new Definers();
        //List<Runtime> runtimes = new List<Runtime>();
        //List<string> bitmapResources = new List<string>();
        //List<string> stringResources = new List<string>();

        //internal string addInFileName = null;
        //AddInManifest manifest = new AddInManifest();
        //Dictionary<string, ExtensionPath> paths = new Dictionary<string, ExtensionPath>();
        //AddInAction action = AddInAction.Disable;
        //bool enabled;

        //static bool hasShownErrorMessage = false;

        //public object CreateObject()
        //{
        //    string classname = (string)this.Definers["class"];
        //    PlugTree.Runners.GetIRunObject(classname);
        //    return null;
        //}

        //public void LoadRuntimeAssemblies()
        //{
        //    LoadDependencies();
        //    foreach (Runtime runtime in runtimes)
        //    {
        //        runtime.Load();
        //    }
        //}

        //bool dependenciesLoaded;

        //void LoadDependencies()
        //{
        //    if (!dependenciesLoaded)
        //    {
        //        dependenciesLoaded = true;
        //        foreach (AddInReference r in manifest.Dependencies)
        //        {
        //            if (r.RequirePreload)
        //            {
        //                bool found = false;
        //                foreach (AddIn addIn in AddInTree.AddIns)
        //                {
        //                    if (addIn.Manifest.Identities.ContainsKey(r.Name))
        //                    {
        //                        found = true;
        //                        addIn.LoadRuntimeAssemblies();
        //                    }
        //                }
        //                if (!found)
        //                {
        //                    throw new AddInLoadException("Cannot load run-time dependency for " + r.ToString());
        //                }
        //            }
        //        }
        //    }
        //}

        //public override string ToString()
        //{
        //    return "[AddIn: " + Name + "]";
        //}

        //string customErrorMessage;

        ///// <summary>
        ///// Gets the message of a custom load error. Used only when AddInAction is set to CustomError.
        ///// Settings this property to a non-null value causes Enabled to be set to false and
        ///// Action to be set to AddInAction.CustomError.
        ///// </summary>
        //public string CustomErrorMessage
        //{
        //    get
        //    {
        //        return customErrorMessage;
        //    }
        //    internal set
        //    {
        //        if (value != null)
        //        {
        //            Enabled = false;
        //            Action = AddInAction.CustomError;
        //        }
        //        customErrorMessage = value;
        //    }
        //}

        ///// <summary>
        ///// Action to execute when the application is restarted.
        ///// </summary>
        //public AddInAction Action
        //{
        //    get
        //    {
        //        return action;
        //    }
        //    set
        //    {
        //        action = value;
        //    }
        //}
    }
}
