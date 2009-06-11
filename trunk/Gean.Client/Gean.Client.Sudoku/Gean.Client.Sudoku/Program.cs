using System;
using System.Windows.Forms;

namespace Gean.Client.Sudoku
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            MainService.InitializeService();

			Application.Run(new MainForm());
		}
		
	}
}
