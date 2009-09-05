using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Gean.Log4net;

namespace Gean.Gui.ChessControl.Demo
{
    class Program
    {
        public static Log4netLoggingService Logger = Log4netLoggingService.Initialize();
        
        public static FormWindowState IsShangBan { get { return FormWindowState.Maximized; } }

        public static string PGNFile_340_Game { get { return @"pgn\340,game.pgn"; } }
        public static string PGNFile_8_Game { get { return @"pgn\8,game.pgn"; } }
        public static string PGNFile_CHS_Game { get { return @"pgn\chs,game.pgn"; } }
        public static string PGNFile_Largeness_Game { get { return @"pgn\largeness,game.pgn"; } }
        public static string PGNFile_Test_1_Game { get { return @"pgn\test,1,game.pgn"; } }
        public static string PGNFile_Test_2_Game { get { return @"pgn\test,2,game.pgn"; } }
        public static string PGNFile_WorldOpen_Game { get { return @"pgn\worldopen,game.pgn"; } }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logger.Info("Application Start...");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Initialize();

            Logger.Info("Application.Run(new DemoMainForm())");
            Application.Run(new DemoMainForm());
        }

        private static void Initialize()
        {
            Logger.Info("ChessBoardService.Initialize()");
            Servicer.Initialize();
        }
    }
}
