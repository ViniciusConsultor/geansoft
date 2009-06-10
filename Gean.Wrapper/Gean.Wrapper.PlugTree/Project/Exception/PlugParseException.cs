using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Gean.Wrapper.PlugTree.Exceptions
{
    [Serializable]
    public class PlugParseException : PlugTreeException
    {
        public PlugParseException()
            : base()
        {
        }

        public PlugParseException(string message)
            : base(message)
        {
        }

        public PlugParseException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        protected PlugParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
