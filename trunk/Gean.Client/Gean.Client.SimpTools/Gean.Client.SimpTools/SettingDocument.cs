using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gean.Xml;

namespace Gean.Client.SimpTools
{
    class OptionService : AbstractXmlDocument
    {
        public OptionService():
            base("SimpTools.gconfig")
        {
        }
    }
}
