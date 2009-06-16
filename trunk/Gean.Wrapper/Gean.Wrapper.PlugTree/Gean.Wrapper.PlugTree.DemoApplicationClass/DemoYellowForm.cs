using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.DemoClass1
{
    public partial class DemoYellowForm : Form
    {
        public DemoYellowForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = Guid.NewGuid().ToString();
        }
    }
}
