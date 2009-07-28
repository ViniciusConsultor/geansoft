using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 坐标集合，一般是应用在棋子类型中，表示一个棋子绑定的路径，该路径由一个一个的坐标组成
    /// </summary>
    public class ChessGirdCollection : Stack<ChessGirdCollection.GridAndAction>
    {



        public class GridAndAction
        {
            public ChessGrid Grid { get; private set; }
            public Enums.Action Action { get; private set; }
            public GridAndAction(ChessGrid rid, Enums.Action action)
            {
                this.Grid = rid;
                this.Action = action;
            }
        }
    }
}