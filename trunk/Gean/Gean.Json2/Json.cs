using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Json2
{
    /// <summary>
    /// 在.Net2.0环境下实现对象的序列化与反序列化
    /// </summary>
    public class Json : Gean.IJson
    {
        #region IJson 成员

        /// <summary>
        /// 序列化一个指定的对象
        /// </summary>
        /// <param name="obj">一个指定的对象.</param>
        /// <returns>以 JSON（JavaScript 对象表示法）格式表示的序列化后的字符串</returns>
        public string SerializeObject(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// 以 JSON（JavaScript 对象表示法）格式读取指定的字符串，并返回反序列化的对象。
        /// </summary>
        /// <typeparam name="T">将返回的反序列化的对象类型</typeparam>
        /// <param name="jsonInput">以 JSON（JavaScript 对象表示法）格式表示的字符串.</param>
        /// <returns>反序列化的对象</returns>
        public T DeserializeObject<T>(string jsonInput)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonInput);
        }

        #endregion
    }
}
