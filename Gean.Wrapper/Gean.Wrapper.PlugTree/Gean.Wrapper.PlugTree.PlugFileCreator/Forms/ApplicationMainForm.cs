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
    public partial class ApplicationMainForm : Form
    {
        public ApplicationMainForm()
        {
            InitializeComponent();

            this.InformationTree.MouseClick += new MouseEventHandler(TreeView_MouseDown);
            this.ProducerTree.MouseClick += new MouseEventHandler(TreeView_MouseDown);
            this.ConditionTree.MouseClick += new MouseEventHandler(TreeView_MouseDown);
            this.PathTree.MouseClick += new MouseEventHandler(TreeView_MouseDown);

            TabelLayout();
        }

        public TreeView ProducerTree;
        public TreeView ConditionTree;
        public TreeView PathTree;
        public TreeView InformationTree;

        private void 完整新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitProject();

            PlugInformationDialog dialog = new PlugInformationDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this._fileLabel.Text = dialog.FileName;
                this.InformationTree.Nodes.Add(new DocumentElementTreeNode(CoreService.PlugDocument.DocumentElement));
            }
        }

        private void InitProject()
        {
            this.ConditionTree.Nodes.Clear();
            this.InformationTree.Nodes.Clear();
            this.PathTree.Nodes.Clear();
            this.ProducerTree.Nodes.Clear();
        }

        private void TreeView_MouseDown(object sender, MouseEventArgs e)
        {
            TreeView tv = (TreeView)sender;
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = tv.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    tv.SelectedNode = tn;
                }
            }
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
                CoreService.PlugDocument.Load(ofd.FileName);
                this.InitProject();
                this.InformationTree.Nodes.Add(
                    new DocumentElementTreeNode(CoreService.PlugDocument.DocumentElement));
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
