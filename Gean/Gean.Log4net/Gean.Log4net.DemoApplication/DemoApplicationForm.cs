using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Log4net.DemoApplication
{
    partial class DemoApplicationForm : Form
    {

        #region

        public int DemoCount
        {
            get { return _DemoCount++; }
        }
        private int _DemoCount = 1;

        public string InitString
        {
            get { return DateTime.Now.ToLongTimeString() + " | Gean.Log4net.DemoApplication Initialized!"; }
        }

        public DemoApplicationForm()
        {
            InitializeComponent();
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
        private void _ClearButton_Click(object sender, EventArgs e)
        {
            this._Listbox.Items.Clear();
            this._Listbox.Update();
            this._TreeView.Nodes.Clear();
            this._TreeView.Update();
            this._PropertyGrid1.SelectedObject = null;
            this._PropertyGrid1.Update();
            this._PropertyGrid2.SelectedObject = null;
            this._PropertyGrid2.Update();
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

        private void _Demo1Button_Click(object sender, EventArgs e)
        {
            this.Demo1Application();
        }

        private void _Demo2Button_Click(object sender, EventArgs e)
        {
            this.Demo2Application();
        }

        private void _Demo3Button_Click(object sender, EventArgs e)
        {
            this.Demo3Application();
        }

        private void _TreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.TreeNodeClick(e.Node);
        }

        #endregion

    }
}
