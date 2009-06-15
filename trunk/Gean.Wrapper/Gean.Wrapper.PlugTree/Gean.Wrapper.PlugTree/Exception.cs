﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Gean.Wrapper.PlugTree
{
    [global::System.Serializable]
    public class PlugTreeException : Exception
    {
        public PlugTreeException() { }
        public PlugTreeException(string message) : base(message) { }
        public PlugTreeException(string message, Exception inner) : base(message, inner) { }
        protected PlugTreeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}