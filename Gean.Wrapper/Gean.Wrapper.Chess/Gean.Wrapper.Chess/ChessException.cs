using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    [global::System.Serializable]
    public class ChessException : ApplicationException
    {
        public ChessException() { }
        public ChessException(string message) : base(message) { }
        public ChessException(string message, Exception inner) : base(message, inner) { }
        protected ChessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [global::System.Serializable]
    public class ChessStepParseException : ChessException
    {
        public ChessStepParseException() { }
        public ChessStepParseException(string message) : base(message) { }
        public ChessStepParseException(string message, Exception inner) : base(message, inner) { }
        protected ChessStepParseException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
