using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;

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

        private static Dictionary<string, string> _optionDictionary = new Dictionary<string, string>();
        private static string _configFile;

        /// <summary>
        /// Initializeses the specified config file.
        /// </summary>
        /// <param name="configFile">The config file.</param>
        public static void Initializes(string configFile)
        {
            _configFile = configFile;
            if (!File.Exists(configFile))//如果用户的配置文件不存在，创建默认的配置文件
            {
                using (XmlTextWriter w = new XmlTextWriter(configFile, Encoding.UTF8))
                {
                    w.WriteStartDocument();
                    w.WriteStartElement("configuration");
                    w.WriteAttributeString("created", DateTime.Now.ToString());
                    w.WriteAttributeString("applicationVersion", "1");
                    w.WriteAttributeString("modified", DateTime.Now.ToString());
                    w.WriteEndElement();
                    w.Flush();
                }
            }
            try
            {
                XmlDocument optionXml = new XmlDocument();
                optionXml.Load(configFile);
                LoadOptions(optionXml);
            }
            catch
            {
                File.Delete(configFile);
                Initializes(configFile);
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
                _optionDictionary.Add(ele.GetAttribute("name"), ele.InnerText);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified name.
        /// </summary>
        /// <value></value>
        public string this[string name]
        {
            get { return this.GetOptionValue(name); }
            set { this.SetOption(name, value); }
        }

        /// <summary>
        /// Gets the option value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string GetOptionValue(string name)
        {
            string value = string.Empty;
            Debug.Assert(TryGetOptionValue(name, out value), string.Format("Option \"{0}\": Value is Null or Empty!", name));
            return value;
        }

        /// <summary>
        /// Tries the get option value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public bool TryGetOptionValue(string name, out string value)
        {
            return _optionDictionary.TryGetValue(name, out value);
        }

        /// <summary>
        /// Sets the option.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        public void SetOption(string name, string value)
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
            optionXml.Load(_configFile);
            while (optionXml.DocumentElement.HasChildNodes)
            {
                optionXml.DocumentElement.RemoveChild(optionXml.DocumentElement.FirstChild);
            }
            foreach (var item in _optionDictionary)
            {
                XmlElement ele = optionXml.CreateElement("section");
                ele.SetAttribute("name", item.Key);
                ele.InnerText = item.Value;
                optionXml.DocumentElement.AppendChild(ele);
            }
            optionXml.Save(_configFile);
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
        public delegate void OptionChangedEventHandler(object sender, OptionChangedEventArgs e);
        public class OptionChangedEventArgs : EventArgs
        {
            public string OptionName { get; private set; }
            public string OptionValue { get; private set; }
            public OptionChangedEventArgs(string name, string value)
            {
                this.OptionName = name;
                this.OptionValue = value;
            }
        }

    }
}
