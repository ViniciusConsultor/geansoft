using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Gean.Wrapper.PlugTree.Components;
using System.Collections;
using Gean.Wrapper.PlugTree.Exceptions;
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
    public class PlugPath : ICloneable, IEnumerable
    {  
        public PlugPathCollection Items { get; private set; }

        public PlugAtomCollection PlugAtoms { get; private set; }

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

        internal PlugPath(string name)
        {
            this.Name = name;
            this.Items = new PlugPathCollection();
            this.PlugAtoms = new PlugAtomCollection();
        }

        private static XmlElement _Element;
        internal static void Setup(XmlElement element, PlugPath plugpath, Plug ownerPlug)
        {
            _Element = element;
            string pathname = _Element.GetAttribute("name");
            if (string.IsNullOrEmpty(pathname))
            {
                throw new PlugTreeException("Aarhus: Path name is Null or Empty!");
            }
            string[] pathStringArrary = _Element.GetAttribute("name").Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            CheckPath(pathStringArrary, plugpath, ownerPlug);
        }

        /// <summary>
        /// 检查路径是否在路径树中
        /// </summary>
        /// <param name="pathStringArray">拆分后的路径字符串数组</param>
        /// <param name="plugpath">路径树</param>
        private static void CheckPath(string[] pathStringArray, PlugPath plugpath, Plug ownerPlug)
        {
            if (pathStringArray.Length <= 0 || pathStringArray == null)
            {
                return;
            }
            bool unSetup = true; // 是否已安装，true表示未安装，false表示已安装
            foreach (var item in plugpath.Items)
            {
                if (item.Name == pathStringArray[0].ToLowerInvariant())
                {
                    // 移除已检查过的数组中的第一个值后，复制到一个新的数组中
                    string[] newPathStringArray = new string[pathStringArray.Length - 1];
                    Array.Copy(pathStringArray, 1, newPathStringArray, 0, pathStringArray.Length - 1);
                    // 向前迭代
                    CheckPath(newPathStringArray, item, ownerPlug);
                    unSetup = false;
                    if (pathStringArray.Length == 1)
                    {
                        // 给迭代到的当前PlugPath绑定Block
                        item.PlugAtoms.AddRange(PlugAtom.Parse(_Element.ChildNodes, item, ownerPlug));
                    }
                    break;
                }
            }
            if (unSetup)
            {
                SetupUnInsertedPath(pathStringArray, plugpath, ownerPlug);
            }
        }

        /// <summary>
        /// 安装路径的子方法
        /// </summary>
        /// <param name="pathStringArray">拆分后的路径字符串数组</param>
        /// <param name="plugpath">路径树</param>
        private static void SetupUnInsertedPath(string[] pathStringArray, PlugPath parentPlugPath, Plug ownerPlug)
        {
            PlugPath newPlugpath = new PlugPath(pathStringArray[0].ToLowerInvariant());
            // 给迭代到的当前PlugPath绑定Block
            if (pathStringArray.Length == 1)
            {
                newPlugpath.PlugAtoms.AddRange(PlugAtom.Parse(_Element.ChildNodes, newPlugpath, ownerPlug));
            }
            newPlugpath.ParentPath = parentPlugPath;
            // 安装一个新的路径描述
            parentPlugPath.Items.Add(newPlugpath);
            // 如果路径字符串数组仍有值，继续安装
            if (pathStringArray.Length > 1) //a,b
            {
                // 移除数组中的刚才已安装的第一个值后，复制到一个新的数组中
                string[] newPathStringArray = new string[pathStringArray.Length - 1];
                Array.Copy(pathStringArray, 1, newPathStringArray, 0, pathStringArray.Length - 1);
                SetupUnInsertedPath(newPathStringArray, newPlugpath, ownerPlug);
            }
        }

        /// <summary>
        /// Builds the child items in this path. Ensures that all items have the type T.
        /// </summary>
        /// <param name="caller">The owner used to create the objects.</param>
        public List<T> BuildChildItems<T>(object caller)
        {
            List<T> items = new List<T>(this.PlugAtoms.Count);
            foreach (PlugAtom plugAtom in this.PlugAtoms)
            {
                ArrayList subItems = null;
                if (this.Items.Contains(plugAtom.OwnerPlugPath))
                {
                    subItems = plugAtom.OwnerPlugPath.BuildChildItems(caller);
                }
                object result = plugAtom.BuildItem(caller, subItems);
                if (result == null)
                    continue;
                if (result is T)
                {
                    items.Add((T)result);
                }
                else
                {
                    throw new InvalidCastException("The AddInTreeNode <" + plugAtom.Name + " id='" + plugAtom.Id
                                                   + "' returned an instance of " + result.GetType().FullName
                                                   + " but the type " + typeof(T).FullName + " is expected.");
                }
            }
            return items;
        }

        /// <summary>
        /// Builds the child items in this path.
        /// </summary>
        /// <param name="caller">The owner used to create the objects.</param>
        public ArrayList BuildChildItems(object caller)
        {
            ArrayList items = new ArrayList(this.PlugAtoms.Count);
            foreach (PlugAtom plugAtom in this.PlugAtoms)
            {
                ArrayList subItems = null;
                if (this.Items.Contains(plugAtom.OwnerPlugPath))
                {
                    subItems = plugAtom.OwnerPlugPath.BuildChildItems(caller);
                }
                object result = plugAtom.BuildItem(caller, subItems);
                if (result == null)
                    continue;
                items.Add(result);
            }
            return items;
        }

        /// <summary>
        /// Builds a specific child items in this path.
        /// </summary>
        /// <param name="childItemID">
        /// The ID of the child item to build.
        /// </param>
        /// <param name="caller">The owner used to create the objects.</param>
        /// <param name="subItems">The subitems to pass to the doozer</param>
        /// <exception cref="TreePathNotFoundException">
        /// Occurs when <paramref name="childItemID"/> does not exist in this path.
        /// </exception>
        public object BuildChildItem(string childItemID, object caller, ArrayList subItems)
        {
            foreach (PlugAtom plugAtom in this.PlugAtoms)
            {
                if (plugAtom.Id == childItemID)
                {
                    return plugAtom.BuildItem(caller, subItems);
                }
            }
            throw new TreePathNotFoundException(childItemID);
        }

        /// <summary>
        /// 路径的完整字符串表示
        /// </summary>
        private string _PathFullString = null;
        /// <summary>
        /// 已重写。生成路径的完整字符串
        /// </summary>
        public override string ToString()
        {
            if (this._PathFullString == null)
            {
                StringBuilder sb = new StringBuilder();
                ToString(sb, this);
                return this._PathFullString = sb.ToString();
            }
            else
            {
                return this._PathFullString;
            }
        }
        /// <summary>
        /// this.ToString()的子方法
        /// </summary>
        private static void ToString(StringBuilder sb, PlugPath plugPath)
        {
            sb.Insert(0, plugPath.Name).Insert(0, '/');
            if (!plugPath.IsRoot)
            {
                ToString(sb, plugPath.ParentPath);//递归
            }
        }

        #region ICloneable 成员

        public object Clone()
        {
            PlugPath colonePlugPath = new PlugPath(this.Name);
            colonePlugPath.PlugAtoms = this.PlugAtoms;
            colonePlugPath.IsRoot = this.IsRoot;
            colonePlugPath.Items = this.Items;
            colonePlugPath.ParentPath = this.ParentPath;
            colonePlugPath._PathFullString = this._PathFullString;
            return colonePlugPath;
        }

        #endregion

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        #endregion

    }


}
