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
    /// 一个灵活的针对键值对进行了扩展的集合类型。
    /// 本类型内部容器是一个 Dictionary<string, object>。
    /// 本类型对序列化已实现。
    /// 本类型实现了XmlReader的从文件读取的静态方法。
    /// 2009-12-19 14:42:08
    /// </summary>
    public class Definition : IEnumerable
    {
        protected Dictionary<string, object> _definitions = new Dictionary<string, object>();

        public string this[string key]
        {
            get
            {
                return Convert.ToString(Get(key), CultureInfo.InvariantCulture);
            }
            set
            {
                Set(key, value);
            }
        }

        public string[] Elements
        {
            get
            {
                lock (_definitions)
                {
                    List<string> ret = new List<string>();
                    foreach (KeyValuePair<string, object> property in _definitions)
                    {
                        ret.Add(property.Key);
                    }
                    return ret.ToArray();
                }
            }
        }

        public object Get(string key)
        {
            lock (_definitions)
            {
                object val;
                _definitions.TryGetValue(key, out val);
                return val;
            }
        }

        public void Set<T>(string key, T value)
        {
            T oldValue = default(T);
            lock (_definitions)
            {
                if (!_definitions.ContainsKey(key))
                {
                    _definitions.Add(key, value);
                }
                else
                {
                    oldValue = Get<T>(key, value);
                    _definitions[key] = value;
                }
            }
            OnDefinitionChanged(new DefinitionChangedEventArgs(this, key, oldValue, value));
        }

        public bool Contains(string key)
        {
            lock (_definitions)
            {
                return _definitions.ContainsKey(key);
            }
        }

        public int Count
        {
            get
            {
                lock (_definitions)
                {
                    return _definitions.Count;
                }
            }
        }

        public bool Remove(string key)
        {
            lock (_definitions)
            {
                return _definitions.Remove(key);
            }
        }

        public override string ToString()
        {
            lock (_definitions)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[Properties:{");
                foreach (KeyValuePair<string, object> entry in _definitions)
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

        internal void ReadDefinition(XmlReader reader, string endElement)
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
                        if (propertyName == "Properties")
                        {
                            propertyName = reader.GetAttribute(0);
                            Definition p = new Definition();
                            p.ReadDefinition(reader, "Properties");
                            _definitions[propertyName] = p;
                        }
                        else if (propertyName == "Array")
                        {
                            propertyName = reader.GetAttribute(0);
                            _definitions[propertyName] = ReadArray(reader);
                        }
                        else if (propertyName == "SerializedValue")
                        {
                            propertyName = reader.GetAttribute(0);
                            _definitions[propertyName] = new SerializedValue(reader.ReadInnerXml());
                        }
                        else
                        {
                            _definitions[propertyName] = reader.HasAttributes ? reader.GetAttribute(0) : null;
                        }
                        break;
                }
            }
        }

        ArrayList ReadArray(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return new ArrayList(0);
            ArrayList l = new ArrayList();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == "Array")
                        {
                            return l;
                        }
                        break;
                    case XmlNodeType.Element:
                        l.Add(reader.HasAttributes ? reader.GetAttribute(0) : null);
                        break;
                }
            }
            return l;
        }

        public void WriteDefinition(XmlWriter writer)
        {
            lock (_definitions)
            {
                List<KeyValuePair<string, object>> sortedProperties = new List<KeyValuePair<string, object>>(_definitions);
                sortedProperties.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Key, b.Key));
                foreach (KeyValuePair<string, object> entry in sortedProperties)
                {
                    object val = entry.Value;
                    if (val is Definition)
                    {
                        writer.WriteStartElement("Properties");
                        writer.WriteAttributeString("name", entry.Key);
                        ((Definition)val).WriteDefinition(writer);
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
                        serializer.Serialize(writer, val, null);
                        writer.WriteEndElement();
                    }
                }
            }
        }

        void WriteValue(XmlWriter writer, object val)
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
                writer.WriteStartElement("Properties");
                WriteDefinition(writer);
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// [否决的][未实现的]
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void BinarySerialize(BinaryWriter writer)
        {
        }

        public static Definition ReadFromAttributes(XmlReader reader)
        {
            Definition properties = new Definition();
            if (reader.HasAttributes)
            {
                for (int i = 0; i < reader.AttributeCount; i++)
                {
                    reader.MoveToAttribute(i);
                    properties[reader.Name] = reader.Value;
                }
                reader.MoveToElement(); //Moves the reader back to the element node.
            }
            return properties;
        }

        public static Definition Load(string fileName)
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
                            case "Properties":
                                Definition properties = new Definition();
                                properties.ReadDefinition(reader, "Properties");
                                return properties;
                        }
                    }
                }
            }
            return null;
        }

        public T Get<T>(string property, T defaultValue)
        {
            lock (_definitions)
            {
                object o;
                if (!_definitions.TryGetValue(property, out o))
                {
                    _definitions.Add(property, defaultValue);
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
                        o = defaultValue;
                    }
                    _definitions[property] = o; // store for future look up
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
                        o = defaultValue;
                    }
                    _definitions[property] = o; // store for future look up
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
                        o = defaultValue;
                    }
                    _definitions[property] = o; // store for future look up
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

        public event DefinitionChangedEventHandler DefinitionChangedEvent;
        protected virtual void OnDefinitionChanged(DefinitionChangedEventArgs e)
        {
            if (DefinitionChangedEvent != null)
                DefinitionChangedEvent(this, e);
        }
        public delegate void DefinitionChangedEventHandler(object sender, DefinitionChangedEventArgs e);
        public class DefinitionChangedEventArgs : ChangedEventArgs<object>
        {
            /// <summary>
            /// Gets or sets 父级容器
            /// </summary>
            /// <value>The definition.</value>
            public Definition Definition { get; private set; }
            /// <summary>
            /// Gets or sets the key.
            /// </summary>
            /// <value>The key.</value>
            public string Key { get; private set; }

            public DefinitionChangedEventArgs(Definition properties, string key, object oldValue, object newValue)
                : base(oldValue, newValue)
            {
                this.Definition = properties;
                this.Key = key;
            }
        }

        /// <summary> 
        /// 需反序列化的特定对象
        /// </summary>
        private class SerializedValue
        {
            string content;

            public string Content
            {
                get { return content; }
            }

            public T Deserialize<T>()
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(new StringReader(content));
            }

            public SerializedValue(string content)
            {
                this.content = content;
            }
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return _definitions.GetEnumerator();
        }

        #endregion
    }
}