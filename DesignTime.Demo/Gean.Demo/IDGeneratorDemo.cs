using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections.Specialized;

namespace Gean.Demo
{
    class IDGeneratorDemo
    {
        public IDGeneratorDemo()
        {
            this.IDGen = new MyId();
            Console.WriteLine("Init complate.");
        }
        private IIDGenerator IDGen { get; set; }

        public void Do()
        {
            Stopwatch sw = new Stopwatch();
            int count = 150000;
            int count1 = 400;
            sw.Start();

            long time = sw.ElapsedMilliseconds;
            int i = 0;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            for (int j = 0; j < count1; j++)
            {
                dic = new Dictionary<string, int>(count);
                while (i < count)
                {
                    string id = this.IDGen.Generate();
                    dic.Add(id, i);
                    i++;
                }
                string[] s = new string[2];
                int m = 0;
                foreach (var item in dic)
                {
                    if (m >= s.Length)
                    {
                        break;
                    }
                    s[m] = item.Key;
                    m++;
                }
                Console.WriteLine("第{0}次，运行时间：{1}，抽样：{2}, {3}", j, (sw.ElapsedMilliseconds - time), s[0], s[1]);
                time = sw.ElapsedMilliseconds;
                i = 0;
            }

            sw.Stop();
            Console.WriteLine("总时间：" + sw.ElapsedMilliseconds.ToString() + "毫秒，共生成" + count1 * count + "个ID值");
        }

        class MyId : IDGenerator
        {
            //protected override string GetSecond()
            //{
            //    StringBuilder sb = new StringBuilder();
            //    sb.Append(DateTime.Now.Second.ToString().PadLeft(2, '0'));
            //    sb.Append(DateTime.Now.Millisecond.ToString().PadLeft(4, '0'));
            //    return sb.ToString();
            //}

            //protected override string GetCount()
            //{
            //    int n = 5;
            //    if (_counter < GetMaxCount(n) - 1)
            //        _counter++;
            //    else
            //        _counter = 1;
            //    return Convert.ToString(_counter).PadLeft(n, '0');
            //    return "";
            //}
        }
    }
}
