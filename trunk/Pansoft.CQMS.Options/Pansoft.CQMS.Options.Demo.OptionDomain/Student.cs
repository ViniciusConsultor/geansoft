using System;
using System.Collections.Generic;
using System.Text;

namespace Pansoft.CQMS.Options.Demo.OptionDomain
{
    [Option("student", "学生")]
    public class Student
    {
        [OptionValue("studentName","黄阳")]
        public string Name { get; private set; }

        [OptionValue("studentId", "huangyang")]
        public string Id { get; private set; }

        [OptionValue("studentAge", 60)]
        public int Age { get; private set; }

        [OptionValue("studentSex", false)]
        public bool Sex { get; private set; }

        [OptionValue("studentBrithday", "1848.12.12 23:20:01")]
        public DateTime Brithday { get; private set; }

        [OptionValue("studentSalary", 12200.00)]
        public float Salary { get; private set; }

    }
}
