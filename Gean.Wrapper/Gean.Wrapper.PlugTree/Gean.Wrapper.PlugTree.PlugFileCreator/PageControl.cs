using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    public partial class Page : TabPage
    {
        public Page()
        {
            Initialization();
        }

        private void Initialization()
        {
            Button _saveButton = new Button();
            Button _clearButton = new Button();
            _clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            _clearButton.Location = new Point(182, 317);
            _clearButton.Name = "_clearButton";
            _clearButton.Size = new System.Drawing.Size(64, 23);
            _clearButton.TabIndex = 1;
            _clearButton.Text = "Clear";
            _clearButton.UseVisualStyleBackColor = true;
            // 
            // _saveButton
            // 
            _saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            _saveButton.Location = new Point(112, 317);
            _saveButton.Name = "_saveButton";
            _saveButton.Size = new Size(64, 23);
            _saveButton.TabIndex = 2;
            _saveButton.Text = "Save";
            _saveButton.UseVisualStyleBackColor = true;

        }
    }
}
