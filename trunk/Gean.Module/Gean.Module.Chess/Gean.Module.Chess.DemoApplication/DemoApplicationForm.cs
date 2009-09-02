using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Module.Chess.DemoApplication
{
    partial class DemoApplicationForm : Form
    {

        #region

        public DemoMethod Method { get; set; }

        public string StatusText1
        {
            get { return _statusLabel1.Text; }
            set { _statusLabel1.Text = value; }
        }
        public string StatusText2
        {
            get { return _statusLabel2.Text; }
            set { _statusLabel2.Text = value; }
        }
        public string TextBox1
        {
            get { return _textBox1.Text; }
            set { _textBox1.Text = value; }
        }
        public string TextBox2
        {
            get { return _textBox2.Text; }
            set { _textBox2.Text = value; }
        }
        public string TextBox3
        {
            get { return _textBox3.Text; }
            set { _textBox3.Text = value; }
        }
        public ListBox.ObjectCollection ListItems
        {
            get { return _Listbox.Items; }
        }
        public TreeNode RootNode
        {
            get { return _TreeNode; }
        }

        public int DemoCount
        {
            get { return _DemoCount++; }
        }
        private int _DemoCount = 1;

        public string InitString
        {
            get { return DateTime.Now.ToLongTimeString() + " | Gean.Module.Chess.DemoApplication Initialized!"; }
        }

        public DemoApplicationForm()
        {
            InitializeComponent();
            this.StatusText2 = "";
            this.Method = new DemoMethod();
            this.UpdateListBox();
        }

        private void _OKButton_Click(object sender, EventArgs e)
        {
            this.MainDemoApplication();
        }
        private void _CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        internal void Clear(object sender, EventArgs e)
        {
            this._textBox1.Clear();
            this._textBox2.Clear();
            this._Listbox.Items.Clear();
            this._Listbox.Update();
            this._TreeView.Nodes.Clear();
            this._TreeView.Update();
            this.UpdateListBox();
            this.Update();
        }

        /// <summary>
        /// 每次运行Demo的记数更新
        /// </summary>
        private void UpdateListBox()
        {
            this._Listbox.Items.Add(this.InitString);
            this.Text = Application.ProductName + " | DemoCount: " + this.DemoCount.ToString();
        }

        private void _TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.TreeNodeClick(e.Node);
        }

        #endregion

    }
}
