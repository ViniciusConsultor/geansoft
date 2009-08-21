using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.DemoApplicationForm
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
            Application.Run(new DemoApplicationForm());
            Application.Idle += new EventHandler(Application_Idle);
        }

        static void Application_Idle(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
