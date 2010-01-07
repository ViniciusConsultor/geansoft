using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Data.Exceptions
{
    public class SQLTextBuilderException : GeanDataException
    {
        public SQLTextBuilderException() { }
        public SQLTextBuilderException(string message) : base(message) { }
        public SQLTextBuilderException(string message, Exception inner) : base(message, inner) { }
        protected SQLTextBuilderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
