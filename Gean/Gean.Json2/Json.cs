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
            throw new NotImplementedException();
        }

        public T DeserializeObject<T>(string jsonInput)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
