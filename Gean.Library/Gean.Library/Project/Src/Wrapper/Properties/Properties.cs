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
    public sealed class Properties : EventIndexedDictionary<string, object>
    {
        internal Properties() {}

        private Dictionary<string, object> _Properties = new Dictionary<string, object>();

        /// <summary>
        /// Sets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Set<T>(string key, T value)
        {
            T oldValue = default(T);
            lock (this._Properties)
            {
                key = key.ToLowerInvariant();
                if (!this._Properties.ContainsKey(key))
                {
                    this._Properties.Add(key, value);
                }
                else
                {
                    oldValue = Get<T>(key, value);
                    this._Properties[key] = value;
                }
            }
            OnPropertyChanged(new PropertiesItemChangedEventArgs(this, key, oldValue, value));
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
            lock (this._Properties)
            {
                object obj;
                if (!this._Properties.TryGetValue(key, out obj))
                {
                    this._Properties.Add(key, defaultValue);
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
                    this._Properties[key] = obj;
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
                    this._Properties[key] = obj;
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
                    this._Properties[key] = obj;
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
        public static Properties Parse(string fileName)
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
                                Properties properties = new Properties();
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
                            Properties p = new Properties();
                            p.ReadProperties(reader, "Properties");
                            this._Properties[propertyName] = p;
                        }
                        else if (propertyName == "Array")
                        {
                            propertyName = reader.GetAttribute(0);
                            this._Properties[propertyName] = ReadArray(reader);
                        }
                        else if (propertyName == "SerializedValue")
                        {
                            propertyName = reader.GetAttribute(0);
                            this._Properties[propertyName] = new SerializedValue(reader.ReadInnerXml());
                        }
                        else
                        {
                            this._Properties[propertyName] = reader.HasAttributes ? reader.GetAttribute(0) : null;
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

        public static Properties Parse(XmlElement element)
        {
            return Parse(element.Attributes);
        }

        public static Properties Parse(XmlAttributeCollection attributes)
        {
            //if (attributes == null || attributes.Count <= 0)
            //    return null;
            Properties properties = new Properties();
            foreach (XmlAttribute item in attributes)
            {
                properties.Add(item.LocalName.ToLowerInvariant(), item.Value);
            }
            return properties;
        }

        #endregion

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
            lock (this._Properties)
            {
                List<KeyValuePair<string, object>> sortedProperties = new List<KeyValuePair<string, object>>(this._Properties);
                sortedProperties.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Key, b.Key));
                foreach (KeyValuePair<string, object> entry in sortedProperties)
                {
                    object val = entry.Value;
                    if (val is Properties)
                    {
                        writer.WriteStartElement("Properties");
                        writer.WriteAttributeString("name", entry.Key);
                        ((Properties)val).WriteProperties(writer);
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
        private void OnPropertyChanged(PropertiesItemChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

    }

    public delegate void PropertiesItemChangedEventHandler(object sender, PropertiesItemChangedEventArgs e);

}
/*

#region IDictionary<string,object> 成员

public void Add(string key, object value)
{
    lock (this._Properties)
    {
        this.Set(key, value);
    }
}

public void AddRange(KeyValuePair<string, object>[] items)
{
        foreach (var item in items)
        {
            this.Add(item);
        }
}

public bool ContainsKey(string key)
{
    lock (this._Properties)
    {
        return this._Properties.ContainsKey(key);
    }
}

public ICollection<string> Keys
{
    get
    {
        lock (this._Properties)
        {
            return this._Properties.Keys;
        }
    }
}

public bool Remove(string key)
{
    lock (this._Properties)
    {
        return this._Properties.Remove(key);
    }
}

public bool TryGetValue(string key, out object value)
{
    lock (this._Properties)
    {
        return this._Properties.TryGetValue(key, out value);
    }
}

public ICollection<object> Values
{
    get
    {
        lock (this._Properties)
        {
            return this._Properties.Values;
        }
    }
}

public object this[string key]
{
    get
    {
        lock (this._Properties)
        {
            if (this.ContainsKey(key))
            {
                return this._Properties[key];
            }
            return null;
        }
    }
    set { this.Set(key, value); }
}

#endregion

#region ICollection<KeyValuePair<string,object>> 成员

public void Add(KeyValuePair<string, object> item)
{
    this.Add(item.Key, item.Value);
}

public void Clear()
{
    lock (this._Properties)
    {
        this._Properties.Clear();
    }
}

public bool Contains(KeyValuePair<string, object> item)
{
    lock (this._Properties)
    {
        if (this._Properties.ContainsKey(item.Key))
        {
            if (this._Properties[item.Key] == item.Value)
            {
                return true;
            }
        }
        return false;
    }
}

public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
{
    foreach (var item in array)
    {
        this.Add(item);
    }
}

public int Count
{
    get
    {
        lock (this._Properties)
        {
            return this._Properties.Count;
        }
    }
}

public bool IsReadOnly
{
    get { return false; }
}

public bool Remove(KeyValuePair<string, object> item)
{
    lock (this._Properties)
    {
        if (this._Properties.ContainsKey(item.Key))
        {
            if (this._Properties[item.Key].Equals(item.Value))
            {
                return this._Properties.Remove(item.Key);
            }
        }
        return false;
    }
}

#endregion

#region IEnumerable<KeyValuePair<string,object>> 成员

public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
{
    return this._Properties.GetEnumerator();
}

#endregion

#region IEnumerable 成员

IEnumerator IEnumerable.GetEnumerator()
{
    return this._Properties.GetEnumerator();
}

#endregion
*/
