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
}
