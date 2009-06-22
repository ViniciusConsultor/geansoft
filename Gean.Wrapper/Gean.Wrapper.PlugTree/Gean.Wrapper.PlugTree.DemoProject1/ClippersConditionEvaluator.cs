using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree.DemoProject1
{
    public class ClippersConditionEvaluator : IConditionEvaluator
    {
        #region IConditionEvaluator 成员

        public bool IsValid(object caller, Condition condition)
        {
            return false;
        }

        #endregion
    }
}
