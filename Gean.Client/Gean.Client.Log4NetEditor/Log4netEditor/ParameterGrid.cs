using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace Gean.Client.Log4NetEditor
{
	/// <summary>
	/// ParameterGrid 的摘要描述。
	/// </summary>
	public class ParameterGrid : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.DataGridTableStyle tsParameters;
		private System.Windows.Forms.DataGridTextBoxColumn parameterName;
		private System.Windows.Forms.DataGridTextBoxColumn dbType;
		private System.Windows.Forms.DataGridTextBoxColumn layout;
		private System.Windows.Forms.DataGridTextBoxColumn conversionPattern;
		private System.Windows.Forms.DataGrid dgParameter;
		private System.Windows.Forms.DataGridTextBoxColumn size;
		/// <summary> 
		/// 設計工具所需的變數。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ParameterGrid()
		{
			// 此為 Windows.Forms 表單設計工具所需的呼叫。
			InitializeComponent();

			// TODO: 在 InitializeComponent 呼叫之後加入任何初始設定
			dsADOParameters.ParametersDataTable dtParams = new dsADOParameters.ParametersDataTable();
			dgParameter.DataSource = dtParams; 
		}

		/// <summary>
		/// 清除任何使用中的資源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region 元件設計工具產生的程式碼
		/// <summary>
		/// 此為設計工具支援所必須的方法 - 請勿使用程式碼編輯器修改
		/// 這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
            this.dgParameter = new System.Windows.Forms.DataGrid();
            this.tsParameters = new System.Windows.Forms.DataGridTableStyle();
            this.parameterName = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dbType = new System.Windows.Forms.DataGridTextBoxColumn();
            this.layout = new System.Windows.Forms.DataGridTextBoxColumn();
            this.conversionPattern = new System.Windows.Forms.DataGridTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgParameter)).BeginInit();
            this.SuspendLayout();
            // 
            // dgParameter
            // 
            this.dgParameter.CaptionText = "Parameters";
            this.dgParameter.DataMember = "";
            this.dgParameter.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgParameter.Location = new System.Drawing.Point(0, 0);
            this.dgParameter.Name = "dgParameter";
            this.dgParameter.Size = new System.Drawing.Size(512, 264);
            this.dgParameter.TabIndex = 0;
            this.dgParameter.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.tsParameters});
            // 
            // tsParameters
            // 
            this.tsParameters.DataGrid = this.dgParameter;
            this.tsParameters.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.parameterName,
            this.dbType,
            this.layout,
            this.conversionPattern,
            this.size});
            this.tsParameters.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.tsParameters.MappingName = "Parameters";
            // 
            // parameterName
            // 
            this.parameterName.Format = "";
            this.parameterName.FormatInfo = null;
            this.parameterName.HeaderText = "Parameter Name";
            this.parameterName.MappingName = "parameterName";
            this.parameterName.Width = 75;
            // 
            // dbType
            // 
            this.dbType.Format = "";
            this.dbType.FormatInfo = null;
            this.dbType.HeaderText = "DBType";
            this.dbType.MappingName = "dbType";
            this.dbType.Width = 75;
            // 
            // layout
            // 
            this.layout.Format = "";
            this.layout.FormatInfo = null;
            this.layout.HeaderText = "Layout";
            this.layout.MappingName = "layout";
            this.layout.Width = 75;
            // 
            // conversionPattern
            // 
            this.conversionPattern.Format = "";
            this.conversionPattern.FormatInfo = null;
            this.conversionPattern.HeaderText = "Conversion Pattern";
            this.conversionPattern.MappingName = "conversionPattern";
            this.conversionPattern.Width = 75;
            // 
            // size
            // 
            this.size.Format = "";
            this.size.FormatInfo = null;
            this.size.HeaderText = "size";
            this.size.MappingName = "size";
            this.size.Width = 75;
            // 
            // ParameterGrid
            // 
            this.Controls.Add(this.dgParameter);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Name = "ParameterGrid";
            this.Size = new System.Drawing.Size(513, 265);
            this.Resize += new System.EventHandler(this.ParameterGrid_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgParameter)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void ParameterGrid_Resize(object sender, System.EventArgs e)
		{
			dgParameter.Width = Width;
			dgParameter.Height = Height;
		}

		public XmlNodeList ParameterXmlNodes
		{
			get 
			{
				XmlDocument oDoc = new XmlDocument();
				XmlNode oRootNode = oDoc.CreateNode(XmlNodeType.Element, "parameters", string.Empty);
				foreach (dsADOParameters.ParametersRow dr in (dsADOParameters.ParametersDataTable)dgParameter.DataSource)
				{
					XmlNode layoutNode;
					XmlNode ParameterNode = oDoc.CreateNode(XmlNodeType.Element, "parameter", string.Empty);
					oRootNode.AppendChild(ParameterNode);
					ParameterNode.AppendChild(CreateParamNode("parameterName", "value", dr.parameterName, oDoc));
					ParameterNode.AppendChild(CreateParamNode("dbType", "value", dr.dbType, oDoc));
					if (0 != dr.size)
					{
						ParameterNode.AppendChild(CreateParamNode("size", "value", dr.size.ToString(), oDoc));
					}
					layoutNode = CreateParamNode("layout", "type", dr.layout, oDoc);
					ParameterNode.AppendChild(layoutNode);
					if (null != dr.conversionPattern && string.Empty != dr.conversionPattern)
					{
						layoutNode.AppendChild(CreateParamNode("conversionPattern", "value", dr.conversionPattern, oDoc));
					}
				}
				return oRootNode.SelectNodes("//parameter");
			}
			set 
			{
				if (null == value) return;
				dsADOParameters.ParametersDataTable dtParams = (dsADOParameters.ParametersDataTable)dgParameter.DataSource;
				dtParams.Clear();
				try
				{
					foreach (XmlNode oNode in value)
					{
						XmlNode tmpNode;
						dsADOParameters.ParametersRow dr = dtParams.NewParametersRow();
						dr.parameterName = oNode.SelectSingleNode("parameterName").Attributes["value"].Value;
						dr.dbType = oNode.SelectSingleNode("dbType").Attributes["value"].Value;
						dr.layout = oNode.SelectSingleNode("layout").Attributes["type"].Value;
						tmpNode = oNode.SelectSingleNode("size");
						if (null != tmpNode) 
						{
							dr.size = int.Parse(tmpNode.Attributes["value"].Value);
						}
						tmpNode = oNode.SelectSingleNode("layout/conversionPattern");
						if (null != tmpNode) 
						{
							dr.conversionPattern = tmpNode.Attributes["value"].Value;
						}
						dtParams.AddParametersRow(dr);
					}
				} 
				catch(Exception oEX)
				{
					System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name + " : " + oEX.ToString());
				}
			}
		}

		private XmlNode CreateParamNode(string NodeName, string ValueName, string Value, XmlDocument oDoc)
		{
			XmlNode oNode = oDoc.CreateNode(XmlNodeType.Element, NodeName, string.Empty);
			XmlAttribute oAttri = oDoc.CreateAttribute(ValueName);
			oAttri.Value = Value;
			oNode.Attributes.Append(oAttri);
			return oNode;
		}
	}
}
