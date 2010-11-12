using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.ProtocolWrapper
{
    public class Protocols
    {
        public static String VerifyConnectionStatus(string id)
        {
            return string.Format("VerifyConnectionStatus|101|{0}|@", id); 
        }
    }
}
