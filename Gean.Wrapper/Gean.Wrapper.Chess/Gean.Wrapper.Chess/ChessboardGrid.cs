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
            : this(new Square(x, y), side) { }
        internal ChessboardGrid(Square square, Enums.ChessboardGridSide side)
        {
            this.Square = square;
            this.Side = side;
        }

        /// <summary>
        /// 返回与设置该棋格的坐标
        /// </summary>
        public Square Square { get; internal set; }
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
                this._ownedChessman = value;
                OnPlay(new PlayEventArgs(value));//注册落子事件
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
        /// 在该棋格中出现落子时发生
        /// </summary>
        public event PlayEventHandler PlayEvent;
        protected virtual void OnPlay(PlayEventArgs e)
        {
            if (PlayEvent != null) 
                PlayEvent(this, e);
        }
        public delegate void PlayEventHandler(object sender, PlayEventArgs e);
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
