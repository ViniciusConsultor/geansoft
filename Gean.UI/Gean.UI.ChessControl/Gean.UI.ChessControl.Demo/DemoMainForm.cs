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
        #region MyRegion

        private string _demoFile = Path.GetDirectoryName(@"..\..\DemoFile\");
        private ChessBoard _board = new ChessBoard();

        public DemoMainForm()
        {
            InitializeComponent();
            this._board.Dock = DockStyle.Fill;
            this._board.BringToFront();
            this._splitContainer.Panel1.Controls.Add(_board);
            this._recordList.SelectedValueChanged += new EventHandler(SelectedRecord);

            this._board.PlayEvent += new ChessBoard.PlayEventHandler(_board_PlayEvent);
            this._board.PlayPairEvent += new ChessBoard.PlayPairEventHandler(_board_PlayPairEvent);

            this.WindowState = FormWindowState.Maximized;
        }


        void _board_PlayPairEvent(object sender, ChessBoard.PlayPairEventArgs e)
        {
            //TreeNode node = new TreeNode(e.ChessStepPair.ToString());
            //this._currTree.Nodes.Add(node);
        }

        void _board_PlayEvent(object sender, ChessBoard.PlayEventArgs e)
        {
            TreeNode node = new TreeNode(e.ChessStep.ToString());
            this._currTree.Nodes.Add(node);
        }

        #endregion

        ChessRecordCollection records = new ChessRecordCollection();

        private void PGNConvent(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            Duration duration = new Duration();
            duration.Start();
            ChessPGNReader reader = new ChessPGNReader();
            reader.Filename = Path.GetFullPath(Path.Combine(_demoFile, @"pgn\__aGame1.pgn")); //__agame.pgn"));
            reader.AddEvents(records);
            reader.Parse();

            this._recordList.Items.Clear();
            foreach (var item in records)
            {
                this._recordList.Items.Add(item.ChessTags);
            }
            duration.Stop();
            this._label.Text = string.Format("[Count: {0} record]. [Duration time: {1}]. [{2} Time/Record.]",
                records.Count, duration.DurationValue, duration.DurationValue / records.Count);
            this.Cursor = Cursors.Default;
        }

        private void SelectedRecord(object sender, EventArgs e)
        {
            this._recordTree.Nodes.Clear();
            int k = this._recordList.SelectedIndex;
            ChessRecord record = records[k];
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
