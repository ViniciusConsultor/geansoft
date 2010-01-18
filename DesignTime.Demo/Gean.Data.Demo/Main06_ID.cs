using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Gean.Data.Demo
{
    class Main06_ID
    {
        private static IIDGenerator idg = new IDGenerator();

        public static void DoTest()
        {
            System.Timers.Timer timer1 = new System.Timers.Timer();
            timer1.Start();
            timer1.Interval = 11000;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(CreateId);

            System.Timers.Timer timer2 = new System.Timers.Timer();
            timer2.Start();
            timer2.Interval = 22000;
            timer2.Elapsed += new System.Timers.ElapsedEventHandler(CreateId);

            System.Timers.Timer timer3 = new System.Timers.Timer();
            timer3.Start();
            timer3.Interval = 33000;
            timer3.Elapsed += new System.Timers.ElapsedEventHandler(CreateId);

            System.Timers.Timer timer4 = new System.Timers.Timer();
            timer4.Start();
            timer4.Interval = 44000;
            timer4.Elapsed += new System.Timers.ElapsedEventHandler(CreateId);

            System.Timers.Timer timer5 = new System.Timers.Timer();
            timer5.Start();
            timer5.Interval = 5500;
            timer5.Elapsed += new System.Timers.ElapsedEventHandler(CreateId);

            System.Timers.Timer timer6 = new System.Timers.Timer();
            timer6.Start();
            timer6.Interval = 6600;
            timer6.Elapsed += new System.Timers.ElapsedEventHandler(CreateId);

            System.Timers.Timer timer7 = new System.Timers.Timer();
            timer7.Start();
            timer7.Interval = 7700;
            timer7.Elapsed += new System.Timers.ElapsedEventHandler(CreateId);

            System.Timers.Timer timer8 = new System.Timers.Timer();
            timer8.Start();
            timer8.Interval = 8800;
            timer8.Elapsed += new System.Timers.ElapsedEventHandler(CreateId);

            System.Timers.Timer timer9 = new System.Timers.Timer();
            timer9.Start();
            timer9.Interval = 9900;
            timer9.Elapsed += new System.Timers.ElapsedEventHandler(CreateId);

        }

        private static void CreateId(object sender, System.Timers.ElapsedEventArgs e)
        {
            while (true)
            {
                string id = idg.Generate();
                Console.WriteLine(id);
            }
        }

        public static void DoTimeTest()
        {
            UtilityRandom random = new UtilityRandom();
            random.Next();
            IDGenerator geanIdg = new IDGenerator();
            geanIdg.Generate();

            int count = 500 * 10000;

            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Start......");

            //Gean.Data的Id生成器-----------------------
            long geanIdTime;
            sw.Reset();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                string id = geanIdg.Generate();
            }
            sw.Stop();
            geanIdTime = sw.ElapsedMilliseconds;

            //随机数字-----------------------
            long randNumTime;
            sw.Reset();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                string id = random.Next(1, count).ToString();
            }
            sw.Stop();
            randNumTime = sw.ElapsedMilliseconds;

            //随机4位小写字母-----------------------
            long randTime;
            sw.Reset();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                string id = random.GetString(4, UtilityRandom.RandomCharType.Lowercased);
            }
            sw.Stop();
            randTime = sw.ElapsedMilliseconds;

            //数字累计-----------------------
            long gerTime;
            sw.Reset();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                string id = (i + i + i).ToString();
            }
            sw.Stop();
            gerTime = sw.ElapsedMilliseconds;

            //IDGenerator生成-----------------------
            long idgTime;
            sw.Reset();
            sw.Start();
            UUIDGenerator idg = new UUIDGenerator();
            for (int i = 0; i < count; i++)
            {
                string id = idg.Generate();
            }
            sw.Stop();
            idgTime = sw.ElapsedMilliseconds;

            //Guid-----------------------
            long guidTime;
            sw.Reset();
            sw.Start();
            for (int i = 0; i < count; i++)
            {
                string id = Guid.NewGuid().ToString();
            }
            sw.Stop();
            guidTime = sw.ElapsedMilliseconds;


            Console.WriteLine("Gean.Data的Id生成器： " + geanIdTime.ToString() + Sudu(geanIdTime, count));
            Console.WriteLine("随机数字： " + randNumTime.ToString() + Sudu(randNumTime, count));
            Console.WriteLine("随机4位小写字母： " + randTime.ToString() + Sudu(randTime, count));
            Console.WriteLine("数字累计： " + gerTime.ToString() + Sudu(gerTime, count));
            Console.WriteLine("IDGenerator生成： " + idgTime.ToString() + Sudu(idgTime, count));
            Console.WriteLine("Guid： " + guidTime.ToString() + Sudu(guidTime, count));
        }

        static string Sudu(long time, int count)
        {
            double dou = count / time;
            return "，每秒生成：" + ((int)(dou * 1000)).ToString();
        }

    }
}
