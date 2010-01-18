using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using Gean.Data.Resources;
using System.Data.SqlTypes;
using System.Xml;
using System.Diagnostics;
using System.IO;
using Gean.Math;

namespace Gean.Data.Demo
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("..........");
            GetAllString("ABGK", 4);//I,O,Z,0,1,2,//ABCDEFGHJKLMNPQRSTUVWXY3456789

            char[] src = "ABCDEFGHJKLMNPQRSTUVWXY3456789".ToCharArray();
            //Permutations<char> permut = new Permutations<char>(src);
            
            

            Console.WriteLine();

            IDGenerator idg = new IDGenerator();
            for (int i = 0; i < 1; i++)
            {
                Console.WriteLine(idg.Generate());
            }
            Console.WriteLine();

            //DoIDTime();

            //Main01_SqlConnectionStringBuilder.Do();
            //Main02_AdoNet.Do();
            //Main03_SQLTextBuilder.Do();
            //Main05_ORM.Do();

            Console.WriteLine("End!... Press any key.");
            Console.ReadKey();

        }


        public static bool GetAllString(string inputString, int len)
        {
            if (len > inputString.Length) { return false; }
            try
            {
                Char[] chr = inputString.ToCharArray();

                File.WriteAllText(@"e:\My Desktop\file0.txt", "", Encoding.Default);

                for (int i = 0; i < len; i++) // 处理 i个字符长度的
                {
                    StreamReader read = new StreamReader(@"e:\My Desktop\file" + i + ".txt", Encoding.Default);
                    StreamWriter write = new StreamWriter(@"e:\My Desktop\file" + (i + 1) + ".txt", false, Encoding.Default);
                    string str = "";
                    while (true)
                    {
                        str = read.ReadLine();
                        if (str == null && i != 0)
                        { 
                            break; 
                        }
                        if (i == 0)
                        {
                            str = ""; 
                        }
                        for (int j = 0; j < chr.Length; j++)
                        {
                            if (str.IndexOf(chr[j]) == -1)   //保证不重复
                            {
                                write.WriteLine(str + chr[j].ToString());
                            }
                        }
                        if (i == 0) { break; }
                    }
                    read.Close();
                    write.Close();
                }
                File.Delete(@"e:\My Desktop\file0.txt");
            }
            catch (Exception) 
            {
                throw;
            }
            return true;
        }

        private static void DoIDTime()
        {
            UtilityRandom random = new UtilityRandom();
            random.Next();

            int count = 100 * 10000;

            Stopwatch sw = new Stopwatch();
            Console.WriteLine("Start......");

            //Gean.Data的Id生成器-----------------------
            long geanIdTime;
            sw.Reset();
            sw.Start();
            IDGenerator geanIdg = new IDGenerator();
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


            Console.WriteLine("Gean.Data的Id生成器： " + geanIdTime.ToString());
            Console.WriteLine("随机数字： " + randTime.ToString());
            Console.WriteLine("随机4位小写字母： " + randTime.ToString());
            Console.WriteLine("数字累计： " + gerTime.ToString());
            Console.WriteLine("IDGenerator生成： " + idgTime.ToString());
            Console.WriteLine("Guid： " + guidTime.ToString());
        }


    }
}
