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
            Console.WriteLine("选项节个数：" + OptionManager.Instance.Options.Count);
            Console.WriteLine();
            Console.WriteLine("选项节名称：" + OptionManager.Instance.Options["student"].Name);

            Student student = (Student)OptionManager.Instance.Options["student"].Entity;
            Console.WriteLine("选项值：" + student.Age);
            Console.WriteLine("选项值：" + student.Id);
            Console.WriteLine("选项值：" + student.Name);
            Console.WriteLine("选项值：" + student.Sex);
            Console.WriteLine("选项值：" + student.Brithday);
            Console.WriteLine("选项值：" + student.Salary);
            Console.WriteLine();

            Option[] options = OptionManager.Instance.Options.GetItem("teacher");
            Console.WriteLine(options.Length);
            foreach (Option option in options)
            {
                Teacher teacher = (Teacher)OptionManager.Instance.Options["teacher"].Entity;
                Console.WriteLine("选项值：" + teacher.AAA);
                Console.WriteLine("选项值：" + teacher.BBB);
                Console.WriteLine("选项值：" + teacher.CCC);
                Console.WriteLine("选项值：" + teacher.DDD);
            }

            Console.ReadKey();
        }
    }
}
