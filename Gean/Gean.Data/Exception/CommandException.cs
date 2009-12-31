using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Gean.Data
{
	/// <summary>
    /// Represents the exception that raises when executing a command.
	/// </summary>
    [global::System.Serializable]
    public class CommandException : GeanDataException
    {
        /// <summary>
        /// ÃüÁîÎÄ±¾
        /// </summary>
        public string CommandText { get; private set; }

        public CommandException(string command, string msg, Exception ex)
            : this(msg, ex)
        {
            this.CommandText = command;
        }

        public CommandException() { }
        public CommandException(string message) : base(message) { }
        public CommandException(string message, Exception inner) : base(message, inner) { }
        protected CommandException(
          SerializationInfo info,
          StreamingContext context)
            : base(info, context) { }
    }
}
