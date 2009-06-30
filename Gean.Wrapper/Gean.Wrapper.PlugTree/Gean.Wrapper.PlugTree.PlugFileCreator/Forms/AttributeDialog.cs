using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    public partial class AttributeDialog : Form
    {
        public AttributeDialog(string key, string value)
        {
            InitializeComponent();
            this.Key = key;
            this.Value = value;
            this._attributeKeyLabel.Text = key;
            this._attributeValueTextbox.Text = value;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }

        private void _okButton_Click(object sender, EventArgs e)
        {
            this.Value = this._attributeValueTextbox.Text;
            this.Close();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this.Value = string.Empty;
            this._attributeValueTextbox.Text = string.Empty;
        }
    }
}
