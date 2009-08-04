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
            this._rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this._actionListBox = new System.Windows.Forms.ListBox();
            this._propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.开局ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.chessRecordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._rightSplitContainer.Panel1.SuspendLayout();
            this._rightSplitContainer.Panel2.SuspendLayout();
            this._rightSplitContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
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
            this._splitContainer.Panel2.Controls.Add(this._rightSplitContainer);
            this._splitContainer.Size = new System.Drawing.Size(632, 399);
            this._splitContainer.SplitterDistance = 382;
            this._splitContainer.SplitterWidth = 3;
            this._splitContainer.TabIndex = 0;
            // 
            // _rightSplitContainer
            // 
            this._rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._rightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this._rightSplitContainer.Name = "_rightSplitContainer";
            // 
            // _rightSplitContainer.Panel1
            // 
            this._rightSplitContainer.Panel1.Controls.Add(this._actionListBox);
            // 
            // _rightSplitContainer.Panel2
            // 
            this._rightSplitContainer.Panel2.Controls.Add(this._propertyGrid);
            this._rightSplitContainer.Size = new System.Drawing.Size(247, 399);
            this._rightSplitContainer.SplitterDistance = 124;
            this._rightSplitContainer.TabIndex = 1;
            // 
            // _actionListBox
            // 
            this._actionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionListBox.FormattingEnabled = true;
            this._actionListBox.Location = new System.Drawing.Point(0, 0);
            this._actionListBox.Name = "_actionListBox";
            this._actionListBox.Size = new System.Drawing.Size(124, 394);
            this._actionListBox.TabIndex = 0;
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertyGrid.Font = new System.Drawing.Font("Tahoma", 8F);
            this._propertyGrid.Location = new System.Drawing.Point(0, 0);
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.Size = new System.Drawing.Size(119, 399);
            this._propertyGrid.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开局ToolStripMenuItem,
            this.chessRecordToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 开局ToolStripMenuItem
            // 
            this.开局ToolStripMenuItem.Name = "开局ToolStripMenuItem";
            this.开局ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.开局ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.开局ToolStripMenuItem.Text = "开局";
            this.开局ToolStripMenuItem.Click += new System.EventHandler(this.开局ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 423);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(632, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // chessRecordToolStripMenuItem
            // 
            this.chessRecordToolStripMenuItem.Name = "chessRecordToolStripMenuItem";
            this.chessRecordToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.chessRecordToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.chessRecordToolStripMenuItem.Text = "ChessRecord";
            this.chessRecordToolStripMenuItem.Click += new System.EventHandler(this.chessRecordToolStripMenuItem_Click);
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
            this._rightSplitContainer.Panel1.ResumeLayout(false);
            this._rightSplitContainer.Panel2.ResumeLayout(false);
            this._rightSplitContainer.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.PropertyGrid _propertyGrid;
        private System.Windows.Forms.SplitContainer _rightSplitContainer;
        private System.Windows.Forms.ListBox _actionListBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 开局ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chessRecordToolStripMenuItem;
    }
}

