using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 这是描述一盘棋局的类。实现了IEnumerable&lt;ChessGrid&gt;。
    /// </summary>
    public class ChessGame : IEnumerable<ChessGrid>
    {
        /// <summary>
        /// 获取与设置一盘棋局的所有棋格类
        /// </summary>
        protected virtual ChessGrid[,] ChessGrids { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ChessGame()
        {
            this.ChessGrids = new ChessGrid[8, 8];
            this.LoadGrids();
            this.Record = new ChessRecord();
        }
        public ChessGame(ChessRecord record)
        {
            this.ChessGrids = new ChessGrid[8, 8];
            this.LoadGrids();
            this.Record = record;
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
            get { return this.ChessGrids[Utility.CharToInt(c) - 1, y - 1]; }
        }

        /// <summary>
        /// 初始化棋格（一个棋盘由64个棋格组成，该方法将初始化整个棋盘的每个棋格）
        /// </summary>
        public virtual void LoadGrids()
        {
            for (int x = 1; x <= 8; x++)
            {
                for (int y = 1; y <= 8; y++)
                {
                    this.ChessGrids[x - 1, y - 1] = new ChessGrid(x, y);
                    this.ChessGrids[x - 1, y - 1].MoveInEvent += new ChessGrid.MoveInEventHandler(ChessGame_MoveInAfterEvent);
                }
            }
        }

        protected virtual void ChessGame_MoveInAfterEvent(object sender, ChessGrid.MoveInEventArgs e)
        {
            //this.Record.Items.Add(e.Action, e.ChessmanSide, e.ChessStep);
        }

        #region IEnumerable

        #region IEnumerable<ChessGrid> 成员

        public IEnumerator<ChessGrid> GetEnumerator()
        {
            return new ChessGridEnumerator(this.ChessGrids);
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new ChessGridEnumerator(this.ChessGrids);
        }

        #endregion

        public class ChessGridEnumerator : IEnumerator<ChessGrid>
        {
            private List<ChessGrid> _chessGrids;
            private int _position = -1;

            public ChessGridEnumerator(ChessGrid[,] rids)
            {
                _chessGrids = new List<ChessGrid>(64);
                for (int y = 0; y <= 7; y++)
                {
                    for (int x = 0; x <= 7; x++)
                    {
                        _chessGrids.Add(rids[x, y]);
                    }
                }
            }

            public bool MoveNext()
            {
                _position++;
                return (_position < _chessGrids.Count);
            }

            public void Reset()
            {
                _position = -1;
            }

            public object Current
            {
                get
                {
                    try 
                    { return _chessGrids[_position]; }
                    catch
                    { throw new InvalidOperationException(); }
                }
            }

            #region IEnumerator<ChessGrid> 成员

            ChessGrid IEnumerator<ChessGrid>.Current
            {
                get { return (ChessGrid)this.Current; }
            }

            #endregion

            #region IDisposable 成员

            public void Dispose()
            {
                _chessGrids = null;
            }

            #endregion
        }

        #endregion
    }
}