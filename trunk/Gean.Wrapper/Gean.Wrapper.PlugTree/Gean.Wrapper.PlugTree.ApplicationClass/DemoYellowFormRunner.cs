using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree.Demo;
using System.Windows.Forms;

namespace Gean.Wrapper.PlugTree.ApplicationClass
{
    public class DemoYellowFormRunner : IRun
    {
        #region IRun 成员

        public object Owner
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Run()
        {
            DemoYellowForm form = new DemoYellowForm();
            form.ShowDialog(Program.MainWorkBench as Form);
        }

        public event EventHandler OwnerChanged;

        #endregion
    }
}
