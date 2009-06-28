namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    partial class _mainForm
    {
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_mainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._runtimeTree = new System.Windows.Forms.TreeView();
            this._pathTree = new System.Windows.Forms.TreeView();
            this._tabControl = new System.Windows.Forms.TabControl();
            this._commonPage = new System.Windows.Forms.TabPage();
            this._commonTable = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._producerPage = new System.Windows.Forms.TabPage();
            this._conditionPage = new System.Windows.Forms.TabPage();
            this._plugPage = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this._clearButton = new System.Windows.Forms.Button();
            this._saveButton = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this._tabControl.SuspendLayout();
            this._commonPage.SuspendLayout();
            this._commonTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(632, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newToolStripButton,
            this._openToolStripButton,
            this._saveToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(632, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _newToolStripButton
            // 
            this._newToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_newToolStripButton.Image")));
            this._newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._newToolStripButton.Name = "_newToolStripButton";
            this._newToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._newToolStripButton.Text = "新建(&N)";
            this._newToolStripButton.Click += new System.EventHandler(this._newToolStripButton_Click);
            // 
            // _openToolStripButton
            // 
            this._openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_openToolStripButton.Image")));
            this._openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._openToolStripButton.Name = "_openToolStripButton";
            this._openToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._openToolStripButton.Text = "打开(&O)";
            this._openToolStripButton.Click += new System.EventHandler(this._openToolStripButton_Click);
            // 
            // _saveToolStripButton
            // 
            this._saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("_saveToolStripButton.Image")));
            this._saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveToolStripButton.Name = "_saveToolStripButton";
            this._saveToolStripButton.Size = new System.Drawing.Size(23, 22);
            this._saveToolStripButton.Text = "保存(&S)";
            this._saveToolStripButton.Click += new System.EventHandler(this._saveToolStripButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 423);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(632, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 49);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this._tabControl);
            this.splitContainer2.Size = new System.Drawing.Size(632, 374);
            this.splitContainer2.SplitterDistance = 371;
            this.splitContainer2.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._runtimeTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._pathTree);
            this.splitContainer1.Size = new System.Drawing.Size(371, 374);
            this.splitContainer1.SplitterDistance = 121;
            this.splitContainer1.TabIndex = 4;
            // 
            // _runtimeTree
            // 
            this._runtimeTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._runtimeTree.Location = new System.Drawing.Point(0, 0);
            this._runtimeTree.Name = "_runtimeTree";
            this._runtimeTree.Size = new System.Drawing.Size(371, 121);
            this._runtimeTree.TabIndex = 0;
            // 
            // _pathTree
            // 
            this._pathTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pathTree.Location = new System.Drawing.Point(0, 0);
            this._pathTree.Name = "_pathTree";
            this._pathTree.Size = new System.Drawing.Size(371, 249);
            this._pathTree.TabIndex = 0;
            // 
            // _tabControl
            // 
            this._tabControl.Controls.Add(this._commonPage);
            this._tabControl.Controls.Add(this._producerPage);
            this._tabControl.Controls.Add(this._conditionPage);
            this._tabControl.Controls.Add(this._plugPage);
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(257, 374);
            this._tabControl.TabIndex = 0;
            // 
            // _commonPage
            // 
            this._commonPage.Controls.Add(this._saveButton);
            this._commonPage.Controls.Add(this._clearButton);
            this._commonPage.Controls.Add(this._commonTable);
            this._commonPage.Location = new System.Drawing.Point(4, 22);
            this._commonPage.Name = "_commonPage";
            this._commonPage.Padding = new System.Windows.Forms.Padding(3);
            this._commonPage.Size = new System.Drawing.Size(249, 348);
            this._commonPage.TabIndex = 3;
            this._commonPage.Text = "Common";
            this._commonPage.UseVisualStyleBackColor = true;
            // 
            // _commonTable
            // 
            this._commonTable.ColumnCount = 2;
            this._commonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this._commonTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this._commonTable.Controls.Add(this.label1, 0, 0);
            this._commonTable.Controls.Add(this.label3, 0, 2);
            this._commonTable.Controls.Add(this.label4, 0, 3);
            this._commonTable.Controls.Add(this.label6, 0, 1);
            this._commonTable.Controls.Add(this.label5, 0, 5);
            this._commonTable.Controls.Add(this.label2, 0, 4);
            this._commonTable.Controls.Add(this.textBox1, 1, 0);
            this._commonTable.Controls.Add(this.textBox2, 1, 1);
            this._commonTable.Controls.Add(this.textBox3, 1, 2);
            this._commonTable.Controls.Add(this.textBox4, 1, 3);
            this._commonTable.Controls.Add(this.textBox5, 1, 4);
            this._commonTable.Controls.Add(this.textBox6, 1, 5);
            this._commonTable.Dock = System.Windows.Forms.DockStyle.Top;
            this._commonTable.Location = new System.Drawing.Point(3, 3);
            this._commonTable.Name = "_commonTable";
            this._commonTable.RowCount = 7;
            this._commonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this._commonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this._commonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this._commonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this._commonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this._commonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this._commonTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this._commonTable.Size = new System.Drawing.Size(243, 293);
            this._commonTable.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "name:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "author:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "copyright:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "website:";
            // 
            // _producerPage
            // 
            this._producerPage.Location = new System.Drawing.Point(4, 21);
            this._producerPage.Name = "_producerPage";
            this._producerPage.Padding = new System.Windows.Forms.Padding(3);
            this._producerPage.Size = new System.Drawing.Size(249, 349);
            this._producerPage.TabIndex = 0;
            this._producerPage.Text = "Producer";
            this._producerPage.UseVisualStyleBackColor = true;
            // 
            // _conditionPage
            // 
            this._conditionPage.Location = new System.Drawing.Point(4, 21);
            this._conditionPage.Name = "_conditionPage";
            this._conditionPage.Padding = new System.Windows.Forms.Padding(3);
            this._conditionPage.Size = new System.Drawing.Size(249, 349);
            this._conditionPage.TabIndex = 1;
            this._conditionPage.Text = "Condition";
            this._conditionPage.UseVisualStyleBackColor = true;
            // 
            // _plugPage
            // 
            this._plugPage.Location = new System.Drawing.Point(4, 21);
            this._plugPage.Name = "_plugPage";
            this._plugPage.Padding = new System.Windows.Forms.Padding(3);
            this._plugPage.Size = new System.Drawing.Size(249, 349);
            this._plugPage.TabIndex = 2;
            this._plugPage.Text = "Plug";
            this._plugPage.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "email:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "version:";
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(75, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(165, 21);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(75, 33);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(165, 21);
            this.textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox3.Location = new System.Drawing.Point(75, 72);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(165, 21);
            this.textBox3.TabIndex = 8;
            // 
            // textBox4
            // 
            this.textBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox4.Location = new System.Drawing.Point(75, 117);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(165, 21);
            this.textBox4.TabIndex = 9;
            // 
            // textBox5
            // 
            this.textBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox5.Location = new System.Drawing.Point(75, 139);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(165, 21);
            this.textBox5.TabIndex = 10;
            // 
            // textBox6
            // 
            this.textBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox6.Location = new System.Drawing.Point(75, 164);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(165, 21);
            this.textBox6.TabIndex = 11;
            // 
            // _clearButton
            // 
            this._clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._clearButton.Location = new System.Drawing.Point(182, 317);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(64, 23);
            this._clearButton.TabIndex = 1;
            this._clearButton.Text = "Clear";
            this._clearButton.UseVisualStyleBackColor = true;
            // 
            // _saveButton
            // 
            this._saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._saveButton.Location = new System.Drawing.Point(112, 317);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(64, 23);
            this._saveButton.TabIndex = 2;
            this._saveButton.Text = "Save";
            this._saveButton.UseVisualStyleBackColor = true;
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 445);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "_mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plug File Creator";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this._commonPage.ResumeLayout(false);
            this._commonTable.ResumeLayout(false);
            this._commonTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton _newToolStripButton;
        private System.Windows.Forms.ToolStripButton _openToolStripButton;
        private System.Windows.Forms.ToolStripButton _saveToolStripButton;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView _runtimeTree;
        private System.Windows.Forms.TreeView _pathTree;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _producerPage;
        private System.Windows.Forms.TabPage _conditionPage;
        private System.Windows.Forms.TabPage _plugPage;
        private System.Windows.Forms.TabPage _commonPage;
        private System.Windows.Forms.TableLayoutPanel _commonTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button _saveButton;
        private System.Windows.Forms.Button _clearButton;
    }
}

