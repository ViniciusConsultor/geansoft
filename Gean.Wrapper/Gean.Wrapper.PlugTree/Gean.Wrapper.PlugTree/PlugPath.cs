using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 这是一个树状结构的描述类，每节点定义为该类型本身。在Items属性节点下绑定其所有的子节点。
    /// 访问该类型的Items属性将会形成一个比较大的树形结构。
    /// 同时，每个PlugPath下面会绑定一个Definers对象。
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

        /// <summary>
        /// 是否是根路径
        /// </summary>
        public bool IsRoot
        {
            get { return _IsRoot; }
            set { _IsRoot = value; }
        }
        private bool _IsRoot = false;

        /// <summary>
        /// 返回是否有子级路径
        /// </summary>
        public bool HasChildPathItems
        {
            get { return this.Items.Count > 0; } 
        }

        public PlugPathCollection Items { get; internal set; }

        public Definers Definers { get; internal set; }

        public object Tag { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        internal PlugPath(string name)
        {
            this.Name = name;
            this.Items = new PlugPathCollection();
        }

        public override string ToString()
        {
            return string.Format("PlugPath: {0}", this.Name);
        }

        /// <summary>
        /// 从一个XmlElement中解析出一个路径安装到一个父级路径中去。
        /// </summary>
        /// <param name="element"></param>
        /// <param name="ownerPath"></param>
        internal static void Install(XmlElement element, PlugPath ownerPath)
        {
            string pathname = element.GetAttribute("name");
            if (string.IsNullOrEmpty(pathname))
            {
                throw new PlugTreeException("Gean: Plug name cannot empty!");
            }
            string[] paths = pathname.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            Definers properties = Definers.Parse(element);
            CheckInstallPath(ownerPath, paths, properties);
        }

        /// <summary>
        /// 检查该路径字符串所表达的路径是符已被安装
        /// </summary>
        /// <param name="parentPath">父级路径</param>
        /// <param name="paths">路径字符串解析出的数组</param>
        /// <param name="properties"></param>
        private static void CheckInstallPath(PlugPath parentPath, string[] paths, Definers definers)
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
                    CheckInstallPath(item, newPathStringArray, definers);
                    isInstall = false;
                    break;
                }
            }
            if (isInstall)
            {
                SetupUnInstallPath(parentPath, paths, definers);
            }
        }

        /// <summary>
        /// 安装路径的子方法
        /// </summary>
        /// <param name="paths">拆分后的路径字符串数组</param>
        /// <param name="parentPath">父级路径</param>
        private static void SetupUnInstallPath(PlugPath parentPath, string[] paths, Definers definers)
        {
            PlugPath newpath = new PlugPath(paths[0].ToLowerInvariant());
            if (paths.Length == 1)
            {
                newpath.Definers = definers;// 给递归到的当前PlugPath绑定Properties
            }
            newpath.ParentPath = parentPath;
            // 安装一个新的路径描述
            parentPath.Items.Add(newpath);
            // 如果路径字符串数组仍有值，继续安装
            if (paths.Length > 1) //a,b
            {
                // 移除数组中的刚才已安装的第一个值后，复制到一个新的数组中
                string[] newPathStringArray = new string[paths.Length - 1];
                Array.Copy(paths, 1, newPathStringArray, 0, paths.Length - 1);
                SetupUnInstallPath(newpath, newPathStringArray, definers);
            }
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        #endregion
    }
}
