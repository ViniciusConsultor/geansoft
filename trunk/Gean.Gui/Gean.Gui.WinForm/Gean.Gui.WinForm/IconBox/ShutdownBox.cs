﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Gean.Gui.WinForm.IconBox
{
    public class ShutdownBox : IconBox
    {
        protected override Icon CoreIcon
        {
            get { return Gean.Gui.WinForm.Properties.Resources.Shutdown; }
        }
    }
}
