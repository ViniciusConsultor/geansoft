using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using System.Xml.XPath;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 对Plug的路径以及该Plug下的PlugAtom集合的描述类。
    /// 在PlugManager中会初始化一个该类型，然后从其他Plug文件中所读取的所有Plug的路径都将会以
    /// 安装到树的动作进行路径的安装。
    /// 访问该类型的Items属性将会形成一个比较大的树形结构。
    /// 同时，每个PlugPath下面会绑定一些(有可能没有，有可能很多)的Block对象。
    /// 初始化完成后，将遍历整个PlugPath树的PlugAtom来Build相应的对象。
    /// 类似于下述节点的中的name属性值：＜Path name = "/Develop/Pads/Browser/ToolBar/Standard"＞。
    /// </summary>
    public sealed class PlugPath : IEnumerable
    {  
        public PlugPath ParentPath { get; private set; }

        public string Name
        {
            get { return this._Name; }
            internal set { this._Name = value.ToLowerInvariant(); }
        }
        private string _Name;

        public bool IsRoot
        {
            get { return _IsRoot; }
            set { _IsRoot = value; }
        }
        private bool _IsRoot = false;

        public bool HasChildPath
        {
            get { return this.Items.Count > 0; } 
        }

        public PlugPathCollection Items { get; set; }

        public Properties Properties { get; set; }

        internal PlugPath(string name)
        {
            this.Name = name;
            this.Items = new PlugPathCollection();
        }

        public override string ToString()
        {
            return (new StringBuilder()).Append("PlugPath: ").Append(this.Name).ToString();
        }

        internal static void Install(XmlElement element, PlugPath ownerPath)
        {
            string pathname = element.GetAttribute("name");
            if (string.IsNullOrEmpty(pathname))
            {
                throw new PlugTreeException("Gean: Plug name cannot empty!");
            }
            string[] paths = pathname.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            Properties properties = Properties.Parse(element);
            CheckInstallPath(ownerPath, paths, properties);
        }

        private static void CheckInstallPath(PlugPath parentPath, string[] paths, Properties properties)
        {
            if (paths.Length <= 0 || paths == null)
            {
                throw new PlugTreeException("Gean: Plug name cannot empty!");
            }
            bool isInstall = true; // 是否已安装，true表示未安装，false表示已安装
            foreach (var item in parentPath.Items)
            {
                if (item.Name == paths[0].ToLowerInvariant())
                {
                    // 移除已检查过的数组中的第一个值后，复制到一个新的数组中
                    string[] newPathStringArray = new string[paths.Length - 1];
                    Array.Copy(paths, 1, newPathStringArray, 0, paths.Length - 1);
                    // 向前递归
                    CheckInstallPath(item, newPathStringArray, properties);
                    isInstall = false;
                    if (paths.Length == 1)
                    {
                        // 给递归到的当前PlugPath绑定Properties
                        item.Properties = properties;
                    }
                    break;
                }
            }
            if (isInstall)
            {
                SetupUnInstallPath(parentPath, paths, properties);
            }
        }

        /// <summary>
        /// 安装路径的子方法
        /// </summary>
        /// <param name="pathStringArray">拆分后的路径字符串数组</param>
        /// <param name="plugpath">路径树</param>
        private static void SetupUnInstallPath(PlugPath parentPath, string[] pathStringArray, Properties properties)
        {
            PlugPath newPlugpath = new PlugPath(pathStringArray[0].ToLowerInvariant());
            if (pathStringArray.Length == 1)
            {
                newPlugpath.Properties = properties;// 给递归到的当前PlugPath绑定Properties
            }
            newPlugpath.ParentPath = parentPath;
            // 安装一个新的路径描述
            parentPath.Items.Add(newPlugpath);
            // 如果路径字符串数组仍有值，继续安装
            if (pathStringArray.Length > 1) //a,b
            {
                // 移除数组中的刚才已安装的第一个值后，复制到一个新的数组中
                string[] newPathStringArray = new string[pathStringArray.Length - 1];
                Array.Copy(pathStringArray, 1, newPathStringArray, 0, pathStringArray.Length - 1);
                SetupUnInstallPath(newPlugpath, newPathStringArray, properties);
            }
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        #endregion


        ///// <summary>
        ///// Builds the child items in this path. Ensures that all items have the type T.
        ///// </summary>
        ///// <param name="caller">The owner used to create the objects.</param>
        //public List<T> BuildChildItems<T>(object caller)
        //{
        //    List<T> items = new List<T>(this.PlugAtoms.Count);
        //    foreach (PlugAtom plugAtom in this.PlugAtoms)
        //    {
        //        ArrayList subItems = null;
        //        if (this.Items.Contains(plugAtom.OwnerPlugPath))
        //        {
        //            subItems = plugAtom.OwnerPlugPath.BuildChildItems(caller);
        //        }
        //        object result = plugAtom.BuildItem(caller, subItems);
        //        if (result == null)
        //            continue;
        //        if (result is T)
        //        {
        //            items.Add((T)result);
        //        }
        //        else
        //        {
        //            throw new InvalidCastException("The AddInTreeNode <" + plugAtom.Name + " id='" + plugAtom.Id
        //                                           + "' returned an instance of " + result.GetType().FullName
        //                                           + " but the type " + typeof(T).FullName + " is expected.");
        //        }
        //    }
        //    return items;
        //}

        ///// <summary>
        ///// Builds the child items in this path.
        ///// </summary>
        ///// <param name="caller">The owner used to create the objects.</param>
        //public ArrayList BuildChildItems(object caller)
        //{
        //    ArrayList items = new ArrayList(this.PlugAtoms.Count);
        //    foreach (PlugAtom plugAtom in this.PlugAtoms)
        //    {
        //        ArrayList subItems = null;
        //        if (this.Items.Contains(plugAtom.OwnerPlugPath))
        //        {
        //            subItems = plugAtom.OwnerPlugPath.BuildChildItems(caller);
        //        }
        //        object result = plugAtom.BuildItem(caller, subItems);
        //        if (result == null)
        //            continue;
        //        items.Add(result);
        //    }
        //    return items;
        //}

        ///// <summary>
        ///// Builds a specific child items in this path.
        ///// </summary>
        ///// <param name="childItemID">
        ///// The ID of the child item to build.
        ///// </param>
        ///// <param name="caller">The owner used to create the objects.</param>
        ///// <param name="subItems">The subitems to pass to the doozer</param>
        ///// <exception cref="TreePathNotFoundException">
        ///// Occurs when <paramref name="childItemID"/> does not exist in this path.
        ///// </exception>
        //public object BuildChildItem(string childItemID, object caller, ArrayList subItems)
        //{
        //    foreach (PlugAtom plugAtom in this.PlugAtoms)
        //    {
        //        if (plugAtom.Id == childItemID)
        //        {
        //            return plugAtom.BuildItem(caller, subItems);
        //        }
        //    }
        //    throw new TreePathNotFoundException(childItemID);
        //}

        ///// <summary>
        ///// 路径的完整字符串表示
        ///// </summary>
        //private string _PathFullString = null;
        ///// <summary>
        ///// 已重写。生成路径的完整字符串
        ///// </summary>
        //public override string ToString()
        //{
        //    if (this._PathFullString == null)
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        ToString(sb, this);
        //        return this._PathFullString = sb.ToString();
        //    }
        //    else
        //    {
        //        return this._PathFullString;
        //    }
        //}
        ///// <summary>
        ///// this.ToString()的子方法
        ///// </summary>
        //private static void ToString(StringBuilder sb, PlugPath plugPath)
        //{
        //    sb.Insert(0, plugPath.Name).Insert(0, '/');
        //    if (!plugPath.IsRoot)
        //    {
        //        ToString(sb, plugPath.ParentPath);//递归
        //    }
        //}

        //#region ICloneable 成员

        //public object Clone()
        //{
        //    PlugPath colonePlugPath = new PlugPath(this.Name);
        //    colonePlugPath.PlugAtoms = this.PlugAtoms;
        //    colonePlugPath.IsRoot = this.IsRoot;
        //    colonePlugPath.Items = this.Items;
        //    colonePlugPath.ParentPath = this.ParentPath;
        //    colonePlugPath._PathFullString = this._PathFullString;
        //    return colonePlugPath;
        //}

        //#endregion

        //#region IEnumerable 成员

        //public IEnumerator GetEnumerator()
        //{
        //    return this.Items.GetEnumerator();
        //}

        //#endregion
    }

    public class PlugPathCollection : List<PlugPath> { }
}
