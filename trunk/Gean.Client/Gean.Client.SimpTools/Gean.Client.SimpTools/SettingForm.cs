using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

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
            _mainNotifyIcon.Icon = new Icon("Resources\\TheIcon.ico");
            _mainNotifyIcon.Text = "Gean SimpTools.";
            RightMenu menu = new RightMenu();
            _mainNotifyIcon.ContextMenuStrip = menu;
        }

        protected override void OnActivated(EventArgs e)
        {
            this.Hide();
        }

        private XmlDocument _settingDoc = new XmlDocument();
        private MenuStrip _rightMenu = new MenuStrip();

        private void RightMenuBuilder()
        { 
            
        }

    }
}
