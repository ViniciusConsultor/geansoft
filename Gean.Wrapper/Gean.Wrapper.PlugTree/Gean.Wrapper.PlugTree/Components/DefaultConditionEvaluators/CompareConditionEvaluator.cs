using System;
using Gean.Wrapper.PlugTree.Components;

namespace Gean.Wrapper.PlugTree
{
	/// <summary>
	/// Compares two strings.
	/// The strings are passed through the StringParser, so it is possible to compare
	/// SharpDevelop properties.<br/>
	/// Useful if you want to run a command only when a setting is active to prevent
	/// loading your addin if that setting isn't set.
	/// </summary>
	/// <attribute name="string">
	/// The first string.
	/// </attribute>
	/// <attribute name="equals">
	/// The second string.
	/// </attribute>
	/// <attribute name="comparisonType">
	/// The mode of the comparison: a field of the System.StringComparison enumeration. The default is
	/// 'InvariantCultureIgnoreCase'.
	/// </attribute>
	/// <example title="Check the value of a property in the PropertyService">
	/// &lt;Condition name = "Compare" string = "${property:SharpDevelop.FiletypesRegisterStartup}" equals = "True"&gt;
	/// </example>
	public class CompareConditionEvaluator : IConditionEvaluator
	{
		public bool IsValid(object caller, Condition condition)
		{
			string comparisonTypeText = (string)condition.Properties["comparisonType"];
			StringComparison comparisonType;
			if (string.IsNullOrEmpty(comparisonTypeText))
				comparisonType = StringComparison.InvariantCultureIgnoreCase;
			else
				comparisonType = (StringComparison)Enum.Parse(typeof(StringComparison), comparisonTypeText);

            return string.Equals((string)condition.Properties["string"],
                                 (string)condition.Properties["equals"],
			                     comparisonType);
		}
	}
}
