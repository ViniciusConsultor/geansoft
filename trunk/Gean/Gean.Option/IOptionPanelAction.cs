using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Options
{
    public interface IOptionPanelAction
    {
        bool Save();
        bool ReSet();
    }
}
