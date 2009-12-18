using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Gean.EastOfArt.Workbench;
using System.Threading;

namespace Gean.EastOfArt
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {

        System.Timers.Timer timer = new System.Timers.Timer();

        public StartWindow()
        {
            InitializeComponent();

            timer.Interval = 1200;
            timer.AutoReset = false;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            timer.Start();
        }

        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.ThisHide();
        }

        private delegate void HideDelegate();
        private void ThisHide()
        {
            // 判断是否在线程中访问
            if (this.Dispatcher.CheckAccess())
            {
                // 不是的话直接操作控件
                this.Hide();
                this.CallCoreWorkbench();
            }
            else
            {
                // 是的话启用delegate访问
                HideDelegate hideDelegate = new HideDelegate(ThisHide);
                // 如使用Invoke会等到函数调用结束，而BeginInvoke不会等待直接往后走
                this.Dispatcher.BeginInvoke(hideDelegate, null);
            }
        }

        private void CallCoreWorkbench()
        {
            CoreWorkbench bench = new CoreWorkbench();
            bench.Closing += new System.ComponentModel.CancelEventHandler(bench_Closing);
            bench.Show();
        }

        void bench_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Close();
        }


    }
}
