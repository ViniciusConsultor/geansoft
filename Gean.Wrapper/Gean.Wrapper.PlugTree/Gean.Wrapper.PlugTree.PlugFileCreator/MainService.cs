using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    public static class MainService
    {
        public static XmlDocument PlugDocument { get; set; }

        public static void Init()
        {
            MainService.PlugDocument = new XmlDocument();
        }
    }
}
