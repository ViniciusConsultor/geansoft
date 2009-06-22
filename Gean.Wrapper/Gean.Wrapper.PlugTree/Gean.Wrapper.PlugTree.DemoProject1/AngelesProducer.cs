using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree.DemoProject1
{
    public class AngelesProducer : IProducer 
    {
        #region IProducer 成员

        public string Name
        {
            get { return this.GetType().Name; }
        }

        public object CreateObject(object caller, Plug plug)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
