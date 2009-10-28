using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Gean.Library.UI.Controls
{
    /// <summary>
    /// �ļ�ѡ�����ؼ�
    /// </summary>
    public class FileSelector : UserControl
    {
        #region ���캯��

        /// <summary>
        /// ���캯��
        /// </summary>
        public FileSelector()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ������������ʹ�õ���Դ��
        /// </summary>
        /// <param name="disposing">���Ӧ�ͷ��й���Դ��Ϊ true������Ϊ false��</param>
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
        /// ���ļ���ѡ��������¼�
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

        #region ����

        /// <summary>
        /// ��ȡ�����ÿؼ�����ʾ��ʽ
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
        /// ������򿪵��ļ����͹�����
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// ���صĵ����ļ��ļ���
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// ���ص�һ���ļ��ļ���
        /// </summary>
        public string[] FileNames { get; private set; }

        /// <summary>
        /// ��ȡ�����ô��ļ��Ի����Ƿ���Զ�ѡ�ļ�
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// ��ȡ�����ô��ļ��Ի���ĳ�ʼĿ¼
        /// </summary>
        public string InitialDirectory { get; set; }

        /// <summary>
        /// ��ȡ�����ô��ļ��Ի���ı���
        /// </summary>
        public string DialogTitle { get; set; }

        /// <summary>
        /// ��ȡ������ViewBox�Ŀ��
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
        /// ��ȡ����������Button����ʾ������
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
        /// ��ȡ������Button����ʾ��ͼ��
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
        /// ��ȡ�������ļ�ѡ�����ʷ��¼
        /// </summary>
        public string[] SelectHistory { get; set; }

        /// <summary>
        /// �Ƿ񱣴���ʷ��¼
        /// </summary>
        public bool IsSaveHistory { get; set; }

        /// <summary>
        /// �Ƿ���ѡ���ļ���
        /// </summary>
        public bool IsSelectFolder { get; set; }

        /// <summary>
        /// �ؼ��Ƿ�ֻ��
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
        /// ��ʼ���ؼ�
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
        /// ���ÿؼ�����Ŀ��
        /// </summary>
        private void SetThisSize()
        {
            int width = this._FileNamesControl.Width + 2 + this._FileSelectorButton.Width;
            this.Size = new Size(width, 23);
        }

        /// <summary>
        /// ���ÿؼ��е�ViewBoxStyle
        /// </summary>
        private void SetViewBoxStyle()
        {
            switch (this._Style) //������ʽ����Box�ؼ���TextBox����CommoBox
            {
                #region ���ݿؼ�����ʽ�趨�ؼ�����ʾ

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
            if (this.IsSelectFolder)//ѡ��Ŀ¼�ķ�ʽ
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
            else//ѡ���ļ��ķ�ʽ
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = this.MultiSelect;
                dialog.Title = this.DialogTitle;
                dialog.InitialDirectory = this.InitialDirectory;
                dialog.Filter = this.Filter;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (this.MultiSelect)//�ж��Ƿ���Զ�ѡ
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
    /// �ļ�ѡ��ؼ���ʽö��
    /// </summary>
    public enum FileSelectControlStyle
    {
        None = 0,
        /// <summary>
        /// �ļ�����ʾ��TextBox��, ��ť����ʾ����
        /// </summary>
        TextBoxAndTextButton = 1,
        /// <summary>
        /// �ļ�����ʾ��TextBox��, ��ť����ʾͼ��
        /// </summary>
        TextBoxAndImageButton = 2,
        /// <summary>
        /// �ļ�����ʾ��TextBox��, ��ť����ʾ������ͼ��
        /// </summary>
        TextBoxAndTextImageButton = 3,
        /// <summary>
        /// �ļ�����ʾ��ComboBox��, ��ť����ʾ����
        /// </summary>
        ComboBoxAndTextButton = 4,
        /// <summary>
        /// �ļ�����ʾ��ComboBox��, ��ť����ʾͼ��
        /// </summary>
        ComboBoxAndImageButton = 5,
        /// <summary>
        /// �ļ�����ʾ��ComboBox��, ��ť����ʾ������ͼ��
        /// </summary>
        ComboBoxAndTextImageButton = 6,
        /// <summary>
        /// ����ť, ��ť����ʾ����
        /// </summary>
        OnlyTextButton = 7,
        /// <summary>
        /// ����ť, ��ť����ʾͼ��
        /// </summary>
        OnlyImageButton = 8,
        /// <summary>
        /// ����ť, ��ť����ʾ������ͼ��
        /// </summary>
        OnlyTextImageButton = 9
    }
}