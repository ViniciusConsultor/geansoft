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
        protected ChessGrid[,] _chessGrids = new ChessGrid[8, 8];

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessGame()
        {
            this.Record = new ChessRecord();
        }

        /// <summary>
        /// 获取本局棋的记录
        /// </summary>
        public virtual ChessRecord Record { get; private set; }

        /// <summary>
        /// 获取指定坐标值的棋格(坐标是象棋规则的1-8)
        /// </summary>
        /// <param name="x">棋格的x坐标(按象棋规则，不能为0)</param>
        /// <param name="y">棋格的y坐标(按象棋规则，不能为0)</param>
        public ChessGrid this[int x, int y]
        {
            get { return this._chessGrids[x - 1, y - 1]; }
        }
        /// <summary>
        /// 获取指定坐标值的棋格(坐标是象棋规则的1-8)
        /// </summary>
        /// <param name="c">棋格的x坐标(按象棋规则，a-h)</param>
        /// <param name="y">棋格的y坐标(按象棋规则，不能为0)</param>
        public ChessGrid this[char c, int y]
        {
            get { return this._chessGrids[Utility.CharToInt(c), y]; }
        }

        /// <summary>
        /// 初始化棋格（一个棋盘由64个棋格组成，该方法将初始化整个棋盘的每个棋格）
        /// </summary>
        public virtual ChessGrid[,] LoadGrids()
        {
            for (int x = 0; x < _chessGrids.GetLength(0); x++)
            {
                for (int y = 0; y < _chessGrids.GetLength(1); y++)
                {
                    _chessGrids[x, y] = new ChessGrid(x + 1, y + 1);
                }
            }
            return this._chessGrids;
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