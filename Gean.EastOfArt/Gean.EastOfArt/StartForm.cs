using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Gean.EastOfArt
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.CloseThis();
        }

        private void CloseThis()
        {
            Thread.Sleep(800);

            //WorkbenchCore bench = new WorkbenchCore();
            //bench.FormClosed += new FormClosedEventHandler(bench_FormClosed);
            //bench.Show();

            Thread.Sleep(1500);
            this.Hide();
        }

        void bench_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }


        #region InitializeComponent

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

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StartForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(480, 260);
            this.ControlBox = false;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(480, 260);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(480, 260);
            this.Name = "StartForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gean East Of Art StartForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

    }
}
