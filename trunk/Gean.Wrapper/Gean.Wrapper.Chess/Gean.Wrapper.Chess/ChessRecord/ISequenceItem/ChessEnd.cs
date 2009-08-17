using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessEnd : ISequenceItem
    {
        public ChessEnd()
        {

        }
        public ChessEnd(string value)
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
