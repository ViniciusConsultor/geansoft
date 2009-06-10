using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree.Components;
using System.Collections;

namespace Gean.Wrapper.PlugTree
{
    public class ClassBuilder: IBuilder
    {
        #region IBuilder 成员

        public bool HandleConditions
        {
            get { throw new NotImplementedException(); }
        }

        public object BuildItem(object caller, PlugAtom plugAtom, ArrayList subItems)
        {
            return plugAtom.OwnerPlug.CreateObject((string)plugAtom.Properties["class"]);
        }

        #endregion
    }
}
