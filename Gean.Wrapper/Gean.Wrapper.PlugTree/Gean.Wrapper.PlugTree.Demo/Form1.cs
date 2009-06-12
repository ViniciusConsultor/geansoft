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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DemoMethod();
        }

        private void DemoMethod()
        {
            string[] files = Directory.GetFiles(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                "Test*.dll", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                Runner runner = new Runner(file);
                IRun i = runner.GetRunObject();
                i.Run();
            }

        }
    }
}
