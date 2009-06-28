using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree.DemoProject1
{
    public class ClippersProducer : IProducer 
    {
        #region IProducer 成员

        public string Name
        {
            get { return this.GetType().Name; }
        }

        public object CreateObject(Plug plug, object caller)
        {
            return null;
        }

        #endregion
    }
}
