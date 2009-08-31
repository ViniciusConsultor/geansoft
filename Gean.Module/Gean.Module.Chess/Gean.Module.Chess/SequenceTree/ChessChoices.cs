using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class ChessChoices : ChessSequence, IItem
    {
        #region ISequenceItem 成员

        public string Value
        {
            get { return this.ToString(); }
        }

        #endregion
    }
}
