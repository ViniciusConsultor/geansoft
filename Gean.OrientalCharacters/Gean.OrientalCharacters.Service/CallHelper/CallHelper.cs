using System.ComponentModel;
using System;
using System.Windows.Forms;
using Gean.OrientalCharacters.CoreWorkbench;
using Gean.OrientalCharacters.Service;
using Gean.SimpleLogger;

namespace Gean.EasternArt.Service
{
    public sealed class CallHelper
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 调入EasternArt主窗体进行显示.
        /// </summary>
        public static void CallCoreWorkbench(Form window)
        {
            _startupWindow = window;

            CoreWorkbench bench = new CoreWorkbench();
            bench.Closing += new CancelEventHandler(StartupWindowClosing);
            bench.Show();
            window.Activate();
            _logger.Info(LogString.Normal(StringService.CoreWorkbenchGettingOpened));
        }
        /// <summary>
        /// 欢迎窗体
        /// </summary>
        private static Form _startupWindow;
        /// <summary>
        /// 欢迎窗体的关闭方法
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private static void StartupWindowClosing(object sender, EventArgs e)
        {
            if (_startupWindow == null)
                return;
            _startupWindow.Close();
            _logger.Info(LogString.Normal(StringService.CoreWorkbenchAlreadyClosed));
        }

    }
}
