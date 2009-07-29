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

            ChessBoardHelper.Initialize();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._propertyGrid.SelectedObject = this._board; 
        }

        private void _openingsMenuButton_Click(object sender, EventArgs e)
        {
            ChessBoardHelper.ChangeBoardImage(ChessResource.board_02);
        }
    }
}
