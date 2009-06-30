using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{

    static class CoreService
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CoreService.Initialize();
            ApplicationMainForm form = new ApplicationMainForm();
            CoreService.MainForm = form;
            Application.Run(form);
        }

        public static string PlugFile { get; set; }
        public static XmlDocument PlugDocument { get; set; }
        public static ApplicationMainForm MainForm { get; private set; }

        public static void Initialize()
        {
            CoreService.PlugDocument = new XmlDocument();
        }
    }
}
