using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gean.Gui.WinForm
{
    /// <summary>
    /// 一个已设置为多行显示的TextBox，有简单的copy,paste等右键菜单
    /// </summary>
    public class QuickTextBox : TextBox
    {
        protected ContextMenuStrip _ContextMenuStrip;
        ToolStripMenuItem _ToolStripMenuItem;

        public QuickTextBox()
        {
            this.SuspendLayout();

            this.Multiline = true;
            this.MaxLength = 640000;
            this.ScrollBars = ScrollBars.Vertical;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);

            _ContextMenuStrip = new ContextMenuStrip();
            _ToolStripMenuItem = new ToolStripMenuItem("全选(&A)");
            _ToolStripMenuItem.Click += new EventHandler(SelectAllEx);
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            _ToolStripMenuItem = new ToolStripMenuItem("拷贝(&C)");
            _ToolStripMenuItem.Click += new EventHandler(CopyEx);
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            _ToolStripMenuItem = new ToolStripMenuItem("粘贴(&P)");
            _ToolStripMenuItem.Click += new EventHandler(PasteEx);
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            _ToolStripMenuItem = new ToolStripMenuItem("剪切(&T)");
            _ToolStripMenuItem.Click += new EventHandler(CutEx);
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            ToolStripSeparator separator = new ToolStripSeparator();
            _ContextMenuStrip.Items.Add(separator);
            _ToolStripMenuItem = new ToolStripMenuItem("还原(&F)");
            _ToolStripMenuItem.Click += new EventHandler(ClearEx);
            _ContextMenuStrip.Items.Add(_ToolStripMenuItem);
            this.ContextMenuStrip = _ContextMenuStrip;
            this.ResumeLayout(false);

        }
        private void PasteEx(object sender, EventArgs e)
        {
            this.Paste();
        }
        private void CutEx(object sender, EventArgs e)
        {
            this.Cut();
        }
        private void ClearEx(object sender, EventArgs e)
        {
            this.Clear();
        }
        private void SelectAllEx(object sender, EventArgs e)
        {
            this.SelectAll();
        }
        private void CopyEx(object sender, EventArgs e)
        {
            this.Copy();
        }
    }
}
