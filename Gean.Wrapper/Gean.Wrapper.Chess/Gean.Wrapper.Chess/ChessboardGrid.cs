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
        public AxisX X { get; set; }
        public AxisY Y { get; set; }
        public Enums.ChessboardGridSide ChessboardGridSide { get; set; }

        internal ChessboardGrid(int x, int y, Chessboard board)
            : this(new AxisX(x), new AxisY(y), board) { }

        internal ChessboardGrid(AxisX x, AxisY y, Chessboard board)
        {
            this.X = x;
            this.Y = y;
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
            return "Grid:" + this.X.ToString() + this.Y.ToString();
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

        /// <summary>
        /// 棋盘的X轴
        /// </summary>
        public struct AxisX
        {
            public int X
            {
                get { return this._x; }
            }
            private int _x;
            private char _word;

            public AxisX(int x)
            {
                this._x = 0;
                this._word = 'z';
                if (x < 1 || x > 8)
                {
                    this._x = x;
                    switch (x)
                    {
                        case 1:
                            this._word = 'a';
                            break;
                        case 2:
                            this._word = 'b';
                            break;
                        case 3:
                            this._word = 'c';
                            break;
                        case 4:
                            this._word = 'd';
                            break;
                        case 5:
                            this._word = 'e';
                            break;
                        case 6:
                            this._word = 'f';
                            break;
                        case 7:
                            this._word = 'g';
                            break;
                        case 8:
                            this._word = 'h';
                            break;
                        default:
                            this._word = 'z';
                            break;
                    }
                }
            }
            public AxisX(char c)
            {
                this._x = 0;
                this._word = 'z';

                if (c < 'a' || c > 'h')
                {
                    string str = (Convert.ToString(c)).Trim().ToLowerInvariant();
                    this._word = char.Parse(str);
                    switch (c)
                    {
                        case 'a':
                            this._x = 1;
                            break;
                        case 'b':
                            this._x = 2;
                            break;
                        case 'c':
                            this._x = 3;
                            break;
                        case 'd':
                            this._x = 4;
                            break;
                        case 'e':
                            this._x = 5;
                            break;
                        case 'f':
                            this._x = 6;
                            break;
                        case 'g':
                            this._x = 7;
                            break;
                        case 'h':
                            this._x = 8;
                            break;
                    }
                }
            }

            public override string ToString()
            {
                return Convert.ToString(this._word);
            }
        }

        /// <summary>
        /// 棋盘的Y轴
        /// </summary>
        public struct AxisY
        {
            public int Y
            {
                get { return this._y; }
            }
            private int _y;

            public AxisY(int y)
            {
                this._y = y;
            }

            public override string ToString()
            {
                return this._y.ToString();
            }
        }

    }
}
