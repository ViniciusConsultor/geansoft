namespace Gean.Wrapper.PlugTree.Demo
{
    partial class Form1
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
            this._CloseButton = new System.Windows.Forms.Button();
            this._BeginDemoButton = new System.Windows.Forms.Button();
            this._MessageListbox = new System.Windows.Forms.ListBox();
            this._ClearButton = new System.Windows.Forms.Button();
            this._PathTreeView = new System.Windows.Forms.TreeView();
            this._PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // _CloseButton
            // 
            this._CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CloseButton.Location = new System.Drawing.Point(829, 422);
            this._CloseButton.Name = "_CloseButton";
            this._CloseButton.Size = new System.Drawing.Size(62, 24);
            this._CloseButton.TabIndex = 1;
            this._CloseButton.Text = "Close";
            this._CloseButton.UseVisualStyleBackColor = true;
            this._CloseButton.Click += new System.EventHandler(this._CloseButton_Click);
            // 
            // _BeginDemoButton
            // 
            this._BeginDemoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._BeginDemoButton.Location = new System.Drawing.Point(770, 148);
            this._BeginDemoButton.Name = "_BeginDemoButton";
            this._BeginDemoButton.Size = new System.Drawing.Size(118, 23);
            this._BeginDemoButton.TabIndex = 0;
            this._BeginDemoButton.Text = "Demo Begin!";
            this._BeginDemoButton.UseVisualStyleBackColor = true;
            this._BeginDemoButton.Click += new System.EventHandler(this._BeginDemoButton_Click);
            // 
            // _MessageListbox
            // 
            this._MessageListbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._MessageListbox.FormattingEnabled = true;
            this._MessageListbox.Location = new System.Drawing.Point(8, 8);
            this._MessageListbox.Name = "_MessageListbox";
            this._MessageListbox.Size = new System.Drawing.Size(880, 134);
            this._MessageListbox.TabIndex = 3;
            // 
            // _ClearButton
            // 
            this._ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._ClearButton.Location = new System.Drawing.Point(689, 148);
            this._ClearButton.Name = "_ClearButton";
            this._ClearButton.Size = new System.Drawing.Size(75, 23);
            this._ClearButton.TabIndex = 2;
            this._ClearButton.Text = "Clear";
            this._ClearButton.UseVisualStyleBackColor = true;
            this._ClearButton.Click += new System.EventHandler(this._ClearButton_Click);
            // 
            // _PathTreeView
            // 
            this._PathTreeView.Location = new System.Drawing.Point(437, 177);
            this._PathTreeView.Name = "_PathTreeView";
            this._PathTreeView.Size = new System.Drawing.Size(451, 239);
            this._PathTreeView.TabIndex = 4;
            // 
            // _PropertyGrid
            // 
            this._PropertyGrid.Location = new System.Drawing.Point(197, 177);
            this._PropertyGrid.Name = "_PropertyGrid";
            this._PropertyGrid.Size = new System.Drawing.Size(234, 239);
            this._PropertyGrid.TabIndex = 5;
            // 
            // Form1
            // 
            this.AcceptButton = this._BeginDemoButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CloseButton;
            this.ClientSize = new System.Drawing.Size(899, 458);
            this.Controls.Add(this._PropertyGrid);
            this.Controls.Add(this._PathTreeView);
            this.Controls.Add(this._ClearButton);
            this.Controls.Add(this._MessageListbox);
            this.Controls.Add(this._BeginDemoButton);
            this.Controls.Add(this._CloseButton);
            this.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PlugTree.Demo.Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _CloseButton;
        private System.Windows.Forms.Button _BeginDemoButton;
        private System.Windows.Forms.ListBox _MessageListbox;
        private System.Windows.Forms.Button _ClearButton;
        private System.Windows.Forms.TreeView _PathTreeView;
        private System.Windows.Forms.PropertyGrid _PropertyGrid;
    }
}

