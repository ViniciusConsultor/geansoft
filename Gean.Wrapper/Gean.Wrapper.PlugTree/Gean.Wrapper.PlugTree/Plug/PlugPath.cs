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
        private const char SPLIT_CHAR = '/';

        public string Name { get; internal set; }

        /// <summary>
        /// 是否是根路径
        /// </summary>
        public bool IsRoot { get; internal set; }

        /// <summary>
        /// 返回是否有子级路径
        /// </summary>
        public bool HasChildPathItems
        {
            get { return this.PlugPaths.Count > 0; } 
        }

        public PlugPath ParentPlugPath { get; private set; }

        public PlugCollection Plugs { get; internal set; }

        public PlugPathCollection PlugPaths { get; internal set; }

        public object Tag { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        internal PlugPath(string name)
        {
            this.Name = name;
            this.PlugPaths = new PlugPathCollection();
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

            //使用List<string>，而不用数组，是便于代码易读性，增加的负担量并不大。
            List<string> paths = new List<string>();
            paths.AddRange(pathname.Split(new char[] { SPLIT_CHAR }, StringSplitOptions.RemoveEmptyEntries));

            PlugCollection plugs = new PlugCollection();
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                plugs.Add(Plug.Parse((XmlElement)node));
            }
            CheckInstallPath(ownerPath, paths, plugs);
        }

        /// <summary>
        /// 检查该路径字符串所表达的路径是符已被安装
        /// </summary>
        /// <param name="parentPath">父级路径</param>
        /// <param name="paths">路径字符串解析出的数组</param>
        /// <param name="properties"></param>
        private static void CheckInstallPath(PlugPath parentPath, List<string> paths, PlugCollection plugs)
        {
            if (paths.Count <= 0 || paths == null)
            {
                throw new PlugTreeException("Gean: Plug name cannot empty!");
            }

            PlugPath existsPath;
            if (parentPath.PlugPaths.TryGetValue(paths[0], out existsPath))
            {
                if (paths.Count == 1)
                {
                    return;//当传入的paths字符串集合中只有一个值的时候，证明已经检查完毕，结束该方法。
                }
                //如果该PlugPath已存在，从Path的List中移除首位，递归检查下面的Path
                paths.RemoveAt(0);
                CheckInstallPath(existsPath, paths, plugs);
            }
            else
            {
                //如果不存在，调用另一个方法安装Path
                SetupUnInstallPath(parentPath, paths, plugs);
            }
        }

        /// <summary>
        /// 安装路径的子方法
        /// </summary>
        /// <param name="paths">拆分后的路径字符串数组</param>
        /// <param name="parentPath">父级路径</param>
        private static void SetupUnInstallPath(PlugPath parentPath, List<string> paths, PlugCollection plugs)
        {
            PlugPath newpath = new PlugPath(paths[0]);
            if (paths.Count == 1)
            {
                newpath.Plugs = plugs;// 给递归到的当前PlugPath绑定所有解析出来的Plug，可能有多个（多数情况下）
            }
            newpath.ParentPlugPath = parentPath;
            // 安装一个新的路径描述
            parentPath.PlugPaths.Add(newpath);
            // 如果路径字符串数组仍有值，继续安装
            if (paths.Count > 1) 
            {
                paths.RemoveAt(0);
                SetupUnInstallPath(newpath, paths, plugs);
            }
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return this.PlugPaths.GetEnumerator();
        }

        #endregion
    }
}
