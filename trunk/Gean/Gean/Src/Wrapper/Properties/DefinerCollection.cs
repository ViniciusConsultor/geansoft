using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Gean
{
    /// <summary>
    /// 属性值集合
    /// </summary>
    public sealed class DefinerCollection : IEnumerable
    {
        internal DefinerCollection() {}

        private Dictionary<string, object> _DefinerCollection = new Dictionary<string, object>();

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set<T>(string key, T value)
        { 
            T oldValue = default(T);
            lock (this._DefinerCollection)
            {
                key = key.ToLowerInvariant();
                if (!this._DefinerCollection.ContainsKey(key))
                {
                    this._DefinerCollection.Add(key, value);
                }
                else
                {
                    oldValue = Get<T>(key, value);
                    this._DefinerCollection[key] = value;
                }
            }
            OnPropertyChanged(new DefinerCollectionItemChangedEventArgs(this, key, oldValue, value));
        }

        /// <summary>
        /// 获得一个值，该值的类型是泛型，
        /// 如果这个key不能取出值，返回这个默认值，并将这个默认值添加进Properties中去
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="defalutValue">如果这个key不能取出值，返回这个默认值</param>
        /// <returns></returns>
        public T Get<T>(string key, T defaultValue)
        {
            lock (this._DefinerCollection)
            {
                object obj;
                if (!this._DefinerCollection.TryGetValue(key, out obj))
                {
                    this._DefinerCollection.Add(key, defaultValue);
                    return defaultValue;
                }

                if (obj is string && typeof(T) != typeof(string))//NUnitTest: TGetTest1
                {
                    TypeConverter conver = TypeDescriptor.GetConverter(typeof(T));
                    try
                    {
                        obj = conver.ConvertFromInvariantString(obj.ToString());
                    }
                    catch (Exception)
                    {
                        obj = defaultValue;
                    }
                    this._DefinerCollection[key] = obj;
                }
                else if (obj is ArrayList && typeof(T).IsArray)//NUnitTest: TGetTest2
                {
                    ArrayList list = (ArrayList)obj;
                    Type elementType = typeof(T).GetElementType();
                    Array arr = System.Array.CreateInstance(elementType, list.Count);
                    TypeConverter conver = TypeDescriptor.GetConverter(elementType);
                    try
                    {
                        for (int i = 0; i < arr.Length; ++i)
                        {
                            if (list[i] != null)
                            {
                                arr.SetValue(conver.ConvertFromInvariantString(list[i].ToString()), i);
                            }
                        }
                        obj = arr;
                    }
                    catch (Exception)
                    {
                        obj = defaultValue;
                    }
                    this._DefinerCollection[key] = obj;
                }
                else if (!(obj is string) && typeof(T) == typeof(string))
                {
                    TypeConverter conver = TypeDescriptor.GetConverter(typeof(T));
                    if (conver.CanConvertTo(typeof(string)))
                    {
                        obj = conver.ConvertToInvariantString(obj);
                    }
                    else
                    {
                        obj = obj.ToString();
                    }
                }
                else if (obj is SerializedValue)//NUnitTest: TGetTestSerializedValue
                {
                    try
                    {
                        obj = ((SerializedValue)obj).Deserialize<T>();
                    }
                    catch (Exception)
                    {
                        obj = defaultValue;
                    }
                    this._DefinerCollection[key] = obj;
                }
                try
                {
                    return (T)obj;
                }
                catch (NullReferenceException)
                {
                    return defaultValue;
                }
            }
        }

        #region 从一个保存有Properties的文件中解析Properties

        /// <summary>
        /// 从指定的文件中载入保存的状态信息
        /// </summary>
        /// <param name="fileName">指定的文件</param>
        /// <returns></returns>
        public static DefinerCollection Parse(string fileName)
        {
            if (!File.Exists(fileName))
                return null;
            using (XmlTextReader reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.LocalName)
                        {
                            case "Properties":
                                DefinerCollection properties = new DefinerCollection();
                                properties.ReadProperties(reader, "Properties");
                                return properties;
                        }
                    }
                }
            }
            return null;
        }
        private void ReadProperties(XmlTextReader reader, string endElement)
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
                            DefinerCollection p = new DefinerCollection();
                            p.ReadProperties(reader, "Properties");
                            this._DefinerCollection[propertyName] = p;
                        }
                        else if (propertyName == "Array")
                        {
                            propertyName = reader.GetAttribute(0);
                            this._DefinerCollection[propertyName] = ReadArray(reader);
                        }
                        else if (propertyName == "SerializedValue")
                        {
                            propertyName = reader.GetAttribute(0);
                            this._DefinerCollection[propertyName] = new SerializedValue(reader.ReadInnerXml());
                        }
                        else
                        {
                            this._DefinerCollection[propertyName] = reader.HasAttributes ? reader.GetAttribute(0) : null;
                        }
                        break;
                }
            }
        }
        private static ArrayList ReadArray(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return new ArrayList(0);
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

        #region 从XmlElement中解析Properties

        public static DefinerCollection Parse(XmlElement element)
        {
            return Parse(element, true);
        }
        public static DefinerCollection Parse(XmlElement element, bool isParseChildNodes)
        {
            if (isParseChildNodes)
            {
                return Parse(element.Attributes) + Parse(element.ChildNodes);
            }
            else
            {
                return Parse(element.Attributes);
            }
        }
        public static DefinerCollection Parse(XmlAttributeCollection attributes)
        {
            if (attributes == null || attributes.Count <= 0)
                return null;
            DefinerCollection properties = new DefinerCollection();
            foreach (XmlAttribute item in attributes)
            {
                properties.Set<string>(item.LocalName.ToLowerInvariant(), item.Value);
            }
            return properties;
        }
        public static DefinerCollection Parse(XmlNodeList childNodes)
        {
            if (childNodes == null || childNodes.Count <= 0)
                return null;
            DefinerCollection properties = new DefinerCollection();
            foreach (XmlNode node in childNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                properties.Set<string>(node.LocalName.ToLowerInvariant(), node.InnerText);
            }
            return properties;
        }

        #endregion

        public static DefinerCollection operator +(DefinerCollection pa, DefinerCollection pb)
        {
            foreach (var item in pb._DefinerCollection)
            {
                if (pa._DefinerCollection.ContainsKey(item.Key))
                {
                    continue;
                }
                pa._DefinerCollection.Add(item.Key, item.Value);
            }
            return pa;
        }

        /// <summary>
        /// 将Properties封装存入指定的文件
        /// </summary>
        /// <param name="fileName">指定的文件</param>
        public void Save(string fileName)
        {
            using (XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("Properties");
                this.WriteProperties(writer);
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// 将Properties写入XmlWriter
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void WriteProperties(XmlWriter writer)
        {
            lock (this._DefinerCollection)
            {
                List<KeyValuePair<string, object>> sortedProperties = new List<KeyValuePair<string, object>>(this._DefinerCollection);
                sortedProperties.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Key, b.Key));
                foreach (KeyValuePair<string, object> entry in sortedProperties)
                {
                    object val = entry.Value;
                    if (val is DefinerCollection)
                    {
                        writer.WriteStartElement("Properties");
                        writer.WriteAttributeString("name", entry.Key);
                        ((DefinerCollection)val).WriteProperties(writer);
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

        /// <summary>
        /// 当Property值发生改变时
        /// </summary>
        public event PropertiesItemChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(DefinerCollectionItemChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return this._DefinerCollection.GetEnumerator();
        }

        #endregion
    }

    public delegate void PropertiesItemChangedEventHandler(object sender, DefinerCollectionItemChangedEventArgs e);

}