using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

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
            MarkSteps(form);
        }

        private static void MarkSteps(DemoApplicationForm form)
        {
            form.Cursor = Cursors.WaitCursor;

            List<string> step2 = new List<string>();
            List<string> step3 = new List<string>();
            List<string> step4 = new List<string>();
            List<string> step5 = new List<string>();
            List<string> step6 = new List<string>();
            List<string> step7 = new List<string>();
            List<string> step8 = new List<string>();
            List<string> stepPr = new List<string>();

            string regex = @"\d+\.";
            string file = @"DemoFile\largeness,game.pgn";
            string[] lines = File.ReadAllLines(file);
            form.ProgressBar.Maximum = lines.Length + 20;
            form.ProgressBar.Value = 0;

            form.StatusText1 = "File read complate. Line count: " + lines.Length.ToString() + ".";
            form.Update();
            Application.DoEvents();

            foreach (string line in lines)
            {
                #region line
                form.ProgressBar.PerformStep();
                form.ProgressBar.Invalidate();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                string v = line.Trim();
                if (v.StartsWith("["))
                {
                    continue;
                }
                string[] step = (new Regex(regex)).Split(v);
                foreach (string str in step)
                {
                    string s = str.Trim();
                    if (string.IsNullOrEmpty(s))
                    {
                        continue;
                    }
                    string[] lei = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in lei)
                    {
                        if (item.IndexOf('-') > 0) continue;
                        if (item.IndexOf('=') > 0)
                        {
                            stepPr.Add(item);
                            continue;
                        }
                        switch (item.Length)
                        {
                            #region case
                            case 2:
                                step2.Add(item);
                                break;
                            case 3:
                                step3.Add(item);
                                break;
                            case 4:
                                step4.Add(item);
                                break;
                            case 5:
                                step5.Add(item);
                                break;
                            case 6:
                                step6.Add(item);
                                break;
                            case 7:
                                step7.Add(item);
                                break;
                            case 8:
                                step8.Add(item);
                                break;
                            default:
                                break;
                            #endregion
                        }
                    }
                }
                #endregion
            }
            stepPr.Sort();
            StringBuilder sbAll = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            MarkStringBuilder(sbAll, sb1, sb2, stepPr, step6, step5, step4, step3, step2);

            File.Delete("stepsAll.txt");
            File.Delete("steps1.txt");
            File.Delete("steps2.txt");
            File.AppendAllText("stepsAll.txt", sbAll.ToString());
            File.AppendAllText("steps1.txt", sb1.ToString());
            File.AppendAllText("steps2.txt", sb2.ToString());

            form.Clear(null, null);
            form.TextBox3 = "Complated!";

            form.Cursor = Cursors.Default;
        }

        private static void MarkStringBuilder(StringBuilder sbAll, StringBuilder sb1, StringBuilder sb2, params List<string>[] steps)
        {
            int j = 1;
            foreach (List<string> item in steps)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    string str = item[i];
                    sbAll.AppendLine(str);
                    if (i % (10 * j) == 0)
                        sb1.AppendLine(str);
                    if (i % (30 * j) == 0)
                        sb2.AppendLine(str);
                }
                j = j * 3;
            }
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
