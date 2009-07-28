using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 坐标集合，一般是应用在棋子类型中，表示一个棋子绑定的路径，该路径由一个一个的坐标组成
    /// </summary>
    public class ChessGirdCollection : IList<ChessGrid>
    {
        List<ChessGrid> _points = new List<ChessGrid>();

        /// <summary>
        /// 返回位于 pointCollection 开始处的(最近发生的) ChessGrid 但不将其移除。
        /// </summary>
        /// <returns></returns>
        public ChessGrid Peek()
        {
            if (_points.Count == 0)
                return null;
            return _points[_points.Count - 1];
        }

        /// <summary>
        /// 移除并返回位于 ChesspointCollection 开始处(最近发生的) ChessGrid 的对象。
        /// </summary>
        /// <returns></returns>
        public ChessGrid Dequeue()
        {
            if (_points.Count == 0)
                return null;
            ChessGrid sq = this.Peek();
            _points.RemoveAt(this._points.Count - 1);
            return sq;
        }

        #region IList<ChessGrid> 成员

        public int IndexOf(ChessGrid item)
        {
            return _points.IndexOf(item);
        }

        public void Insert(int index, ChessGrid item)
        {
            _points.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _points.RemoveAt(index);
        }

        public ChessGrid this[int index]
        {
            get { return _points[index]; }
            set { _points[index] = value; }
        }

        #endregion

        #region ICollection<ChessGrid> 成员

        public void Add(ChessGrid item)
        {
            _points.Add(item);
        }

        public void Clear()
        {
            _points.Clear();
        }

        public bool Contains(ChessGrid item)
        {
            return _points.Contains(item);
        }

        public void CopyTo(ChessGrid[] array, int arrayIndex)
        {
            _points.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _points.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ChessGrid item)
        {
            return _points.Remove(item);
        }

        #endregion

        #region IEnumerable<ChessGrid> 成员

        public IEnumerator<ChessGrid> GetEnumerator()
        {
            return _points.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _points.GetEnumerator();
        }

        #endregion
    }
}
