using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Threading;

namespace Gean.Client.MathsExercise
{
    public partial class MainForm : Form
    {
        private static Random _Random = new Random(unchecked((int)DateTime.Now.Ticks));
        private int _Count;
        private StringBuilder _KousuanStringBuilder = new StringBuilder();

        public MainForm()
        {
            InitializeComponent();
            this._Count = (int)this._NumericBox.Value;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new AboutForm()).ShowDialog(this);
        }

        private void _三个数的加减口算Button_Click(object sender, EventArgs e)
        {
            this._Count = (int)this._NumericBox.Value;
            for (int i = 0; i < _Count; i++)
            {
                int[] arr = new int[3];

                //生成三个小于100的用于口算的数字
                for (int j = 0; j < arr.Length; j++)
                {
                    arr[j] = _Random.Next(11, 100);
                }

                StringBuilder sb = new StringBuilder();

                if ((arr[0] - arr[1]) > arr[2])//连减
                {
                    sb.Append(arr[0]).Append(" - ").Append(arr[1]).Append(" - ").Append(arr[2]).Append(" = ");
                    this._KousuanStringBuilder.Append(sb.ToString()).Append("\r\n");
                }
                else if (arr[0] > arr[1])
                {
                    if (arr[0] - arr[1] + arr[2] > 100)
                    {
                        arr[2] = _Random.Next(10, 50);
                    }
                    sb.Append(arr[0]).Append(" - ").Append(arr[1]).Append(" + ").Append(arr[2]).Append(" = ");
                    this._KousuanStringBuilder.Append(sb.ToString()).Append("\r\n");
                }
                else
                {
                    if ((arr[0] + arr[1]) > arr[2])//30+30-25
                    {
                        if ((arr[0] + arr[1]) > 100)//60+60>100
                        {
                            arr[0] = _Random.Next(10, 40);
                            arr[1] = _Random.Next(10, 40);
                            if ((arr[0] + arr[1]) < arr[2])//15+15<40
                            {
                                arr[2] = _Random.Next(5, arr[0] + arr[1]);
                            }
                        }
                        sb.Append(arr[0]).Append(" + ").Append(arr[1]).Append(" - ").Append(arr[2]).Append(" = ");
                        this._KousuanStringBuilder.Append(sb.ToString()).Append("\r\n");
                    }
                    else
                    {
                        if ((arr[0] + arr[1] + arr[2]) > 100)//60+60>100
                        {
                            arr[0] = _Random.Next(10, 40);
                            arr[1] = _Random.Next(10, 40);
                            arr[2] = _Random.Next(1, 100 - arr[0] - arr[1]);
                        }
                        sb.Append(arr[0]).Append(" + ").Append(arr[1]).Append(" + ").Append(arr[2]).Append(" = ");
                        this._KousuanStringBuilder.Append(sb.ToString()).Append("\r\n");
                    }
                }//if
            }//for
            this.OutputData(this._KousuanStringBuilder);
        }

        private void _两个数的加减口算Button_Click(object sender, EventArgs e)
        {
            this._Count = (int)this._NumericBox.Value;
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < this._Count; i++)
            {
                int aa = 0;
                int bb = 0;

                while (aa == bb)
                {
                    aa = _Random.Next(10, 99);
                    bb = _Random.Next(10, 99);
                }

                if (aa + bb <= 100)
                {
                    sb.Append(aa.ToString()).Append(" + ").Append(bb.ToString()).Append(" = ").Append("\r\n");
                }
                else if (aa >= bb)
                {
                    sb.Append(aa.ToString()).Append(" - ").Append(bb.ToString()).Append(" = ").Append("\r\n");
                }
                else
                {
                    sb.Append(bb.ToString()).Append(" - ").Append(aa.ToString()).Append(" = ").Append("\r\n");
                }
            }
            this.OutputData(sb);
        }

        private void OutputData(StringBuilder sb)
        {
            Clipboard.Clear();
            Clipboard.SetText(sb.ToString());
            MessageBox.Show("共 " + _Count + " 道口算题置入剪贴板，拷贝进Word编辑即可。");
        }

    }
}
