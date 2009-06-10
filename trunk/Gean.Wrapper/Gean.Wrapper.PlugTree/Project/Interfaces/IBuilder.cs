using System;
using System.Collections;

namespace Gean.Wrapper.PlugTree
{
	/// <summary>
    /// 这个接口标志着该对象可以从PlugAtom对象中Build一个object(即BuildItem()方法)。
	/// </summary>
    public interface IBuilder
	{
		/// <summary>
		/// Gets if the doozer handles plugAtom conditions on its own.
		/// If this property return false, the item is excluded when the condition is not met.
		/// </summary>
		bool HandleConditions { get; }
		
		/// <summary>
		/// Construct the item.
		/// </summary>
		/// <param name="caller">The caller passed to <see cref="PlugTree.BuildItem"/>.</param>
		/// <param name="plugAtom">The plugAtom to build.</param>
		/// <param name="subItems">The list of objects created by (other) doozers for the sub items.</param>
		/// <returns>The constructed item.</returns>
        object BuildItem(object caller, PlugAtom plugAtom, ArrayList subItems);
	}
}
