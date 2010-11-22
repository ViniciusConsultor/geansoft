﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gean.Options
{
    public sealed partial class OptionForm : Form, IOptionPanelAction
    {
        private List<OptionPanel> OptionPanelList { get; protected set; }

        #region 单件实例

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static OptionForm ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton() { Instance = new OptionForm(); }
            internal static readonly OptionForm Instance = null;
        }

        #endregion

        private OptionForm()
        {
            InitializeComponent();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public bool ReSet()
        {
            throw new NotImplementedException();
        }
    }
}
