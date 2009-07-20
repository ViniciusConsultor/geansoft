using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.UI.ChessboardControl.DemoApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ChessboardControl chessCtr = new ChessboardControl();
            chessCtr.Dock = DockStyle.Fill;
            this._splitContainer.Panel2.Controls.Add(chessCtr);
        }
    }
}
