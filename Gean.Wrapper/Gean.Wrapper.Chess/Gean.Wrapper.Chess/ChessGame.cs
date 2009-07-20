using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 这是描述一盘棋局的类
    /// </summary>
    public class ChessGame : IEnumerable<ChessSquare>
    {
        private ChessSquare[,] _squares = new ChessSquare[8, 8];
        private ChessmanCollection _chessmans = new ChessmanCollection();

        public ChessGame()
        {
            this.SetSquare();
        }

        /// <summary>
        /// 获取棋盘的棋局是否是开局状态。
        /// 当棋格全部实例化，棋子全部实例化并落入指定棋格后，该值为False。
        /// </summary>
        public bool IsOpennings
        {
            get { return this._isOpennings; }
        }
        private bool _isOpennings = true;

        /// <summary>
        /// 获取指定坐标值的棋格
        /// </summary>
        public ChessSquare this[int x, int y]
        {
            get { return this.GetSquare(x, y); }
        }

        /// <summary>
        /// 初始化棋格（一个棋盘由64个棋格组成，该方法将初始化整个棋盘的每个棋格）
        /// </summary>
        private void SetSquare()
        {
            for (int x = 0; x < _squares.GetLength(0); x++)
            {
                for (int y = 0; y < _squares.GetLength(1); y++)
                {
                    if ((y % 2) == 0)
                    {
                        if ((x % 2) == 0)
                            _squares[x, y] = new ChessSquare(x + 1, y + 1, Enums.ChessSquareSide.White);
                        else
                            _squares[x, y] = new ChessSquare(x + 1, y + 1, Enums.ChessSquareSide.Black);
                    }
                    else
                    {
                        if ((x % 2) == 0)
                            _squares[x, y] = new ChessSquare(x + 1, y + 1, Enums.ChessSquareSide.Black);
                        else
                            _squares[x, y] = new ChessSquare(x + 1, y + 1, Enums.ChessSquareSide.White);
                    }
                }
            }
        }

        /// <summary>
        /// 初始化开局棋子(32个棋子)
        /// </summary>
        internal void SetOpenningsChessmans()
        {
            this.SetOpenningsChessmans(Chessman.GetOpennings().ToArray());
        }

        /// <summary>
        /// 初始化指定的开局棋子集合，该方法一般使用场合为残局类，中盘类棋局
        /// </summary>
        internal void SetOpenningsChessmans(IEnumerable<Chessman> chessmans)
        {
            this._chessmans.AddRange(chessmans);
            foreach (Chessman man in chessmans)
            {
                this.SetSquareOwnedChessman(man, man.Squares.Peek());
            }
            this._isOpennings = false;//棋子设置完毕，将开局判断设置为false
            //注册开局设置结束事件
            OnSetOpenningsFinished(new ChessboardEventArgs(this));
        }

        /// <summary>
        /// 设置指定的棋格拥有的棋子。(下棋的动作:移动棋子到指定的棋格,在初始化时,可以理解为是摆棋)
        /// </summary>
        /// <param name="newSquare">棋子将被移动到的指定棋格的坐标</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ChessmanMovedException"></exception>
        public ChessStep SetSquareOwnedChessman(Chessman man, ChessSquare newSquare)
        {
            if (man == null)
                throw new ArgumentOutOfRangeException("Chessman:chessman is Null.");
            if (newSquare == null)
                throw new ArgumentOutOfRangeException("Square:newSquare is Null.");
            ChessSquare oldSquare = man.Squares.Peek();

            ChessSquare oldGrid = this.GetSquare(oldSquare);
            ChessSquare newGrid = this.GetSquare(newSquare);
            if (newGrid.OwnedChessman != Chessman.Empty)
            {
                if (newGrid.OwnedChessman.ChessmanSide == man.ChessmanSide)//新棋格拥有的棋子与将要移动棋子是一样的战方时
                {
                    throw new ChessmanMovedException(
                        string.Format("{0} and {1} is same Side, cannot move!",
                        newGrid.OwnedChessman.ToString(), man.ToString()));
                }
            }

            ChessmanMoveEventArgs e = new ChessmanMoveEventArgs(man, man.Squares.Peek(), newSquare);

            //注册移动前事件
            OnMoving(e);

            if (!this._isOpennings)//非初始化棋局时
            {
                man.Squares.Add(newSquare);
                this.GetSquare(oldSquare).OwnedChessman = Chessman.Empty;//将棋子的历史棋格的棋子状态置为空
            }

            Enums.AccessorialAction action = Enums.AccessorialAction.General;

            if (newGrid.OwnedChessman != Chessman.Empty)
            {
                //注册棋子即将被杀死的事件
                OnKilling(new ChessmanKillEventArgs(man, newGrid));

                //新棋格中如有棋子，置该棋子为杀死状态
                newGrid.OwnedChessman.IsKilled = true;
                action = Enums.AccessorialAction.Kill;

                //注册棋子被杀死后的事件
                OnKilled(new ChessmanKillEventArgs(man, newGrid));
            }

            //绑定新棋格拥有的棋子
            this.GetSquare(newSquare).OwnedChessman = man;
            
            //注册移动后事件
            OnMoved(e);

            //生成一个棋步
            ChessStep step = new ChessStep(man.ChessmanSide, man.ChessmanType, newSquare, oldSquare, action);
            return step;
        }

        /// <summary>
        /// 获取指定坐标的棋格
        /// </summary>
        /// <param name="x">指定坐标的x轴</param>
        /// <param name="y">指定坐标的y轴</param>
        /// <returns></returns>
        public ChessSquare GetSquare(int x, int y)
        {
            return this._squares[x - 1, y - 1];
        }
        /// <summary>
        /// 获取指定坐标的棋格
        /// </summary>
        /// <param name="square">指定坐标</param>
        /// <returns></returns>
        public ChessSquare GetSquare(ChessSquare square)
        {
            return this.GetSquare(square.X, square.Y);
        }

        /// <summary>
        /// 获取所有黑色棋格
        /// </summary>
        public ChessSquare[] GetBlackSquares()
        {
            List<ChessSquare> squares = new List<ChessSquare>();
            foreach (ChessSquare square in this)
            {
                if (square.ChessSquareSide == Enums.ChessSquareSide.Black)
                    squares.Add(square);
            }
            return squares.ToArray();
        }
        /// <summary>
        /// 获取所有白色棋格
        /// </summary>
        public ChessSquare[] GetWhiteSquares()
        {
            List<ChessSquare> squares = new List<ChessSquare>();
            foreach (ChessSquare square in this)
            {
                if (square.ChessSquareSide == Enums.ChessSquareSide.White)
                    squares.Add(square);
            }
            return squares.ToArray();
        }

        /// <summary>
        /// 获取所有活着的棋子
        /// </summary>
        public Chessman[] GetLivingChessmans()
        {
            ChessmanCollection mans = new ChessmanCollection();
            foreach (Chessman man in this._chessmans)
            {
                if (man.IsKilled == false)
                    mans.Add(man);
            }
            return mans.ToArray();
        }
        /// <summary>
        /// 获取所有被杀死的棋子
        /// </summary>
        /// <returns></returns>
        public Chessman[] GetKilledChessmans()
        {
            ChessmanCollection mans = new ChessmanCollection();
            foreach (Chessman man in this._chessmans)
            {
                if (man.IsKilled == true)
                    mans.Add(man);
            }
            return mans.ToArray();
        }

        #region IEnumerable<ChessSquare> 成员

        public IEnumerator<ChessSquare> GetEnumerator()
        {
            return (IEnumerator<ChessSquare>)this._squares.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._squares.GetEnumerator();
        }

        #endregion

        #region custom event

        /// <summary>
        /// 在棋子被移动的时候发生
        /// </summary>
        public event MovingEventHandler ChessmanMovingEvent;
        protected virtual void OnMoving(ChessmanMoveEventArgs e)
        {
            if (ChessmanMovingEvent != null)
                ChessmanMovingEvent(this, e);
        }
        public delegate void MovingEventHandler(object sender, ChessmanMoveEventArgs e);

        /// <summary>
        /// 在棋子被移动后发生
        /// </summary>
        public event MovedEventHandler ChessmanMovedEvent;
        protected virtual void OnMoved(ChessmanMoveEventArgs e)
        {
            if (ChessmanMovedEvent != null)
                ChessmanMovedEvent(this, e);
        }
        public delegate void MovedEventHandler(object sender, ChessmanMoveEventArgs e);

        /// <summary>
        /// 包含棋子移动事件的数据
        /// </summary>
        public class ChessmanMoveEventArgs : ChessmanEventArgs
        {
            public ChessSquare OldSquare { get; set; }
            public ChessSquare NewSquare { get; set; }
            public ChessmanMoveEventArgs(Chessman chessman, ChessSquare oldSquare, ChessSquare newSquare)
                : base(chessman)
            {
                this.OldSquare = oldSquare;
                this.NewSquare = newSquare;
            }
        }

        /// <summary>
        /// 在棋局开始的时候发生
        /// </summary>
        public event ChessboardOpeningEventHandler ChessboardOpeningEvent;
        protected virtual void OnChessboardOpening(ChessboardEventArgs e)
        {
            if (ChessboardOpeningEvent != null)
                ChessboardOpeningEvent(this, e);
        }
        public delegate void ChessboardOpeningEventHandler(object sender, ChessboardEventArgs e);

        /// <summary>
        /// 在棋局开始后发生
        /// </summary>
        public event ChessboardOpenedEventHandler ChessboardOpenedEvent;
        protected virtual void OnChessboardOpened(ChessboardEventArgs e)
        {
            if (ChessboardOpenedEvent != null)
                ChessboardOpenedEvent(this, e);
        }
        public delegate void ChessboardOpenedEventHandler(object sender, ChessboardEventArgs e);

        /// <summary>
        /// 在棋局结束的时候发生
        /// </summary>
        public event ChessboardFinishingEventHandler ChessboardFinishingEvent;
        protected virtual void OnChessboardFinishing(ChessboardEventArgs e)
        {
            if (ChessboardFinishingEvent != null)
                ChessboardFinishingEvent(this, e);
        }
        public delegate void ChessboardFinishingEventHandler(object sender, ChessboardEventArgs e);

        /// <summary>
        /// 在棋局结束后发生
        /// </summary>
        public event ChessboardFinishedEventHandler ChessboardFinishedEvent;
        protected virtual void OnChessboardFinished(ChessboardEventArgs e)
        {
            if (ChessboardFinishedEvent != null)
                ChessboardFinishedEvent(this, e);
        }
        public delegate void ChessboardFinishedEventHandler(object sender, ChessboardEventArgs e);

        /// <summary>
        /// 在开局设置完毕时发生
        /// </summary>
        public event SetOpenningsFinishedEventHandler SetOpenningsFinishedEvent;
        protected virtual void OnSetOpenningsFinished(ChessboardEventArgs e)
        {
            if (SetOpenningsFinishedEvent != null)
                SetOpenningsFinishedEvent(this, e);
        }
        public delegate void SetOpenningsFinishedEventHandler(object sender, ChessboardEventArgs e);

        public class ChessboardEventArgs : EventArgs
        {
            public ChessGame Chessboard { get; private set; }
            public ChessboardEventArgs(ChessGame board)
            {
                this.Chessboard = board;
            }
        }

        /// <summary>
        /// 在该棋子被杀死后发生
        /// </summary>
        public event KilledEventHandler KilledEvent;
        protected virtual void OnKilled(ChessmanKillEventArgs e)
        {
            if (KilledEvent != null)
                KilledEvent(this, e);
        }
        public delegate void KilledEventHandler(object sender, ChessmanKillEventArgs e);

        /// <summary>
        /// 在该棋子正在被杀死（也可理解为，即将被杀死时）发生
        /// </summary>
        public event KillingEventHandler KillingEvent;
        protected virtual void OnKilling(ChessmanKillEventArgs e)
        {
            if (KillingEvent != null)
                KillingEvent(this, e);
        }
        public delegate void KillingEventHandler(object sender, ChessmanKillEventArgs e);

        /// <summary>
        /// 包含棋子杀死事件的数据
        /// </summary>
        public class ChessmanKillEventArgs : ChessmanEventArgs
        {
            /// <summary>
            /// 被杀死的棋子所在棋格
            /// </summary>
            public ChessSquare CurrGrid { get; private set; }
            /// <param name="man">被杀死的棋子</param>
            /// <param name="currGrid">被杀死的棋子所在棋格</param>
            public ChessmanKillEventArgs(Chessman man, ChessSquare currGrid)
                : base(man)
            {
                this.CurrGrid = currGrid;
            }
        }

        #endregion
    }
}
/*
        public override ChessboardGrid[] GetGridsByPath()
        {
            List<ChessboardGrid> grids = new List<ChessboardGrid>();
            int x = this.GridOwner.Square.X;
            int y = this.GridOwner.Square.Y;

            for (int i = x - 1; i >= Utility.LEFT; i--)//当前点向左
            {
                ChessboardGrid gr = this.GridOwner.Parent.GetGrid(i, y);
                grids.Add(gr);
                if (gr.ChessmanOwner != null)
                    break;
            }
            for (int i = x + 1; i <= Utility.RIGHT; i++)//当前点向右
            {
                ChessboardGrid gr = this.GridOwner.Parent.GetGrid(i, y);
                grids.Add(gr);
                if (gr.ChessmanOwner != null)
                    break;
            }
            for (int i = y - 1; i >= Utility.FOOTER; i--)//当前点向下
            {
                ChessboardGrid gr = this.GridOwner.Parent.GetGrid(x, i);
                grids.Add(gr);
                if (gr.ChessmanOwner != null)
                    break;
            }
            for (int i = y + 1; i <= Utility.TOP; i++)//当前点向上
            {
                ChessboardGrid gr = this.GridOwner.Parent.GetGrid(x, i);
                grids.Add(gr);
                if (gr.ChessmanOwner != null)
                    break;
            }
            return grids.ToArray();
        }

*/