﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Gean.Module.Chess;

namespace Gean.Gui.ChessControl.Demo
{
    public partial class DemoMainForm : Form
    {


        public FormWindowState IsShangBan { get { return Program.IsShangBan; } }

        public string PGNFile { get { return Program.PGNFile_Test_2_Game; } } 
        private string _demoFile = Path.GetDirectoryName(@"..\..\DemoFile\");
        private Board _board = new Board();
        private Records records = new Records();

        /// <summary>构造函数</summary>
        public DemoMainForm()
        {
            InitializeComponent();
            this._board.Dock = DockStyle.Fill;
            this._board.BringToFront();
            this._splitContainer3.Panel1.Controls.Add(_board);
            this._label.Text = "OK";
            this._recordListView.SelectedIndexChanged += new EventHandler(SelectedRecord);

            this._board.PlayEvent += new Board.PlayEventHandler(WhilePlayed);

            RecordPlayToolStrip strip = new RecordPlayToolStrip();

            this._stripContainer.TopToolStripPanel.Controls.Add(strip);
            this._stripContainer.TopToolStripPanel.Controls.Add(_mainMenuStrip);

            this.WindowState = this.IsShangBan;
        }

        /// <summary>当下棋后发生</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WhilePlayed(object sender, Board.PlayEventArgs e)
        {
            this._fenTextBox.Text = _board.Game.ToString() + "\r\n" + _board.Game.Generator() + "\r\n";// +e.GetHashCode();
        }

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
            this._label.Text = string.Format("[Count: {0} record]. [Duration time: {1}]. [{2} Time/Record.]",
                records.Count, duration.DurationValue, duration.DurationValue / records.Count);
            this.Cursor = Cursors.Default;
        }

        private void SelectedRecord(object sender, EventArgs e)
        {
            if (this._recordListView.SelectedRecord == null || this._recordListView.SelectedRecord.Length == 0)
            {
                return;
            }
            _recordTree.Nodes.Clear();
            Record record = this._recordListView.SelectedRecord[0];
            TreeNode node = new TreeNode("Game");

            this.MarkNode(record, node, new StringBuilder());

            _recordTree.BeginUpdate();
            _recordTree.Nodes.Add(node);
            _recordTree.ShowLines = true;
            _recordTree.ExpandAll();
            _recordTree.EndUpdate();
        }

        private void MarkNode(ITree tree, TreeNode node, StringBuilder text)
        {
            foreach (IItem item in tree.Items)
            {
                TreeNode subnode = new TreeNode();
                if (item is Step)
                {
                    Step step = (Step)item;
                    if (step.GameSide == Enums.GameSide.White)
                    {
                        text = new StringBuilder();
                        text.Append(item.ItemType).Append(": ").Append(step.Generator());
                    }
                    if (step.GameSide == Enums.GameSide.Black)
                    {
                        text.Append(' ').Append(step.Generator());
                        subnode.Text = text.ToString();
                        node.Nodes.Add(subnode);
                    }
                    if (item is ITree)
                    {
                        ITree subtree = (ITree)item;
                        if (subtree.HasChildren)
                        {
                            MarkNode(subtree, subnode, new StringBuilder());
                        }
                    }
                }
                else
                {
                    text = new StringBuilder();
                    text.Append(item.ItemType).Append(": ").Append(item.Value);
                    subnode.Text = text.ToString();
                    node.Nodes.Add(subnode);
                }
            }
        }
        private void NewGame(object sender, EventArgs e)
        {
            this._board.LoadSituation();
            this.WhilePlayed(null, null);
        }
    }
}