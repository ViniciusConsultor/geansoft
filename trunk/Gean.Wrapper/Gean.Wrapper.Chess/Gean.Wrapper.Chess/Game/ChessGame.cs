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
        private ChessGrid[,] _chessGrids = new ChessGrid[8, 8];

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
            this.LoadGrids();//初始化所有的棋格
            this.Record = new ChessRecord();
        }

        /// <summary>
        /// 获取本局棋的记录
        /// </summary>
        public ChessRecord Record { get; private set; }

        /// <summary>
        /// 获取指定坐标值的棋格(坐标是象棋规则的1-8)
        /// </summary>
        public ChessGrid this[int x, int y]
        {
            get { return this._chessGrids[x - 1, y - 1]; }
        }

        /// <summary>
        /// 初始化棋格（一个棋盘由64个棋格组成，该方法将初始化整个棋盘的每个棋格）
        /// </summary>
        private void LoadGrids()
        {
            for (int x = 0; x < _chessGrids.GetLength(0); x++)
            {
                for (int y = 0; y < _chessGrids.GetLength(1); y++)
                {
                    _chessGrids[x, y] = new ChessGrid(x + 1, y + 1);
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
        /// 初始化指定的开局棋子集合，直接使用该方法一般的场合为残局类，中盘类棋局
        /// Opennings：n. 开局
        /// </summary>
        public void LoadOpennings(IEnumerable<Chessman> chessmans)
        {
            //注册棋局即将开始事件
            OnGameStarting(new ChessGameEventArgs(this));

            foreach (Chessman man in chessmans)
            {
                man.ChessSteps.Peek().TargetGrid.MoveIn(man, Enums.Action.Opennings);
            }

            //棋子设置完毕，将开局判断设置为false
            this._isOpennings = false;

            //注册棋局开局设置结束事件
            OnSetOpenningsFinished(new ChessGameEventArgs(this)); 
            //注册棋局开始后事件
            OnGameStarted(new ChessGameEventArgs(this));
        }

        /// <summary>
        /// 获取指定坐标的棋格(坐标是象棋规则的1-8)
        /// </summary>
        /// <param name="x">指定坐标的x轴</param>
        /// <param name="y">指定坐标的y轴</param>
        /// <returns></returns>
        public ChessGrid GetGrid(int x, int y)
        {
            return this[x, y];
        }
        /// <summary>
        /// 获取指定坐标的棋格(坐标是象棋规则的1-8)
        /// </summary>
        /// <param name="point">指定坐标</param>
        /// <returns></returns>
        public ChessGrid GetGrid(char c, int y)
        {
            return this.GetGrid(Utility.CharToInt(c), y);
        }

        /// <summary>
        /// 获取所有黑色棋格
        /// </summary>
        public ChessGrid[] GetBlackGrids()
        {
            List<ChessGrid> points = new List<ChessGrid>();
            foreach (ChessGrid rid in this)
            {
                if (rid.GridSide == Enums.ChessGridSide.Black)
                    points.Add(rid);
            }
            return points.ToArray();
        }
        /// <summary>
        /// 获取所有白色棋格
        /// </summary>
        public ChessGrid[] GetWhiteGrids()
        {
            List<ChessGrid> points = new List<ChessGrid>();
            foreach (ChessGrid rid in this)
            {
                if (rid.GridSide == Enums.ChessGridSide.White)
                    points.Add(rid);
            }
            return points.ToArray();
        }

        /// <summary>
        /// 获取所有活着的棋子
        /// </summary>
        public Chessman[] GetLivingChessmans()
        {
            ChessmanCollection mans = new ChessmanCollection();
            foreach (ChessGrid rid in this._chessGrids)
            {
                Chessman human = rid.OwnedChessman;
                if (human == Chessman.NullOrEmpty)
                    continue;
                if (human.IsKilled == false)
                    mans.Add(human);
            }
            return mans.ToArray();
        }

        #region IEnumerable<Chesspoint> 成员

        public IEnumerator<ChessGrid> GetEnumerator()
        {
            return (IEnumerator<ChessGrid>)this._chessGrids.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._chessGrids.GetEnumerator();
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