using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.DemoApplicationForm
{
    partial class DemoApplicationForm
    {
        /// <summary>
        /// 主Demo应用方法
        /// </summary>
        private void MainDemoApplication()
        {
            MessageBox.Show("MainDemoApplication");
        }
        /// <summary>
        /// Demo 1 应用方法
        /// </summary>
        private void Demo1Application()
        {
            MessageBox.Show("Demo1Application");
        }
        /// <summary>
        /// Demo 2 应用方法
        /// </summary>
        private void Demo2Application()
        {
            MessageBox.Show("Demo2Application");
        }
        /// <summary>
        /// Demo 3 应用方法
        /// </summary>
        private void Demo3Application()
        {
            MessageBox.Show("Demo3Application");
        }

        /// <summary>
        /// 程序界面中TreeView中的TreeNode的点击事件的方法
        /// </summary>
        /// <param name="treeNode">被点击的TreeNode</param>
        private void TreeNodeClick(TreeNode treeNode)
        {
            MessageBox.Show("TreeNodeClick");
        }
    }
}
