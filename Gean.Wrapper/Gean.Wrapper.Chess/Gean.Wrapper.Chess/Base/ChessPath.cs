using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessPath
    {
        public static bool TryMoveInGrid(Chessman chessman, ChessGrid rid, ChessGame game, out Enums.Action action)
        {
            action = Enums.Action.General;
            return true;
        }
    }
}
