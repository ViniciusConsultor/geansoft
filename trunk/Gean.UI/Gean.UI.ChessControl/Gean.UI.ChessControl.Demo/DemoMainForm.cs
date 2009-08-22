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
        public FormWindowState IsShangBan { get { return FormWindowState.Maximized; } }
        public string PGNFile { get { return @"pgn\aero09.pgn"; } }

        #region MyRegion

        private string _demoFile = Path.GetDirectoryName(@"..\..\DemoFile\");
        private ChessBoard _board = new ChessBoard();

        public DemoMainForm()
        {
            InitializeComponent();
            this._board.Dock = DockStyle.Fill;
            this._board.BringToFront();
            this._mainSpliter.Panel1.Controls.Add(_board);
            this._statusLabel.Text = "OK";
            this._recordListView.SelectedIndexChanged += new EventHandler(SelectedRecord);

            this._board.PlayEvent += new ChessBoard.PlayEventHandler(_board_PlayEvent);

            ChessRecordPlayToolStrip strip = new ChessRecordPlayToolStrip();

            this._stripContainer.TopToolStripPanel.Controls.Add(strip);
            this._stripContainer.TopToolStripPanel.Controls.Add(_mainMenuStrip);

            this.WindowState = this.IsShangBan;
        }

        void _board_PlayEvent(object sender, ChessBoard.PlayEventArgs e)
        {
            TreeNode node = new TreeNode(e.ChessStep.ToString());
            this._currTree.Nodes.Add(node);
            this._statusLabel.Text = e.ChessStep.ToString();
            FENBuilder fen = FENBuilder.CreateFENBuilder(_board.OwnedChessGame);
            _FENStringLabel.Text = fen.ToFENString();
        }

        #endregion

        ChessRecordFile records = new ChessRecordFile();

        private void PGNConvent(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Duration duration = new Duration();
            duration.Start();
            PGNReader reader = new PGNReader();
            reader.Filename = Path.GetFullPath(Path.Combine(_demoFile, PGNFile)); 
            reader.AddEvents(records);
            reader.Parse();
            string s = records[0].ToString();

            this._recordListView.Items.Clear();
            foreach (var item in records)
            {
                this._recordListView.Add(item);
            }
            duration.Stop();
            this._statusLabel.Text = string.Format("[Count: {0} record]. [Duration time: {1}]. [{2} Time/Record.]",
                records.Count, duration.DurationValue, duration.DurationValue / records.Count);
            this.Cursor = Cursors.Default;
        }

        private void SelectedRecord(object sender, EventArgs e)
        {
            if (this._recordListView.SelectedRecord == null || this._recordListView.SelectedRecord.Length == 0)
            {
                return;
            }
            this._recordTree.Nodes.Clear();
            ChessRecord record = this._recordListView.SelectedRecord[0];
            TreeNode node = new TreeNode("Game");
            foreach (var item in record.Items)
            {
                GetSubTreenode(item, node);
            }
            this._recordTree.BeginUpdate();
            this._recordTree.Nodes.Add(node);
            this._recordTree.ShowLines = true;
            this._recordTree.ExpandAll();
            this._recordTree.EndUpdate();
        }

        private static void GetSubTreenode(ISequenceItem item, TreeNode parNode)
        {
            TreeNode squNode = new TreeNode(item.Value);
            if (item is IStepTree)
            {
                IStepTree st = (IStepTree)item;
                if (st.HasChildren)
                {
                    foreach (var subItem in st.Items)
                    {
                        GetSubTreenode(subItem, squNode);
                    }
                }
            }
            parNode.Nodes.Add(squNode);
        }

        private void NewGame(object sender, EventArgs e)
        {
            this._board.LoadGame();
        }
    }
}
