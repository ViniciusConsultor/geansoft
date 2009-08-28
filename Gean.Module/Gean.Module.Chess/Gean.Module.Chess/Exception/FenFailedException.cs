using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    [Serializable]
    public class FenFailedException : ChessException
    {
        public string FENMessage { get; private set; }
        public FenFailedException() { }
        public FenFailedException(string message)
            : base(message)
        {
            this.FENMessage = message;
        }
        public FenFailedException(string message, Exception inner) : base(message, inner) { }
        protected FenFailedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
