using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Xml;

namespace Gean.Client.Log4NetEditor
{
	/// <summary>
	/// Form1 的摘要描述。
	/// </summary>
	public class frmL4JLocation : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpbl4nPath;
		private System.Windows.Forms.TextBox txtl4nPath;
		private System.Windows.Forms.Button btnBrowe;
		private System.Windows.Forms.TabControl tabLOG4NET;
		private System.Windows.Forms.TabPage tabAppenders;
		private System.Windows.Forms.TabPage tabLoggers;
		private System.Windows.Forms.DataGrid dgAppenders;
		private System.Windows.Forms.DataGrid dgLoggers;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnReload;
		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.SaveFileDialog saveFile;
		private System.Windows.Forms.Button btnRemoveAppender;
		private System.Windows.Forms.Button btnAddAppender;
		private System.Windows.Forms.Button btnRemoveLogger;
		private System.Windows.Forms.Button btnAddLogger;
		private XmlDocument moDoc = new XmlDocument();
		/// <summary>
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmL4JLocation()
		{
			//
			// Windows Form 設計工具支援的必要項
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 呼叫之後加入任何建構函式程式碼
			//
		}

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form 設計工具產生的程式碼
		/// <summary>
		/// 此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
            this.gpbl4nPath = new System.Windows.Forms.GroupBox();
            this.btnBrowe = new System.Windows.Forms.Button();
            this.txtl4nPath = new System.Windows.Forms.TextBox();
            this.tabLOG4NET = new System.Windows.Forms.TabControl();
            this.tabAppenders = new System.Windows.Forms.TabPage();
            this.btnAddAppender = new System.Windows.Forms.Button();
            this.btnRemoveAppender = new System.Windows.Forms.Button();
            this.dgAppenders = new System.Windows.Forms.DataGrid();
            this.tabLoggers = new System.Windows.Forms.TabPage();
            this.btnAddLogger = new System.Windows.Forms.Button();
            this.btnRemoveLogger = new System.Windows.Forms.Button();
            this.dgLoggers = new System.Windows.Forms.DataGrid();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.gpbl4nPath.SuspendLayout();
            this.tabLOG4NET.SuspendLayout();
            this.tabAppenders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAppenders)).BeginInit();
            this.tabLoggers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLoggers)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbl4nPath
            // 
            this.gpbl4nPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbl4nPath.Controls.Add(this.btnBrowe);
            this.gpbl4nPath.Controls.Add(this.txtl4nPath);
            this.gpbl4nPath.Location = new System.Drawing.Point(8, 8);
            this.gpbl4nPath.Name = "gpbl4nPath";
            this.gpbl4nPath.Size = new System.Drawing.Size(576, 56);
            this.gpbl4nPath.TabIndex = 0;
            this.gpbl4nPath.TabStop = false;
            this.gpbl4nPath.Text = "Log4net Config file path:";
            // 
            // btnBrowe
            // 
            this.btnBrowe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowe.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBrowe.Location = new System.Drawing.Point(526, 22);
            this.btnBrowe.Name = "btnBrowe";
            this.btnBrowe.Size = new System.Drawing.Size(42, 25);
            this.btnBrowe.TabIndex = 1;
            this.btnBrowe.Text = "...";
            this.btnBrowe.Click += new System.EventHandler(this.btnBrowe_Click);
            // 
            // txtl4nPath
            // 
            this.txtl4nPath.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtl4nPath.BackColor = System.Drawing.Color.White;
            this.txtl4nPath.Location = new System.Drawing.Point(8, 24);
            this.txtl4nPath.Name = "txtl4nPath";
            this.txtl4nPath.ReadOnly = true;
            this.txtl4nPath.Size = new System.Drawing.Size(512, 21);
            this.txtl4nPath.TabIndex = 0;
            // 
            // tabLOG4NET
            // 
            this.tabLOG4NET.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabLOG4NET.Controls.Add(this.tabAppenders);
            this.tabLOG4NET.Controls.Add(this.tabLoggers);
            this.tabLOG4NET.Location = new System.Drawing.Point(8, 92);
            this.tabLOG4NET.Name = "tabLOG4NET";
            this.tabLOG4NET.SelectedIndex = 0;
            this.tabLOG4NET.Size = new System.Drawing.Size(579, 320);
            this.tabLOG4NET.TabIndex = 1;
            // 
            // tabAppenders
            // 
            this.tabAppenders.Controls.Add(this.btnAddAppender);
            this.tabAppenders.Controls.Add(this.btnRemoveAppender);
            this.tabAppenders.Controls.Add(this.dgAppenders);
            this.tabAppenders.Location = new System.Drawing.Point(4, 22);
            this.tabAppenders.Name = "tabAppenders";
            this.tabAppenders.Size = new System.Drawing.Size(571, 294);
            this.tabAppenders.TabIndex = 0;
            this.tabAppenders.Text = "Appenders";
            // 
            // btnAddAppender
            // 
            this.btnAddAppender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAppender.Enabled = false;
            this.btnAddAppender.Location = new System.Drawing.Point(411, 262);
            this.btnAddAppender.Name = "btnAddAppender";
            this.btnAddAppender.Size = new System.Drawing.Size(75, 23);
            this.btnAddAppender.TabIndex = 2;
            this.btnAddAppender.Text = "Add";
            this.btnAddAppender.Click += new System.EventHandler(this.btnAddAppender_Click);
            // 
            // btnRemoveAppender
            // 
            this.btnRemoveAppender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAppender.Enabled = false;
            this.btnRemoveAppender.Location = new System.Drawing.Point(491, 262);
            this.btnRemoveAppender.Name = "btnRemoveAppender";
            this.btnRemoveAppender.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveAppender.TabIndex = 1;
            this.btnRemoveAppender.Text = "Remove";
            this.btnRemoveAppender.Click += new System.EventHandler(this.btnRemoveAppender_Click);
            // 
            // dgAppenders
            // 
            this.dgAppenders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAppenders.DataMember = "";
            this.dgAppenders.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgAppenders.Location = new System.Drawing.Point(8, 8);
            this.dgAppenders.Name = "dgAppenders";
            this.dgAppenders.ReadOnly = true;
            this.dgAppenders.Size = new System.Drawing.Size(555, 246);
            this.dgAppenders.TabIndex = 0;
            this.dgAppenders.CurrentCellChanged += new System.EventHandler(this.dgAppenders_CurrentCellChanged);
            // 
            // tabLoggers
            // 
            this.tabLoggers.Controls.Add(this.btnAddLogger);
            this.tabLoggers.Controls.Add(this.btnRemoveLogger);
            this.tabLoggers.Controls.Add(this.dgLoggers);
            this.tabLoggers.Location = new System.Drawing.Point(4, 21);
            this.tabLoggers.Name = "tabLoggers";
            this.tabLoggers.Size = new System.Drawing.Size(571, 293);
            this.tabLoggers.TabIndex = 1;
            this.tabLoggers.Text = "Loggers";
            // 
            // btnAddLogger
            // 
            this.btnAddLogger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLogger.Enabled = false;
            this.btnAddLogger.Location = new System.Drawing.Point(510, 250);
            this.btnAddLogger.Name = "btnAddLogger";
            this.btnAddLogger.Size = new System.Drawing.Size(75, 23);
            this.btnAddLogger.TabIndex = 2;
            this.btnAddLogger.Text = "Add";
            this.btnAddLogger.Click += new System.EventHandler(this.btnAddLogger_Click);
            // 
            // btnRemoveLogger
            // 
            this.btnRemoveLogger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveLogger.Enabled = false;
            this.btnRemoveLogger.Location = new System.Drawing.Point(590, 250);
            this.btnRemoveLogger.Name = "btnRemoveLogger";
            this.btnRemoveLogger.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveLogger.TabIndex = 1;
            this.btnRemoveLogger.Text = "Remove";
            this.btnRemoveLogger.Click += new System.EventHandler(this.btnRemoveLogger_Click);
            // 
            // dgLoggers
            // 
            this.dgLoggers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgLoggers.DataMember = "";
            this.dgLoggers.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgLoggers.Location = new System.Drawing.Point(8, 8);
            this.dgLoggers.Name = "dgLoggers";
            this.dgLoggers.ReadOnly = true;
            this.dgLoggers.Size = new System.Drawing.Size(654, 234);
            this.dgLoggers.TabIndex = 0;
            this.dgLoggers.CurrentCellChanged += new System.EventHandler(this.dgLoggers_CurrentCellChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(512, 72);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Enabled = false;
            this.btnReload.Location = new System.Drawing.Point(432, 72);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Reload";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // openFile
            // 
            this.openFile.DefaultExt = "config";
            this.openFile.Filter = "log4net configuration|*.config|All Files|*.*";
            // 
            // frmL4JLocation
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.ClientSize = new System.Drawing.Size(594, 424);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabLOG4NET);
            this.Controls.Add(this.gpbl4nPath);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmL4JLocation";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log4net Location";
            this.gpbl4nPath.ResumeLayout(false);
            this.gpbl4nPath.PerformLayout();
            this.tabLOG4NET.ResumeLayout(false);
            this.tabAppenders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAppenders)).EndInit();
            this.tabLoggers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLoggers)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void btnBrowe_Click(object sender, System.EventArgs e)
		{
			if (DialogResult.OK == openFile.ShowDialog(this))
			{
				txtl4nPath.Text = openFile.FileName;
				btnReload.Enabled = true;
				btnSave.Enabled = true;
				btnReload_Click(sender, e);
			}
		}

		private void BindAppenderGrid()
		{
			dgAppenders.DataSource = PrepareAppenderDT(moDoc);
		}

		private void BindLoggerGrid()
		{
			dgLoggers.DataSource = PrepareLoggerDT(moDoc);
		}

		private void btnReload_Click(object sender, System.EventArgs e)
		{
			if (string.Empty != txtl4nPath.Text)
			{
				try
				{
					moDoc.Load(txtl4nPath.Text);
					BindAppenderGrid();
					BindLoggerGrid();
					btnAddAppender.Enabled = true;
					btnRemoveAppender.Enabled = true;
					btnAddLogger.Enabled = true;
					btnRemoveLogger.Enabled = true;
				} 
				catch (Exception oEX)
				{
					btnReload.Enabled = false;
					MessageBox.Show(this, oEX.Message, "OPEN ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if (DialogResult.OK == saveFile.ShowDialog(this))
			{
				txtl4nPath.Text = saveFile.FileName;
				SaveXmlDocument(moDoc, txtl4nPath.Text);
			}
		}

		private DataTable PrepareAppenderDT(XmlDocument oDoc)
		{
			DataTable table1 = new DataTable("Appenders");
			table1.Columns.Add("Alias Name", typeof(string));
			table1.Columns.Add("Appender Class", typeof(string));
			XmlNodeList list1 = oDoc.SelectNodes("//appender");
			foreach (XmlNode node1 in list1)
			{
				DataRow row1 = table1.NewRow();
				row1[table1.Columns[0]] = node1.Attributes["name"].Value;
				row1[table1.Columns[1]] = node1.Attributes["type"].Value;
				table1.Rows.Add(row1);
			}
			return table1;
		}

		private DataTable PrepareLoggerDT(XmlDocument oDoc)
		{
			DataRow row1;
			DataTable table1 = new DataTable("Loggers");
			table1.Columns.Add("Logger Name", typeof(string));
			table1.Columns.Add("Additivity", typeof(bool));
			XmlNode node1 = oDoc.SelectSingleNode("//root");
			if (node1 != null)
			{
				row1 = table1.NewRow();
				row1[table1.Columns[0]] = "root";
				row1[table1.Columns[1]] = (node1.Attributes["additivity"] == null) || bool.Parse(node1.Attributes["additivity"].Value);
				table1.Rows.Add(row1);
			}
			XmlNodeList list1 = oDoc.SelectNodes("//logger");
			foreach (XmlNode node2 in list1)
			{
				row1 = table1.NewRow();
				row1[table1.Columns[0]] = node2.Attributes["name"].Value;
				row1[table1.Columns[1]] = (node2.Attributes["additivity"] == null) || bool.Parse(node2.Attributes["additivity"].Value);
				table1.Rows.Add(row1);
			}
			return table1;
		}

		private void dgAppenders_CurrentCellChanged(object sender, System.EventArgs e)
		{
			DataGridCell cell1 = dgAppenders.CurrentCell;
			if (cell1.ColumnNumber == 0)
			{
                frmAppender oAppenderEditor = new frmAppender(moDoc);
				oAppenderEditor.EditExistedAppender((string)dgAppenders[cell1]);
				oAppenderEditor.ShowDialog(this);
			}
		}

		private void dgLoggers_CurrentCellChanged(object sender, EventArgs e)
		{
			DataGridCell cell1 = dgLoggers.CurrentCell;
			if (cell1.ColumnNumber == 0)
			{
				frmLogger oLoggerEditor = new frmLogger(moDoc);
				oLoggerEditor.EditExistedLogger((string)dgLoggers[cell1]);
				oLoggerEditor.ShowDialog(this);
			}
		}

		private void btnAddAppender_Click(object sender, System.EventArgs e)
		{
			frmAppender oAppenderEditor = new frmAppender(moDoc);
			if (System.Windows.Forms.DialogResult.OK == oAppenderEditor.ShowDialog(this))
			{
				BindAppenderGrid();
			}
		}

		private void btnRemoveAppender_Click(object sender, System.EventArgs e)
		{
			string sAliasName = (string)dgAppenders[dgAppenders.CurrentRowIndex, 0];
			foreach (XmlNode oApder in moDoc.SelectNodes("//appender"))
			{
				if (oApder.Attributes["name"].Value == sAliasName)
				{
					moDoc.DocumentElement.RemoveChild(oApder);
					break;
				}
			}
		}

		private void btnAddLogger_Click(object sender, System.EventArgs e)
		{
			frmLogger oLoggerEditor = new frmLogger(moDoc);
			if (System.Windows.Forms.DialogResult.OK == oLoggerEditor.ShowDialog(this))
			{
				BindLoggerGrid();
			}
		}

		private void btnRemoveLogger_Click(object sender, System.EventArgs e)
		{
			string sLoggerName = (string)dgLoggers[dgLoggers.CurrentRowIndex, 0];
			foreach (XmlNode node1 in moDoc.SelectNodes("//logger"))
			{
				if (node1.Attributes["name"].Value == sLoggerName)
				{
					moDoc.DocumentElement.RemoveChild(node1);
					break;
				}
			}
		}

		private static void SaveXmlDocument(XmlDocument oDoc, string sXmlFilePath)
		{
			XmlTextWriter writer1 = new XmlTextWriter(sXmlFilePath, Encoding.UTF8);
			try
			{
				writer1.Formatting = Formatting.Indented;
				writer1.Indentation = 4;
				oDoc.WriteTo(writer1);
				writer1.Flush();
			}
			catch (Exception exception1)
			{
				MessageBox.Show(exception1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			finally
			{
				if (writer1 != null)
				{
					writer1.Close();
				}
			}
		}

	}
}
