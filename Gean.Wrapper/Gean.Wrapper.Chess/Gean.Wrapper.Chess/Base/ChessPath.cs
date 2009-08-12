using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public static class ChessPath
    {
        /// <summary>
        /// 获取指定的棋子是否能够从指定的源棋格移动到指定的目标棋格，并返回该步棋的Enums.Action值
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        /// <param name="sourceGrid">指定的棋子所在的源棋格</param>
        /// <param name="targetGrid">指定的目标棋格</param>
        /// <param name="action">该步棋的Enums.Action值</param>
        /// <returns></returns>
        public static bool TryMoveIn(Chessman chessman, ChessGrid sourceGrid, ChessGrid targetGrid, out Enums.Action action)
        {
            if (chessman == null) throw new ArgumentNullException("Chessman cannot NULL.");
            if (sourceGrid == null) throw new ArgumentNullException("Source ChessGrid cannot NULL.");
            if (targetGrid == null) throw new ArgumentNullException("Target ChessGrid cannot NULL.");
            
            action = Enums.Action.Invalid;



            if (!Chessman.IsNullOrEmpty(targetGrid.Occupant))
                action = Enums.Action.Kill;
            else
                action = Enums.Action.General;
            return true;
        }

        public static ChessPosition GetSourcePosition(ChessStep step, Enums.ChessmanSide chessmanSide, ChessGame chessGame)
        {
            throw new NotImplementedException();
        }
    }
}
