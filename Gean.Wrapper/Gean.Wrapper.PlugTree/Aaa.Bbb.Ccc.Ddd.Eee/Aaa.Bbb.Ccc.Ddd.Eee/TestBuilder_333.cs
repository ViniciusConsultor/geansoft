using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree;

namespace Aaa.Bbb.Ccc.Ddd.Eee
{
    public class TestBuilder_333 : IBuilder
    {       
        #region IBuilder 成员

        public bool HandleConditions
        {
            get { return false; }
        }

        public object BuildItem(object caller, Aarhus.Wrapper.PlugTree.Components.Block block, System.Collections.ArrayList subItems)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
