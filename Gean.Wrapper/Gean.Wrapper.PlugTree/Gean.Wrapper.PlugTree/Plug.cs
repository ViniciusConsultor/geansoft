using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;

namespace Gean.Wrapper.PlugTree
{
    public sealed class Plug
    {
        public Definers Definers { get; private set; }

        internal static Plug Parse(XmlElement element)
        {
            Plug plug = new Plug();
            plug.Definers = Definers.Parse(element);
            return plug;
        }
    }
}
