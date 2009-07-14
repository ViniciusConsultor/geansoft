﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述一个棋招序列（IList集合,集合的元素为<see>ChessStepPair</see>）。
    /// 它可能描述的是一整局棋，也可能是描述的是一整局棋的一部份，如变招的描述与记录。
    /// </summary>
    public class ChessStepPairSequence : IList<ChessStepPair>
    {
        List<ChessStepPair> steps = new List<ChessStepPair>();

        #region IList<ChessStepPair> 成员

        public int IndexOf(ChessStepPair item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ChessStepPair item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public ChessStepPair this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region ICollection<ChessStepPair> 成员

        public void Add(ChessStepPair item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ChessStepPair item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ChessStepPair[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(ChessStepPair item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<ChessStepPair> 成员

        public IEnumerator<ChessStepPair> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
