namespace Gean.UI.WinForms.Demo
{
    partial class CloudsPanelForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._propertyGrid = new System.Windows.Forms.PropertyGrid();
            this._okButton = new System.Windows.Forms.Button();
            this._canelButton = new System.Windows.Forms.Button();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _splitContainer
            // 
            this._splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._splitContainer.Location = new System.Drawing.Point(15, 16);
            this._splitContainer.Name = "_splitContainer";
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._propertyGrid);
            this._splitContainer.Size = new System.Drawing.Size(872, 368);
            this._splitContainer.SplitterDistance = 597;
            this._splitContainer.TabIndex = 0;
            // 
            // _propertyGrid
            // 
            this._propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._propertyGrid.Location = new System.Drawing.Point(0, 0);
            this._propertyGrid.Name = "_propertyGrid";
            this._propertyGrid.Size = new System.Drawing.Size(271, 368);
            this._propertyGrid.TabIndex = 0;
            // 
            // _okButton
            // 
            this._okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._okButton.Location = new System.Drawing.Point(703, 390);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(89, 29);
            this._okButton.TabIndex = 1;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _canelButton
            // 
            this._canelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._canelButton.Location = new System.Drawing.Point(798, 390);
            this._canelButton.Name = "_canelButton";
            this._canelButton.Size = new System.Drawing.Size(89, 29);
            this._canelButton.TabIndex = 1;
            this._canelButton.Text = "Canel";
            this._canelButton.UseVisualStyleBackColor = true;
            this._canelButton.Click += new System.EventHandler(this._canelButton_Click);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(15, 390);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(597, 20);
            this.hScrollBar1.TabIndex = 2;
            // 
            // CloudsPanelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 431);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this._canelButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._splitContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "CloudsPanelForm";
            this.Padding = new System.Windows.Forms.Padding(12, 13, 12, 13);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CloudsPanelForm";
            this._splitContainer.Panel2.ResumeLayout(false);
            this._splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _splitContainer;
        private System.Windows.Forms.PropertyGrid _propertyGrid;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _canelButton;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}