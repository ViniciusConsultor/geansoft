using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.OrientalCharacters.CoreWorkbench
{
    public partial class CoreWorkbench : Form
    {
        public CoreWorkbench()
        {
            InitializeComponent();
        }

        private ToolStripContainer _coreToolStripContainer;
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._coreToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._coreToolStripContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _coreToolStripContainer
            // 
            // 
            // _coreToolStripContainer.ContentPanel
            // 
            this._coreToolStripContainer.ContentPanel.Size = new System.Drawing.Size(632, 445);
            this._coreToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._coreToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._coreToolStripContainer.Name = "_coreToolStripContainer";
            this._coreToolStripContainer.Size = new System.Drawing.Size(632, 445);
            this._coreToolStripContainer.TabIndex = 0;
            // 
            // CoreWorkbench
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 445);
            this.Controls.Add(this._coreToolStripContainer);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.IsMdiContainer = true;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "CoreWorkbench";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._coreToolStripContainer.ResumeLayout(false);
            this._coreToolStripContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion


    }
}
