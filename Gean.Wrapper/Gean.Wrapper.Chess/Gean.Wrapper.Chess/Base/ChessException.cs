using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    [Serializable]
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

    [Serializable]
    public class FENValidationException : ChessRecordException
    {
        public string FENMessage { get; private set; }
        public FENValidationException() { }
        public FENValidationException(string message) : base(message)
        {
            this.FENMessage = message;
        }
        public FENValidationException(string message, Exception inner) : base(message, inner) { }
        protected FENValidationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
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

    [Serializable]
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

    [Serializable]
    public class ChessGameException : ChessException
    {
        public ChessGameException() { }
        public ChessGameException(string message) : base(message) { }
        public ChessGameException(string message, Exception inner) : base(message, inner) { }
        protected ChessGameException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
