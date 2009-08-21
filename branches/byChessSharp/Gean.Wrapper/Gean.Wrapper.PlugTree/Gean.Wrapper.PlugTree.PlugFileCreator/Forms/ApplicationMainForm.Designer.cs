using System.Windows.Forms;
namespace Gean.Wrapper.PlugTree.PlugFileCreator
{
    partial class ApplicationMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationMainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.完整新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.从程序集新建AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.保存SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出XToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具TToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自定义CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选项OToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this._fileLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.InformationTree = new System.Windows.Forms.TreeView();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.ProducerTree = new System.Windows.Forms.TreeView();
            this.ConditionTree = new System.Windows.Forms.TreeView();
            this.PathTree = new System.Windows.Forms.TreeView();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem,
            this.工具TToolStripMenuItem,
            this.帮助HToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建NToolStripMenuItem,
            this.打开OToolStripMenuItem,
            this.toolStripSeparator,
            this.保存SToolStripMenuItem,
            this.另存为AToolStripMenuItem,
            this.toolStripSeparator1,
            this.退出XToolStripMenuItem});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // 新建NToolStripMenuItem
            // 
            this.新建NToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.完整新建ToolStripMenuItem,
            this.从程序集新建AToolStripMenuItem});
            this.新建NToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新建NToolStripMenuItem.Image")));
            this.新建NToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.新建NToolStripMenuItem.Name = "新建NToolStripMenuItem";
            this.新建NToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.新建NToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新建NToolStripMenuItem.Text = "新建(&N)";
            // 
            // 完整新建ToolStripMenuItem
            // 
            this.完整新建ToolStripMenuItem.Name = "完整新建ToolStripMenuItem";
            this.完整新建ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.完整新建ToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.完整新建ToolStripMenuItem.Text = "完整新建";
            this.完整新建ToolStripMenuItem.Click += new System.EventHandler(this.NewPlugToolStripMenuItem_Click);
            // 
            // 从程序集新建AToolStripMenuItem
            // 
            this.从程序集新建AToolStripMenuItem.Name = "从程序集新建AToolStripMenuItem";
            this.从程序集新建AToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.从程序集新建AToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.从程序集新建AToolStripMenuItem.Text = "从程序集新建";
            // 
            // 打开OToolStripMenuItem
            // 
            this.打开OToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("打开OToolStripMenuItem.Image")));
            this.打开OToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.打开OToolStripMenuItem.Name = "打开OToolStripMenuItem";
            this.打开OToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.打开OToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开OToolStripMenuItem.Text = "打开(&O)";
            this.打开OToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(149, 6);
            // 
            // 保存SToolStripMenuItem
            // 
            this.保存SToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("保存SToolStripMenuItem.Image")));
            this.保存SToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.保存SToolStripMenuItem.Name = "保存SToolStripMenuItem";
            this.保存SToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.保存SToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.保存SToolStripMenuItem.Text = "保存(&S)";
            // 
            // 另存为AToolStripMenuItem
            // 
            this.另存为AToolStripMenuItem.Name = "另存为AToolStripMenuItem";
            this.另存为AToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.另存为AToolStripMenuItem.Text = "另存为(&A)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // 退出XToolStripMenuItem
            // 
            this.退出XToolStripMenuItem.Name = "退出XToolStripMenuItem";
            this.退出XToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出XToolStripMenuItem.Text = "退出(&X)";
            // 
            // 工具TToolStripMenuItem
            // 
            this.工具TToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自定义CToolStripMenuItem,
            this.选项OToolStripMenuItem});
            this.工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            this.工具TToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.工具TToolStripMenuItem.Text = "工具(&T)";
            // 
            // 自定义CToolStripMenuItem
            // 
            this.自定义CToolStripMenuItem.Name = "自定义CToolStripMenuItem";
            this.自定义CToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.自定义CToolStripMenuItem.Text = "自定义(&C)";
            // 
            // 选项OToolStripMenuItem
            // 
            this.选项OToolStripMenuItem.Name = "选项OToolStripMenuItem";
            this.选项OToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.选项OToolStripMenuItem.Text = "选项(&O)";
            // 
            // 帮助HToolStripMenuItem
            // 
            this.帮助HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于AToolStripMenuItem});
            this.帮助HToolStripMenuItem.Name = "帮助HToolStripMenuItem";
            this.帮助HToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.帮助HToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 关于AToolStripMenuItem
            // 
            this.关于AToolStripMenuItem.Name = "关于AToolStripMenuItem";
            this.关于AToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.关于AToolStripMenuItem.Text = "关于(&A)...";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 423);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(632, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // _fileLabel
            // 
            this._fileLabel.Name = "_fileLabel";
            this._fileLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.PathTree);
            this.splitContainer1.Size = new System.Drawing.Size(632, 399);
            this.splitContainer1.SplitterDistance = 406;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.InformationTree);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(406, 399);
            this.splitContainer2.SplitterDistance = 127;
            this.splitContainer2.TabIndex = 6;
            // 
            // InformationTree
            // 
            this.InformationTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InformationTree.Location = new System.Drawing.Point(0, 0);
            this.InformationTree.Name = "InformationTree";
            this.InformationTree.Size = new System.Drawing.Size(406, 127);
            this.InformationTree.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.ProducerTree);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.ConditionTree);
            this.splitContainer3.Size = new System.Drawing.Size(406, 268);
            this.splitContainer3.SplitterDistance = 204;
            this.splitContainer3.TabIndex = 0;
            // 
            // ProducerTree
            // 
            this.ProducerTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProducerTree.Location = new System.Drawing.Point(0, 0);
            this.ProducerTree.Name = "ProducerTree";
            this.ProducerTree.Size = new System.Drawing.Size(204, 268);
            this.ProducerTree.TabIndex = 0;
            // 
            // ConditionTree
            // 
            this.ConditionTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConditionTree.Location = new System.Drawing.Point(0, 0);
            this.ConditionTree.Name = "ConditionTree";
            this.ConditionTree.Size = new System.Drawing.Size(198, 268);
            this.ConditionTree.TabIndex = 0;
            // 
            // PathTree
            // 
            this.PathTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PathTree.Location = new System.Drawing.Point(0, 0);
            this.PathTree.Name = "PathTree";
            this.PathTree.Size = new System.Drawing.Size(222, 399);
            this.PathTree.TabIndex = 1;
            // 
            // ApplicationMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 445);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ApplicationMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plug File Creator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private ToolStripMenuItem 文件FToolStripMenuItem;
        private ToolStripMenuItem 新建NToolStripMenuItem;
        private ToolStripMenuItem 完整新建ToolStripMenuItem;
        private ToolStripMenuItem 从程序集新建AToolStripMenuItem;
        private ToolStripMenuItem 打开OToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem 保存SToolStripMenuItem;
        private ToolStripMenuItem 另存为AToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem 退出XToolStripMenuItem;
        private ToolStripMenuItem 工具TToolStripMenuItem;
        private ToolStripMenuItem 自定义CToolStripMenuItem;
        private ToolStripMenuItem 选项OToolStripMenuItem;
        private ToolStripMenuItem 帮助HToolStripMenuItem;
        private ToolStripMenuItem 关于AToolStripMenuItem;
        private ToolStripStatusLabel _fileLabel;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
    }
}

