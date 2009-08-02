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
        /// <summary>
        /// 获取与设置一盘棋局的所有棋格类
        /// </summary>
        protected ChessGrid[,] ChessGrids { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessGame()
        {
            this.ChessGrids = new ChessGrid[8, 8];
            this.LoadGrids();
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
        public virtual ChessGrid this[int x, int y]
        {
            get { return this.ChessGrids[x - 1, y - 1]; }
        }
        /// <summary>
        /// 获取指定坐标值的棋格(坐标是象棋规则的1-8)
        /// </summary>
        /// <param name="c">棋格的x坐标(按象棋规则，a-h)</param>
        /// <param name="y">棋格的y坐标(按象棋规则，不能为0)</param>
        public virtual ChessGrid this[char c, int y]
        {
            get { return this.ChessGrids[Utility.CharToInt(c), y]; }
        }

        /// <summary>
        /// 初始化棋格（一个棋盘由64个棋格组成，该方法将初始化整个棋盘的每个棋格）
        /// </summary>
        public virtual ChessGrid[,] LoadGrids()
        {
            for (int x = 0; x < ChessGrids.GetLength(0); x++)
            {
                for (int y = 0; y < ChessGrids.GetLength(1); y++)
                {
                    ChessGrids[x, y] = new ChessGrid(x + 1, y + 1);
                    ChessGrids[x, y].MoveInEvent += new ChessGrid.MoveInEventHandler(ChessGame_MoveInAfterEvent);
                }
            }
            return this.ChessGrids;
        }

        protected virtual void ChessGame_MoveInAfterEvent(object sender, ChessGrid.MoveInEventArgs e)
        {
        }

        #region IEnumerable<Chesspoint> 成员

        public IEnumerator<ChessGrid> GetEnumerator()
        {
            return (IEnumerator<ChessGrid>)this.ChessGrids.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.ChessGrids.GetEnumerator();
        }

        #endregion
    }
}