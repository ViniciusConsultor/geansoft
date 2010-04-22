using System;
using System.IO;
using System.Runtime.Serialization;
using Gean.Json2;

namespace Gean.Client.IHMSJsonBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //288个营业点，每营业点中：6个业务，8个窗口，12个柜员
            //IHMSGroupJsonData dataByJson = new IHMSGroupJsonData(288, 6, 8, 12);

            //6个营业点，每营业点中：3个业务，4个窗口，5个柜员
            IHMSGroupJsonData dataByJson = new IHMSGroupJsonData(6, 3, 4, 5);

            Json json = new Json();
            string stringByJson = json.SerializeObject(dataByJson);

            File.AppendAllText(Path.Combine(@"F:\My Desktop\", "json" + DateTime.Now.Ticks + ".txt"), stringByJson);
        }

    }

    [Serializable]
    class IHMSGroupJsonData
    {
        public Group[] Staticstics { get; set; }
        public IHMSGroupJsonData(int g, int o, int c, int w)
        {
            this.Staticstics = new Group[g];
            for (int i = 0; i < this.Staticstics.Length; i++)
            {
                this.Staticstics[i] = new Group(o, c, w);
            }
        }
    }

    [Serializable]
    class Group
    {
        public string Id { get; set; }
        public Operation[] Os { get; set; }
        public Counter[] Cs { get; set; }
        public Worker[] Ws { get; set; }
        public Group(int o, int c, int w)
        {
            this.Id = Guid.NewGuid().ToString("D");
            this.Os = new Operation[o];
            this.Cs = new Counter[c];
            this.Ws = new Worker[w];

            this.Init();
        }

        private void Init()
        {
            for (int i = 0; i < this.Os.Length; i++)
            {
                this.Os[i] = new Operation();
            }
            for (int i = 0; i < this.Cs.Length; i++)
            {
                this.Cs[i] = new Counter();
            }
            for (int i = 0; i < this.Ws.Length; i++)
            {
                this.Ws[i] = new Worker();
            }
        }
    }

    [Serializable]
    class Operation
    {
        public string Id { get; private set; }
        public Amount Amount { get; set; }
        public Operation()
        {
            this.Id = Guid.NewGuid().ToString("D");
            this.Amount = new Amount();
        }
    }

    [Serializable]
    class Counter
    {
        public string Id { get; private set; }
        public Amount Amount { get; set; }
        public Counter()
        {
            this.Id = Guid.NewGuid().ToString("D");
            this.Amount = new Amount();
        }
    }

    [Serializable]
    class Worker
    {
        public string Id { get; private set; }
        public Amount Amount { get; set; }
        public Worker()
        {
            this.Id = Guid.NewGuid().ToString("D");
            this.Amount = new Amount();
        }
    }

    [Serializable]
    class Amount
    {
        private static Random random = null;
        static Amount()
        {
            random = new Random(unchecked((int)DateTime.Now.Ticks));
        }

        //public JData[] Hour { get; set; }
        public JData D { get; set; }
        public JData W { get; set; }
        public JData TW { get; set; }
        public JData M { get; set; }
        public JData Q { get; set; }
        public JData M6 { get; set; }
        public JData Y { get; set; }
        public JData LY { get; set; }

        public Amount()
        {
            //this.Hour = new JData[24];
            this.D = new JData();
            this.W = new JData();
            this.TW = new JData();
            this.M = new JData();
            this.Q = new JData();
            this.M6 = new JData();
            this.Y = new JData();
            this.LY = new JData();

            this.Init();
        }

        private void Init()//假定早9点上班，晚6点下班
        {
            //午夜0点至早上9点
            //for (int i = 0; i < 9; i++)
            //{
            //    this.Hour[i] = GetJData(0, 0);
            //}
            //this.Hour[9] = GetJData(20, 40);
            //this.Hour[10] = GetJData(40, 100);
            //this.Hour[11] = GetJData(60, 140);
            //this.Hour[12] = GetJData(80, 160);
            //this.Hour[13] = GetJData(60, 160);
            //this.Hour[14] = GetJData(80, 160);
            //this.Hour[15] = GetJData(80, 160);
            //this.Hour[16] = GetJData(80, 160);
            //this.Hour[17] = GetJData(60, 140);
            //this.Hour[18] = GetJData(60, 80);
            //this.Hour[19] = GetJData(0, 6);
            //for (int i = 20; i < 24; i++)
            //{
            //    this.Hour[i] = GetJData(0, 0);
            //}
            //小时结束

            //当天
            this.D = GetJData(400, 1100);
            //
            this.W = this.D.Multiplication(7, random);
            this.TW = this.W.Multiplication(2, random);
            this.M = this.TW.Multiplication(2, random);
            this.Q = this.M.Multiplication(3, random);
            this.M6 = this.Q.Multiplication(2, random);
            this.Y = this.M6.Multiplication(2, random);
            this.LY = this.Y.Append(this.D);
        }

        private JData MultHour(JData[] jData)
        {
            JData jd = new JData();
            foreach (var item in jData)
            {
                jd = jd.Append(item);
            }
            return jd;
        }

        private static JData GetJData(int m, int n)
        {
            JData jdata = new JData();
            if (m == 0 && n == 0)
            {
                return jdata;
            }

            jdata.T[2] = random.Next(m, n); //总取票量
            jdata.T[3] = Convert.ToInt32((jdata.T[0] * random.Next(30, 80)) / 100); //有效服务人数
            jdata.T[4] = jdata.T[0] - jdata.T[1]; //弃票人数
            jdata.T[0] = random.Next(5, Convert.ToInt32(jdata.T[2] / 3)); //正在等待人数
            jdata.T[1] = random.Next(2, Convert.ToInt32(jdata.T[2]/4)); //正在办理人数

            //评价人数，未评价，未请评价，非常满意人数，满意人数，一般人数，不满意人数
            int ev = Convert.ToInt32((jdata.T[1] * random.Next(40, 80)) / 100);
            jdata.E[0] = ev; //评价人数
            jdata.E[1] = Convert.ToInt32(((jdata.T[1] - ev) * random.Next(60, 90)) / 100);//未评价
            jdata.E[2] = jdata.T[1] - jdata.E[0] - jdata.E[1];//未请评价
            jdata.E[3] = Convert.ToInt32((ev * random.Next(15, 25)) / 100);//非常满意人数
            jdata.E[5] = Convert.ToInt32((ev * random.Next(15, 25)) / 100);//一般人数
            jdata.E[6] = Convert.ToInt32((ev * random.Next(15, 25)) / 100);//不满意人数
            jdata.E[4] = ev - jdata.E[3] - jdata.E[5] - jdata.E[6];//满意人数(考虑现实生活中这种评价较多一些)

            //<=3分钟，3至5分钟，5至10分钟，10至20分钟，20分钟以上
            jdata.W[0] = Convert.ToInt32((jdata.T[1] * random.Next(15, 20)) / 100);//3--
            jdata.W[1] = Convert.ToInt32((jdata.T[1] * random.Next(15, 20)) / 100);//3-5
            jdata.W[2] = Convert.ToInt32((jdata.T[1] * random.Next(15, 20)) / 100);//5-10
            jdata.W[4] = Convert.ToInt32((jdata.T[1] * random.Next(15, 20)) / 100);//20++
            jdata.W[3] = jdata.T[1] - jdata.W[0] - jdata.W[1] - jdata.W[2] - jdata.W[4];//10-20分钟

            //平均服务时间，最长服务时间，最短服务时间
            jdata.S[0] = random.Next(5, 25);
            jdata.S[1] = random.Next(20, 70);
            jdata.S[2] = random.Next(2, 6);

            return jdata;
        }
    }

    [DataContractAttribute()]
    class JData
    {
        public Int32[] T { get; set; }
        public Int32[] E { get; set; }
        public Int32[] W { get; set; }
        public Int32[] S { get; set; }

        public JData()
        {
            this.T = new Int32[5];//正在等待人数, 正在办理人数, 取票总量, 弃票人数, 有效服务人数
            this.E = new Int32[7];
            this.W = new Int32[5];
            this.S = new Int32[3];
        }

        /// <summary>
        /// 递增n倍
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public JData Multiplication(int n, Random random)
        {
            JData newJData = new JData();
            for (int i = 0; i < newJData.E.Length; i++)
            {
                newJData.E[i] = this.E[i] * n;
            }
            for (int i = 0; i < newJData.T.Length; i++)
            {
                newJData.T[i] = this.T[i] * n;
            }
            for (int i = 0; i < newJData.W.Length; i++)
            {
                newJData.W[i] = this.W[i] * n;
            }
            newJData.S[0] = random.Next(5, 25);
            newJData.S[1] = random.Next(20, 70);
            newJData.S[2] = random.Next(2, 6);
            return newJData;
        }

        public JData Append(JData b)
        {
            JData jd = new JData();
            for (int i = 0; i < this.E.Length; i++)
                jd.E[i] = this.E[i] + b.E[i];

            for (int i = 0; i < this.S.Length; i++)
                jd.S[i] = Convert.ToInt32((this.S[i] + b.S[i]) / 2);

            for (int i = 0; i < this.T.Length; i++)
                jd.T[i] = this.T[i] + b.T[i];

            for (int i = 0; i < this.W.Length; i++)
                jd.W[i] = this.W[i] + b.W[i];

            return jd;
        }
    }
}
