using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 坐标集合，一般是应用在棋子类型中，表示一个棋子绑定的路径，该路径由一个一个的坐标组成
    /// </summary>
    public class ChessGirdCollection : Stack<ChessGrid>
    {
    }
}
/*
        List<ChessGrid> _chessGrids = new List<ChessGrid>();

        /// <summary>
        /// 返回位于 ChessGirdCollection 开始处的(最近发生的) ChessGrid 但不将其移除。
        /// </summary>
        /// <returns></returns>
        public ChessGrid Peek()
        {
            if (_chessGrids.Count == 0)
                return null;
            return _chessGrids[_chessGrids.Count - 1];
        }
        
        /// <summary>
        /// 返回位于 ChessGirdCollection 开始处的(最近发生的)两个 ChessGrid 但不将其移除。
        /// </summary>
        /// <returns></returns>
        public ChessGrid[] PeekPair()
        {
            ChessGrid[] rids = new ChessGrid[2];
            if (_chessGrids.Count < 2)
                return null;
            rids[1] = _chessGrids[_chessGrids.Count - 1];
            rids[0] = _chessGrids[_chessGrids.Count - 2];
            return rids;
        }

        /// <summary>
        /// 移除并返回位于 ChessGirdCollection 开始处(最近发生的) ChessGrid 的对象。
        /// </summary>
        /// <returns></returns>
        public ChessGrid Dequeue()
        {
            if (_chessGrids.Count == 0)
                return null;
            ChessGrid sq = this.Peek();
            _chessGrids.RemoveAt(this._chessGrids.Count - 1);
            return sq;
        }

        /// <summary>
        /// 将 ChessGirdCollection 的元素复制到新数组中。
        /// </summary>
        /// <returns></returns>
        public ChessGrid[] ToArray()
        {
            return _chessGrids.ToArray();
        }

        /// <summary>
        /// 将指定集合的元素添加到 ChessGirdCollection 的末尾。
        /// </summary>
        /// <param name="rids">指定集合</param>
        public void AddRange(IEnumerable<ChessGrid> rids)
        {
            this._chessGrids.AddRange(rids);
        }

        #region IList<ChessGrid> 成员

        public int IndexOf(ChessGrid item)
        {
            return _chessGrids.IndexOf(item);
        }

        public void Insert(int index, ChessGrid item)
        {
            _chessGrids.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _chessGrids.RemoveAt(index);
        }

        public ChessGrid this[int index]
        {
            get { return _chessGrids[index]; }
            set { _chessGrids[index] = value; }
        }

        #endregion

        #region ICollection<ChessGrid> 成员

        public void Add(ChessGrid item)
        {
            _chessGrids.Add(item);
        }

        public void Clear()
        {
            _chessGrids.Clear();
        }

        public bool Contains(ChessGrid item)
        {
            return _chessGrids.Contains(item);
        }

        public void CopyTo(ChessGrid[] array, int arrayIndex)
        {
            _chessGrids.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _chessGrids.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ChessGrid item)
        {
            return _chessGrids.Remove(item);
        }

        #endregion

        #region IEnumerable<ChessGrid> 成员

        public IEnumerator<ChessGrid> GetEnumerator()
        {
            return _chessGrids.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _chessGrids.GetEnumerator();
        }

        #endregion
    }
}
*/