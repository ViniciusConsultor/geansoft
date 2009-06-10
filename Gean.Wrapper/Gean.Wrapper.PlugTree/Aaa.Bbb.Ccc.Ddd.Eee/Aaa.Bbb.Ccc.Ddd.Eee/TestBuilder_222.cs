using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree;
using Gean.Framework;
using Gean.Wrapper.PlugTree.Components;
using System.Collections;

namespace Aaa.Bbb.Ccc.Ddd.Eee
{
    public class TestBuilder_222 : IBuilder
    {       
        #region IBuilder 成员

        /// <summary>
		/// Gets if the doozer handles codon conditions on its own.
		/// If this property return false, the item is excluded when the condition is not met.
		/// </summary>
        public bool HandleConditions
        {
            get { return true; }
        }
		
		public object BuildItem(object caller, Block block, ArrayList subItems)
		{
            return "TestBuilder_222_String";
		}

        #endregion
    }
}
