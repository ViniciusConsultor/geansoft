using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree;

namespace Gean.Wrapper.PlugTree.ApplicationClass
{
    public class DemoRunner : IRun
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
            System.Windows.Forms.MessageBox.Show(this.GetType().ToString());
        }

        public event EventHandler OwnerChanged;

        #endregion
    }
}
