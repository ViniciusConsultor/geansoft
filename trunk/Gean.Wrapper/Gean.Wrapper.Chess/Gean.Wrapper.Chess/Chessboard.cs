using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 一个描述棋盘的类
    /// </summary>
    public class Chessboard : IEnumerable<ChessboardGrid>
    {
        private ChessboardGrid[,] _boardGrids = new ChessboardGrid[8, 8];
        private ChessmanCollection _chessmans = new ChessmanCollection();

        public Chessboard()
        {
            this.SetGrid();
        }

        /// <summary>
        /// 初始化组件（棋盘，即初始化整个棋盘的每个棋格）
        /// </summary>
        private void SetGrid()
        {
            for (int x = 0; x < _boardGrids.GetLength(0); x++)
            {
                for (int y = 0; y < _boardGrids.GetLength(1); y++)
                {
                    if ((y % 2) == 0)
                    {
                        if ((x % 2) == 0)
                            _boardGrids[x, y] = new ChessboardGrid(x + 1, y + 1, Enums.ChessboardGridSide.White);
                        else
                            _boardGrids[x, y] = new ChessboardGrid(x + 1, y + 1, Enums.ChessboardGridSide.Black);
                    }
                    else
                    {
                        if ((x % 2) == 0)
                            _boardGrids[x, y] = new ChessboardGrid(x + 1, y + 1, Enums.ChessboardGridSide.Black);
                        else
                            _boardGrids[x, y] = new ChessboardGrid(x + 1, y + 1, Enums.ChessboardGridSide.White);
                    }
                }
            }
        }

        internal void SetChessmans()
        {
            this.SetChessmans(Utility.GetOpenningsChessmans());
        }

        internal void SetChessmans(IEnumerable<ChessmanState> chessmans)
        {
            //this._chessmans.AddRange(chessmans);
            //foreach (ChessmanBase man in chessmans)
            //{
            //    //man.RegistGrid(man.GridOwner);
            //}
        }

        /// <summary>
        /// 获取指定坐标的棋格
        /// </summary>
        /// <param name="x">指定坐标的x轴</param>
        /// <param name="y">指定坐标的y轴</param>
        /// <returns></returns>
        public ChessboardGrid GetGrid(int x, int y)
        {
            return this._boardGrids[x - 1, y - 1];
        }
        /// <summary>
        /// 获取指定坐标的棋格
        /// </summary>
        /// <param name="square">指定坐标</param>
        /// <returns></returns>
        public ChessboardGrid GetGrid(Square square)
        {
            return this.GetGrid(square.X, square.Y);
        }
        /// <summary>
        /// 获取指定坐标字符串的棋格
        /// </summary>
        /// <param name="step">指定坐标字符串</param>
        /// <returns></returns>
        public ChessboardGrid GetGrid(string square)
        {
            return this.GetGrid(Square.Parse(square));
        }

        /// <summary>
        /// 获取指定的棋子所能到达的棋格。
        /// TODO:该方法代码未完成。不要使用该方法。
        /// </summary>
        /// <param name="chessman">指定的棋子</param>
        /// <returns>能到达的棋格</returns>
        public ChessboardGrid[] GetUsableGrids(Chessman chessman)
        {
            List<ChessboardGrid> grids = new List<ChessboardGrid>();
            switch (chessman.ChessmanType)
            {
                case Enums.ChessmanType.Rook:
                    //grids.AddRange(Helper.GetRookGrid(this, chessman.GridOwner, chessman.ChessmanType, chessman.ChessmanSide));
                    break;
                case Enums.ChessmanType.Knight:
                    break;
                case Enums.ChessmanType.Bishop:
                    break;
                case Enums.ChessmanType.Queen:
                    break;
                case Enums.ChessmanType.King:
                    break;
                case Enums.ChessmanType.Pawn:
                    break;
                default:
                    break;
            }
            return grids.ToArray();
        }

        public ChessboardGrid[] GetGridsByStep(ChessStep step)
        {
            List<ChessboardGrid> grids = new List<ChessboardGrid>();
            return grids.ToArray();
        }

        /// <summary>
        /// 将棋子绑定到指定的棋格中
        /// </summary>
        /// <param name="grid"></param>
        public void ChessmanBind(Chessman man, ChessboardGrid oldGrid, ChessboardGrid newGrid)
        {
            if (man == null)
                throw new ArgumentOutOfRangeException(man.ToString());
            ChessmanMoveEventArgs e = new ChessmanMoveEventArgs(man, oldGrid, newGrid);

            //注册移动前事件
            OnMoving(e);
            //if (this.GridOwner != null)
            //    this.GridOwner.ChessmanOwner = null;
            //this.GridOwner = grid;
            //this.GridOwner.ChessmanOwner = this;
            //注册移动后事件
            OnMoved(e);
        }

        #region IEnumerable<ChessboardGrid> 成员

        public IEnumerator<ChessboardGrid> GetEnumerator()
        {
            return (IEnumerator<ChessboardGrid>)this._boardGrids.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._boardGrids.GetEnumerator();
        }

        #endregion

        static class Helper
        {
        }

        #region custom event

        /// <summary>
        /// 在棋子被移动后发生
        /// </summary>
        public event MovedEventHandler MovedEvent;
        protected virtual void OnMoved(ChessmanMoveEventArgs e)
        {
            if (MovedEvent != null)
                MovedEvent(this, e);
        }
        public delegate void MovedEventHandler(object sender, ChessmanMoveEventArgs e);

        /// <summary>
        /// 在棋子被移动前发生
        /// </summary>
        public event MovingEventHandler MovingEvent;
        protected virtual void OnMoving(ChessmanMoveEventArgs e)
        {
            if (MovingEvent != null)
                MovingEvent(this, e);
        }
        public delegate void MovingEventHandler(object sender, ChessmanMoveEventArgs e);

        /// <summary>
        /// 包含棋子移动事件的数据
        /// </summary>
        public class ChessmanMoveEventArgs : ChessmanEventArgs
        {
            public ChessboardGrid OldGrid { get; set; }
            public ChessboardGrid NewGrid { get; set; }
            public ChessmanMoveEventArgs(Chessman chessman, ChessboardGrid oldGrid, ChessboardGrid newGrid)
                : base(chessman)
            {
                this.OldGrid = oldGrid;
                this.NewGrid = newGrid;
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