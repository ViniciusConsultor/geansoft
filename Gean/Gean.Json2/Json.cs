using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Json2
{
    public class Json : IJson
    {
        #region IJson 成员

        public string SerializeObject(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }

        public T DeserializeObject<T>(string jsonInput)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonInput);
        }

        #endregion
    }
}
