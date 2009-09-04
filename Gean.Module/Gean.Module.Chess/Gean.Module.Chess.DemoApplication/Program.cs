using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Gean.Module.Chess.Demo
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
            DemoMethod.Initialize();
            Application.Run(new MyDemoDialog());
        }

        public static string PGNFile_340_Game { get { return @"DemoFile\g340,game.pgn"; } }
        public static string PGNFile_8_Game { get { return @"DemoFile\8,game.pgn"; } }
        public static string PGNFile_CHS_Game { get { return @"DemoFile\chs,game.pgn"; } }
        public static string PGNFile_Largeness_Game { get { return @"DemoFile\largeness,game.pgn"; } }
        public static string PGNFile_Test_1_Game { get { return @"DemoFile\test,1,game.pgn"; } }
        public static string PGNFile_Test_2_Game { get { return @"DemoFile\test,2,game.pgn"; } }
        public static string PGNFile_Test_3_Game { get { return @"DemoFile\test,3,game.pgn"; } }
        public static string PGNFile_WorldOpen_Game { get { return @"DemoFile\worldopen,game.pgn"; } }

    }
}
