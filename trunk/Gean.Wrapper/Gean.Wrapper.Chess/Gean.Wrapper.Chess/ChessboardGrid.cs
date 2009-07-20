using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一个描述棋盘的每个格子类
    /// </summary>
    public class ChessboardGrid
    {
        internal ChessboardGrid(int x, int y, Enums.ChessboardGridSide side)
            : this(new ChessSquare(x, y), side) { }
        internal ChessboardGrid(ChessSquare square, Enums.ChessboardGridSide side)
        {
            this.Square = square;
            this.Side = side;
        }

        /// <summary>
        /// 返回与设置该棋格的坐标
        /// </summary>
        public ChessSquare Square { get; internal set; }
        /// <summary>
        /// 黑格,白格
        /// </summary>
        public Enums.ChessboardGridSide Side { get; internal set; }

        /// <summary>
        /// 获取或设置当前格子中拥有的棋子
        /// </summary>
        public Chessman OwnedChessman 
        {
            get { return this._ownedChessman; }
            set
            {
                OnPlayBefore(new PlayEventArgs(value));//注册落子前事件
                this._ownedChessman = value;
                OnPlayAfter(new PlayEventArgs(value));//注册落子后事件
            }
        }
        private Chessman _ownedChessman = null;

        /// <summary>
        /// 判断指定的棋子是否能够落入该棋格
        /// </summary>
        /// <param name="chessmanBase">指定的棋子</param>
        /// <returns></returns>
        internal bool IsUsable(Chessman chessmanBase)
        {
            return true;//TODO:判断棋子落入该棋格是否符合规则
        }

        public override string ToString()
        {
            return "Grid: " + this.Square.ToString();
        }

        /// <summary>
        /// 检索棋盘中能够移动到指定的棋格的指定类型的棋子
        /// </summary>
        /// <param name="type">指定的棋子类型</param>
        /// <param name="side">指定的棋子的战方</param>
        /// <returns></returns>
        internal Chessman FindMovableChessman(Enums.ChessmanType type, Enums.ChessmanSide side)
        {
            switch (type)
            {
                case Enums.ChessmanType.Rook:
                    return null;
                case Enums.ChessmanType.Knight:
                    return null;
                case Enums.ChessmanType.Bishop:
                    return null;
                case Enums.ChessmanType.Queen:
                    return null;
                case Enums.ChessmanType.King:
                    return null;
                case Enums.ChessmanType.Pawn:
                    return null;
                default:
                    return null;
            }
        }

        #region custom event

        /// <summary>
        /// 在该棋格中拥有的棋子发生变化的时候(变化前、即将发生变化)发生。
        /// (包括该在棋格落子和移走该棋格拥有的棋子)
        /// </summary>
        public event PlayBeforeEventHandler PlayBeforeEvent;
        protected virtual void OnPlayBefore(PlayEventArgs e)
        {
            if (PlayBeforeEvent != null) 
                PlayBeforeEvent(this, e);
        }
        public delegate void PlayBeforeEventHandler(object sender, PlayEventArgs e);

        /// <summary>
        /// 在该棋格中拥有的棋子发生变化后发生(包括该在棋格落子和移走该棋格拥有的棋子)
        /// </summary>
        public event PlayAfterEventHandler PlayAfterEvent;
        protected virtual void OnPlayAfter(PlayEventArgs e)
        {
            if (PlayAfterEvent != null)
                PlayAfterEvent(this, e);
        }
        public delegate void PlayAfterEventHandler(object sender, PlayEventArgs e);

        public class PlayEventArgs : ChessmanEventArgs
        {
            public PlayEventArgs(Chessman man)
                : base(man)
            {

            }
        }

        #endregion
    }
}
