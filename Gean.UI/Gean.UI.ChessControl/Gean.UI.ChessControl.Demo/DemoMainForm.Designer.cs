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
            this._propertyGrid = new System.Windows.Forms.PropertyGrid();
            this._leftSplitContainer = new System.Windows.Forms.SplitContainer();
            this._actionListBox = new System.Windows.Forms.ListBox();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._leftSplitContainer.Panel1.SuspendLayout();
            this._leftSplitContainer.Panel2.SuspendLayout();
            this._leftSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(10, 11);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._leftSplitContainer);
            this._splitContainer.Size = new System.Drawing.Size(612, 423);
            this._splitContainer.SplitterDistance = 235;
            this._splitContainer.SplitterWidth = 3;
            this._splitContainer.TabIndex = 0;
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertyGrid.Font = new System.Drawing.Font("Tahoma", 8F);
            this._propertyGrid.Location = new System.Drawing.Point(0, 0);
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.Size = new System.Drawing.Size(119, 423);
            this._propertyGrid.TabIndex = 0;
            // 
            // _leftSplitContainer
            // 
            this._leftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._leftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this._leftSplitContainer.Name = "_leftSplitContainer";
            // 
            // _leftSplitContainer.Panel1
            // 
            this._leftSplitContainer.Panel1.Controls.Add(this._propertyGrid);
            // 
            // _leftSplitContainer.Panel2
            // 
            this._leftSplitContainer.Panel2.Controls.Add(this._actionListBox);
            this._leftSplitContainer.Size = new System.Drawing.Size(235, 423);
            this._leftSplitContainer.SplitterDistance = 119;
            this._leftSplitContainer.TabIndex = 1;
            // 
            // _actionListBox
            // 
            this._actionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionListBox.FormattingEnabled = true;
            this._actionListBox.Location = new System.Drawing.Point(0, 0);
            this._actionListBox.Name = "_actionListBox";
            this._actionListBox.Size = new System.Drawing.Size(112, 420);
            this._actionListBox.TabIndex = 0;
            // 
            // DemoMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 445);
            this.Controls.Add(this._splitContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "DemoMainForm";
            this.Padding = new System.Windows.Forms.Padding(10, 11, 10, 11);
            this.ShowIcon = false;
            this.Text = "DemoMainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.ResumeLayout(false);
            this._leftSplitContainer.Panel1.ResumeLayout(false);
            this._leftSplitContainer.Panel2.ResumeLayout(false);
            this._leftSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.PropertyGrid _propertyGrid;
        private System.Windows.Forms.SplitContainer _leftSplitContainer;
        private System.Windows.Forms.ListBox _actionListBox;
    }
}

