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
            this._WorkButton = new System.Windows.Forms.Button();
            this._NumericBox = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this._NumericBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _WorkButton
            // 
            this._WorkButton.Location = new System.Drawing.Point(296, 141);
            this._WorkButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this._NumericBox.Location = new System.Drawing.Point(31, 109);
            this._NumericBox.Maximum = new decimal(new int[] {
            512000,
            0,
            0,
            0});
            this._NumericBox.Name = "_NumericBox";
            this._NumericBox.Size = new System.Drawing.Size(407, 25);
            this._NumericBox.TabIndex = 1;
            this._NumericBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._NumericBox.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            // 
            // MainForm
            // 
            this.AcceptButton = this._WorkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 325);
            this.Controls.Add(this._NumericBox);
            this.Controls.Add(this._WorkButton);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "浪浪口算题目生成器";
            ((System.ComponentModel.ISupportInitialize)(this._NumericBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _WorkButton;
        private System.Windows.Forms.NumericUpDown _NumericBox;
    }
}

