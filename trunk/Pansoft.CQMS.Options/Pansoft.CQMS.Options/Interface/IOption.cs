using System;

namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// 选项节接口
    /// </summary>
    /// <remarks>
    /// 形如下面的XML节表示一个选项节：
    /// <code>
    /// &lt;app my="myProperty"&gt;myValue&lt;/app&gt;
    /// </code>
    /// 此时，<c>Name="app"</c>，Value的值为"myValue"，Property的值为"myProperty"
    /// </remarks>
	public interface IOption : ICloneable
	{
		/// <summary>
		/// 当前选项节是否只读
		/// </summary>
		bool ReadOnly { get; }

		/// <summary>
		/// 此选项节的名
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 此选项节实际名称
		/// </summary>
		string OptionName { get; }

		/// <summary>
		/// 此选项节的名/值
		/// </summary>
		IOptionValue Value { get; }

		/// <summary>
		/// 包含此选项节的父选项节
		/// </summary>
		IOption Parent { get; }

		/// <summary>
		/// 此选项节包含的子选项节数目
		/// </summary>
		int Children { get; }

		/// <summary>
		/// 选项节属性
		/// </summary>
        IOptionSection OptionSection { get; }

        /// <summary>
        /// 获取子选项节
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// 如果不存在，将返回<c>null</c>
        /// </remarks>
		IOption this[string childOptionName] { get; }

        /// <summary>
        /// 获取子选项节
        /// </summary>
        /// <value></value>
        /// <remarks>
        /// 如果不存在，将返回null
        /// </remarks>
		IOption this[int childOptionIndex] { get; }

        /// <summary>
        /// 获取所有子选项节
        /// </summary>
        /// <returns>选项节数组</returns>
		IOption[] GetChildOptions();

        /// <summary>
        /// 按XPath方式获取选项节
        /// </summary>
        /// <param name="xpath">XPath</param>
        /// <returns>选项节</returns>
        /// <remarks>
        /// XPath为类似XML的XPath，形如<c>framework/modules"</c><br/>
        /// 如果有相同的选项节，则返回第一个选项节
        /// </remarks>
		IOption GetChildOption(string xpath);

        /// <summary>
        /// 按多级方式获取选项节
        /// </summary>
        /// <param name="optionName">Name of the option.</param>
        /// <returns>选项节</returns>
        /// <remarks>
        /// 多级的选项节名，形如有如下选项：
        /// <code>
        /// &lt;app1&gt;
        /// &lt;app2&gt;
        /// &lt;app3&gt;&lt;/app3&gt;
        /// &lt;/app2&gt;
        /// &lt;/app1&gt;
        /// </code>
        /// 则按顺序传入，比如<c>GetChildSetting("app1", "app2", "app3")</c>，此时返回名为<c>app3</c>的选项节
        /// </remarks>
		IOption GetChildOption(params string[] optionName);

        /// <summary>
        /// 获取根选项节
        /// </summary>
        /// <returns>选项节</returns>
		IOption GetRootOption();

        /// <summary>
        /// 合并选项节
        /// </summary>
        /// <param name="option">The option.</param>
		void Merge(IOption option);

		/// <summary>
		/// 克隆此选项节
		/// </summary>
		/// <param name="readonly">是否只读</param>
		/// <param name="deep">是否深层次的克隆</param>
		/// <returns>选项节</returns>
		IOption Clone(bool @readonly, bool deep);
	}
}
