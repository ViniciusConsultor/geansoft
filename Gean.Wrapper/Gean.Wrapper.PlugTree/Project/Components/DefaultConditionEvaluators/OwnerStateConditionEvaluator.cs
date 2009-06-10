using System;
using Gean.Wrapper.PlugTree.Components;
using Gean.Framework;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// Condition evaluator that compares the state of the caller/owner with a specified value.
    /// The caller/owner has to implement <see cref="IOwnerState"/>.
    /// </summary>
    public class OwnerStateConditionEvaluator : IConditionEvaluator
    {
        public bool IsValid(object caller, Condition condition)
        {
            if (caller is IOwnerState)
            {
                try
                {
                    System.Enum state = ((IOwnerState)caller).InternalState;
                    System.Enum conditionEnum = (System.Enum)Enum.Parse(state.GetType(), (string)condition.Properties["ownerstate"]);

                    int stateInt = Int32.Parse(state.ToString("D"));
                    int conditionInt = Int32.Parse(conditionEnum.ToString("D"));

                    return (stateInt & conditionInt) > 0;
                }
                catch (Exception)
                {
                    throw new ApplicationException("can't parse '" + condition.Properties["state"] + "'. Not a valid value.");
                }
            } 
            return false;
        }
    }
}
