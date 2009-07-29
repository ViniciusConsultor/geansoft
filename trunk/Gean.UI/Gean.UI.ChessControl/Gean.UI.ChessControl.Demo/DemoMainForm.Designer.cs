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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoMainForm));
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._leftSplitContainer = new System.Windows.Forms.SplitContainer();
            this._propertyGrid = new System.Windows.Forms.PropertyGrid();
            this._actionListBox = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._openingsMenuButton = new System.Windows.Forms.ToolStripButton();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this._leftSplitContainer.Panel1.SuspendLayout();
            this._leftSplitContainer.Panel2.SuspendLayout();
            this._leftSplitContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(10, 36);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._leftSplitContainer);
            this._splitContainer.Size = new System.Drawing.Size(612, 398);
            this._splitContainer.SplitterDistance = 234;
            this._splitContainer.SplitterWidth = 3;
            this._splitContainer.TabIndex = 0;
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
            this._leftSplitContainer.Size = new System.Drawing.Size(234, 398);
            this._leftSplitContainer.SplitterDistance = 118;
            this._leftSplitContainer.TabIndex = 1;
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertyGrid.Font = new System.Drawing.Font("Tahoma", 8F);
            this._propertyGrid.Location = new System.Drawing.Point(0, 0);
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.Size = new System.Drawing.Size(118, 398);
            this._propertyGrid.TabIndex = 0;
            // 
            // _actionListBox
            // 
            this._actionListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._actionListBox.FormattingEnabled = true;
            this._actionListBox.Location = new System.Drawing.Point(0, 0);
            this._actionListBox.Name = "_actionListBox";
            this._actionListBox.Size = new System.Drawing.Size(112, 394);
            this._actionListBox.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._openingsMenuButton});
            this.toolStrip1.Location = new System.Drawing.Point(10, 11);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(612, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _openingsMenuButton
            // 
            this._openingsMenuButton.Image = ((System.Drawing.Image)(resources.GetObject("_openingsMenuButton.Image")));
            this._openingsMenuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._openingsMenuButton.Name = "_openingsMenuButton";
            this._openingsMenuButton.Size = new System.Drawing.Size(66, 22);
            this._openingsMenuButton.Text = "开局(&C)";
            this._openingsMenuButton.Click += new System.EventHandler(this._openingsMenuButton_Click);
            // 
            // DemoMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 445);
            this.Controls.Add(this._splitContainer);
            this.Controls.Add(this.toolStrip1);
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
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.PropertyGrid _propertyGrid;
        private System.Windows.Forms.SplitContainer _leftSplitContainer;
        private System.Windows.Forms.ListBox _actionListBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _openingsMenuButton;
    }
}

