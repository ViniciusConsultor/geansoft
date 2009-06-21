namespace Gean.Client.MathsExercise
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._WorkButton = new System.Windows.Forms.Button();
            this._NumericBox = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._NumericBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _WorkButton
            // 
            this._WorkButton.Location = new System.Drawing.Point(193, 237);
            this._WorkButton.Margin = new System.Windows.Forms.Padding(4);
            this._WorkButton.Name = "_WorkButton";
            this._WorkButton.Size = new System.Drawing.Size(142, 41);
            this._WorkButton.TabIndex = 0;
            this._WorkButton.Text = "开始生成";
            this._WorkButton.UseVisualStyleBackColor = true;
            this._WorkButton.Click += new System.EventHandler(this._WorkButton_Click);
            // 
            // _NumericBox
            // 
            this._NumericBox.Increment = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._NumericBox.Location = new System.Drawing.Point(193, 205);
            this._NumericBox.Maximum = new decimal(new int[] {
            512000,
            0,
            0,
            0});
            this._NumericBox.Name = "_NumericBox";
            this._NumericBox.Size = new System.Drawing.Size(234, 25);
            this._NumericBox.TabIndex = 1;
            this._NumericBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._NumericBox.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this._AboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(472, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ExitToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // _ExitToolStripMenuItem
            // 
            this._ExitToolStripMenuItem.Name = "_ExitToolStripMenuItem";
            this._ExitToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this._ExitToolStripMenuItem.Text = "退出(&X)";
            this._ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // _AboutToolStripMenuItem
            // 
            this._AboutToolStripMenuItem.Name = "_AboutToolStripMenuItem";
            this._AboutToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this._AboutToolStripMenuItem.Text = "关于(&A)";
            this._AboutToolStripMenuItem.Click += new System.EventHandler(this._AboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 303);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(472, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(337, 89);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 39);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this._WorkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 325);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this._NumericBox);
            this.Controls.Add(this._WorkButton);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "小学生数学练习生成器";
            ((System.ComponentModel.ISupportInitialize)(this._NumericBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _WorkButton;
        private System.Windows.Forms.NumericUpDown _NumericBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _AboutToolStripMenuItem;
        private System.Windows.Forms.Button button1;
    }
}

