using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.AloneSocket
{
    class LogMessage
    {
        private DateTime _timestamp;
        private string _message;
        private LogCategory _category;

        public string FormattedMessage
        {
            get
            {
                return string.Format("{0} - {1}: {2}{3}", _timestamp.ToString("HH:mm:ss.ffffff"), _category.ToString(), _message, Environment.NewLine);
            }
        }
        public LogMessage(DateTime Timestamp, string Message, LogCategory Category)
        {
            _timestamp = Timestamp;
            _message = Message;
            _category = Category;
        }
    }
}
