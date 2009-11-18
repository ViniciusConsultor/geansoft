using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gean.Client.SimpTools
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            InitializeIcon();
        }

        private void InitializeIcon()
        {
            _mainNotifyIcon.Icon = new Icon("TheIcon.ico");
            _mainNotifyIcon.Text = "Gean SimpTools.";
        }

        protected override void OnActivated(EventArgs e)
        {
            this.Hide();
        } 

    }
}
