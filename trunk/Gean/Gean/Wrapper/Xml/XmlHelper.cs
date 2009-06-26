using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Xml.Schema;

namespace Gean.Xml
{
    /// <summary>
    /// 针对XmlDocument的一些扩展操作方法。静态类。
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// 根据XmlElement的LocalName获取一组XmlElement中的第一个Element
        /// </summary>
        /// <param name="node">将要查找的父级XmlNode</param>
        /// <param name="name">要查找的Element的LcoalName</param>
        /// <returns></returns>
        static public XmlElement GetElementByName(XmlNode node, string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(name));

            //return (XmlElement)node.SelectSingleNode(string.Format(@"(.//{0})[1]", name));

            XmlNode subnode = node.SelectSingleNode(string.Format(@"(.//{0})[1]", name));
            if (subnode == null)
            {
                XmlElement ele = node.OwnerDocument.CreateElement(name);
                if (name == "ID")
                {
                    ele.InnerText = UtilityGuid.Get();
                }
                node.AppendChild(ele);
                subnode = GetElementByName(node, name);
            }
            return (XmlElement)subnode;
        }
        /// <summary>
        /// 如果Element有一个属性的名为“id”，根据XmlElement的LocalName和id的值获取一组XmlElement中的第一个Element
        /// </summary>
        /// <param name="node">将要查找的父级XmlNode</param>
        /// <param name="name">要查找的Element的LcoalName</param>
        /// <param name="id">id的值</param>
        /// <returns></returns>
        static public XmlElement GetElementById(XmlNode node, string id, string name)
        {
            Debug.Assert(!string.IsNullOrEmpty(id));
            Debug.Assert(!string.IsNullOrEmpty(name));

            //XPath如：(.//channel[@id='1234'])[1]
            return (XmlElement)node.SelectSingleNode(string.Format(@"(.//{1}[@id='{0}'])[1]", id, name));
        }

        /// <summary>
        /// 从XmlElement里查代一组值是否存在，返回不存在的值，已存在的值的不返回。
        /// 如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">将搜索的XmlElement</param>
        /// <param name="itemNodeName">子XmlElement的LocalName</param>
        /// <param name="nodetype">数据存储的类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="valueList">值的数组</param>
        /// <param name="attributeName">当数据存储节点类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <returns>返回一组不包含在父级Element中的值的数组</returns>
        static public string[] ContainsValuesByGroupItems
            (XmlElement groupEle, string itemNodeName, XmlNodeType nodetype, string attributeName, params string[] valueList)
        {
            List<string> t = new List<string>();
            t.AddRange(valueList);
            return ContainsValuesByGroupItems(groupEle, itemNodeName, nodetype, attributeName, t);
        }
        
        /// <summary>
        /// 从XmlElement里查代一组值是否存在，返回不存在的值，已存在的值的不返回。
        /// 如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">将搜索的XmlElement</param>
        /// <param name="itemNodeName">子XmlElement的LocalName</param>
        /// <param name="nodetype">数据存储的类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="valueList">值的集合</param>
        /// <param name="attributeName">当数据存储节点类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <returns>返回一组不包含在父级Element中的值的数组</returns>
        static public string[] ContainsValuesByGroupItems
            (XmlElement groupEle, string itemNodeName, XmlNodeType nodetype, string attributeName, List<string> valueList)
        {
            List<string> returnValues = new List<string>(valueList.Count);
            XmlNodeList nodes = groupEle.SelectNodes(itemNodeName);

            switch (nodetype)
            {
                #region case
                case XmlNodeType.Attribute:
                    {
                        Debug.Assert(!string.IsNullOrEmpty(attributeName));
                        foreach (XmlNode node in nodes)
                        {
                            XmlElement ele = node as XmlElement;
                            string value = ele.GetAttribute(attributeName);
                            if (!valueList.Contains(value))
                            {
                                returnValues.Add(value);
                            }
                        }
                        break;
                    }
                case XmlNodeType.CDATA:
                case XmlNodeType.Text:
                    {
                        foreach (XmlNode node in nodes)
                        {
                            XmlElement ele = node as XmlElement;
                            string value = ele.InnerText;
                            if (!valueList.Contains(value))
                            {
                                returnValues.Add(value);
                            }
                        }
                        break;
                    }
                case XmlNodeType.Comment:
                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.DocumentType:
                case XmlNodeType.Element:
                case XmlNodeType.EndElement:
                case XmlNodeType.EndEntity:
                case XmlNodeType.Entity:
                case XmlNodeType.EntityReference:
                case XmlNodeType.None:
                case XmlNodeType.Notation:
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.SignificantWhitespace:
                case XmlNodeType.Whitespace:
                case XmlNodeType.XmlDeclaration:
                default:
                    Debug.Fail("值只能存放在Attribute，CDATA，Text三种类型的节点中！");
                    break;

                #endregion
            }

            return returnValues.ToArray();
        }

        /// <summary>
        /// 从XmlElement里获取一组值。
        /// 如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">从此XmlElement里获取值</param>
        /// <param name="itemNodeName">子节点的LocalName</param>
        /// <param name="nodetype">数据存储的类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="attributeName">当数据存储节点类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <returns>一组值的字符串</returns>
        static public string[] GetGroupItemsValue
            (XmlElement groupEle, string itemNodeName, XmlNodeType nodetype, string attributeName)
        {
            XmlNodeList nodes = groupEle.SelectNodes(itemNodeName);
            string[] returnItems = new string[nodes.Count];

            switch (nodetype)
            {
                #region case
                case XmlNodeType.Attribute:
                    {
                        Debug.Assert(!string.IsNullOrEmpty(attributeName));
                        int i = 0;
                        foreach (XmlNode node in nodes)
                        {
                            returnItems[i] = node.Attributes[attributeName].Value;
                            i++;
                        }
                        break;
                    }
                case XmlNodeType.CDATA:
                case XmlNodeType.Text:
                    {
                        int i = 0;
                        foreach (XmlNode node in nodes)
                        {
                            returnItems[i] = node.InnerText;
                            i++;
                        }
                        break;
                    }
                case XmlNodeType.Comment:
                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.DocumentType:
                case XmlNodeType.Element:
                case XmlNodeType.EndElement:
                case XmlNodeType.EndEntity:
                case XmlNodeType.Entity:
                case XmlNodeType.EntityReference:
                case XmlNodeType.None:
                case XmlNodeType.Notation:
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.SignificantWhitespace:
                case XmlNodeType.Whitespace:
                case XmlNodeType.XmlDeclaration:
                default:
                    Debug.Fail("值只能存放在Attribute，CDATA，Text三种类型的节点中！");
                    break;
                #endregion
            }

            return returnItems;
        }

        /// <summary>
        /// 向XmlElement里追加一组节点，并设置这组节点的值。
        /// 如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">将设置的XmlElement</param>
        /// <param name="itemNodeName">子XmlElement的LocalName</param>
        /// <param name="nodetype">数据存储的节点类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="attributeName">当数据存储类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <param name="isRepeat">是否允许有重复的值,true允许,false不允许(如不允许将增加大量的运算时间)</param>
        /// <param name="valueList">值的数组</param>
        static public void AppendGroupItemsValue
            (XmlElement groupEle, string itemNodeName, XmlNodeType nodetype, string attributeName, bool isRepeat, params string[] valueList)
        {
            List<string> t = new List<string>();
            t.AddRange(valueList);
            AppendGroupItemsValue(groupEle, itemNodeName, nodetype, attributeName, isRepeat, t);
        }        
        
        /// <summary>
        /// 向XmlElement里追加一组节点，并设置这组节点的值。
        /// 如：＜groups＞＜item value="a"＞＜/item＞＜item value="B"＞＜/item＞＜/groups＞
        /// </summary>
        /// <param name="groupEle">将设置的XmlElement</param>
        /// <param name="itemNodeName">子XmlElement的LocalName</param>
        /// <param name="nodetype">数据存储的节点类型,值只能存放在Attribute，CDATA，Text三种类型的节点中</param>
        /// <param name="attributeName">当数据存储类型为Attribute时的属性的LocalName，当其他类型时输入Null</param>
        /// <param name="isRepeat">是否允许有重复的值,true允许,false不允许(如不允许将增加大量的运算时间)</param>
        /// <param name="valueList">值的集合</param>
        static public void AppendGroupItemsValue
            (XmlElement groupEle, string itemNodeName, XmlNodeType nodetype, string attributeName, bool isRepeat, List<string> valueList)
        {
            switch (nodetype)
            {
                #region case
                case XmlNodeType.Attribute:
                    {
                        if (isRepeat)
                        {
                            foreach (string value in valueList)
                            {
                                XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                item.SetAttribute(attributeName, value);
                                groupEle.AppendChild(item);
                            }
                        }
                        else
                        {
                            List<string> vList = new List<string>(XmlHelper.GetGroupItemsValue(groupEle, itemNodeName, nodetype, ""));
                            foreach (string value in valueList)
                            {
                                if (!vList.Contains(value))
                                {
                                    XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                    item.SetAttribute(attributeName, value);
                                    groupEle.AppendChild(item);
                                }
                            }
                        }
                        break;
                    }
                case XmlNodeType.CDATA:
                    {
                        if (isRepeat)
                        {
                            foreach (string value in valueList)
                            {
                                XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                XmlCDataSection cdata = groupEle.OwnerDocument.CreateCDataSection(value);
                                item.AppendChild(cdata);
                                groupEle.AppendChild(item);
                            }
                        }
                        else
                        {
                            List<string> vList = new List<string>(XmlHelper.GetGroupItemsValue(groupEle, itemNodeName, nodetype, ""));
                            foreach (string value in valueList)
                            {
                                if (!vList.Contains(value))
                                {
                                    XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                    XmlCDataSection cdata = groupEle.OwnerDocument.CreateCDataSection(value);
                                    item.AppendChild(cdata);
                                    groupEle.AppendChild(item);
                                }
                            }
                        }
                        break;
                    }
                case XmlNodeType.Text:
                    {
                        if (isRepeat)
                        {
                            foreach (string value in valueList)
                            {
                                XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                item.InnerText = value;
                                groupEle.AppendChild(item);
                            }
                        }
                        else
                        {
                            List<string> vList = new List<string>(XmlHelper.GetGroupItemsValue(groupEle, itemNodeName, nodetype, ""));
                            foreach (string value in valueList)
                            {
                                if (!vList.Contains(value))
                                {
                                    XmlElement item = groupEle.OwnerDocument.CreateElement(itemNodeName);
                                    item.InnerText = value;
                                    groupEle.AppendChild(item);
                                }
                            }
                        }
                        break;
                    }
                case XmlNodeType.Comment:
                case XmlNodeType.Document:
                case XmlNodeType.DocumentFragment:
                case XmlNodeType.DocumentType:
                case XmlNodeType.Element:
                case XmlNodeType.EndElement:
                case XmlNodeType.EndEntity:
                case XmlNodeType.Entity:
                case XmlNodeType.EntityReference:
                case XmlNodeType.None:
                case XmlNodeType.Notation:
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.SignificantWhitespace:
                case XmlNodeType.Whitespace:
                case XmlNodeType.XmlDeclaration:
                default:
                    Debug.Fail("值只能存放在Attribute，CDATA，Text三种类型的节点中！");
                    break;

                #endregion
            }
        }

        /// <summary>
        /// 清除所有NodeType为Element的子节点
        /// </summary>
        /// <param name="groupEle">所有子节点的父节点</param>
        static public void RemoveAllChilds(XmlElement groupEle)
        {
            while (groupEle.ChildNodes != null)
            {
                groupEle.RemoveChild(groupEle.FirstChild);
            }
        }

        /// <summary>
        /// 创建一个新的Xml文件，如文件存在，将覆盖。
        /// </summary>
        /// <param name="file">Xml文件全名</param>
        /// <param name="rootnodename">根节点的LocalName</param>
        /// <param name="encoding">编码的字符串表示</param>
        /// <returns></returns>
        static public XmlDocument CreatNewDoucmnet(string file, string rootnodename, string encoding)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", encoding, null);
            XmlElement rootele = doc.CreateElement(rootnodename);
            doc.AppendChild(declaration);
            doc.AppendChild(rootele);
            if (!File.Exists(file))
            {
                Directory.CreateDirectory(file.Substring(0, file.LastIndexOf(@"\")));
            }
            doc.Save(file);
            return doc;
        }
        /// <summary>
        /// 创建一个新的Xml文件，如文件存在，将覆盖。默认为utf-8编码模式。
        /// </summary>
        /// <param name="file">Xml文件全名</param>
        /// <param name="rootnodename">根节点的LocalName</param>
        /// <returns></returns>
        static public XmlDocument CreatNewDoucmnet(string file, string rootnodename)
        {
            return CreatNewDoucmnet(file, rootnodename, "utf-8");
        }
        /// <summary>
        /// 创建一个新的Xml文件，如文件存在，将覆盖。默认为utf-8编码模式。默认根节点名root。
        /// </summary>
        /// <param name="file">Xml文件全名</param>
        /// <param name="rootnodename">根节点的LocalName</param>
        /// <returns></returns>
        static public XmlDocument CreatNewDoucmnet(string file)
        {
            return CreatNewDoucmnet(file, "root");
        }


        public static bool VerifyXmlFile(string file, XmlSchema xmlSchema)
        {
            return true;
        }
    }
}
