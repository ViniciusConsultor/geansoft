using System;
using System.Collections.Generic;
using System.Text;

namespace Pansoft.CQMS.Options
{
    public interface IOptionSerializable
    {
        String OptionLocalName { get; }
        Object GetValue(String arg);

    }
}
