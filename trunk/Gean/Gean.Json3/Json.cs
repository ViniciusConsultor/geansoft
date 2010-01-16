using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Gean.Json3
{
    public class Json : IJson
    {
        #region IJson 成员

        /// <summary>
        /// 序列化一个指定的对象
        /// </summary>
        /// <param name="obj">一个指定的对象.</param>
        /// <returns>以 JSON（JavaScript 对象表示法）格式表示的序列化后的字符串</returns>
        public string SerializeObject(object obj)
        {
            DataContractJsonSerializer serial = new DataContractJsonSerializer(obj.GetType());

            MemoryStream mstream = new MemoryStream();
            serial.WriteObject(mstream, obj);

            string json = Encoding.UTF8.GetString(mstream.ToArray());
            mstream.Close();

            return json;
        }

        /// <summary>
        /// 以 JSON（JavaScript 对象表示法）格式读取指定的字符串，并返回反序列化的对象。
        /// </summary>
        /// <typeparam name="T">将返回的反序列化的对象类型</typeparam>
        /// <param name="jsonInput">以 JSON（JavaScript 对象表示法）格式表示的字符串.</param>
        /// <returns>反序列化的对象</returns>
        public T DeserializeObject<T>(string jsonInput)
        {
            MemoryStream mstream = new MemoryStream(Encoding.UTF8.GetBytes(jsonInput));
            DataContractJsonSerializer serial = new DataContractJsonSerializer(typeof(T));
            T t = (T)serial.ReadObject(mstream);
            mstream.Close();

            return t;
        }

        #endregion
    }
}
