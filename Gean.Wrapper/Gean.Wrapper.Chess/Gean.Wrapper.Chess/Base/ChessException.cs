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
    public class ChessRecordException : ChessException
    {
        public ChessRecordException() { }
        public ChessRecordException(string message) : base(message) { }
        public ChessRecordException(string message, Exception inner) : base(message, inner) { }
        protected ChessRecordException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [global::System.Serializable]
    public class ChessmanException : ChessException
    {
        public ChessmanException() { }
        public ChessmanException(string message) : base(message) { }
        public ChessmanException(string message, Exception inner) : base(message, inner) { }
        protected ChessmanException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [global::System.Serializable]
    public class ChessGridException : ChessException
    {
        public ChessGridException() { }
        public ChessGridException(string message) : base(message) { }
        public ChessGridException(string message, Exception inner) : base(message, inner) { }
        protected ChessGridException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
