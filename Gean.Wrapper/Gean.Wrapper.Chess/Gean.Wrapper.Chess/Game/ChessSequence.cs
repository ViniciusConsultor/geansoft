using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    
    /// <summary>
    /// 一个棋局记录中核心棋招序列
    /// </summary>
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
