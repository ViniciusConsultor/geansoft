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
            this._MainMessageTextbox = new System.Windows.Forms.ListBox();
            this._ClearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _CloseButton
            // 
            this._CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._CloseButton.Location = new System.Drawing.Point(542, 253);
            this._CloseButton.Name = "_CloseButton";
            this._CloseButton.Size = new System.Drawing.Size(62, 24);
            this._CloseButton.TabIndex = 1;
            this._CloseButton.Text = "Close";
            this._CloseButton.UseVisualStyleBackColor = true;
            this._CloseButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // _BeginDemoButton
            // 
            this._BeginDemoButton.Location = new System.Drawing.Point(418, 254);
            this._BeginDemoButton.Name = "_BeginDemoButton";
            this._BeginDemoButton.Size = new System.Drawing.Size(118, 23);
            this._BeginDemoButton.TabIndex = 0;
            this._BeginDemoButton.Text = "Demo Begin!";
            this._BeginDemoButton.UseVisualStyleBackColor = true;
            this._BeginDemoButton.Click += new System.EventHandler(this._BeginDemoButton_Click);
            // 
            // _MainMessageTextbox
            // 
            this._MainMessageTextbox.Dock = System.Windows.Forms.DockStyle.Top;
            this._MainMessageTextbox.FormattingEnabled = true;
            this._MainMessageTextbox.Location = new System.Drawing.Point(8, 8);
            this._MainMessageTextbox.Name = "_MainMessageTextbox";
            this._MainMessageTextbox.Size = new System.Drawing.Size(596, 238);
            this._MainMessageTextbox.TabIndex = 3;
            // 
            // _ClearButton
            // 
            this._ClearButton.Location = new System.Drawing.Point(8, 254);
            this._ClearButton.Name = "_ClearButton";
            this._ClearButton.Size = new System.Drawing.Size(75, 23);
            this._ClearButton.TabIndex = 2;
            this._ClearButton.Text = "Clear";
            this._ClearButton.UseVisualStyleBackColor = true;
            this._ClearButton.Click += new System.EventHandler(this._ClearButton_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this._BeginDemoButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._CloseButton;
            this.ClientSize = new System.Drawing.Size(612, 288);
            this.Controls.Add(this._ClearButton);
            this.Controls.Add(this._MainMessageTextbox);
            this.Controls.Add(this._BeginDemoButton);
            this.Controls.Add(this._CloseButton);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
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
        private System.Windows.Forms.ListBox _MainMessageTextbox;
        private System.Windows.Forms.Button _ClearButton;
    }
}

