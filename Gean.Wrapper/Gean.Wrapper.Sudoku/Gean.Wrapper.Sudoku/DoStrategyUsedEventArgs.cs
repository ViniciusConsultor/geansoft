using System;

namespace Gean.Wrapper.Sudoku
{

	/// <summary>
	/// Args for a SudokuStrategyUsed event.
	/// </summary>
	public class DoStrategyUsedEventArgs : EventArgs
	{

		/// <summary>Strategy selected to solve a cell.</summary>
		public readonly DoStrategy Strategy;

		/// <summary>Row for the cell solved (zero-based).</summary>
		public readonly int Row;

		/// <summary>Column for the cell solved (zero-based).</summary>
		public readonly int Col;

		/// <summary>Selected value for the cell.</summary>
		public readonly int Value;

		/// <summary>
		/// Constructor for this class.
		/// </summary>
		/// <param name="strategy">Strategy selected to solve a cell.</param>
		/// <param name="cell">Cell just solved.</param>
		public DoStrategyUsedEventArgs(DoStrategy strategy, DoCell cell)
		{
			this.Strategy = strategy;
			this.Row = cell.Row;
			this.Col = cell.Column;
			this.Value = cell.Value;
		}

	}

	/// <summary>
	/// Delegate definition for a SudokuStrategyUsed event handler.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	public delegate void SudokuStrategyUsedEventHandler(object sender, DoStrategyUsedEventArgs e);

}