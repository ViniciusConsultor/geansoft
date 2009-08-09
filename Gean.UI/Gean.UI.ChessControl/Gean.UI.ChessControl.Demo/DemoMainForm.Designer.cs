namespace Gean.UI.ChessControl.Demo
{
    partial class DemoMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._recordTree = new System.Windows.Forms.TreeView();
            this._rightSplit = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this._recordList = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开局ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._pgnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._pgnConventToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._label = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this._currTree = new System.Windows.Forms.TreeView();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._rightSplit.Panel1.SuspendLayout();
            this._rightSplit.Panel2.SuspendLayout();
            this._rightSplit.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(0, 24);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this.splitContainer1);
            this._splitContainer.Size = new System.Drawing.Size(632, 399);
            this._splitContainer.SplitterDistance = 204;
            this._splitContainer.SplitterWidth = 3;
            this._splitContainer.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._recordTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._rightSplit);
            this.splitContainer1.Size = new System.Drawing.Size(425, 399);
            this.splitContainer1.SplitterDistance = 108;
            this.splitContainer1.TabIndex = 1;
            // 
            // _recordTree
            // 
            this._recordTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._recordTree.Location = new System.Drawing.Point(0, 0);
            this._recordTree.Name = "_recordTree";
            this._recordTree.Size = new System.Drawing.Size(108, 399);
            this._recordTree.TabIndex = 0;
            // 
            // _rightSplit
            // 
            this._rightSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rightSplit.Location = new System.Drawing.Point(0, 0);
            this._rightSplit.Name = "_rightSplit";
            this._rightSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _rightSplit.Panel1
            // 
            this._rightSplit.Panel1.Controls.Add(this.tabControl1);
            // 
            // _rightSplit.Panel2
            // 
            this._rightSplit.Panel2.Controls.Add(this._recordList);
            this._rightSplit.Size = new System.Drawing.Size(313, 399);
            this._rightSplit.SplitterDistance = 273;
            this._rightSplit.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(313, 273);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(305, 247);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "棋盘选项";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(305, 247);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "棋谱选项";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // _recordList
            // 
            this._recordList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._recordList.FormattingEnabled = true;
            this._recordList.Location = new System.Drawing.Point(0, 0);
            this._recordList.Name = "_recordList";
            this._recordList.Size = new System.Drawing.Size(313, 121);
            this._recordList.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开局ToolStripMenuItem,
            this._pgnToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开局ToolStripMenuItem
            // 
            this.开局ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newGameToolStripMenuItem});
            this.开局ToolStripMenuItem.Name = "开局ToolStripMenuItem";
            this.开局ToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.开局ToolStripMenuItem.Text = "新开局";
            // 
            // _newGameToolStripMenuItem
            // 
            this._newGameToolStripMenuItem.Name = "_newGameToolStripMenuItem";
            this._newGameToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this._newGameToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this._newGameToolStripMenuItem.Text = "新开棋局";
            this._newGameToolStripMenuItem.Click += new System.EventHandler(this.NewGame);
            // 
            // _pgnToolStripMenuItem
            // 
            this._pgnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._pgnConventToolStripMenuItem1});
            this._pgnToolStripMenuItem.Name = "_pgnToolStripMenuItem";
            this._pgnToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this._pgnToolStripMenuItem.Text = "已有棋谱";
            // 
            // _pgnConventToolStripMenuItem1
            // 
            this._pgnConventToolStripMenuItem1.Name = "_pgnConventToolStripMenuItem1";
            this._pgnConventToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this._pgnConventToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this._pgnConventToolStripMenuItem1.Text = "转换棋谱";
            this._pgnConventToolStripMenuItem1.Click += new System.EventHandler(this.PGNConvent);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._label});
            this.statusStrip1.Location = new System.Drawing.Point(0, 423);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(632, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _label
            // 
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this._currTree);
            this.splitContainer2.Size = new System.Drawing.Size(299, 241);
            this.splitContainer2.SplitterDistance = 99;
            this.splitContainer2.TabIndex = 0;
            // 
            // _currTree
            // 
            this._currTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._currTree.Location = new System.Drawing.Point(0, 0);
            this._currTree.Name = "_currTree";
            this._currTree.Size = new System.Drawing.Size(99, 241);
            this._currTree.TabIndex = 0;
            // 
            // DemoMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 445);
            this.Controls.Add(this._splitContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DemoMainForm";
            this.ShowIcon = false;
            this.Text = "DemoMainForm";
            this._splitContainer.Panel2.ResumeLayout(false);
            this._splitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._rightSplit.Panel1.ResumeLayout(false);
            this._rightSplit.Panel2.ResumeLayout(false);
            this._rightSplit.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.ListBox _recordList;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开局ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer _rightSplit;
        private System.Windows.Forms.TreeView _recordTree;
        private System.Windows.Forms.ToolStripMenuItem _pgnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _pgnConventToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel _label;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TreeView _currTree;
    }
}

