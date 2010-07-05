using System;
using Gean;

namespace Pansoft.CQMS.Options
{
	/// <summary>
	/// 选项值接口
	/// </summary>
	/// <remarks>
	///	形如下面的XML节表示一个选项节：
	///		<code>
	///			&lt;app my="myProperty"&gt;myValue&lt;/app&gt;
	///		</code>
	///	此时，<c>Name="app"</c>，Value的值为"myValue"，Property的值为"myProperty"
	/// </remarks>
	public interface IOptionValue : ICloneable, IConverting
	{
		/// <summary>
		/// 当前选项值是否只读
		/// </summary>
		bool ReadOnly { get; }

		/// <summary>
		/// 选项值名
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 选项值
		/// </summary>
		string Value { get; }

		/// <summary>
		///  克隆选项值
		/// </summary>
		/// <param name="readonly">是否只读</param>
		/// <returns>ISettingValue</returns>
		IOptionValue Clone(bool @readonly);
	}
}