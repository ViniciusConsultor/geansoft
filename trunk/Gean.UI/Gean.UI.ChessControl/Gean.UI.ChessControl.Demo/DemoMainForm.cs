using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Gean.Wrapper.Chess;

namespace Gean.UI.ChessControl.Demo
{
    public partial class DemoMainForm : Form
    {
        private string _demoFile = Path.GetDirectoryName(@"..\..\DemoFile\");
        private ChessBoard _board = new ChessBoard();

        public DemoMainForm()
        {
            InitializeComponent();
            this._board.Dock = DockStyle.Fill;
            this._board.BringToFront();
            this._splitContainer.Panel1.Controls.Add(_board);

            //this._board.PlayEvent += new ChessBoard.PlayEventHandler(_board_PlayEvent);
            this._board.PlayPairEvent += new ChessBoard.PlayPairEventHandler(_board_PlayPairEvent);
        }

        void _board_PlayPairEvent(object sender, ChessBoard.PlayPairEventArgs e)
        {
            this._actionListBox.Items.Add(e.ChessStepPair);
        }

        void _board_PlayEvent(object sender, ChessBoard.PlayEventArgs e)
        {
            this._actionListBox.Items.Add(e.ChessStep);//.ToString());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._propertyGrid.SelectedObject = this._board; 
        }

        private void 开局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._board.LoadGame();
        }

    }

    static class First
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
            ChessBoardHelper.Initialize();
        }
    }

}
