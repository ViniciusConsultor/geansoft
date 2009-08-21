using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 枚举：当codition evaluator为false时的动作
    /// </summary>
    public enum ConditionFalseAction
    {
        /// <summary>
        /// 嘛事不做
        /// </summary>
        Nothing,
        /// <summary>
        /// 排除，不可见
        /// </summary>
        Exclude,
        /// <summary>
        /// 不可用，可见
        /// </summary>
        Disable
    }
}
