﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessmanCollection : IList<ChessmanBase>
    {
        List<ChessmanBase> _chessmans = new List<ChessmanBase>();

        public void AddRange(IEnumerable<ChessmanBase> chessmans)
        {
            this._chessmans.AddRange(chessmans);
            this._chessmans.TrimExcess();
        }
        public ChessmanBase[] ToArray()
        {
            return _chessmans.ToArray();
        }

        #region IList<ChessmanBase> 成员

        public int IndexOf(ChessmanBase item)
        {
            return _chessmans.IndexOf(item);
        }

        public void Insert(int index, ChessmanBase item)
        {
            _chessmans.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _chessmans.RemoveAt(index);
        }

        public ChessmanBase this[int index]
        {
            get { return _chessmans[index]; }
            set { _chessmans[index] = value; }
        }

        #endregion

        #region ICollection<ChessmanBase> 成员

        public void Add(ChessmanBase item)
        {
            _chessmans.Add(item);
            _chessmans.TrimExcess();
        }

        public void Clear()
        {
            _chessmans.Clear();
        }

        public bool Contains(ChessmanBase item)
        {
            return _chessmans.Contains(item);
        }

        public void CopyTo(ChessmanBase[] array, int arrayIndex)
        {
            _chessmans.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _chessmans.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ChessmanBase item)
        {
            return _chessmans.Remove(item);
        }

        #endregion

        #region IEnumerable<ChessmanBase> 成员

        public IEnumerator<ChessmanBase> GetEnumerator()
        {
            return _chessmans.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._chessmans.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// 获取正式棋局的全部棋子
        /// </summary>
    }
}