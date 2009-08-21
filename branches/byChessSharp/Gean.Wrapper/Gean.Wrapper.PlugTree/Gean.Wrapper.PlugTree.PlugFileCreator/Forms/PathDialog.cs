using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    public partial class PathDialog : Form
    {
        public PathDialog()
        {
            InitializeComponent();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
