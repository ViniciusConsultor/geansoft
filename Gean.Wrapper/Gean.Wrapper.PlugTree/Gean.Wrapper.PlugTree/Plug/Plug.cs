using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 一个Plug
    /// </summary>
    public sealed class Plug
    {
        internal Plug() { }

        public string Name { get; internal set; }

        /// <summary>
        /// 该Plug是否可用
        /// </summary>
        public bool Enabled { get; internal set; }

        /// <summary>
        /// 描述该路径的数据集合
        /// </summary>
        public Definers Definers { get; private set; }

        public object Tag { get; set; }

        internal static Plug Parse(XmlElement element)
        {
            Plug plug = new Plug();
            plug.Name = element.LocalName;
            plug.Definers = Definers.Parse(element);
            return plug;
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

        public static ConditionFalseAction GetFailedAction(object caller)
        {
            throw new NotImplementedException();
        }
    }
}
