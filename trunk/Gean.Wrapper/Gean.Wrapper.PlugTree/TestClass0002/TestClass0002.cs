using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree;

namespace TestClass0002
{
    public class TestClass0002 :IRun
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
