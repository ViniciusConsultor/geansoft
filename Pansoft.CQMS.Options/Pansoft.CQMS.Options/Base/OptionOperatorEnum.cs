namespace Pansoft.CQMS.Options
{
	/// <summary>
	/// 选项节命令
	/// </summary>
	public enum OptionOperatorEnum
	{
		/// <summary>
		/// 添加选项节
		/// </summary>
		Add = 1,

		/// <summary>
		/// 移除选项节
		/// </summary>
		Remove,

		/// <summary>
		/// 移动选项节
		/// </summary>
		Move,

		/// <summary>
		/// 清除所有选项节
		/// </summary>
		Clear,

		/// <summary>
		/// 更新（合并）选项节（如果不存在，则忽略此命令）
		/// </summary>
		Update,

		/// <summary>
		/// 设置选项节，如果存在则合并，否则添加
		/// </summary>
		Set,
	}
}