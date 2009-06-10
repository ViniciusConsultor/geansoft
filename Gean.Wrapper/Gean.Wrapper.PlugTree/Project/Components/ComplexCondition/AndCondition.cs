using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Gean.Wrapper.PlugTree.Components.ComplexCondition
{
    /// <summary>
    /// Gives back the and result of two conditions.
    /// </summary>
    public class AndCondition : AbstractCondition
    {
        public AndCondition(ICondition condition)
            : base(condition, "And")
        {

        }
    }
}
