﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    /// <summary>
    /// 描述棋局中的回合序列(实现IList接口, 集合的元素为<see>ChessStepPair</see>)。
    /// 它可能描述的是一整局棋，也可能是描述的是一整局棋的一部份，如变招的描述与记录。
    /// </summary>
    public class ChessSequence : IList<ISequenceItem>
    {
        protected List<ISequenceItem> SequenceItemList { get; private set; }

        public ChessSequence()
        {
            this.SequenceItemList = new List<ISequenceItem>();
        }

        public ChessStep Peek()
        {
            return this.SequenceItemList[this.SequenceItemList.Count - 1] as ChessStep;
        }

        public ChessStep[] PeekPair()
        {
            if (this.SequenceItemList.Count < 2)
            {
                return null;
            }
            ChessStep[] steps = new ChessStep[2];
            steps[0] = this.SequenceItemList[this.SequenceItemList.Count - 1] as ChessStep;
            steps[1] = this.SequenceItemList[this.SequenceItemList.Count - 2] as ChessStep;
            return steps;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ISequenceItem item in this.SequenceItemList)
            {
                this.GetItemString(item, sb);
            }
            return sb.ToString().Replace("  ", " ");
        }
        /// <summary>
        /// 序列树生成字符串<see>TooString()</see>的递归子方法
        /// </summary>
        private void GetItemString(ISequenceItem item, StringBuilder sb)
        {
            sb.Append(item.Value).Append(' ');
            if (item is IStepTree)
            {
                IStepTree tree = (IStepTree)item;
                if (tree.HasChildren)
                {
                    sb.Append(' ').Append('(');
                    foreach (var subItem in tree.Items)
                    {
                        this.GetItemString(subItem, sb);
                    }
                    sb.Append(')').Append(' ');
                }
            }
        }

        #region IList<ISequenceItem> 成员

        public int IndexOf(ISequenceItem item)
        {
            return this.SequenceItemList.IndexOf(item);
        }

        public void Insert(int index, ISequenceItem item)
        {
            this.SequenceItemList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.SequenceItemList.RemoveAt(index);
        }

        public ISequenceItem this[int index]
        {
            get { return this.SequenceItemList[index]; }
            set { this.SequenceItemList[index] = value; }
        }

        #endregion

        #region ICollection<ISequenceItem> 成员

        public void Add(ISequenceItem item)
        {
            this.SequenceItemList.Add(item);
        }

        public void Clear()
        {
            this.SequenceItemList.Clear();
        }

        public bool Contains(ISequenceItem item)
        {
            return this.SequenceItemList.Contains(item);
        }

        public void CopyTo(ISequenceItem[] array, int arrayIndex)
        {
            this.SequenceItemList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.SequenceItemList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ISequenceItem item)
        {
            return this.SequenceItemList.Remove(item);
        }

        #endregion

        #region IEnumerable<ISequenceItem> 成员

        public IEnumerator<ISequenceItem> GetEnumerator()
        {
            return this.SequenceItemList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.SequenceItemList.GetEnumerator();
        }

        #endregion


        protected static void Parse(string value)
        {
            throw new NotImplementedException();
        }
    }
}
