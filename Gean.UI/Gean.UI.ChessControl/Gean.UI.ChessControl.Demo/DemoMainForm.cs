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
        private ChessBoard _board = new ChessBoard();

        public DemoMainForm()
        {
            InitializeComponent();

            this._splitContainer.Panel2.Controls.Add(_board);
            this._board.Dock = DockStyle.Fill;
            this._board.ChessPlayedEvent += new ChessBoard.ChessPlayedEventHandler(_board_ChessPlayedEvent);
        }

        void _board_ChessPlayedEvent(object sender, ChessBoard.ChessPlayedEventArgs e)
        {
            this._actionListBox.Items.Add(e.OldGrid.SquareX.ToString() + "," + e.OldGrid.SquareY.ToString() + " -> " +
                e.NewGrid.SquareX.ToString() + "," + e.NewGrid.SquareY.ToString());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._propertyGrid.SelectedObject = this._board; 
        }
    }
}
