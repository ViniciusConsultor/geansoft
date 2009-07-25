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

        /// <summary>
        /// 获取棋盘的棋局是否是开局状态。
        /// 当棋格全部实例化，棋子全部实例化并落入指定棋格后，该值将被设置为False。
        /// </summary>
        private bool _isOpennings = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessGame()
        {
            this.LoadSquares();//初始化所有的棋格
            this.Record = new ChessRecord();
        }

        /// <summary>
        /// 获取本局棋的记录
        /// </summary>
        public ChessRecord Record { get; private set; }

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
        private void LoadSquares()
        {
            for (int x = 0; x < _squares.GetLength(0); x++)
            {
                for (int y = 0; y < _squares.GetLength(1); y++)
                {
                    _squares[x, y] = new ChessSquare(x + 1, y + 1);
                }
            }
        }

        /// <summary>
        /// 初始化开局棋子(32个棋子)。
        /// Opennings：n. 开局
        /// </summary>
        internal void LoadOpennings()
        {
            this.LoadOpennings(ChessmanBase.GetOpennings().ToArray());
        }
        /// <summary>
        /// 初始化指定的开局棋子集合，该方法一般使用场合为残局类，中盘类棋局
        /// Opennings：n. 开局
        /// </summary>
        internal void LoadOpennings(IEnumerable<ChessmanBase> chessmans)
        {
            this._chessmans.AddRange(chessmans);
            foreach (ChessmanBase man in chessmans)
            {
                this.Play(man, man.Squares.Peek());
            }
            this._isOpennings = false;//棋子设置完毕，将开局判断设置为false
            //注册开局设置结束事件
            OnSetOpenningsFinished(new ChessGameEventArgs(this));
        }

        /// <summary>
        /// 设置指定的棋格拥有的棋子。(下棋的动作:移动棋子到指定的棋格,在初始化时,可以理解为是摆棋)
        /// </summary>
        /// <param name="newSquare">棋子将被移动到的指定棋格的坐标</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ChessmanMovedException"></exception>
        public ChessStep Play(ChessmanBase man, ChessSquare newSquare)
        {
            if (man == null)
                throw new ArgumentOutOfRangeException("Chessman: chessman is Null.");
            if (newSquare == null)
                throw new ArgumentOutOfRangeException("Square: newSquare is Null.");

            ChessSquare oldSquare = man.Squares.Peek();
            if (newSquare.OwnedChessman != ChessmanBase.Empty)
            {
                if (newSquare.OwnedChessman.ChessmanSide == man.ChessmanSide)//新棋格拥有的棋子与将要移动棋子是一样的战方时
                {
                    throw new ChessmanMovedException(
                        string.Format("{0} and {1} is same Side, cannot move!",
                                        newSquare.OwnedChessman.ToString(), man.ToString()));
                }
            }

            ChessmanMoveEventArgs e = new ChessmanMoveEventArgs(man, man.Squares.Peek(), newSquare);

            //注册移动前事件
            OnMoving(e);

            if (!this._isOpennings)//非初始化棋局时
            {
                man.Squares.Add(newSquare);
                oldSquare.OwnedChessman = ChessmanBase.Empty;//将棋子的历史棋格的棋子状态置为空
            }

            Enums.ActionDescription action = Enums.ActionDescription.General;

            if (newSquare.OwnedChessman != ChessmanBase.Empty)
            {
                //注册棋子即将被杀死的事件
                OnKilling(new ChessmanKillEventArgs(man, newSquare));

                //新棋格中如有棋子，置该棋子为杀死状态
                newSquare.OwnedChessman.IsKilled = true;
                action = Enums.ActionDescription.Kill;

                //注册棋子被杀死后的事件
                OnKilled(new ChessmanKillEventArgs(man, newSquare));
            }

            //绑定新棋格拥有的棋子
            newSquare.OwnedChessman = man;
            
            //注册移动后事件
            OnMoved(e);

            //生成一个棋步
            ChessStep step = new ChessStep(Enums.Castling.None, man.ChessmanType, action, oldSquare, newSquare);
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
        public ChessSquare GetSquare(char c, int y)
        {
            return this.GetSquare(Utility.CharToInt(c), y);
        }

        /// <summary>
        /// 获取所有黑色棋格
        /// </summary>
        public ChessSquare[] GetBlackSquares()
        {
            List<ChessSquare> squares = new List<ChessSquare>();
            foreach (ChessSquare square in this)
            {
                if (square.SquareSide == Enums.ChessSquareSide.Black)
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
                if (square.SquareSide == Enums.ChessSquareSide.White)
                    squares.Add(square);
            }
            return squares.ToArray();
        }

        /// <summary>
        /// 获取所有活着的棋子
        /// </summary>
        public ChessmanBase[] GetLivingChessmans()
        {
            ChessmanCollection mans = new ChessmanCollection();
            foreach (ChessmanBase man in this._chessmans)
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
        public ChessmanBase[] GetKilledChessmans()
        {
            ChessmanCollection mans = new ChessmanCollection();
            foreach (ChessmanBase man in this._chessmans)
            {
                if (man.IsKilled == true)
                    mans.Add(man);
            }
            return mans.ToArray();
        }

        public string ToPGNString()
        {
            return string.Empty;
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
            public ChessmanMoveEventArgs(ChessmanBase chessman, ChessSquare oldSquare, ChessSquare newSquare)
                : base(chessman)
            {
                this.OldSquare = oldSquare;
                this.NewSquare = newSquare;
            }
        }

        /// <summary>
        /// 在棋局开始的时候发生
        /// </summary>
        public event GameStartingEventHandler GameStartingEvent;
        protected virtual void OnGameStarting(ChessGameEventArgs e)
        {
            if (GameStartingEvent != null)
                GameStartingEvent(this, e);
        }
        public delegate void GameStartingEventHandler(object sender, ChessGameEventArgs e);

        /// <summary>
        /// 在棋局开始后发生
        /// </summary>
        public event GameStartedEventHandler GameStartedEvent;
        protected virtual void OnGameStarted(ChessGameEventArgs e)
        {
            if (GameStartedEvent != null)
                GameStartedEvent(this, e);
        }
        public delegate void GameStartedEventHandler(object sender, ChessGameEventArgs e);

        /// <summary>
        /// 在棋局结束的时候发生
        /// </summary>
        public event GameStoppingEventHandler GameStoppingEvent;
        protected virtual void OnGameStopping(ChessGameEventArgs e)
        {
            if (GameStoppingEvent != null)
                GameStoppingEvent(this, e);
        }
        public delegate void GameStoppingEventHandler(object sender, ChessGameEventArgs e);

        /// <summary>
        /// 在棋局结束后发生
        /// </summary>
        public event GameStoppedEventHandler GameStoppedEvent;
        protected virtual void OnGameStopped(ChessGameEventArgs e)
        {
            if (GameStoppedEvent != null)
                GameStoppedEvent(this, e);
        }
        public delegate void GameStoppedEventHandler(object sender, ChessGameEventArgs e);

        /// <summary>
        /// 在开局设置完毕时发生
        /// </summary>
        public event SetOpenningsFinishedEventHandler SetOpenningsFinishedEvent;
        protected virtual void OnSetOpenningsFinished(ChessGameEventArgs e)
        {
            if (SetOpenningsFinishedEvent != null)
                SetOpenningsFinishedEvent(this, e);
        }
        public delegate void SetOpenningsFinishedEventHandler(object sender, ChessGameEventArgs e);

        public class ChessGameEventArgs : EventArgs
        {
            public ChessGame ChessGame { get; private set; }
            public ChessGameEventArgs(ChessGame game)
            {
                this.ChessGame = game;
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
            public ChessmanKillEventArgs(ChessmanBase man, ChessSquare currGrid)
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