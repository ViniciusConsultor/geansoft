using System;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 描述一个或一个复杂的条件的接口。
    /// Condition：状况, 状态; 地位; 健康状况, 可使用的状况; 条件, 先决条件 
    /// </summary>
    public interface ICondition
    {
        string Name { get; }

        /// <summary>
        /// Returns the action which occurs, when this condition fails.
        /// </summary>
        ConditionFailedAction Action { get; set; }

        /// <summary>
        /// 返回true时，条件是有效的。其他情况时返回false。
        /// </summary>
        bool IsValid(object caller);
    }
}
