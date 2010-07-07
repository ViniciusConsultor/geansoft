using System;
using System.Collections.Generic;
using System.Text;

namespace Pansoft.CQMS.Options
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class OptionAttribute : Attribute
    {
        public OptionAttribute(string sectionName, string displayName)
            : this(sectionName, displayName, false, "")
        {
        }
        public OptionAttribute(string sectionName, string displayName, Boolean isCollection, string parentSectionName)
        {
            this.OptionSectionName = sectionName;
            this.OptionDisplayName = displayName;
            this.IsCollection = isCollection;
            this.ParentSectionName = parentSectionName;
        }

        public Boolean IsCollection { get; private set; }
        public string OptionSectionName { get; private set; }
        public string OptionDisplayName { get; private set; }
        public string ParentSectionName { get; private set; }
    }
}
