using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    public partial class _mainForm : Form
    {
        public _mainForm()
        {
            InitializeComponent();
            TabelLayout();
        }

        private void _newToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "*.gplug";
            sfd.Filter = "插件文件(*.gplug)|*.gplug";
            sfd.OverwritePrompt = true;
            sfd.ShowHelp = false;
            sfd.Title = "New a GPlug File";
            switch (sfd.ShowDialog())
            {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    this.CreateNewPlugFile(sfd.FileName);
                    break;
                #region
                case DialogResult.Abort:
                case DialogResult.Ignore:
                case DialogResult.No:
                case DialogResult.None:
                case DialogResult.Retry:
                case DialogResult.Yes:
                default:
                    break;
                #endregion
            }
        }
        private void _openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.gplug";
            ofd.Filter = "插件文件(*.gplug)|*.gplug";
            ofd.Multiselect = false;
            ofd.ShowHelp = false;
            ofd.Title = "Open a GPlug File";
            switch (ofd.ShowDialog())
            {
                case DialogResult.Cancel:
                    break;
                case DialogResult.OK:
                    if (File.Exists(ofd.FileName))
                    {
                        MessageBox.Show(ofd.FileName);
                    }
                    else
                    {

                    }
                    break;
                #region
                case DialogResult.Abort:
                case DialogResult.Ignore:
                case DialogResult.No:
                case DialogResult.None:
                case DialogResult.Retry:
                case DialogResult.Yes:
                default:
                    break;
                #endregion
            }
        }
        private void _saveToolStripButton_Click(object sender, EventArgs e)
        {

        }


        private void TabelLayout()
        {
            this.SuspendLayout();
            #region Plug的公共的一些属性的Table
            foreach (RowStyle row in this._commonTable.RowStyles)
            {
                row.Height = 30;
            }
            foreach (Control ctr in this._commonTable.Controls)
            {
                if (ctr is Label)
                    ctr.Anchor = AnchorStyles.Right;
                if (ctr is TextBoxBase)
                    ctr.Anchor = AnchorStyles.Left;
            }
            #endregion
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        XmlDocument _plugDocument = new XmlDocument();

        private void CreateNewPlugFile(string file)
        {
            File.Delete(file);
            using (FileStream fs = File.Create(file))
            {
                XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);
                xtw.Formatting = Formatting.Indented;
                xtw.WriteStartDocument();
                {
                    xtw.WriteStartElement("DocumentElement");
                    {
                        xtw.WriteStartElement("Runtime");
                        xtw.WriteEndElement();
                    }
                    {
                        xtw.WriteStartElement("Plug");
                        xtw.WriteEndElement();
                    }
                    xtw.WriteEndElement();
                }
                xtw.Flush();
                fs.Flush();
                fs.Close();
                fs.Dispose();
            }
            _plugDocument.Load(file);
        }


    }
}
