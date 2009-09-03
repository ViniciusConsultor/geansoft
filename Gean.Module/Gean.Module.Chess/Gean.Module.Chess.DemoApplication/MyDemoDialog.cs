using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Module.Chess.Demo
{
    partial class MyDemoDialog : Form
    {

        #region static property

        private static MyDemoDialog _form = null;

        public static string Title
        {
            get { return _form.Text; }
            set { _form.Text = value; }
        }
        public static string StatusText1
        {
            get { return _form._statusLabel1.Text; }
            set { _form._statusLabel1.Text = value; }
        }
        public static string StatusText2
        {
            get { return _form._statusLabel2.Text; }
            set { _form._statusLabel2.Text = value; }
        }
        public static string TextBox1
        {
            get { return _form._textBox1.Text; }
            set { _form._textBox1.Text = value; }
        }
        public static string TextBox2
        {
            get { return _form._textBox2.Text; }
            set { _form._textBox2.Text = value; }
        }
        public static string TextBox3
        {
            get { return _form._textBox3.Text; }
            set { _form._textBox3.Text = value; }
        }
        public static ProgressBar ProgressBar
        {
            get { return _form._progressBar; }
        }
        public static ListBox.ObjectCollection ListItems
        {
            get { return _form._Listbox.Items; }
        }
        public static TreeNode RootNode
        {
            get { return _form._TreeNode; }
        }
        public static Cursor FormCursor
        {
            get { return _form.Cursor; }
            set { _form.Cursor = value; }
        }

        #endregion

        #region any Form method...

        public DemoMethod Method { get; set; }
        public int DemoCount
        {
            get { return _DemoCount++; }
        }
        private int _DemoCount = 1;

        public string InitString
        {
            get { return DateTime.Now.ToLongTimeString() + " | DemoApplication1 Initialized!"; }
        }

        public MyDemoDialog()
        {
            InitializeComponent();
            _form = this;
            StatusText2 = "";
            this.Method = new DemoMethod();
            this.UpdateListBox();
            this._Listbox.SelectedIndexChanged += new EventHandler(_Listbox_SelectedIndexChanged);
        }

        void _Listbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(_Listbox.SelectedItem is ITree))
                return;
            _TreeView.Nodes.Clear();
            Record record = this._Listbox.SelectedItem as Record;
            TreeNode node = new TreeNode("Game");
            foreach (var item in record.Items)
            {
                GetSubTreenode(item, node);
                _textBox2.Text = record.ToString();
            }
            _TreeView.BeginUpdate();
            _TreeView.Nodes.Add(node);
            _TreeView.ShowLines = true;
            _TreeView.ExpandAll();
            _TreeView.EndUpdate();
        }

        /// <summary>生成ChessStep树递归子方法</summary>
        /// <param name="item"></param>
        /// <param name="parNode"></param>
        private static void GetSubTreenode(IItem item, TreeNode parNode)
        {
            TreeNode squNode = new TreeNode(item.Value);
            if (item is ITree)
            {
                ITree st = (ITree)item;
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

        private void _OKButton_Click(object sender, EventArgs e)
        {
            this.MainDemoApplication();
        }
        private void _CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static void Clear()
        {
            _form.Clear(null, null);
        }
        private void Clear(object sender, EventArgs e)
        {
            this._textBox1.Clear();
            this._textBox2.Clear();
            this._textBox3.Clear();
            this._statusLabel1.Text = "";
            this._statusLabel2.Text = "";
            this._progressBar.Value = 0;
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

        #region call

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Method.OnLoadMethod();
        }

        /// <summary>
        /// 主Demo应用方法
        /// </summary>
        private void MainDemoApplication()
        {
            this.Method.MainClick();
        }

        /// <summary>
        /// 程序界面中TreeView中的TreeNode的点击事件的方法
        /// </summary>
        /// <param name="treeNode">被点击的TreeNode</param>
        private void TreeNodeClick(TreeNode treeNode)
        {
            this.Method.NodeClick(treeNode);
        }

        #endregion
    }
}
