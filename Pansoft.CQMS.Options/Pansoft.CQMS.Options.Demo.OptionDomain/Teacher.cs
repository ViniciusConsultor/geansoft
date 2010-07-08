using System;
using System.Collections.Generic;
using System.Text;

namespace Pansoft.CQMS.Options.Demo.OptionDomain
{
    [Option("teacher", "老师", true, "teacherList")]
    public class Teacher
    {
        [OptionValue("AAA", "a-a-a")]
        public string AAA { get; private set; }
        [OptionValue("BBB", "b-b-b")]
        public string BBB { get; private set; }
        [OptionValue("CCC", "c-c-c")]
        public string CCC { get; private set; }
        [OptionValue("DDD", "d-d-d")]
        public string DDD { get; private set; }
    }
}
