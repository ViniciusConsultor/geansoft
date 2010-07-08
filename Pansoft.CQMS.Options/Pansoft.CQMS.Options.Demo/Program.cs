using System;
using System.Collections.Generic;
using System.Text;
using Pansoft.CQMS.Options.Demo.OptionDomain;

namespace Pansoft.CQMS.Options.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            OptionManager.Instance.Initializes("my.option");

            Console.WriteLine("所有选项节共计：" + OptionManager.Instance.Options.Count);
            Console.WriteLine();
            Console.WriteLine("--------");
            Console.WriteLine("单选项节测试");
            Console.WriteLine("选项节名称：" + OptionManager.Instance.Options["student"].Name);
            Console.WriteLine("选项节名称：" + OptionManager.Instance.Options["student"].XmlElement.OuterXml);

            Student student = (Student)OptionManager.Instance.Options["student"].Entity;
            Console.WriteLine("选项值Name：\t\t" + student.Name);
            Console.WriteLine("选项值Id：\t\t" + student.Id);
            Console.WriteLine("选项值Age：\t\t" + student.Age);
            Console.WriteLine("选项值Sex：\t\t" + student.Sex);
            Console.WriteLine("选项值Brithday：\t" + student.Brithday);
            Console.WriteLine("选项值Salary：\t\t" + student.Salary);
            Console.WriteLine();

            Console.WriteLine("--------");
            Console.WriteLine("集合选项节测试");
            Option[] options = OptionManager.Instance.Options.GetItem("teacher");
            Console.WriteLine("teacher选项个数：" + options.Length);
            foreach (Option option in options)
            {
                Console.WriteLine("--------");
                Teacher teacher = (Teacher)option.Entity;
                Console.WriteLine("选项值：" + teacher.AAA);
                //Console.WriteLine("选项值：" + teacher.BBB);
                //Console.WriteLine("选项值：" + teacher.CCC);
                //Console.WriteLine("选项值：" + teacher.DDD);
            }

            Console.WriteLine("--------");
            Console.WriteLine("选项值更改测试");
            OptionManager.Instance.Options["student"].SetOptionValue("studentName", "马英豪");
            student = (Student)OptionManager.Instance.Options["student"].Entity;
            Console.WriteLine("选项值Name：\t\t" + student.Name);
            Console.WriteLine("选项节名称：" + OptionManager.Instance.Options["student"].XmlElement.OuterXml);

            Console.WriteLine("--------");
            Console.WriteLine("选项重载、保存、备份测试");

            Console.WriteLine(">>> 选项重载测试");
            OptionManager.Instance.ReLoad();
            student = (Student)OptionManager.Instance.Options["student"].Entity;
            Console.WriteLine("选项值Name：\t\t" + student.Name);
            Console.WriteLine("选项节名称：" + OptionManager.Instance.Options["student"].XmlElement.OuterXml);

            Console.WriteLine(">>> 选项保存测试");
            OptionManager.Instance.Options["student"].SetOptionValue("studentName", "马英豪");
            OptionManager.Instance.Save();
            student = (Student)OptionManager.Instance.Options["student"].Entity;
            Console.WriteLine("选项值Name：\t\t" + student.Name);
            Console.WriteLine("选项节名称：" + OptionManager.Instance.Options["student"].XmlElement.OuterXml);


            Console.ReadKey();
        }
    }
}
