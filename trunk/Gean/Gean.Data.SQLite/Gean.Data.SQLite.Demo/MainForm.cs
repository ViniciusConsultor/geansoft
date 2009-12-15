using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using Gean.LinqToData;

namespace Gean.Data.SQLite.Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void _canelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            //_resultListBox.Items.Add(UtilityNet.GetSubnet().ToString());
            //_resultListBox.Items.Add(UtilityNet.GetDefaultIPGateway().ToString());
            //_resultListBox.Items.Add(UtilityNet.NetPing("www.nsimple.cn").ToString());

            _textBox.Text = this.ConnString();

            //this.LinqToData();
        }

        private void LinqToData()
        {
            Database db = Database.New(ConnString());
        }

        private string ConnString()
        {
            SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();
            connBuilder.DataSource = "localhost";
            connBuilder.InitialCatalog = "mydemo_db";
            connBuilder.Password = "sa";
            connBuilder.UserID = "sa";

            return connBuilder.ConnectionString;
        }


    }
}
