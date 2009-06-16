using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.PlugTree.ApplicationClass
{
    public class EditSaveMenuItemCondition : ICondition
    {
        #region ICondition 成员

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public object Owner
        {
            get { return "Gean|PlugTree.Demo|MainMenu|File|YellowForm|Project|BigRunnerMenu"; }
        }

        public bool SetByCondition(PlugPath path)
        {
            return true;
        }

        #endregion
    }
}
