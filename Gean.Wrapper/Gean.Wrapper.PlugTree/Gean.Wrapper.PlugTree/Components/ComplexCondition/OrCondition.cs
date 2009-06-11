using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;

namespace Gean.Wrapper.PlugTree.Components.ComplexCondition
{
    /// <summary>
    /// Gives back the or result of two conditions.
    /// </summary>
    public class OrCondition : AbstractCondition
    {
        public OrCondition(ICondition condition)
            : base(condition, "Or")
        {

        }

    }
}
