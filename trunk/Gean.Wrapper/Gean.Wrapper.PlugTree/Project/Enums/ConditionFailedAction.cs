using System;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 当Condition求值失败时的动作
    /// </summary>
    public enum ConditionFailedAction
    {
        /// <summary>
        /// 嘛也不是,当然什么也不做
        /// </summary>
        None,
        /// <summary>
        /// 排除; 不包括在内
        /// </summary>
        Exclude,
        /// <summary>
        /// 不可用
        /// </summary>
        Disable,
    }

}
