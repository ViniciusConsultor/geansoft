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

        private void 完整新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitProject();

            PlugInitDialog dialog = new PlugInitDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this._fileLabel.Text = dialog.FileName;
                this._runtimeTree.Nodes.Add(new DocumentElementTreeNode(MainService.PlugDocument.DocumentElement));
            }
        }

        private void InitProject()
        {
            this._runtimeTree.Nodes.Clear();
            this._pathTree.Nodes.Clear();
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.gplug";
            ofd.Filter = "插件文件(*.gplug)|*.gplug";
            ofd.Multiselect = false;
            ofd.ShowHelp = false;
            ofd.Title = "Open a GPlug File";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                this._fileLabel.Text = ofd.FileName;
                MainService.PlugDocument.Load(ofd.FileName);
                this.InitProject();
                this._runtimeTree.Nodes.Add(new DocumentElementTreeNode(MainService.PlugDocument.DocumentElement));
            }

        }

        private void TabelLayout()
        {
            this.SuspendLayout();
            #region Plug的公共的一些属性的Table
            //foreach (RowStyle row in this._commonTable.RowStyles)
            //{
            //    row.Height = 30;
            //}
            //foreach (Control ctr in this._commonTable.Controls)
            //{
            //    if (ctr is Label)
            //        ctr.Anchor = AnchorStyles.Right;
            //    if (ctr is TextBoxBase)
            //        ctr.Anchor = AnchorStyles.Left;
            //}
            #endregion
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void plugDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


    }
}
