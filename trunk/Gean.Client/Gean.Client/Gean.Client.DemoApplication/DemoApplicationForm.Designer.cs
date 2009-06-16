namespace Gean.Client.DemoApplication
{
    partial class DemoApplicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DemoApplicationForm));
            this._ClearButton = new System.Windows.Forms.Button();
            this._OKButton = new System.Windows.Forms.Button();
            this._CancelButton = new System.Windows.Forms.Button();
            this._Demo1Button = new System.Windows.Forms.Button();
            this._Demo2Button = new System.Windows.Forms.Button();
            this._Demo3Button = new System.Windows.Forms.Button();
            this._Listbox = new System.Windows.Forms.ListBox();
            this._TreeView = new System.Windows.Forms.TreeView();
            this._PropertyGrid2 = new System.Windows.Forms.PropertyGrid();
            this._PropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // _ClearButton
            // 
            this._ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._ClearButton.Location = new System.Drawing.Point(409, 399);
            this._ClearButton.Name = "_ClearButton";
            this._ClearButton.Size = new System.Drawing.Size(87, 34);
            this._ClearButton.TabIndex = 5;
            this._ClearButton.Text = "Clear";
            this._ClearButton.UseVisualStyleBackColor = true;
            this._ClearButton.Click += new System.EventHandler(this._ClearButton_Click);
            // 
            // _OKButton
            // 
            this._OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._OKButton.Location = new System.Drawing.Point(520, 399);
            this._OKButton.Name = "_OKButton";
            this._OKButton.Size = new System.Drawing.Size(87, 34);
            this._OKButton.TabIndex = 0;
            this._OKButton.Text = "OK";
            this._OKButton.UseVisualStyleBackColor = true;
            this._OKButton.Click += new System.EventHandler(this._OKButton_Click);
            // 
            // _CancelButton
            // 
            this._CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CancelButton.Location = new System.Drawing.Point(613, 399);
            this._CancelButton.Name = "_CancelButton";
            this._CancelButton.Size = new System.Drawing.Size(87, 34);
            this._CancelButton.TabIndex = 1;
            this._CancelButton.Text = "Cancel";
            this._CancelButton.UseVisualStyleBackColor = true;
            this._CancelButton.Click += new System.EventHandler(this._CancelButton_Click);
            // 
            // _Demo1Button
            // 
            this._Demo1Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._Demo1Button.Location = new System.Drawing.Point(12, 399);
            this._Demo1Button.Name = "_Demo1Button";
            this._Demo1Button.Size = new System.Drawing.Size(75, 23);
            this._Demo1Button.TabIndex = 2;
            this._Demo1Button.Text = "Demo1";
            this._Demo1Button.UseVisualStyleBackColor = true;
            this._Demo1Button.Click += new System.EventHandler(this._Demo1Button_Click);
            // 
            // _Demo2Button
            // 
            this._Demo2Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._Demo2Button.Location = new System.Drawing.Point(93, 399);
            this._Demo2Button.Name = "_Demo2Button";
            this._Demo2Button.Size = new System.Drawing.Size(75, 23);
            this._Demo2Button.TabIndex = 3;
            this._Demo2Button.Text = "Demo2";
            this._Demo2Button.UseVisualStyleBackColor = true;
            this._Demo2Button.Click += new System.EventHandler(this._Demo2Button_Click);
            // 
            // _Demo3Button
            // 
            this._Demo3Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._Demo3Button.Location = new System.Drawing.Point(174, 399);
            this._Demo3Button.Name = "_Demo3Button";
            this._Demo3Button.Size = new System.Drawing.Size(75, 23);
            this._Demo3Button.TabIndex = 4;
            this._Demo3Button.Text = "Demo3";
            this._Demo3Button.UseVisualStyleBackColor = true;
            this._Demo3Button.Click += new System.EventHandler(this._Demo3Button_Click);
            // 
            // _Listbox
            // 
            this._Listbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._Listbox.FormattingEnabled = true;
            this._Listbox.Location = new System.Drawing.Point(12, 12);
            this._Listbox.Name = "_Listbox";
            this._Listbox.Size = new System.Drawing.Size(688, 186);
            this._Listbox.TabIndex = 6;
            // 
            // _TreeView
            // 
            this._TreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._TreeView.Location = new System.Drawing.Point(409, 204);
            this._TreeView.Name = "_TreeView";
            this._TreeView.Size = new System.Drawing.Size(291, 189);
            this._TreeView.TabIndex = 7;
            this._TreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this._TreeView_NodeMouseClick);
            // 
            // _PropertyGrid2
            // 
            this._PropertyGrid2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._PropertyGrid2.Location = new System.Drawing.Point(211, 204);
            this._PropertyGrid2.Name = "_PropertyGrid2";
            this._PropertyGrid2.Size = new System.Drawing.Size(190, 189);
            this._PropertyGrid2.TabIndex = 9;
            // 
            // _PropertyGrid1
            // 
            this._PropertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this._PropertyGrid1.Location = new System.Drawing.Point(12, 204);
            this._PropertyGrid1.Name = "_PropertyGrid1";
            this._PropertyGrid1.Size = new System.Drawing.Size(190, 189);
            this._PropertyGrid1.TabIndex = 8;
            // 
            // DemoApplicationForm
            // 
            this.AcceptButton = this._OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CancelButton;
            this.ClientSize = new System.Drawing.Size(712, 445);
            this.Controls.Add(this._PropertyGrid1);
            this.Controls.Add(this._PropertyGrid2);
            this.Controls.Add(this._TreeView);
            this.Controls.Add(this._Listbox);
            this.Controls.Add(this._Demo3Button);
            this.Controls.Add(this._Demo2Button);
            this.Controls.Add(this._Demo1Button);
            this.Controls.Add(this._CancelButton);
            this.Controls.Add(this._OKButton);
            this.Controls.Add(this._ClearButton);
            this.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DemoApplicationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DemoApplicationForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _ClearButton;
        private System.Windows.Forms.Button _OKButton;
        private System.Windows.Forms.Button _CancelButton;
        private System.Windows.Forms.Button _Demo1Button;
        private System.Windows.Forms.Button _Demo2Button;
        private System.Windows.Forms.Button _Demo3Button;
        private System.Windows.Forms.ListBox _Listbox;
        private System.Windows.Forms.TreeView _TreeView;
        private System.Windows.Forms.PropertyGrid _PropertyGrid2;
        private System.Windows.Forms.PropertyGrid _PropertyGrid1;
    }
}

