﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessChoices : ChessSequence, ISequenceItem
    {
        #region ISequenceItem 成员

        public string Value
        {
            get { return this.ToString(); }
            set { ChessSequence.Parse(value); }
        }

        #endregion
    }
}
