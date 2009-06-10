using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Gean.Wrapper.PlugTree.Components.ComplexCondition
{
    /// <summary>
    /// Negates a condition
    /// </summary>
    public class NegatedCondition : AbstractCondition
    {
        public NegatedCondition(ICondition condition)
            : base(condition, "Not")
        {

        }
    }
}
