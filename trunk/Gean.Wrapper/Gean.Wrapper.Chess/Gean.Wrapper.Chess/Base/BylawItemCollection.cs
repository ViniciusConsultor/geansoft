using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.Chess
{
    public abstract class BylawItemCollection : IList<BylawItem>
    {
        List<BylawItem> _values = new List<BylawItem>();

        /// <summary>
        /// 返回所有评论的编号
        /// </summary>
        public int[] Indexs
        {
            get
            {
                List<int> indexs = new List<int>();
                foreach (var item in _values)
                    indexs.Add(item.Number);
                return indexs.ToArray();
            }
        }

        /// <summary>
        /// 返回所有评论的用户名
        /// </summary>
        public string[] UserIds
        {
            get
            {
                List<string> userIdList = new List<string>();
                foreach (var item in _values)
                {
                    if (!string.IsNullOrEmpty(item.UserID))
                    {
                        userIdList.Add(item.UserID);
                    }
                }
                return userIdList.ToArray();
            }
        }

        /// <summary>
        /// 返回所有的评论的编号
        /// </summary>
        public string[] Values
        {
            get
            {
                List<string> values = new List<string>();
                foreach (var item in _values)
                    values.Add(item.BylawValue);
                return values.ToArray();
            }
        }

        #region IList<BylawItem> 成员

        public int IndexOf(BylawItem item)
        {
            return _values.IndexOf(item);
        }

        public void Insert(int index, BylawItem item)
        {
            _values.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _values.RemoveAt(index);
        }

        public BylawItem this[int index]
        {
            get { return _values[index]; }
            set { _values[index] = value; }
        }

        #endregion

        #region ICollection<BylawItem> 成员

        public void Add(BylawItem item)
        {
            _values.Add(item);
        }

        public void Clear()
        {
            _values.Clear();
        }

        public bool Contains(BylawItem item)
        {
            return _values.Contains(item);
        }

        public void CopyTo(BylawItem[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _values.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(BylawItem item)
        {
            return _values.Remove(item);
        }

        #endregion

        #region IEnumerable<BylawItem> 成员

        public IEnumerator<BylawItem> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        #endregion
    }
}
