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
        ChessRecordCollection records = new ChessRecordCollection();

        private void PGNConvent(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Duration duration = new Duration();
            duration.Start();
            ChessPGNReader reader = new ChessPGNReader();
            reader.Filename = Path.GetFullPath(Path.Combine(_demoFile, @"pgn\__aGame2.pgn")); //__agame.pgn"));
            reader.AddEvents(records);
            reader.Parse();

            foreach (var item in records)
            {
                this._actionListBox.Items.Add(item.ChessTags);
            }
            duration.Stop();
            this._label.Text = string.Format("[Count: {0} record]. [Duration time: {1}]. [{2} Time/Record.]",
                records.Count, duration.DurationValue, duration.DurationValue / records.Count);
            this.Cursor = Cursors.Default;
        }

        private void SelectedRecord(object sender, EventArgs e)
        {
            this._recordTreeView.Nodes.Clear();
            int k = this._actionListBox.SelectedIndex;
            ChessRecord record = records[k];
            this._propertyGrid.SelectedObject = record;
            TreeNode node = new TreeNode("Game");
            foreach (var item in record.Items)
            {
                GetSubTreenode(item, node);
            }
            this._recordTreeView.BeginUpdate();
            this._recordTreeView.Nodes.Add(node);
            this._recordTreeView.ShowLines = true;
            this._recordTreeView.ExpandAll();
            this._recordTreeView.EndUpdate();
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


        #region MyRegion

        private string _demoFile = Path.GetDirectoryName(@"..\..\DemoFile\");
        private ChessBoard _board = new ChessBoard();

        public DemoMainForm()
        {
            InitializeComponent();
            this._board.Dock = DockStyle.Fill;
            this._board.BringToFront();
            this._splitContainer.Panel1.Controls.Add(_board);
            this._actionListBox.SelectedValueChanged += new EventHandler(SelectedRecord);

            this._board.PlayEvent += new ChessBoard.PlayEventHandler(_board_PlayEvent);
            this._board.PlayPairEvent += new ChessBoard.PlayPairEventHandler(_board_PlayPairEvent);
        }


        void _board_PlayPairEvent(object sender, ChessBoard.PlayPairEventArgs e)
        {
            this._actionListBox.Items.Add(e.ChessStepPair);
        }

        void _board_PlayEvent(object sender, ChessBoard.PlayEventArgs e)
        {
            this._actionListBox.Items.Add(e.ChessStep);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._propertyGrid.SelectedObject = this._board;
        }

        #endregion
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
