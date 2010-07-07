using System;
using System.Collections.Generic;
using System.Text;

namespace Pansoft.CQMS.Options
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class OptionAttribute : Attribute
    {
        public OptionAttribute(string name, string displayName, object defaultValue)
        {
            this.OptionName = name;
            this.OptionDisplayName = displayName;
            this.OptionDefaultValue = defaultValue;
        }

        public string OptionName { get; private set; }
        public string OptionDisplayName { get; private set; }
        public object OptionDefaultValue { get; private set; }
    }
}
