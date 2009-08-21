using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Gean.Library.UI.Controls
{
    /// <summary>
    /// 文件选择器控件
    /// </summary>
    public class FileSelector : UserControl
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public FileSelector()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        /// <summary>
        /// 当文件被选择后发生的事件
        /// </summary>
        public event EventHandler FileSelected;
        protected void OnFileSelected(EventArgs e)
        {
            if (FileSelected != null)
            {
                FileSelected(this, e);
            }
        }

        private Button _FileSelectorButton;
        private Control _FileNamesControl;

        #region 属性

        /// <summary>
        /// 获取或设置控件的显示样式
        /// </summary>
        public FileSelectControlStyle Style
        {
            get { return this._Style; }
            set
            {
                this._Style = value;
                this.SetViewBoxStyle();
            }
        }
        private FileSelectControlStyle _Style = FileSelectControlStyle.TextBoxAndTextButton;

        /// <summary>
        /// 设置需打开的文件类型过滤器
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 返回的单个文件文件名
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 返回的一组文件文件名
        /// </summary>
        public string[] FileNames { get; private set; }

        /// <summary>
        /// 获取或设置打开文件对话框是否可以多选文件
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// 获取或设置打开文件对话框的初始目录
        /// </summary>
        public string InitialDirectory { get; set; }

        /// <summary>
        /// 获取或设置打开文件对话框的标题
        /// </summary>
        public string DialogTitle { get; set; }

        /// <summary>
        /// 获取或设置ViewBox的宽度
        /// </summary>
        public int ViewBoxWidth
        {
            get { return this._FileNamesControl.Width; }
            set
            {
                if (this._FileNamesControl == null)
                {
                    this._FileNamesControl.Width = value;
                    this._FileSelectorButton.Location = new Point(this._FileNamesControl.Width + 2, 0);
                    this.SetThisSize();
                }
            }
        }

        /// <summary>
        /// 获取或设置文字Button上显示的文字
        /// </summary>
        public string ButtonText
        {
            get { return this._FileSelectorButton.Text; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = "&Browser";
                }
                this._FileSelectorButton.Text = value;
                this._FileSelectorButton.Width =
                    (int)(this.CreateGraphics().MeasureString(value, this.Font).Width) + 20;
                this.SetThisSize();
            }
        }

        /// <summary>
        /// 获取或设置Button上显示的图标
        /// </summary>
        public Image ButtonImage
        {
            get { return this._FileSelectorButton.Image; }
            set
            {
                if (value != null)
                {
                    this._FileSelectorButton.Text = string.Empty;
                    this._FileSelectorButton.Image = value;
                    this._FileSelectorButton.Width = (int)(value.Width) + 20;
                    this.SetThisSize();
                }
            }
        }

        /// <summary>
        /// 获取或设置文件选择的历史记录
        /// </summary>
        public string[] SelectHistory { get; set; }

        /// <summary>
        /// 是否保存历史记录
        /// </summary>
        public bool IsSaveHistory { get; set; }

        /// <summary>
        /// 是否是选择文件夹
        /// </summary>
        public bool IsSelectFolder { get; set; }

        /// <summary>
        /// 控件是否只读
        /// </summary>
        public bool ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                this._ReadOnly = value;
                if (this._FileNamesControl is TextBox)
                {
                    ((TextBox)this._FileNamesControl).ReadOnly = value;
                }
            }
        }
        private bool _ReadOnly = false;

        #endregion

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuspendLayout();

            this._Style = FileSelectControlStyle.TextBoxAndTextButton;
            this.SetViewBoxStyle();

            this.InitialDirectory = string.Empty;
            this.DialogTitle = string.Empty;
            this.FileName = this.InitialDirectory;
            this.FileNames = null;
            this.ButtonImage = null;

            this._FileNamesControl.Location = new Point(0, 1);
            this._FileNamesControl.Size = new Size(150, 23);
            this.Controls.Add(this._FileNamesControl);

            this._FileSelectorButton = new Button();
            this._FileSelectorButton.Location = new Point(this._FileNamesControl.Width + 2, 0);
            this._FileSelectorButton.Size = new Size(23, 23);
            this._FileSelectorButton.Click += new EventHandler(_FileSelectorButton_Click);
            this.Controls.Add(this._FileSelectorButton);

            this.SetThisSize();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// 设置控件本身的宽度
        /// </summary>
        private void SetThisSize()
        {
            int width = this._FileNamesControl.Width + 2 + this._FileSelectorButton.Width;
            this.Size = new Size(width, 23);
        }

        /// <summary>
        /// 设置控件中的ViewBoxStyle
        /// </summary>
        private void SetViewBoxStyle()
        {
            switch (this._Style) //根据样式控制Box控件是TextBox还是CommoBox
            {
                #region 根据控件的样式设定控件的显示

                case FileSelectControlStyle.None:
                case FileSelectControlStyle.TextBoxAndTextButton:
                case FileSelectControlStyle.TextBoxAndImageButton:
                case FileSelectControlStyle.TextBoxAndTextImageButton:
                    #region
                    {
                        this._FileNamesControl = new TextBox();
                        this._FileNamesControl.Name = "View_TextBox";
                        break;
                    }
                    #endregion
                case FileSelectControlStyle.ComboBoxAndTextButton:
                case FileSelectControlStyle.ComboBoxAndImageButton:
                case FileSelectControlStyle.ComboBoxAndTextImageButton:
                    #region
                    {
                        this._FileNamesControl = new ComboBox();
                        this._FileNamesControl.Name = "View_ComboBox";
                        break;
                    }
                    #endregion
                case FileSelectControlStyle.OnlyTextButton:
                case FileSelectControlStyle.OnlyImageButton:
                case FileSelectControlStyle.OnlyTextImageButton:
                default:
                    #region
                    {
                        this._FileNamesControl = null;
                        break;
                    }
                    #endregion

                #endregion
            }
        }

        private void _FileSelectorButton_Click(object sender, EventArgs e)
        {
            if (this.IsSelectFolder)//选择目录的方式
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.SelectedPath = this.Text;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.FileName = dialog.SelectedPath;
                    if (this.Controls.Contains(this._FileNamesControl))
                    {
                        this._FileNamesControl.Text = this.FileName;
                    }
                    OnFileSelected(EventArgs.Empty);
                }
            }
            else//选择文件的方式
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = this.MultiSelect;
                dialog.Title = this.DialogTitle;
                dialog.InitialDirectory = this.InitialDirectory;
                dialog.Filter = this.Filter;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (this.MultiSelect)//判断是否可以多选
                    {
                        this.FileNames = dialog.FileNames;
                        if (this._FileNamesControl != null)
                        {
                            if (this.FileNames.Length > 1)
                            {
                                this._FileNamesControl.Text = this.FileNames[0] + ", ...";
                            }
                            else
                            {
                                this._FileNamesControl.Text = this.FileNames[0];
                            }
                        }
                    }
                    else
                    {
                        this.FileName = dialog.FileName;
                        if (this._FileNamesControl != null)
                        {
                            if (this._FileNamesControl is TextBox)
                            {
                                ((TextBox)this._FileNamesControl).Text = this.FileName;
                            }
                        }
                    }
                    OnFileSelected(EventArgs.Empty);
                }
            }
            base.OnClick(e);
        }

    }
    /// <summary>
    /// 文件选择控件样式枚举
    /// </summary>
    public enum FileSelectControlStyle
    {
        None = 0,
        /// <summary>
        /// 文件名显示在TextBox中, 按钮上显示文字
        /// </summary>
        TextBoxAndTextButton = 1,
        /// <summary>
        /// 文件名显示在TextBox中, 按钮上显示图像
        /// </summary>
        TextBoxAndImageButton = 2,
        /// <summary>
        /// 文件名显示在TextBox中, 按钮上显示文字与图像
        /// </summary>
        TextBoxAndTextImageButton = 3,
        /// <summary>
        /// 文件名显示在ComboBox中, 按钮上显示文字
        /// </summary>
        ComboBoxAndTextButton = 4,
        /// <summary>
        /// 文件名显示在ComboBox中, 按钮上显示图像
        /// </summary>
        ComboBoxAndImageButton = 5,
        /// <summary>
        /// 文件名显示在ComboBox中, 按钮上显示文字与图像
        /// </summary>
        ComboBoxAndTextImageButton = 6,
        /// <summary>
        /// 仅按钮, 按钮上显示文字
        /// </summary>
        OnlyTextButton = 7,
        /// <summary>
        /// 仅按钮, 按钮上显示图像
        /// </summary>
        OnlyImageButton = 8,
        /// <summary>
        /// 仅按钮, 按钮上显示文字与图像
        /// </summary>
        OnlyTextImageButton = 9
    }
}