using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Gean.Data.Exceptions
{
    /// <summary>
    /// Represents the exception that raises when connecting to database.
    /// </summary>
    [global::System.Serializable]
    public class ConnectionException : GeanDataException
    {
        public string ConnectionString { get; private set; }

        public ConnectionException(string msg, string connstring)
            : this(msg)
        {
            this.ConnectionString = connstring;
        }

        public ConnectionException() { }
        public ConnectionException(string message) : base(message) { }
        public ConnectionException(string message, Exception inner) : base(message, inner) { }
    }

}
