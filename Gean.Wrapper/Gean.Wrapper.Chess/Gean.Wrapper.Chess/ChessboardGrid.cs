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
        public Square Square { get; set; }
        public Enums.ChessboardGridSide ChessboardGridSide { get; set; }

        internal ChessboardGrid(int x, int y, Chessboard board)
            : this(new Square(x, y), board) { }

        internal ChessboardGrid(Square square, Chessboard board)
        {
            this.Square = square;
            this.Parent = board;
        }

        /// <summary>
        /// 获取设置该棋格的父容器（棋盘）
        /// </summary>
        public Chessboard Parent { get; private set; }

        /// <summary>
        /// 获取或设置当前格子中拥有的棋子
        /// </summary>
        public ChessmanBase ChessmanOwner 
        {
            get { return this._chessmanOwner; }
            set
            {
                this._chessmanOwner = value;
                OnPlay(new PlayEventArgs(value));//注册落子事件
            }
        }
        private ChessmanBase _chessmanOwner;

        /// <summary>
        /// 判断指定的棋子是否能够落入该棋格
        /// </summary>
        /// <param name="chessmanBase">指定的棋子</param>
        /// <returns></returns>
        internal bool IsUsable(ChessmanBase chessmanBase)
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
        internal ChessmanBase FindMovableChessman(Enums.ChessmanType type, Enums.ChessmanSide side)
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
            public PlayEventArgs(ChessmanBase man)
                : base(man)
            {

            }
        }

        #endregion
    }
}
