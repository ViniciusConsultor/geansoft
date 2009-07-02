namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    partial class AttributeDialog
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
            this._groupBox = new System.Windows.Forms.GroupBox();
            this._attributeValueTextbox = new System.Windows.Forms.TextBox();
            this._attributeKeyLabel = new System.Windows.Forms.Label();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._clearButton = new System.Windows.Forms.Button();
            this._groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _groupBox
            // 
            this._groupBox.Controls.Add(this._attributeValueTextbox);
            this._groupBox.Controls.Add(this._attributeKeyLabel);
            this._groupBox.Location = new System.Drawing.Point(12, 10);
            this._groupBox.Name = "_groupBox";
            this._groupBox.Size = new System.Drawing.Size(365, 112);
            this._groupBox.TabIndex = 0;
            this._groupBox.TabStop = false;
            // 
            // _attributeValueTextbox
            // 
            this._attributeValueTextbox.Location = new System.Drawing.Point(16, 33);
            this._attributeValueTextbox.Multiline = true;
            this._attributeValueTextbox.Name = "_attributeValueTextbox";
            this._attributeValueTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._attributeValueTextbox.Size = new System.Drawing.Size(336, 69);
            this._attributeValueTextbox.TabIndex = 0;
            // 
            // _attributeKeyLabel
            // 
            this._attributeKeyLabel.AutoSize = true;
            this._attributeKeyLabel.Location = new System.Drawing.Point(15, 18);
            this._attributeKeyLabel.Name = "_attributeKeyLabel";
            this._attributeKeyLabel.Size = new System.Drawing.Size(68, 13);
            this._attributeKeyLabel.TabIndex = 0;
            this._attributeKeyLabel.Text = "AttributeKey";
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(221, 130);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 0;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(302, 130);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _clearButton
            // 
            this._clearButton.Location = new System.Drawing.Point(12, 130);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(75, 23);
            this._clearButton.TabIndex = 2;
            this._clearButton.Text = "Clear";
            this._clearButton.UseVisualStyleBackColor = true;
            this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
            // 
            // AttributeDialog
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(389, 166);
            this.ControlBox = false;
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._clearButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this._groupBox);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "AttributeDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AttributeDialog";
            this._groupBox.ResumeLayout(false);
            this._groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBox;
        private System.Windows.Forms.TextBox _attributeValueTextbox;
        private System.Windows.Forms.Label _attributeKeyLabel;
        private System.Windows.Forms.Button _okButton;
        private System.Windows.Forms.Button _cancelButton;
        private System.Windows.Forms.Button _clearButton;
    }
}