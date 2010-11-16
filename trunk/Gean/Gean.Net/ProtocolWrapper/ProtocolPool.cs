using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.ProtocolWrapper
{
    public class ProtocolPool
    {
        public Queue<string> SendingQueue { get { return _SendingQueue; } }
        private Queue<string> _SendingQueue;

        public Queue<string> SendingQueue { get { return _SendingQueue; } }
        private Queue<string> _ReceivingQueue;

    }
}
