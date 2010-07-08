using System;
using System.Collections.Generic;
using System.Text;

namespace Pansoft.CQMS.Options
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class OptionValueAttribute : Attribute
    {
        public OptionValueAttribute(String name, Object defaultValue)
        {
            this.Name = name;
            this.DefaultValue = defaultValue;
        }

        public String Name { get; private set; }
        public Object DefaultValue { get; private set; }
    }
}
