using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree.DemoProject1
{
    public class ActiveContentExtension : IRun
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
            throw new NotImplementedException();
        }

        public event EventHandler OwnerChanged;

        #endregion
    }
}
