using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.Demo
{
    public static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainWorkBench = new Form1();
            Application.Run((Form)MainWorkBench);
        }

        public static IWorkBench MainWorkBench { get; private set; }
    }
}
