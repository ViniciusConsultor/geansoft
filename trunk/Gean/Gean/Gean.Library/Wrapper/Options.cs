using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;

namespace Gean
{
    public class Options
    {
        #region 单件实例

        /// <summary>
        /// Initializes a new instance of the <see cref="Options"/> class.
        /// </summary>
        private Options()
        {
            //在这里添加构造函数的代码
        }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static Options Instance
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton()
            {
                Instance = new Options();
            }

            internal static readonly Options Instance = null;
        }

        #endregion

        private static Dictionary<String, Object> _optionDictionary = new Dictionary<String, Object>();
        private static String _optionFile;

        /// <summary>
        /// Initializeses the specified option file.
        /// </summary>
        /// <param name="optionFile">option file的完全路径.</param>
        public static void Initializes(String optionFile)
        {
            _optionFile = optionFile;
            if (!File.Exists(optionFile))//如果用户的选项文件不存在，创建默认的选项文件
            {
                using (XmlTextWriter w = new XmlTextWriter(optionFile, Encoding.UTF8))
                {
                    w.WriteStartDocument();
                    w.WriteStartElement("Options");
                    w.WriteAttributeString("ProductName", Application.ProductName);
                    w.WriteAttributeString("Created", DateTime.Now.ToString());
                    w.WriteAttributeString("ApplicationVersion", Application.ProductVersion);
                    w.WriteAttributeString("OptionVersion", "1");
                    w.WriteAttributeString("UpdateCount", "0");
                    w.WriteAttributeString("Modified", DateTime.Now.ToString());
                    w.WriteEndElement();
                    w.Flush();
                }
            }
            try
            {
                XmlDocument optionXml = new XmlDocument();
                optionXml.Load(optionFile);
                LoadOptions(optionXml);
            }
            catch
            {
                File.Delete(optionFile);
                Initializes(optionFile);
            }
        }

        /// <summary>
        /// Loads the options.
        /// </summary>
        /// <param name="optionXml">The option XML.</param>
        private static void LoadOptions(XmlDocument optionXml)
        {
            foreach (XmlNode item in optionXml.DocumentElement.ChildNodes)
            {
                if (item.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement ele = (XmlElement)item;
                Object value = new Object();
                //如果选项的第一个子节点是一个Element节点的话，一般来讲是数组类型的Option节
                if (ele.FirstChild.NodeType == XmlNodeType.Element)
                {
                    value = new List<string>();
                    foreach (XmlNode arrayNode in ele.ChildNodes)
                    {
                        if (arrayNode.NodeType != XmlNodeType.Element)
                        {
                            Debug.Assert(arrayNode.NodeType == XmlNodeType.Element, arrayNode.OuterXml);
                            break;
                        }
                        XmlElement arrayEle = (XmlElement)arrayNode;
                        ((List<string>)value).Add(arrayEle.InnerText);
                    }
                }
                else
                {
                    value = ele.InnerText;
                }
                _optionDictionary.Add(ele.GetAttribute("name"), value);
            }
        }

        /// <summary>
        /// 获取是否有选项值
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has option; otherwise, <c>false</c>.
        /// </value>
        public bool HasOption { get { return _optionDictionary.Count > 0; } }

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified name.
        /// </summary>
        /// <value></value>
        public Object this[String name]
        {
            get { return this.GetOptionValue(name); }
            set { this.SetOption(name, value); }
        }

        /// <summary>
        /// Gets the option value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Object GetOptionValue(String name)
        {
            Object value = new Object();
            bool flag = TryGetOptionValue(name, out value);
            Debug.Assert(flag, String.Format("Option \"{0}\": Value is Null or Empty!", name));
            return value;
        }

        /// <summary>
        /// Tries the get option value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryGetOptionValue(String name, out Object value)
        {
            return _optionDictionary.TryGetValue(name, out value);
        }

        /// <summary>
        /// Sets the option.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void SetOption(String name, Object value)
        {
            if (_optionDictionary.ContainsKey(name))
            {
                //更新option的值
                _optionDictionary[name] = value;
            }
            else
            {
                //增加option的值
                _optionDictionary.Add(name, value);
            }

            //注册选项发生改变的项引发的事件
            OnOptionChanged(new OptionChangedEventArgs(name, value));
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            XmlDocument optionXml = new XmlDocument();
            optionXml.Load(_optionFile);
            while (optionXml.DocumentElement.HasChildNodes)
            {
                optionXml.DocumentElement.RemoveChild(optionXml.DocumentElement.FirstChild);
            }
            foreach (var item in _optionDictionary)
            {
                XmlElement ele = optionXml.CreateElement("section");
                if (item.Value is String)//如果值是普通字符串
                {
                    ele.SetAttribute("name", item.Key);
                    ele.InnerText = (String)item.Value;
                }
                else if (item.Value is IEnumerable)//如果值是集合类型
                {
                    ele.SetAttribute("name", item.Key);
                    IEnumerable values = (IEnumerable)item.Value;
                    foreach (var subItem in values)
                    {
                        XmlElement subEle = optionXml.CreateElement("value");
                        subEle.InnerText = (String)subItem;
                        ele.AppendChild(subEle);
                    }
                }
                optionXml.DocumentElement.AppendChild(ele);
            }
            int i;
            if (!int.TryParse(optionXml.DocumentElement.Attributes["UpdateCount"].Value, out i))
            {
                optionXml.DocumentElement.SetAttribute("UpdateCount", "1");
            }
            else
            {
                optionXml.DocumentElement.Attributes["UpdateCount"].Value = (i + 1).ToString();
            }
            optionXml.DocumentElement.Attributes["Modified"].Value = DateTime.Now.ToString();
            optionXml.Save(_optionFile);
        }

        /// <summary>
        /// 当选项改变的时候发生的事件
        /// </summary>
        public event OptionChangedEventHandler OptionChangedEvent;
        private void OnOptionChanged(OptionChangedEventArgs e)
        {
            if (OptionChangedEvent != null)
                OptionChangedEvent(this, e);
        }
        public delegate void OptionChangedEventHandler(Object sender, OptionChangedEventArgs e);
        public class OptionChangedEventArgs : EventArgs
        {
            public String OptionName { get; private set; }
            public Object OptionValue { get; private set; }
            public OptionChangedEventArgs(String name, Object value)
            {
                this.OptionName = name;
                this.OptionValue = value;
            }
        }

    }
}
