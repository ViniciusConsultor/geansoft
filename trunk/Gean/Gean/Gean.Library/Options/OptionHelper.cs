using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Gean.Options
{
    public static class OptionHelper
    {
        public static KeyValuePair<string, T> InterfaceBuilder<T>(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node", "参数不能为空");
            }
            string name = node.Attributes["name"].Value;
            Type type = Type.GetType(node.Attributes["class"].Value);
            object klass = UtilityType.CreateObject(type, typeof(T), true);
            return new KeyValuePair<string, T>(name, (T)klass); ;
        }
    }
}
