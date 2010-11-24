using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.AloneSocket
{
    class Host
    {
        private string _IP;
        private int _port;

        public string IP
        {
            get
            {
                return _IP;
            }
        }

        public int Port
        {
            get
            {
                return _port;
            }
        }
        public Host(string IP, int Port)
        {
            _IP = IP;
            _port = Port;
        }
    }
}
