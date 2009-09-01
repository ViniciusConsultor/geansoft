using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Module.Chess.DemoApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TreeNode treenode = new TreeNode();
            treenode.Nodes.Add("text");
        }
    }
}
