using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
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
            MainService.Init();
            Application.Run(new _mainForm());
        }
    }
}
