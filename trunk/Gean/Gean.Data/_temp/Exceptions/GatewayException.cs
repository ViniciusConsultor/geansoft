using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Data.Exceptions
{
    /// <summary>
    /// Represents the exception that raises when executing a command.
    /// </summary>
    [Serializable]
    public class GatewayException : GeanDataException
    {
        private object _customSource;
        private string _message;

        public object CustomSource
        {
            get
            {
                return _customSource;
            }
        }

        public string Text
        {
            get
            {
                return _message;
            }
        }

        public GatewayException()
            : base()
        {
        }

        public GatewayException(string message)
            : base(message)
        {
            _message = message;
        }

        public GatewayException(string message, object customSource)
            : base(message)
        {
            _customSource = customSource;
            _message = message;
        }

        public GatewayException(string message, object customSource, Exception ex)
            : base(message, ex)
        {
            _customSource = customSource;
            _message = message;
        }

        public GatewayException(string message, Exception ex)
            : base(message, ex)
        {
            _message = message;
        }

        public override string ToString()
        {
            return this.Message;
        }

    }
}
