using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

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

            this._board.SetGridImage(white, black);
            this._board.ChessPlayedEvent += new ChessBoard.ChessPlayedEventHandler(_board_ChessPlayedEvent);
        }

        void _board_ChessPlayedEvent(object sender, ChessBoard.ChessPlayedEventArgs e)
        {
            ChessGrid oldrid = e.OldGrid;
            ChessGrid newrid = e.NewGrid;
            this._actionListBox.Items.Add(oldrid.Square.ToString() + "  ->  " + newrid.Square.ToString());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._propertyGrid.SelectedObject = this._board; 
        }

        private void _openingsMenuButton_Click(object sender, EventArgs e)
        {
            this._board.ChessGameBinding();
        }
    }
}
