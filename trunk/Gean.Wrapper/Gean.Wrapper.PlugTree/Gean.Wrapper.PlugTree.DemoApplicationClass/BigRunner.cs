using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree;
using Gean.Wrapper.PlugTree.Demo;

namespace Gean.Wrapper.PlugTree.ApplicationClass
{
    public class BigRunner : IRun
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
            Program.MainWorkBench.MessageBench = DateTime.Now.ToLongTimeString() + " | " + this.GetType().ToString();
        }

        public event EventHandler OwnerChanged;

        #endregion
    }
}
