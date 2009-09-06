using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    public class PositionPair : Pair<Position, Position>
    {
        public PositionPair(Position srcPos, Position tgtPos)
            : base(srcPos, tgtPos)
        {

        }
    }
}
