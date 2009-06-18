using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// Default actions, when a condition is failed.
    /// </summary>
    public enum ConditionFailedAction
    {
        Nothing,
        Exclude,
        Disable
    }
}
