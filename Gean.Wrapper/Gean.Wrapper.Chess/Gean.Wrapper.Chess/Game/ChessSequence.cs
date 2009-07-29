using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public sealed class ChessSequence : ChessStepPairSequence
    {
        public ChessSequence()
            : base("", "", 0)
        {
        }

        public override string ToString()
        {
            return this.SequenceToString();
        }
    }
}
