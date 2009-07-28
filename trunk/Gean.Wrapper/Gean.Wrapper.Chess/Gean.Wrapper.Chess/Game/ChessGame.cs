using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 这是描述一盘棋局的类
    /// </summary>
    public class ChessGame : IEnumerable<ChessGrid>
    {
        private ChessGrid[,] _points = new ChessGrid[8, 8];

        public ChessmanCollection Chessmans
        {
            get { return _chessmans; }
            set { _chessmans = value; }
        }
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
            this.Loadpoints();//初始化所有的棋格
            this.Record = new ChessRecord();
        }

        /// <summary>
        /// 获取本局棋的记录
        /// </summary>
        public ChessRecord Record { get; private set; }

        /// <summary>
        /// 获取指定坐标值的棋格
        /// </summary>
        public ChessGrid this[int x, int y]
        {
            get { return this.Getpoint(x, y); }
        }

        /// <summary>
        /// 初始化棋格（一个棋盘由64个棋格组成，该方法将初始化整个棋盘的每个棋格）
        /// </summary>
        private void Loadpoints()
        {
            for (int x = 0; x < _points.GetLength(0); x++)
            {
                for (int y = 0; y < _points.GetLength(1); y++)
                {
                    _points[x, y] = new ChessGrid(x + 1, y + 1);
                }
            }
        }

        /// <summary>
        /// 初始化开局棋子(32个棋子)。
        /// Opennings：n. 开局
        /// </summary>
        public void LoadOpennings()
        {
            this.LoadOpennings(Chessman.GetOpennings().ToArray());
        }
        /// <summary>
        /// 初始化指定的开局棋子集合，该方法一般使用场合为残局类，中盘类棋局
        /// Opennings：n. 开局
        /// </summary>
        public void LoadOpennings(IEnumerable<Chessman> chessmans)
        {
            OnGameStarting(new ChessGameEventArgs(this));
            this._chessmans.AddRange(chessmans);
            foreach (Chessman man in chessmans)
            {
                this.Play(man, man.ChessGrids.Peek());
            }
            this._isOpennings = false;//棋子设置完毕，将开局判断设置为false
            //注册开局设置结束事件
            OnSetOpenningsFinished(new ChessGameEventArgs(this));
            OnGameStarted(new ChessGameEventArgs(this));
        }

        /// <summary>
        /// 设置指定的棋格拥有的棋子。(下棋的动作:移动棋子到指定的棋格,在初始化时,可以理解为是摆棋)
        /// </summary>
        /// <param name="newpoint">棋子将被移动到的指定棋格的坐标</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ChessmanMovedException"></exception>
        public ChessStep Play(Chessman man, ChessGrid newpoint)
        {
            if (man == null)
                throw new ArgumentOutOfRangeException("Chessman: chessman is Null.");
            if (newpoint == null)
                throw new ArgumentOutOfRangeException("point: newpoint is Null.");

            ChessGrid oldpoint = man.ChessGrids.Peek();
            if (newpoint.OwnedChessman != Chessman.Empty)
            {
                if (newpoint.OwnedChessman.ChessmanSide == man.ChessmanSide)//新棋格拥有的棋子与将要移动棋子是一样的战方时
                {
                    throw new ChessmanException(
                        string.Format("{0} and {1} is same Side, cannot move!",
                                        newpoint.OwnedChessman.ToString(), man.ToString()));
                }
            }

            if (!this._isOpennings)//非初始化棋局时
            {
                man.ChessGrids.Add(newpoint);
                oldpoint.MoveOut();//将棋子的历史棋格的棋子状态置为空
            }

            Enums.ActionDescription action = Enums.ActionDescription.General;

            if (newpoint.OwnedChessman != Chessman.Empty)
            {
                //新棋格中如有棋子，置该棋子为杀死状态
                newpoint.OwnedChessman.IsKilled = true;
                action = Enums.ActionDescription.Kill;
            }

            //绑定新棋格拥有的棋子
            newpoint.MoveIn(man);
            
            //生成一个棋步
            ChessStep step = new ChessStep(Enums.Castling.None, man.ChessmanType, action, oldpoint, newpoint);
            return step;
        }

        /// <summary>
        /// 获取指定坐标的棋格
        /// </summary>
        /// <param name="x">指定坐标的x轴</param>
        /// <param name="y">指定坐标的y轴</param>
        /// <returns></returns>
        public ChessGrid Getpoint(int x, int y)
        {
            return this._points[x - 1, y - 1];
        }
        /// <summary>
        /// 获取指定坐标的棋格
        /// </summary>
        /// <param name="point">指定坐标</param>
        /// <returns></returns>
        public ChessGrid Getpoint(char c, int y)
        {
            return this.Getpoint(Utility.CharToInt(c), y);
        }

        /// <summary>
        /// 获取所有黑色棋格
        /// </summary>
        public ChessGrid[] GetBlackpoints()
        {
            List<ChessGrid> points = new List<ChessGrid>();
            foreach (ChessGrid point in this)
            {
                if (point.GridSide == Enums.ChessGridSide.Black)
                    points.Add(point);
            }
            return points.ToArray();
        }
        /// <summary>
        /// 获取所有白色棋格
        /// </summary>
        public ChessGrid[] GetWhitepoints()
        {
            List<ChessGrid> points = new List<ChessGrid>();
            foreach (ChessGrid point in this)
            {
                if (point.GridSide == Enums.ChessGridSide.White)
                    points.Add(point);
            }
            return points.ToArray();
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

        public string ToPGNString()
        {
            return string.Empty;
        }

        #region IEnumerable<Chesspoint> 成员

        public IEnumerator<ChessGrid> GetEnumerator()
        {
            return (IEnumerator<ChessGrid>)this._points.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._points.GetEnumerator();
        }

        #endregion

        #region custom event

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

        #endregion
    }
}
/*
        public override ChessboardGrid[] GetGridsByPath()
        {
            List<ChessboardGrid> grids = new List<ChessboardGrid>();
            int x = this.GridOwner.point.X;
            int y = this.GridOwner.point.Y;

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