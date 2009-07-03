using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    public partial class PlugInformationDialog : Form
    {
        public PlugInformationDialog()
        {
            InitializeComponent();
        }
        public PlugInformationDialog(XmlElement element)
        {
            InitializeComponent();

            this.FileName = CoreService.PlugFile;

            this._authorTextbox.Text = this.GetAttribute(element, "author");
            this._copyrightTextbox.Text = this.GetAttribute(element, "copyright");
            this._descriptionTextbox.Text = this.GetAttribute(element, "description");
            this._emailTextbox.Text = this.GetAttribute(element, "email");
            this._homesiteTextbox.Text = this.GetAttribute(element, "homesite");
            this._namespaceTextbox.Text = this.GetAttribute(element, "namespace");
            this._parentAsseTextbox.Text = this.GetAttribute(element, "parentAssembly");
            this._versionTextbox.Text = this.GetAttribute(element, "version");

            this._plugPlaceTextbox.Text = CoreService.PlugFile;
        }

        private string GetAttribute(XmlElement element, string attributeName)
        {
            string value = element.GetAttribute(attributeName);
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }
            return " ";
        }

        public string FileName { get; private set; }

        private void _plugPlaceButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择一个目录，在这个目录将存放这个Plug的描述文件及这个Plug的VS项目。";
            dialog.ShowNewFolderButton = true;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this._plugPlaceTextbox.Text = dialog.SelectedPath;
                this.FileName = dialog.SelectedPath;
            }
        }

        private void _parentAsseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "插件文件(*.gplug)|*.gplug";
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                this._parentAsseTextbox.Text = dialog.FileName;
            }
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            if (VerifyInput())
            {
                if (CoreService.PlugDocument.DocumentElement == null)
                {
                    string file = Path.Combine(this._plugPlaceTextbox.Text, this._namespaceTextbox.Text + ".gplug");
                    this.CreateNewPlugFile(file);
                    MessageBox.Show(this, file + " has already Created!", "Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    CoreService.PlugDocument.Save(CoreService.PlugFile);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private bool VerifyInput()
        {
            if (string.IsNullOrEmpty(this._parentAsseTextbox.Text))
            {
                if (MessageBox.Show
                    (this,
                    "确认没有父级插件？",
                    "确认",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) != DialogResult.OK)
                {
                    this._parentAsseTextbox.BackColor = Color.Yellow;
                    this._parentAsseTextbox.TextChanged += new EventHandler(Textbox_TextChanged);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(this._namespaceTextbox.Text))
            {
                this._namespaceTextbox.BackColor = Color.Yellow;
                this._namespaceTextbox.TextChanged += new EventHandler(Textbox_TextChanged);
                return false;
            }
            if (string.IsNullOrEmpty(this._versionTextbox.Text))
            {
                this._versionTextbox.BackColor = Color.Yellow;
                this._versionTextbox.TextChanged += new EventHandler(Textbox_TextChanged);
                return false;
            }
            if (string.IsNullOrEmpty(this._plugPlaceTextbox.Text))
            {
                this._plugPlaceTextbox.BackColor = Color.Yellow;
                this._plugPlaceTextbox.TextChanged += new EventHandler(Textbox_TextChanged);
                return false;
            }
            return true;
        }

        void Textbox_TextChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = SystemColors.Window;
        }

        private void CreateNewPlugFile(string file)
        {
            //File.Delete(file);
            using (FileStream fs = File.Create(file))
            {
                XmlTextWriter xtw = new XmlTextWriter(fs, Encoding.UTF8);
                xtw.Formatting = Formatting.Indented;
                xtw.WriteStartDocument();
                {
                    xtw.WriteStartElement("Root");
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
            CoreService.PlugDocument.Load(file);
            CoreService.PlugFile = file;
            if (!string.IsNullOrEmpty(this._parentAsseTextbox.Text))
            {
                string pastr = (new FileInfo(this._parentAsseTextbox.Text)).Name;
                CoreService.PlugDocument.DocumentElement.SetAttribute("parentAssembly", pastr);
            }
            if (!string.IsNullOrEmpty(this._namespaceTextbox.Text))
                CoreService.PlugDocument.DocumentElement.SetAttribute("namespace", this._namespaceTextbox.Text);
            if (!string.IsNullOrEmpty(this._versionTextbox.Text))
                CoreService.PlugDocument.DocumentElement.SetAttribute("version", this._versionTextbox.Text);
            if (!string.IsNullOrEmpty(this._copyrightTextbox.Text))
                CoreService.PlugDocument.DocumentElement.SetAttribute("copyright", this._copyrightTextbox.Text);
            if (!string.IsNullOrEmpty(this._authorTextbox.Text))
                CoreService.PlugDocument.DocumentElement.SetAttribute("author", this._authorTextbox.Text);
            if (!string.IsNullOrEmpty(this._homesiteTextbox.Text))
                CoreService.PlugDocument.DocumentElement.SetAttribute("homesite", this._homesiteTextbox.Text);
            if (!string.IsNullOrEmpty(this._emailTextbox.Text))
                CoreService.PlugDocument.DocumentElement.SetAttribute("email", this._emailTextbox.Text);
            if (!string.IsNullOrEmpty(this._descriptionTextbox.Text))
                CoreService.PlugDocument.DocumentElement.SetAttribute("description", this._descriptionTextbox.Text);
            CoreService.PlugDocument.Save(file);
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (item is TextBoxBase)
                {
                    ((TextBoxBase)item).Text = string.Empty;
                }
            }
            foreach (Control item in this.groupBox1.Controls)
            {
                if (item is TextBoxBase)
                {
                    ((TextBoxBase)item).Text = string.Empty;
                }
            }
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
