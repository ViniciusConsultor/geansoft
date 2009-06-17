using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace Gean.Wrapper.PlugTree.DemoApplicationForm
{
    partial class DemoApplicationForm
    {
        /// <summary>
        /// 主Demo应用方法
        /// </summary>
        private void MainDemoApplication()
        {
            StartupService.Initializes(Application.ProductName, AppDomain.CurrentDomain.BaseDirectory);
        }



        /// <summary>
        /// Definers类型的Demo方法
        /// </summary>
        private void Demo1Application()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("Definers.xml");
            Definers definers;

            definers = Definers.Parse(doc.DocumentElement.SelectSingleNode("element").ChildNodes);
            this._Listbox.Items.Add("XmlNodeList");
            foreach (KeyValuePair<string,object> item in definers)
            {
                this._Listbox.Items.Add(string.Format("{0} | {1}", item.Key, item.Value as string));
            }

            definers = Definers.Parse(doc.DocumentElement.SelectSingleNode("attributes").Attributes);
            this._Listbox.Items.Add("XmlAttributeCollection");
            foreach (KeyValuePair<string, object> item in definers)
            {
                this._Listbox.Items.Add(string.Format("{0} | {1}", item.Key, item.Value as string));
            }

            definers = Definers.Parse((XmlElement)doc.DocumentElement.SelectSingleNode("ele_att"));
            this._Listbox.Items.Add("XmlElement");
            foreach (KeyValuePair<string, object> item in definers)
            {
                this._Listbox.Items.Add(string.Format("{0} | {1}", item.Key, item.Value as string));
            }

            string filename = "Definers.Save.Test.xml";
            definers.Save(filename);

            doc.Load(filename);
            this._Listbox.Items.Add("XML Save:");
            this._Listbox.Items.Add(doc.DocumentElement.InnerXml);
            FileInfo fi = new FileInfo(filename);
            string fileinfo = fi.FullName + "\r\n" + fi.Length + "\r\n" + fi.LastWriteTime.ToLongTimeString() + "\r\n";
            MessageBox.Show(fileinfo + doc.DocumentElement.InnerXml);
        }
        /// <summary>
        /// PlugPath类型的Demo方法
        /// </summary>
        private void Demo2Application()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("PlugPaths.xml");
            StartupService.ScanPlugPath(doc);

            PlugPath paths = StartupService.PlugPath;
            TreeNode treenode = new TreeNode();
            this.TreeNodeBegin(paths, treenode);
            this._TreeView.Nodes.Add(treenode);
        }

        private void TreeNodeBegin(PlugPath paths, TreeNode node)
        {
            node.Text = paths.Name;
            if (paths.HasChildPathItems)
            {
                foreach (PlugPath item in paths.Items)
                {
                    TreeNode subnode = new TreeNode();
                    node.Nodes.Add(subnode);
                    this.TreeNodeBegin(item, subnode);
                }
            }
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
            //MessageBox.Show("TreeNodeClick");
        }
    }
}
