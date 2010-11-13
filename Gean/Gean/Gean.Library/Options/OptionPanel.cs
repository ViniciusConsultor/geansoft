using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Gean.Options
{
    public abstract partial class OptionPanel : Component
    {
        public OptionPanel()
        {
            InitializeComponent();
        }

        public OptionPanel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
