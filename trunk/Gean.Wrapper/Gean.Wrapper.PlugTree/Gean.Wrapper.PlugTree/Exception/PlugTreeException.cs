using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Gean.Wrapper.PlugTree.Exceptions
{
    [Serializable()]
    public class PlugTreeException : System.Exception
    {
        public PlugTreeException()
            : base()
        {
        }

        public PlugTreeException(string message)
            : base(message)
        {
        }

        public PlugTreeException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected PlugTreeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
