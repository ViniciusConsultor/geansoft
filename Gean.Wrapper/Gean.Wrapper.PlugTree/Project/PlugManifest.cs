using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree.Components;
using System.Xml;
using Gean.Framework;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 描述一个Plug签名的信息。一般来讲是在Plug的Xml描述文件中根节点下的Manife节点。
    /// ＜Manifest＞
    ///	    ＜Identity name = "ICSharpCode.WixBinding"/＞
    ///	    ＜Dependency addin = "ICSharpCode.FormsDesigner" requirePreload = "true"/＞
    ///	    ＜Conflict addin = "ICSharpCode.XmlEditor" requirePreload = "true"/＞
    /// ＜/Manifest＞
    /// </summary>
    public sealed class PlugManifest
    {
        public string Name { get; private set; }

        public Plug OwnerPlug { get; private set; }

        public Properties Properties { get; private set; }

        public Dictionary<string, Version> Identities { get; private set; }

        public VersionPairCollection Dependencies { get; private set; }

        public VersionPairCollection Conflicts { get; private set; }

        private PlugManifest(string name)
        {
            this.Identities = new Dictionary<string, Version>();
            this.Dependencies = new VersionPairCollection();
            this.Conflicts = new VersionPairCollection();
        }

        /// <summary>
        /// 解析Manife节点的XmlElement
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
            if (plug.PlugManifest == null)
            {
                plug.PlugManifest = new PlugManifest(name);
            }
            plug.PlugManifest.OwnerPlug = plug;
            plug.PlugManifest.Properties = Properties.Parse(element);
            foreach (XmlNode item in element.ChildNodes) // 遍历Manifest的所有子节点
            {
                if (item.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement ele = (XmlElement)item;
                Properties ppties = Properties.Parse(ele);
                switch (ele.LocalName.ToLowerInvariant())
                {
                    case "identity":
                        {
                            plug.PlugManifest.Identities.Add(
                               ele.GetAttribute("name"),
                               VersionPair.ParseVersion(ele.GetAttribute("version")));
                            break;
                        }
                    case "dependency":
                        {
                            plug.PlugManifest.Dependencies.Add(
                               ele.GetAttribute("name"),
                               VersionPair.Create(ppties));
                            break;
                        }
                    case "conflict":
                        {
                            plug.PlugManifest.Conflicts.Add(
                               ele.GetAttribute("name"),
                               VersionPair.Create(ppties));
                            break;
                        }
                    case "disableplug":
                        {
                            plug.CustomErrorMessage = ele.GetAttribute("message");
                            break;
                        }
                    default:
                        break;
                }
            }
        }

    }
}
