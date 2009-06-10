using System;
using Gean.Wrapper.PlugTree.Components;

namespace Gean.Wrapper.PlugTree
{
	/// <summary>
	/// Interface for classes that can evaluate conditions defined in the addin tree.
	/// </summary>
	public interface IConditionEvaluator
	{
		bool IsValid(object caller, Condition condition);
	}
}
