using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.Specialized;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 这是一个Plug的树状结构的描述类，每节点定义为该类型本身。
    /// 访问该类型的PlugPathItems属性将会形成一个比较大的树形结构。
    /// 同时，每个PlugPath下面将绑定一组Plug(PlugItems)，
    /// 如果该PlugPath属于对路径的描述性质，就不绑定Plug。
    /// Gean: 2009-06-27 1:28:32
    /// </summary>
    public sealed class PlugPath
    {
        private const char SPLIT_CHAR = '/';

        /// <summary>
        /// 路径的名字
        /// </summary>
        public string Name { get; internal set; }

        #region (PlugPath)路径相关

        /// <summary>
        /// 是否是根路径
        /// </summary>
        public bool IsRoot { get; internal set; }

        /// <summary>
        /// 返回是否有子级路径
        /// </summary>
        public bool HasChildPathItems
        {
            get { return this.PlugPathItems.Count > 0; }
        }

        /// <summary>
        /// 父级路径
        /// </summary>
        public PlugPath ParentPlugPath { get; private set; }

        /// <summary>
        /// 描述当前路径下所有子路径的集合
        /// </summary>
        public PlugPathCollection PlugPathItems { get; internal set; }

        public PlugPath SelectSingerPath(string pathstring)
        {
            List<string> paths = new List<string>();
            paths.AddRange(pathstring.Split(new char[] { SPLIT_CHAR }, StringSplitOptions.RemoveEmptyEntries));
            return this.SelectSingerPath(paths);
        }

        private PlugPath SelectSingerPath(List<string> paths)
        {
            foreach (PlugPath path in this.PlugPathItems)
            {
                if (path.Name.Equals(paths[0]))
                {
                    if (paths.Count == 1)
                    {
                        return path;
                    }
                    paths.RemoveAt(0);
                    path.SelectSingerPath(paths);
                }
            }
            return null;
        }


        #endregion

        #region (Plug)相关

        /// <summary>
        /// 该路径上是否有绑定插件(Plug)
        /// </summary>
        public bool HasPlug
        {
            get { return this.PlugItems != null && this.PlugItems.Count > 0; }
        }

        /// <summary>
        /// 描述当前路径下的所有Plug的集合,该集合原型是List。
        /// </summary>
        public PlugCollection PlugItems { get; internal set; }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name"></param>
        internal PlugPath(string name)
        {
            this.Name = name;
            this.PlugPathItems = new PlugPathCollection();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("| PathName: ").Append(this.Name);
#if DEBUG
            if (this.HasChildPathItems)
            {
                sb.Append("\r\n").Append("| Child Path Count: ").Append(this.PlugPathItems.Count.ToString());
            }
            else
            {
                sb.Append("\r\n| Has not ChildPlugPath.");
            }
            if (this.HasPlug)
            {
                sb.Append("\r\n").Append("| Plug Count: ").Append(this.PlugItems.Count.ToString());
                foreach (Plug plug in this.PlugItems)
                {
                    sb.Append("\r\n ---").Append(plug.Name);
                }
            }
            else
            {
                sb.Append("\r\n| Has not Plug.");
            }
#endif
            return sb.ToString();
        }

        #region 几个静态方法，供创建路径树使用

        /// <summary>
        /// 从一个XmlElement中解析出一个路径安装到路径树中去。
        /// </summary>
        /// <param name="element">一个描述路径的XmlElement</param>
        /// <param name="ownerPath">请输入原始路径树的根</param>
        internal static void Install(XmlElement element, PlugPath ownerPath, StringCollection enablePlus)
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
                Plug newPlug = Plug.Parse((XmlElement)node);
                foreach (string enablePlugName in enablePlus)//根据用户配置需求置Plug是否为可用状态
                {
                    if (enablePlugName.Equals(newPlug.Name))
                    {
                        newPlug.Enabled = false;
                        break;
                    }
                }
                plugs.Add(newPlug);
            }
            PlugPath.CheckInstallPath(ownerPath, paths, plugs);
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
            if (parentPath.PlugPathItems.TryGetValue(paths[0], out existsPath))
            {
                if (paths.Count == 1)
                {
                    return;//当传入的paths字符串集合中只有一个值的时候，证明已经检查完毕，结束该方法。
                }
                //如果该PlugPath已存在，从Path的List中移除首位，递归检查下面的Path
                paths.RemoveAt(0);
                PlugPath.CheckInstallPath(existsPath, paths, plugs);
            }
            else
            {
                //如果不存在，调用另一个方法安装Path
                PlugPath.SetupUnInstallPath(parentPath, paths, plugs);
            }
        }

        /// <summary>
        /// 安装路径的子方法
        /// </summary>
        /// <param name="paths">拆分后的路径字符串数组</param>
        /// <param name="parentPath">父级路径</param>
        private static void SetupUnInstallPath(PlugPath parentPath, List<string> paths, PlugCollection plugItems)
        {
            PlugPath newpath = new PlugPath(paths[0]);
            if (paths.Count == 1)
            {
                newpath.PlugItems = plugItems;// 给递归到的当前PlugPath绑定所有解析出来的Plug，可能有多个（多数情况下）
            }
            newpath.ParentPlugPath = parentPath;
            // 安装一个新的路径描述
            parentPath.PlugPathItems.Add(newpath);
            // 如果路径字符串数组仍有值，继续安装
            if (paths.Count > 1)
            {
                paths.RemoveAt(0);
                PlugPath.SetupUnInstallPath(newpath, paths, plugItems);//递归
            }
        }

        #endregion

    }
}
