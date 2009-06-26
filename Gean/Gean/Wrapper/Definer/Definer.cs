using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Gean
{
    /// <summary>
    /// 一个类似Hashtable的保存“定义”的集合类(数据包)。
    /// Definer：可以直译为“定义者”，也就是对某事物(对象)的定义，即该对象的各个属性解释、定义，
    /// 所有属性(定义)以 Dictionary(string, object)的方式进行存储。键为字符串，而值可以为任何
    /// 可序列化的类型。一般来讲，该集合均为从XML的节点中解析得到，同样，他也可以序列化成 XML
    /// 节点或文件。
    /// 
    /// Gean: 2009-06-23 23:05:25
    /// </summary>
    public class Definer : IEnumerable
    {
        private Dictionary<string, object> definer = new Dictionary<string, object>();

        public string this[string key]
        {
            get { return Convert.ToString(Get(key), CultureInfo.InvariantCulture); }
            set { this.Set(key, value); }
        }

        public string[] Keys
        {
            get
            {
                lock (definer)
                {
                    List<string> ret = new List<string>();
                    foreach (KeyValuePair<string, object> property in definer)
                    {
                        ret.Add(property.Key);
                    }
                    return ret.ToArray();
                }
            }
        }

        public object Get(string key)
        {
            lock (definer)
            {
                object val;
                definer.TryGetValue(key, out val);
                return val;
            }
        }

        public T Get<T>(string key, T defaultValue)
        {
            lock (definer)
            {
                object o;
                if (!definer.TryGetValue(key, out o))
                {
                    definer.Add(key, defaultValue);
                    return defaultValue;
                }

                if (o is string && typeof(T) != typeof(string))
                {
                    TypeConverter c = TypeDescriptor.GetConverter(typeof(T));
                    try
                    {
                        o = c.ConvertFromInvariantString(o.ToString());
                    }
                    catch (Exception ex)
                    {
                        //MessageService.ShowWarning("Error loading property '" + property + "': " + ex.Message);
                        o = defaultValue;
                    }
                    definer[key] = o; // store for future look up
                }
                else if (o is ArrayList && typeof(T).IsArray)
                {
                    ArrayList list = (ArrayList)o;
                    Type elementType = typeof(T).GetElementType();
                    Array arr = System.Array.CreateInstance(elementType, list.Count);
                    TypeConverter c = TypeDescriptor.GetConverter(elementType);
                    try
                    {
                        for (int i = 0; i < arr.Length; ++i)
                        {
                            if (list[i] != null)
                            {
                                arr.SetValue(c.ConvertFromInvariantString(list[i].ToString()), i);
                            }
                        }
                        o = arr;
                    }
                    catch (Exception ex)
                    {
                        //MessageService.ShowWarning("Error loading property '" + property + "': " + ex.Message);
                        o = defaultValue;
                    }
                    definer[key] = o; // store for future look up
                }
                else if (!(o is string) && typeof(T) == typeof(string))
                {
                    TypeConverter c = TypeDescriptor.GetConverter(typeof(T));
                    if (c.CanConvertTo(typeof(string)))
                    {
                        o = c.ConvertToInvariantString(o);
                    }
                    else
                    {
                        o = o.ToString();
                    }
                }
                else if (o is SerializedValue)
                {
                    try
                    {
                        o = ((SerializedValue)o).Deserialize<T>();
                    }
                    catch (Exception ex)
                    {
                        //MessageService.ShowWarning("Error loading property '" + property + "': " + ex.Message);
                        o = defaultValue;
                    }
                    definer[key] = o; // store for future look up
                }
                try
                {
                    return (T)o;
                }
                catch (NullReferenceException)
                {
                    // can happen when configuration is invalid -> o is null and a value type is expected
                    return defaultValue;
                }
            }
        }
        
        public void Set<T>(string key, T value)
        {
            T oldValue = default(T);
            lock (definer)
            {
                if (!definer.ContainsKey(key))
                {
                    definer.Add(key, value);
                }
                else
                {
                    oldValue = Get<T>(key, value);
                    definer[key] = value;
                }
            }
            OnDefinerChanged(new DefinerChangedEventArgs(this, key, oldValue, value));
        }

        public bool Contains(string key)
        {
            lock (definer)
            {
                return definer.ContainsKey(key);
            }
        }

        public int Count
        {
            get
            {
                lock (definer)
                {
                    return definer.Count;
                }
            }
        }

        public bool Remove(string key)
        {
            lock (definer)
            {
                return definer.Remove(key);
            }
        }

        public bool TryGetValue(string key, out object value)
        {
            lock (this.definer)
            {
                return this.definer.TryGetValue(key, out value);
            }
        }

        public override string ToString()
        {
            lock (definer)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[Definer:{");
                foreach (KeyValuePair<string, object> entry in definer)
                {
                    sb.Append(entry.Key);
                    sb.Append("=");
                    sb.Append(entry.Value);
                    sb.Append(",");
                }
                sb.Append("}]");
                return sb.ToString();
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.definer.GetEnumerator();
        }

        #region 从XML文件或节点中读取数据的方法

        /// <summary>
        /// 从一个指定的文件读取，通常这个文件中一定要有“Definer”元素，
        /// 通常我们公认所有的数据也都是保存在这个元素的Attributes中。
        /// </summary>
        /// <param name="fileName">一个指定的XML文件</param>
        public static Definer Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.LocalName)
                        {
                            case "Definer":
                                Definer properties = new Definer();
                                properties.ReadXmlAttributes(reader, "Definer");
                                return properties;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 从XML的一个有效节点的Attribute中读取相关的数据以转换成Definer。
        /// </summary>
        /// <param name="reader">一个包含相应Xml节点信息的XmlReader</param>
        /// <returns></returns>
        public static Definer ReadFromAttributes(XmlReader reader)
        {
            Definer definer = new Definer();
            if (reader.HasAttributes)
            {
                for (int i = 0; i < reader.AttributeCount; i++)
                {
                    reader.MoveToAttribute(i);
                    definer[reader.Name] = reader.Value;
                }
                reader.MoveToElement(); 
            }
            return definer;
        }

        private void ReadXmlAttributes(XmlReader reader, string endElement)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == endElement)
                        {
                            return;
                        }
                        break;
                    case XmlNodeType.Element:
                        string propertyName = reader.LocalName;
                        if (propertyName == "Definer")
                        {
                            propertyName = reader.GetAttribute(0);
                            Definer p = new Definer();
                            p.ReadXmlAttributes(reader, "Definer");
                            definer[propertyName] = p;
                        }
                        else if (propertyName == "Array")
                        {
                            propertyName = reader.GetAttribute(0);
                            definer[propertyName] = ReadArray(reader);
                        }
                        else if (propertyName == "SerializedValue")
                        {
                            propertyName = reader.GetAttribute(0);
                            definer[propertyName] = new SerializedValue(reader.ReadInnerXml());
                        }
                        else
                        {
                            definer[propertyName] = reader.HasAttributes ? reader.GetAttribute(0) : null;
                        }
                        break;
                }
            }
        }

        private ArrayList ReadArray(XmlReader reader)
        {
            if (reader.IsEmptyElement)
            {
                return new ArrayList(0);
            }
            ArrayList array = new ArrayList();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == "Array")
                        {
                            return array;
                        }
                        break;
                    case XmlNodeType.Element:
                        array.Add(reader.HasAttributes ? reader.GetAttribute(0) : null);
                        break;
                }
            }
            return array;
        }

        #endregion

        #region 向XML文件或节点中写入数据的方法

        public void WriteDefine(XmlWriter writer)
        {
            lock (definer)
            {
                List<KeyValuePair<string, object>> sortedProperties = new List<KeyValuePair<string, object>>(definer);
                sortedProperties.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Key, b.Key));
                foreach (KeyValuePair<string, object> entry in sortedProperties)
                {
                    object val = entry.Value;
                    if (val is Definer)
                    {
                        writer.WriteStartElement("Definer");
                        writer.WriteAttributeString("name", entry.Key);
                        ((Definer)val).WriteDefine(writer);
                        writer.WriteEndElement();
                    }
                    else if (val is Array || val is ArrayList)
                    {
                        writer.WriteStartElement("Array");
                        writer.WriteAttributeString("name", entry.Key);
                        foreach (object o in (IEnumerable)val)
                        {
                            writer.WriteStartElement("Element");
                            WriteValue(writer, o);
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    else if (TypeDescriptor.GetConverter(val).CanConvertFrom(typeof(string)))
                    {
                        writer.WriteStartElement(entry.Key);
                        WriteValue(writer, val);
                        writer.WriteEndElement();
                    }
                    else if (val is SerializedValue)
                    {
                        writer.WriteStartElement("SerializedValue");
                        writer.WriteAttributeString("name", entry.Key);
                        writer.WriteRaw(((SerializedValue)val).Content);
                        writer.WriteEndElement();
                    }
                    else
                    {
                        writer.WriteStartElement("SerializedValue");
                        writer.WriteAttributeString("name", entry.Key);
                        XmlSerializer serializer = new XmlSerializer(val.GetType());
                        serializer.Serialize(writer, val);
                        writer.WriteEndElement();
                    }
                }
            }
        }

        private void WriteValue(XmlWriter writer, object val)
        {
            if (val != null)
            {
                if (val is string)
                {
                    writer.WriteAttributeString("value", val.ToString());
                }
                else
                {
                    TypeConverter c = TypeDescriptor.GetConverter(val.GetType());
                    writer.WriteAttributeString("value", c.ConvertToInvariantString(val));
                }
            }
        }

        public void Save(string fileName)
        {
            using (XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("Definer");
                WriteDefine(writer);
                writer.WriteEndElement();
            }
        }

        #endregion

        protected virtual void OnDefinerChanged(DefinerChangedEventArgs e)
        {
            if (DefinerChanged != null)
            {
                DefinerChanged(this, e);
            }
        }
        public event DefinerChangedEventHandler DefinerChanged;

        /// <summary>
        /// 描述一个可序列化的类
        /// </summary>
        class SerializedValue
        {
            public string Content { get; private set; }

            public T Deserialize<T>()
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(new StringReader(Content));
            }

            public SerializedValue(string content)
            {
                this.Content = content;
            }
        }
    }
}