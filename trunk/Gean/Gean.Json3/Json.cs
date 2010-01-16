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
        /// Serializes the object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
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
        /// Deserializes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonInput">The json input.</param>
        /// <returns></returns>
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
