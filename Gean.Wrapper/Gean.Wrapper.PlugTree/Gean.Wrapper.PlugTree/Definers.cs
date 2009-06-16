using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 一个类似Hashtable的保存“定义”的集合类(数据包)。
    /// Definer：可以直译为“定义者”，也就是对某事物(对象)的定义，对该对象的各个属性解释、定义，
    /// 所有属性(定义)以 Dictionary(string, object)的方式进行存储。键为字符串，而值可以为任何
    /// 可序列化的类型。一般来讲，该集合均为从XML的节点中解析得到，同样，他也可以序列化成 XML
    /// 节点或文件。
    /// Gean: 2009-06-16 11:27:24
    /// </summary>
    public sealed class Definers : IEnumerable
    {
        internal Definers() {}

        private Dictionary<string, object> _Definers = new Dictionary<string, object>();

        /// <summary>
        /// 设置(增加)一组键与值
        /// </summary>
        public void Set<T>(string key, T value)
        { 
            T oldValue = default(T);
            lock (this._Definers)
            {
                key = key.ToLowerInvariant();
                if (!this._Definers.ContainsKey(key))
                {
                    this._Definers.Add(key, value);
                }
                else
                {   //如果已有该值，记录原值后，进行设置
                    oldValue = Get<T>(key, value);
                    this._Definers[key] = value;
                }
            }
            OnDefinersItemChanged(new DefinersItemChangedEventArgs(this, key, oldValue, value));
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
            lock (this._Definers)
            {
                object obj;
                if (!this._Definers.TryGetValue(key, out obj))
                {
                    this._Definers.Add(key, defaultValue);
                    return defaultValue;
                }

                if (obj is string && typeof(T) != typeof(string))
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
                    this._Definers[key] = obj;
                }
                else if (obj is ArrayList && typeof(T).IsArray)
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
                    this._Definers[key] = obj;
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
                else if (obj is SerializedValue)
                {
                    try
                    {
                        obj = ((SerializedValue)obj).Deserialize<T>();
                    }
                    catch (Exception)
                    {
                        obj = defaultValue;
                    }
                    this._Definers[key] = obj;
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
        public static Definers Parse(string fileName)
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
                                Definers properties = new Definers();
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
                            Definers p = new Definers();
                            p.ReadProperties(reader, "Properties");
                            this._Definers[propertyName] = p;
                        }
                        else if (propertyName == "Array")
                        {
                            propertyName = reader.GetAttribute(0);
                            this._Definers[propertyName] = ReadArray(reader);
                        }
                        else if (propertyName == "SerializedValue")
                        {
                            propertyName = reader.GetAttribute(0);
                            this._Definers[propertyName] = new SerializedValue(reader.ReadInnerXml());
                        }
                        else
                        {
                            this._Definers[propertyName] = reader.HasAttributes ? reader.GetAttribute(0) : null;
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

        public static Definers Parse(XmlElement element)
        {
            return Parse(element, true);
        }
        public static Definers Parse(XmlElement element, bool isParseChildNodes)
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
        public static Definers Parse(XmlAttributeCollection attributes)
        {
            if (attributes == null || attributes.Count <= 0)
                return null;
            Definers properties = new Definers();
            foreach (XmlAttribute item in attributes)
            {
                properties.Set<string>(item.LocalName.ToLowerInvariant(), item.Value);
            }
            return properties;
        }
        public static Definers Parse(XmlNodeList childNodes)
        {
            if (childNodes == null || childNodes.Count <= 0)
                return null;
            Definers properties = new Definers();
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

        public static Definers operator +(Definers definersA, Definers definersB)
        {
            if (definersA == null && definersB == null)
                return null;
            if (definersA == null && definersB != null)
                return definersB;
            if (definersB == null && definersA != null)
                return definersA;
            foreach (var item in definersB._Definers)
            {
                if (definersA._Definers.ContainsKey(item.Key))
                {
                    continue;
                }
                definersA._Definers.Add(item.Key, item.Value);
            }
            return definersA;
        }

        /// <summary>
        /// 将Definers封装存入指定的文件
        /// </summary>
        /// <param name="fileName">指定的文件</param>
        public void Save(string fileName)
        { 
            using (XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument();
                writer.WriteStartElement("Definers");
                this.WriteDefiners(writer);
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// 将Definers写入XmlWriter
        /// </summary>
        /// <param name="writer">The writer.</param>
        public void WriteDefiners(XmlWriter writer)
        {
            lock (this._Definers)
            {
                List<KeyValuePair<string, object>> sortedDefiners = new List<KeyValuePair<string, object>>(this._Definers);
                sortedDefiners.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Key, b.Key));
                foreach (KeyValuePair<string, object> entry in sortedDefiners)
                {
                    object val = entry.Value;
                    if (val is Definers)
                    {
                        writer.WriteStartElement("Definers");
                        writer.WriteAttributeString("name", entry.Key);
                        ((Definers)val).WriteDefiners(writer);
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
        public event DefinersItemChangedEventHandler DefinersItemChanged;
        private void OnDefinersItemChanged(DefinersItemChangedEventArgs e)
        {
            if (DefinersItemChanged != null)
            {
                DefinersItemChanged(this, e);
            }
        }

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return this._Definers.GetEnumerator();
        }

        #endregion
    }

    public delegate void DefinersItemChangedEventHandler(object sender, DefinersItemChangedEventArgs e);

    public class DefinersItemChangedEventArgs : EventArgs
    {
        /// <returns>
        /// returns the changed Definers object
        /// </returns>
        public Definers Definers { get; private set; }

        /// <returns>
        /// The key of the changed Definers
        /// </returns>
        public string Key { get; private set; }

        /// <returns>
        /// The new value of the Definers
        /// </returns>
        public object NewValue { get; private set; }

        /// <returns>
        /// The new value of the Definers
        /// </returns>
        public object OldValue { get; private set; }

        public DefinersItemChangedEventArgs(Definers definers, string key, object oldValue, object newValue)
        {
            this.Definers = definers;
            this.Key = key;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }

    /// <summary>
    /// 描述该Value值可序列化，并提供反序列化的方法
    /// </summary>
    internal class SerializedValue
    {
        public string Content { get; private set; }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Deserialize<T>()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(new StringReader(this.Content));
        }

        public SerializedValue(string content)
        {
            this.Content = content;
        }

    }

}