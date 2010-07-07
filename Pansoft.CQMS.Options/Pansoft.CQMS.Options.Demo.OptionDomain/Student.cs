using System;
using System.Collections.Generic;
using System.Text;

namespace Pansoft.CQMS.Options.Demo.OptionDomain
{
    [OptionFile("student.option")]
    public class Student : IOptionSerializable
    {
        [Option("name","名字","studentstudent")]
        public string Name { get; set; }
        [Option("id","abcdddd","defaultid")]
        public string Id { get; set; }

        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }
        public int MyProperty3 { get; set; }
        public int MyProperty4 { get; set; }
        public int MyProperty5 { get; set; }
        public int MyProperty6 { get; set; }
        public int MyProperty7 { get; set; }
        public int MyProperty8 { get; set; }
        public int MyProperty9 { get; set; }

        public string OptionLocalName
        {
            get { throw new NotImplementedException(); }
        }

        public object GetValue(string arg)
        {
            throw new NotImplementedException();
        }
    }
}
