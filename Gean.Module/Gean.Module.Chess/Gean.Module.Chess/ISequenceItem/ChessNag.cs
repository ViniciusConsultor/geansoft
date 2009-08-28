using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class ChessNag : ISequenceItem
    {
        public ChessNag()
        {

        }
        public ChessNag(string value)
        {
            this.Value = value;
        }

        public string Value { get; set; }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
