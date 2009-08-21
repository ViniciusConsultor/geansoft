using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 描述一个“条件”的接口
    /// </summary>
    public interface ICondition
    {
        string Name
        {
            get;
        }
        /// <summary>
        /// 当一个条件失效时，返回应做的动作
        /// </summary>
        ConditionFalseAction Action
        {
            get;
            set;
        }

        /// <summary>
        /// 仅当前条件有效时为真，其余为假
        /// </summary>
        bool IsValid(object caller);
    }
}
