using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Gean.Data.Exceptions
{
   public class DbTypeNotSupportException : GeanDataException
    {
        public DbTypeNotSupportException() { }
        public DbTypeNotSupportException(string message) : base(message) { }
        public DbTypeNotSupportException(string message, Exception inner) : base(message, inner) { }
        protected DbTypeNotSupportException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) { }
    }
}
