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
            string[] files = Directory.GetFiles(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                "*.gplug", SearchOption.TopDirectoryOnly);

            this._MainMessageTextbox.Items.AddRange(files);

            foreach (string file in files)
            {
                PlugFile plugfile = new PlugFile(file);
                IRun i = plugfile.Runner.GetRunObject();
                i.Run();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region IWorkBench 成员

        public string MessageBench
        {
            get
            {
                return this._MainMessageTextbox.Text;
            }
            set
            {
                this._MainMessageTextbox.Items.Add(value);
            }
        }

        #endregion

        private void _BeginDemoButton_Click(object sender, EventArgs e)
        {
            this.DemoMethod();
        }

        private void _ClearButton_Click(object sender, EventArgs e)
        {
            this._MainMessageTextbox.Items.Clear();
            this.Update();
        }
    }
}
