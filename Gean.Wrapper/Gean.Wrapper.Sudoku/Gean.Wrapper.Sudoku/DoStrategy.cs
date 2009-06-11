using System;

namespace Gean.Wrapper.Sudoku
{

	/// <summary>
	/// 解题策略
	/// </summary>
	public enum DoStrategy
	{

        /// <summary>
        /// 单元格唯一值
        /// </summary>
		UniqueValueForCell,

        /// <summary>
        /// 行唯一值
        /// </summary>
		UniqueValueForRow,

        /// <summary>
        /// 列唯一值
        /// </summary>
		UniqueValueForColumn,

        /// <summary>
        /// 3×3的矩形唯一值
        /// </summary>
		UniqueValueForSquare,

        /// <summary>
        /// 随机选择
        /// </summary>
		RandomPick

	}

}