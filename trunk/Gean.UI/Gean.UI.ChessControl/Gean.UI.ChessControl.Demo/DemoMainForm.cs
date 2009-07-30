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

        private void 有棋测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._actionListBox.Items.Clear();
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    this._actionListBox.Items.Add(x.ToString() + " " + y.ToString());
                    if (this._board.OwnedChessGame[x, y].OwnedChessman != null)
                        this._actionListBox.Items.Add("  +!!!!!! " + x.ToString() + " " + y.ToString() + " !!!!!!");
                }
            }
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
