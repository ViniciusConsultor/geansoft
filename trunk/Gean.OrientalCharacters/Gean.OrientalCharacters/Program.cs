using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Gean.Logging;

namespace Gean.OrientalCharacters
{
    static class Program
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            _logger.Info(LogString.NormalStart("=========")); 
            _logger.Info(LogString.NormalStart("NLog服务."));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartupWindow());
        }
    }
}
