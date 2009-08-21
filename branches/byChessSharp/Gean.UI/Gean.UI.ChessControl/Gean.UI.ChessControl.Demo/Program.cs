using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gean.UI.ChessControl.Demo
{

    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Initialize();

            Application.Run(new DemoMainForm());
        }

        private static void Initialize()
        {
            ChessBoardService.Initialize();
        }
    }

}
