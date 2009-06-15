using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Gean.Wrapper.PlugTree.Demo
{
    public partial class Form1 : Form, IWorkBench
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DemoMethod()
        {
            StartupService.Initializes(Application.ProductName, AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
            this._MessageListbox.Items.AddRange(StartupService.PlugFiles);
            foreach (var item in StartupService.Runners)
            {
                this._MessageListbox.Items.Add(item.ToString());
            }
            foreach (var item in StartupService.PlugPath)
            {
                this._MessageListbox.Items.Add(item.ToString());
            }
        }

        private void _BeginDemoButton_Click(object sender, EventArgs e)
        {
            this.DemoMethod();
            this.Text = DateTime.Now.ToLongTimeString() + "  Demo completed!";
            MessageBox.Show("Test");
            StartupService.Runners.GetIRunObject("Gean.Wrapper.PlugTree.ApplicationClass.DemoYellowFormRunner").Run();
        }

        private void _ClearButton_Click(object sender, EventArgs e)
        {
            this._MessageListbox.Items.Clear();
            this.Update();
        }

        private void _CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region IWorkBench 成员

        public string MessageBench
        {
            get
            {
                return this._MessageListbox.Text;
            }
            set
            {
                this._MessageListbox.Items.Add(value);
            }
        }

        #endregion

    }
}
