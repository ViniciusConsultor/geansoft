using System;
using System.Collections.Generic;
using System.Text;
using Gean.Exceptions;

namespace Gean.Data.Exceptions
{
    /// <summary>
    /// Gean.Data的基础异常类
    /// </summary>
    [global::System.Serializable]
    public class GeanDataException : GeanException
    {
        public GeanDataException() { }
        public GeanDataException(string message) : base(message) { }
        public GeanDataException(string message, Exception inner) : base(message, inner) { }
        protected GeanDataException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
