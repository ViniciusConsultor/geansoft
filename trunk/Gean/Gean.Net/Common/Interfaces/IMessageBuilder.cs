using System;
using System.Collections.Generic;
using System.Text;
using Gean.Net.Messages;

namespace Gean.Net.Messages
{
    public interface IMessageBuilder
    {
        MessageWrapper Builder(string command, params object[] args);
    }
}
