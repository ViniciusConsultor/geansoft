using System;

namespace Gean.Net.AloneSocket
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message, int queuedMessageCount, MessageStatus status, string error)
        {
            _message = message;
            _queuedMessageCount = queuedMessageCount;
            _status = status;
            _error = error;
        }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private int _queuedMessageCount;

        public int QueuedMessageCount
        {
            get { return _queuedMessageCount; }
            set { _queuedMessageCount = value; }
        }
        private MessageStatus _status;

        public MessageStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; }
        }
    }
}
