using System;
using System.Xml;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Gean.Client.Log4NetEditor
{
	/// <summary>
	/// frmAppender ªººK­n´y­z¡C
	/// </summary>
	public class frmAppender : Form
	{
		private bool bSkipScrollChangeEvent;
		private const string msCONST_DefaultAppender = "OutputDebugStringAppender";
		private const int mnCONST_SpacingInControls = 5;
		private AppenderConsulter moConsulter;
		private XmlDocument moXmlDoc;
		private string msAliasName;

		#region Form Controls
		private Button btnCancel;
		private Button btnSave;
		private IContainer components;
		private ComboBox ddlAppenderClasses;
		private GroupBox gpArgContainer;
		private Label lblAlias;
		private Label lblAppenderClass;
		private Label lblDesc;
		private Panel argPanel;
		private ToolTip toolTip;
		private TextBox txtAlias;
		private TextBox txtDesc;
		private System.Windows.Forms.GroupBox gbLayoutGroupBox;
		private System.Windows.Forms.Label lblLayoutType;
		private System.Windows.Forms.ComboBox ddlLayoutType;
		private System.Windows.Forms.TextBox txtConversionPattern;
		private System.Windows.Forms.Label lblCnsnPtn;
		private System.Windows.Forms.Label lblPtnPreview;
		private System.Windows.Forms.Label lblPreviewResult;
		private System.Windows.Forms.Label lblDemoString;
		private System.Windows.Forms.TextBox txtDemoString;
		private System.Windows.Forms.Button btnPatternHelp;
		private VScrollBar vSrlBar4grp;
		#endregion

		public XmlDocument Current_Log4net_config_XmlDoc
		{
			get { return moXmlDoc; }
		}
		public frmAppender(XmlDocument log4net_config_XmlDoc)
		{
			bSkipScrollChangeEvent = true;
			moXmlDoc = log4net_config_XmlDoc;
			InitializeComponent();
			InitAppenderDropDownList();
		}

		private void ArrangeControls()
		{
			int nBottom = 0;
			int nTop = -vSrlBar4grp.Value;
			int nLeftSpace = (int) (argPanel.Width * 0.1);
			if (argPanel.Controls.Count > 0)
			{
				foreach (Control ArgControl in argPanel.Controls)
				{
					if (gbLayoutGroupBox != ArgControl)
					{
						ArgControl.Visible = true;
						ArgControl.Width = (int) (argPanel.Width * 0.8);
						ArgControl.Left = nLeftSpace;
					}
					ArgControl.Top = nTop;
					nTop += ArgControl.Height + 5;
				}
			}
			nBottom = nTop + vSrlBar4grp.Value;
			if (nBottom > argPanel.Height)
			{
				vSrlBar4grp.Visible = true;
				vSrlBar4grp.Maximum = (nBottom - argPanel.Height) + 5;
			}
			else
			{
				vSrlBar4grp.Visible = false;
				vSrlBar4grp.Value = 0;
				vSrlBar4grp.Maximum = 0;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			base.Close();
			base.Dispose();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			XmlNode oCurrentAppender = null;
			msAliasName = txtAlias.Text;

			#region Update arguments
			foreach (Control control1 in argPanel.Controls)
			{
				if (!(control1 is Label))
				{
					if (control1 is TextBox)
					{
						moConsulter.SearchUpdateArg((string) control1.Tag, ((TextBox) control1).Text);
					}
					else if (control1 is ComboBox)
					{
						moConsulter.SearchUpdateArg((string) control1.Tag, ((ComboBox) control1).Text);
					}
					else if (control1 is ParameterGrid)
					{
						moConsulter.SearchUpdateArg((string) control1.Tag, string.Empty).ParameterXml = ((ParameterGrid)control1).ParameterXmlNodes;
					}
				}
			}
			#endregion

			#region Update Layout & pattern
			moConsulter.SearchUpdateArg("layout", ddlLayoutType.Text);
			moConsulter.SearchUpdateArg("conversionpattern", txtConversionPattern.Text);
			#endregion

			#region Update & Save log4net.config for this appender
			foreach (XmlNode oAppender in moXmlDoc.SelectNodes("//appender"))
			{
				if (oAppender.Attributes["name"].Value == msAliasName)
				{
					oCurrentAppender = oAppender;
					break;
				}
			}
			try
			{
				if (oCurrentAppender == null)
				{
					oCurrentAppender = moXmlDoc.CreateElement("appender");
					XmlAttribute oAttri = moXmlDoc.CreateAttribute("name");
					oAttri.Value = msAliasName;
					oCurrentAppender.Attributes.Append(oAttri);
					oAttri = moXmlDoc.CreateAttribute("type");
					oAttri.Value = "log4net.Appender." + ddlAppenderClasses.Text;
					oCurrentAppender.Attributes.Append(oAttri);
					oCurrentAppender.InnerXml = moConsulter.GetConfigXML();
					moXmlDoc.DocumentElement.InsertBefore(oCurrentAppender, moXmlDoc.DocumentElement.FirstChild);
				}
				else
				{
					oCurrentAppender.Attributes["type"].Value = "log4net.Appender." + ddlAppenderClasses.Text;
					oCurrentAppender.InnerXml = moConsulter.GetConfigXML();
				}
			}
			catch (Exception oEX)
			{
				MessageBox.Show(this, oEX.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			#endregion
			DialogResult = DialogResult.OK;

			base.Close();
		}

		private void ddlAppenderClasses_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bSkipScrollChangeEvent)
			{
				bSkipScrollChangeEvent = false;
			}
			else
			{
				moConsulter = null;
				InitGroupArgument();
			}
		}

		public void EditExistedAppender(string sAlias)
		{
			XmlNode node1 = null;
			msAliasName = sAlias;
			foreach (XmlNode node2 in moXmlDoc.SelectNodes("//appender"))
			{
				if (node2.Attributes["name"].Value == msAliasName)
				{
					node1 = node2;
					break;
				}
			}
			if (node1 == null)
			{
				throw new ApplicationException("This Appender named '" + msAliasName + "' is not existed in this log4net config file.");
			}
			txtAlias.Text = msAliasName;
			txtAlias.ReadOnly = true;
			try
			{
				string[] textArray1 = node1.Attributes["type"].Value.Split(new char[] { '.' });
				bSkipScrollChangeEvent = true;
				ddlAppenderClasses.Text = textArray1[textArray1.Length - 1];
				moConsulter = AppenderConsulter.GetAppender(textArray1[textArray1.Length - 1]);
				if (null == moConsulter)
				{
					moConsulter = AppenderConsulter.GetAppender(msCONST_DefaultAppender);
				}
				moConsulter.RestoreArgsFromXml(node1);
			}
			catch (InvalidCastException)
			{
				moConsulter = null;
			}
			InitGroupArgument();
		}

		private void InitAppenderDropDownList()
		{
			ddlAppenderClasses.DataSource = Helper.GetAppenders();
		}

		private void InitGroupArgument()
		{
			if (moConsulter == null)
			{
				try
				{
					moConsulter = AppenderConsulter.GetAppender(ddlAppenderClasses.Text);
				}
				catch (InvalidCastException)
				{
					moConsulter = null;
				}
			}
			argPanel.Controls.Clear();
			vSrlBar4grp.Value = 0;
			if (moConsulter == null)
			{
				txtDesc.Text = "Default basic consulter can't work.";
				btnSave.Enabled = false;
			}
			else
			{
				txtDesc.Text = moConsulter.GetAppenderDesc();
			}
			btnSave.Enabled = true;
			ArgumentStruct[] structArray1 = moConsulter.Arguments;
			if (structArray1 != null)
			{
				foreach (ArgumentStruct struct1 in structArray1)
				{
					RecursiveGenerateControls(struct1);
				}
			}
			if (Constants.msCONST_NOLAYOUT_APPENDER.ToLower() != ddlAppenderClasses.Text.ToLower())
			{
				argPanel.Controls.Add(gbLayoutGroupBox);
			}
			ArrangeControls();
		}


		#region Code generated by Visual Studio 2003
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.lblAlias = new System.Windows.Forms.Label();
            this.lblAppenderClass = new System.Windows.Forms.Label();
            this.ddlAppenderClasses = new System.Windows.Forms.ComboBox();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPatternHelp = new System.Windows.Forms.Button();
            this.vSrlBar4grp = new System.Windows.Forms.VScrollBar();
            this.gpArgContainer = new System.Windows.Forms.GroupBox();
            this.argPanel = new System.Windows.Forms.Panel();
            this.gbLayoutGroupBox = new System.Windows.Forms.GroupBox();
            this.txtDemoString = new System.Windows.Forms.TextBox();
            this.lblDemoString = new System.Windows.Forms.Label();
            this.lblPreviewResult = new System.Windows.Forms.Label();
            this.lblPtnPreview = new System.Windows.Forms.Label();
            this.lblCnsnPtn = new System.Windows.Forms.Label();
            this.txtConversionPattern = new System.Windows.Forms.TextBox();
            this.ddlLayoutType = new System.Windows.Forms.ComboBox();
            this.lblLayoutType = new System.Windows.Forms.Label();
            this.gpArgContainer.SuspendLayout();
            this.argPanel.SuspendLayout();
            this.gbLayoutGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAlias
            // 
            this.lblAlias.AutoSize = true;
            this.lblAlias.Location = new System.Drawing.Point(8, 25);
            this.lblAlias.Name = "lblAlias";
            this.lblAlias.Size = new System.Drawing.Size(41, 13);
            this.lblAlias.TabIndex = 0;
            this.lblAlias.Text = "Name :";
            // 
            // lblAppenderClass
            // 
            this.lblAppenderClass.AutoSize = true;
            this.lblAppenderClass.Location = new System.Drawing.Point(8, 52);
            this.lblAppenderClass.Name = "lblAppenderClass";
            this.lblAppenderClass.Size = new System.Drawing.Size(88, 13);
            this.lblAppenderClass.TabIndex = 1;
            this.lblAppenderClass.Text = "Appender Type :";
            // 
            // ddlAppenderClasses
            // 
            this.ddlAppenderClasses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlAppenderClasses.Location = new System.Drawing.Point(107, 49);
            this.ddlAppenderClasses.Name = "ddlAppenderClasses";
            this.ddlAppenderClasses.Size = new System.Drawing.Size(417, 21);
            this.ddlAppenderClasses.TabIndex = 2;
            this.ddlAppenderClasses.SelectedIndexChanged += new System.EventHandler(this.ddlAppenderClasses_SelectedIndexChanged);
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(107, 22);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(417, 21);
            this.txtAlias.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(450, 409);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(369, 409);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.Location = new System.Drawing.Point(8, 82);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(67, 13);
            this.lblDesc.TabIndex = 0;
            this.lblDesc.Text = "Description :";
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(107, 79);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.ReadOnly = true;
            this.txtDesc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDesc.Size = new System.Drawing.Size(417, 54);
            this.txtDesc.TabIndex = 7;
            // 
            // btnPatternHelp
            // 
            this.btnPatternHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPatternHelp.Location = new System.Drawing.Point(447, 14);
            this.btnPatternHelp.Name = "btnPatternHelp";
            this.btnPatternHelp.Size = new System.Drawing.Size(32, 23);
            this.btnPatternHelp.TabIndex = 8;
            this.btnPatternHelp.Text = "?";
            this.toolTip.SetToolTip(this.btnPatternHelp, "How to use?");
            this.btnPatternHelp.Click += new System.EventHandler(this.btnPatternHelp_Click);
            // 
            // vSrlBar4grp
            // 
            this.vSrlBar4grp.Location = new System.Drawing.Point(496, 0);
            this.vSrlBar4grp.Name = "vSrlBar4grp";
            this.vSrlBar4grp.Size = new System.Drawing.Size(17, 264);
            this.vSrlBar4grp.TabIndex = 0;
            this.vSrlBar4grp.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vSrlBar4grp_Scroll);
            // 
            // gpArgContainer
            // 
            this.gpArgContainer.Controls.Add(this.vSrlBar4grp);
            this.gpArgContainer.Controls.Add(this.argPanel);
            this.gpArgContainer.Location = new System.Drawing.Point(12, 139);
            this.gpArgContainer.Name = "gpArgContainer";
            this.gpArgContainer.Size = new System.Drawing.Size(513, 264);
            this.gpArgContainer.TabIndex = 8;
            this.gpArgContainer.TabStop = false;
            this.gpArgContainer.Text = "Arguments";
            // 
            // argPanel
            // 
            this.argPanel.Controls.Add(this.gbLayoutGroupBox);
            this.argPanel.Location = new System.Drawing.Point(8, 17);
            this.argPanel.Name = "argPanel";
            this.argPanel.Size = new System.Drawing.Size(485, 227);
            this.argPanel.TabIndex = 0;
            // 
            // gbLayoutGroupBox
            // 
            this.gbLayoutGroupBox.Controls.Add(this.btnPatternHelp);
            this.gbLayoutGroupBox.Controls.Add(this.txtDemoString);
            this.gbLayoutGroupBox.Controls.Add(this.lblDemoString);
            this.gbLayoutGroupBox.Controls.Add(this.lblPreviewResult);
            this.gbLayoutGroupBox.Controls.Add(this.lblPtnPreview);
            this.gbLayoutGroupBox.Controls.Add(this.lblCnsnPtn);
            this.gbLayoutGroupBox.Controls.Add(this.txtConversionPattern);
            this.gbLayoutGroupBox.Controls.Add(this.ddlLayoutType);
            this.gbLayoutGroupBox.Controls.Add(this.lblLayoutType);
            this.gbLayoutGroupBox.Location = new System.Drawing.Point(0, 0);
            this.gbLayoutGroupBox.Name = "gbLayoutGroupBox";
            this.gbLayoutGroupBox.Size = new System.Drawing.Size(485, 213);
            this.gbLayoutGroupBox.TabIndex = 1;
            this.gbLayoutGroupBox.TabStop = false;
            this.gbLayoutGroupBox.Text = "Layout";
            // 
            // txtDemoString
            // 
            this.txtDemoString.Location = new System.Drawing.Point(117, 97);
            this.txtDemoString.Name = "txtDemoString";
            this.txtDemoString.Size = new System.Drawing.Size(346, 21);
            this.txtDemoString.TabIndex = 7;
            this.txtDemoString.Text = "Demo info(You may change it)";
            this.txtDemoString.TextChanged += new System.EventHandler(this.txtDemoString_TextChanged);
            // 
            // lblDemoString
            // 
            this.lblDemoString.AutoSize = true;
            this.lblDemoString.Location = new System.Drawing.Point(24, 100);
            this.lblDemoString.Name = "lblDemoString";
            this.lblDemoString.Size = new System.Drawing.Size(72, 13);
            this.lblDemoString.TabIndex = 6;
            this.lblDemoString.Text = "Demo String :";
            // 
            // lblPreviewResult
            // 
            this.lblPreviewResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPreviewResult.Location = new System.Drawing.Point(117, 125);
            this.lblPreviewResult.Name = "lblPreviewResult";
            this.lblPreviewResult.Size = new System.Drawing.Size(346, 73);
            this.lblPreviewResult.TabIndex = 5;
            this.lblPreviewResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPtnPreview
            // 
            this.lblPtnPreview.AutoSize = true;
            this.lblPtnPreview.Location = new System.Drawing.Point(24, 125);
            this.lblPtnPreview.Name = "lblPtnPreview";
            this.lblPtnPreview.Size = new System.Drawing.Size(91, 13);
            this.lblPtnPreview.TabIndex = 4;
            this.lblPtnPreview.Text = "Pattern Preview :";
            // 
            // lblCnsnPtn
            // 
            this.lblCnsnPtn.AutoSize = true;
            this.lblCnsnPtn.Location = new System.Drawing.Point(24, 73);
            this.lblCnsnPtn.Name = "lblCnsnPtn";
            this.lblCnsnPtn.Size = new System.Drawing.Size(107, 13);
            this.lblCnsnPtn.TabIndex = 3;
            this.lblCnsnPtn.Text = "Conversion Pattern :";
            // 
            // txtConversionPattern
            // 
            this.txtConversionPattern.Location = new System.Drawing.Point(137, 70);
            this.txtConversionPattern.Name = "txtConversionPattern";
            this.txtConversionPattern.Size = new System.Drawing.Size(326, 21);
            this.txtConversionPattern.TabIndex = 2;
            this.txtConversionPattern.TextChanged += new System.EventHandler(this.txtConversionPattern_TextChanged);
            // 
            // ddlLayoutType
            // 
            this.ddlLayoutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlLayoutType.Location = new System.Drawing.Point(87, 43);
            this.ddlLayoutType.Name = "ddlLayoutType";
            this.ddlLayoutType.Size = new System.Drawing.Size(376, 21);
            this.ddlLayoutType.TabIndex = 1;
            // 
            // lblLayoutType
            // 
            this.lblLayoutType.AutoSize = true;
            this.lblLayoutType.Location = new System.Drawing.Point(24, 46);
            this.lblLayoutType.Name = "lblLayoutType";
            this.lblLayoutType.Size = new System.Drawing.Size(38, 13);
            this.lblLayoutType.TabIndex = 0;
            this.lblLayoutType.Text = "Type :";
            // 
            // frmAppender
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(536, 456);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.lblAppenderClass);
            this.Controls.Add(this.lblAlias);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ddlAppenderClasses);
            this.Controls.Add(this.gpArgContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAppender";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log4net Appender Editor";
            this.gpArgContainer.ResumeLayout(false);
            this.argPanel.ResumeLayout(false);
            this.gbLayoutGroupBox.ResumeLayout(false);
            this.gbLayoutGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#endregion

		private void RecursiveGenerateControls(ArgumentStruct oArg)
		{
			Control control1;
			switch (oArg.Name)
			{
				case "layout":
					foreach (string sEnumValue in oArg.EnumValues)
					{
						ddlLayoutType.Items.Add(sEnumValue);
					}
					ddlLayoutType.Text = oArg.Value;
					break;
				case "conversionPattern":
					txtConversionPattern.Text = oArg.Value;
					break;
				default:
					Label label1 = new Label();
					label1.Text = oArg.Name + " : ";
					label1.AutoSize = true;
					switch (oArg.UIType)
					{
						case UIControlType.MultiLineTextBox:
							control1 = new TextBox();
							((TextBox) control1).Multiline = true;
							((TextBox) control1).ScrollBars = ScrollBars.Both;
							((TextBox) control1).Text = oArg.Value;
							control1.Height *= 5;
							break;

						case UIControlType.DropDownList:
							control1 = new ComboBox();
							((ComboBox) control1).DropDownStyle = ComboBoxStyle.DropDownList;
							foreach (string text2 in oArg.EnumValues)
							{
								((ComboBox) control1).Items.Add(text2);
							}
							((ComboBox) control1).Text = oArg.Value;
							break;

						case UIControlType.ListBox:
							control1 = new ListBox();
							foreach (string text1 in oArg.EnumValues)
							{
								((ListBox) control1).Items.Add(text1);
							}
							((ListBox) control1).Text = oArg.Value;
							break;

						case UIControlType.ParameterGrid:
							control1 = new ParameterGrid();
							((ParameterGrid)control1).ParameterXmlNodes = oArg.ParameterXml;
							break;

						case UIControlType.None:
							control1 = new Label();
							break;

						default:
							control1 = new TextBox();
							((TextBox) control1).Text = oArg.Value;
							break;
					}
					control1.Tag = oArg.Name;
					toolTip.SetToolTip(control1, oArg.Description);
					argPanel.Controls.Add(label1);
					argPanel.Controls.Add(control1);
					break;
			}
			if (oArg.ChildArguments != null)
			{
				foreach (ArgumentStruct struct1 in oArg.ChildArguments)
				{
					RecursiveGenerateControls(struct1);
				}
			}
		}

		public new DialogResult ShowDialog()
		{
			return ShowDialog(null);
		}

		public new DialogResult ShowDialog(IWin32Window owner)
		{
			if (moConsulter == null)
			{
				InitGroupArgument();
			}
			return base.ShowDialog(owner);
		}

		private void vSrlBar4grp_Scroll(object sender, ScrollEventArgs e)
		{
			ArrangeControls();
		}

		public string CurrentAliasName
		{
			get
			{
				return msAliasName;
			}
		}
 
		private void txtConversionPattern_TextChanged(object sender, System.EventArgs e)
		{
			RenderPatternDemo();
		}

		private void txtDemoString_TextChanged(object sender, System.EventArgs e)
		{
            RenderPatternDemo();
		}

		private void RenderPatternDemo()
		{
			try
			{
				lblPreviewResult.Text = Helper.PreviewPattern(txtDemoString.Text, ddlLayoutType.Text, txtConversionPattern.Text, this).Replace("\t", "    ");		
			} 
			catch (Exception oEX)
			{
				MessageBox.Show(oEX.ToString(), oEX.Message);
			}
		}

		private void btnPatternHelp_Click(object sender, System.EventArgs e)
		{
			System.Diagnostics.Process.Start("IExplore.exe", "http://logging.apache.org/log4net/release/sdk/log4net.Layout.PatternLayout.html");
		}
	}
}
