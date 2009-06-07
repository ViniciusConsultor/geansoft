using System.IO;
using System.Xml.Serialization;

namespace Gean
{
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
