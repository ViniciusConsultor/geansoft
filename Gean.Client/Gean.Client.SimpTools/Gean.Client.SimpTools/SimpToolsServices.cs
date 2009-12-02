using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Gean.Client.SimpTools
{
    internal static class SimpToolsServices
    {
        static SimpToolsServices()
        {
            Options.Initializes("Resources\\SimpTools.gconfig");
            SimpToolsServices.Options = Options.Instance;
        }

        public static Options Options { get; private set; }

        /// <summary>
        /// Reads the application from directory.
        /// </summary>
        /// <returns></returns>
        public static SimpApplication[] ReadApplicationFromDirectory()
        {
            List<SimpApplication> fileList = new List<SimpApplication>();
            string dir = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Tools");
            string[] files = Directory.GetFiles(dir, "*.gconfig", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                string appName = Path.Combine
                    (
                        Path.GetDirectoryName(file),
                        ((XmlElement)(doc.DocumentElement.SelectNodes(@"//tool[@appName]")[0])).GetAttribute("appName")
                    );
                string text = ((XmlElement)(doc.DocumentElement.SelectNodes(@"//tool[@text]")[0])).GetAttribute("text");
                string tooltip = ((XmlElement)(doc.DocumentElement.SelectNodes(@"//tool[@tooltip]")[0])).GetAttribute("tooltip");
                if (File.Exists(appName))
                {
                    SimpApplication sa = new SimpApplication(appName, text, tooltip);
                    fileList.Add(sa);
                }
                else
                {
                    Debug.Fail(string.Format("{0} is NOT FOUND.", appName));
                }
            }
            return fileList.ToArray();
        }

        public class SimpApplication
        {
            public SimpApplication(string name, string text, string tooltip)
            {
                this.Name = name;
                this.Text = text;
                this.ToolTip = tooltip;
            }
            public string Name { get; private set; }
            public string Text { get; private set; }
            public string ToolTip { get; private set; }
        }

        public static void StartApplication(string appName)
        {
            try
            {
                Process proc = Process.Start(appName);
                if (proc != null)
                {
                    proc.WaitForExit();
                    if (!proc.HasExited)
                    {
                        proc.Kill();// 如果外部程序没有结束运行则强行终止。
                    }
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, appName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

/* 启动外部程序
 * 
 *  1. 启动外部程序，不等待其退出。
    2. 启动外部程序，等待其退出。
    3. 启动外部程序，无限等待其退出。
    4. 启动外部程序，通过事件监视其退出。

    // using System.Diagnostics;
    private string appName = "calc.exe";
    /// <summary>
    /// 1. 启动外部程序，不等待其退出
    /// </summary>
    private void button1_Click(object sender, EventArgs e)
    {
        Process.Start(appName);
        MessageBox.Show(String.Format("外部程序 {0} 启动完成！", this.appName), this.Text,
        MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// 2. 启动外部程序，等待其退出
    /// </summary>
    private void button2_Click(object sender, EventArgs e)
    {
        try
        {
            Process proc = Process.Start(appName);
            if (proc != null)
            {
                proc.WaitForExit(3000);
                if (proc.HasExited) 
                    MessageBox.Show(String.Format("外部程序 {0} 已经退出！", this.appName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    // 如果外部程序没有结束运行则强行终止之。
                    proc.Kill();
                    MessageBox.Show(String.Format("外部程序 {0} 被强行终止！", this.appName), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>

    /// 3. 启动外部程序，无限等待其退出

    /// </summary>

    private void button3_Click(object sender, EventArgs e)

    {

    try

    {

    Process proc = Process.Start(appName);

    if (proc != null)

    {

    proc.WaitForExit();

    MessageBox.Show(String.Format("外部程序 {0} 已经退出！", this.appName), this.Text,

    MessageBoxButtons.OK, MessageBoxIcon.Information);

    }

    }

    catch (ArgumentException ex)

    {

    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

    }

    }

    /// <summary>

    /// 4. 启动外部程序，通过事件监视其退出

    /// </summary>

    private void button4_Click(object sender, EventArgs e)

    {

    try

    {

    //启动外部程序

    Process proc = Process.Start(appName);

    if (proc != null)

    {

    //监视进程退出

    proc.EnableRaisingEvents = true;

    //指定退出事件方法

    proc.Exited += new EventHandler(proc_Exited);

    }

    }

    catch (ArgumentException ex)

    {

    MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

    }

    }

    /// <summary>

    ///启动外部程序退出事件

    /// </summary>

    void proc_Exited(object sender, EventArgs e)

    {

    MessageBox.Show(String.Format("外部程序 {0} 已经退出！", this.appName), this.Text,

    MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
*/
