﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gean.UI.WinForms.Demo
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

            //Application.Run(new CloudsPanelForm());
            Application.Run(new PictureForm());
        }
    }
}
