using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 对条件进行求值(Evaluator)(与，或等)
    /// </summary>
    public interface IConditionEvaluator
    {
        bool IsValid(object caller, Condition condition);
    }
}
