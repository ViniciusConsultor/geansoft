using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Gean.Module.Chess.DemoApplication
{
    internal class DemoMethod
    {
        /// <summary>
        /// 整个Demo程序的一些静态初始化内容
        /// </summary>
        public static void Initialize()
        {
            //TODO: 在这里构建整个Demo程序的一些静态初始化内容
        }

        /// <summary>
        /// Demo程序的主窗体的OnLoad事件
        /// </summary>
        /// <param name="form"></param>
        internal void OnLoadMethod(DemoApplicationForm form)
        {
            form.StatusText1 = "Success of OnLoad method.";
        }

        /// <summary>
        /// Demo程序的主窗体中的主Button的Click事件
        /// </summary>
        /// <param name="form"></param>
        internal void MainClick(DemoApplicationForm form)
        {
            form.TextBox3 = "Click...";
        }

        /// <summary>
        /// Demo程序的主窗体中的TreeView的节点TreeNode的Click事件
        /// </summary>
        internal void NodeClick(DemoApplicationForm form, TreeNode treeNode)
        {
            form.StatusText2 = "Node Click...";
        }
    }
}
