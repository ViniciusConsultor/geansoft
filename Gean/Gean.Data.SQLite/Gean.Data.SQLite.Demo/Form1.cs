using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Gean.Data.SQLite.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SQLiteConnection conn = SQLiteHelper.GetSQLiteConnection(@"SQLiteLite.gdb");
        }
    }
}
