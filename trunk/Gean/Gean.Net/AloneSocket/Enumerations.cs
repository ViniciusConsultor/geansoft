using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.AloneSocket
{
    public enum MessageStatus
    {
        Success = 0,
        Failure = 1,
        Connecting = 2,
        PingFailed = 3,
        PingSuccess = 4,
        ConnectionFailed = 5,
        Connected = 6
    }
    public enum LogCategory
    {
        Connection = 0,
        Message = 1
    }
}
