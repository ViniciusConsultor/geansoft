using System;
using System.Collections.Generic;
using System.Text;

namespace Gean
{
    /// <summary>
    /// 实现Json的序列化与反序列化的接口。之所以用这样一个接口，是因为.Net Framework2.0中没有对Json的支持，在3.5中已有对Json的支持。故通过这样的一个接口以反应在 Gean.Json2(.Net2.0) 与 Gean.Json3(.Net3.5) 两个项目的不同.Net版本的实现。
    /// </summary>
    public interface IJson
    {
        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        string SerializeObject(object obj);
        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonInput">The json input.</param>
        /// <returns></returns>
        T DeserializeObject<T>(string jsonInput);
    }
}
