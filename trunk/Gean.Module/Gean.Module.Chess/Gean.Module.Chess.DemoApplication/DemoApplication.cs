using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Module.Chess.DemoApplication
{
    partial class DemoApplicationForm
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.Method.OnLoadMethod(this);
        }

        /// <summary>
        /// 主Demo应用方法
        /// </summary>
        private void MainDemoApplication()
        {
            this.Method.MainClick(this);
        }

        /// <summary>
        /// 程序界面中TreeView中的TreeNode的点击事件的方法
        /// </summary>
        /// <param name="treeNode">被点击的TreeNode</param>
        private void TreeNodeClick(TreeNode treeNode)
        {
            this.Method.NodeClick(this, treeNode);
        }
    }
}
