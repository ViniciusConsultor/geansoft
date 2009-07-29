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
        private string _whiteDir = "";
        private string _blackDir = "";
        private ChessBoard _board = new ChessBoard();

        public DemoMainForm()
        {
            InitializeComponent();
            this._board.Dock = DockStyle.Fill;
            this._board.BringToFront();
            this._splitContainer.Panel2.Controls.Add(_board);

            this._whiteDir = Path.Combine(_demoFile, @"ChessBoardGrid\White");
            this._blackDir = Path.Combine(_demoFile, @"ChessBoardGrid\Black");

            Image white = Image.FromFile(Path.Combine(_whiteDir, "BoardGrid - White - 001.jpg"));
            Image black = Image.FromFile(Path.Combine(_blackDir, "BoardGrid - Black - 001.jpg"));

            ChessSerivce.Initialize();
            ChessSerivce.InitializeChessmanImage();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._propertyGrid.SelectedObject = this._board; 
        }
    }
}
