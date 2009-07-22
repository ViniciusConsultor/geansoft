using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessCommentCollection : IList<ChessComment>
    {
        List<ChessComment> _comments = new List<ChessComment>();

        /// <summary>
        /// 返回所有评论的编号
        /// </summary>
        public int[] Indexs
        {
            get
            {
                List<int> indexs = new List<int>();
                foreach (var item in _comments)
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
                foreach (var item in _comments)
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
        public string[] Comments
        {
            get
            {
                List<string> commentList = new List<string>();
                foreach (var item in _comments)
                {
                    commentList.Add(item.Comment);
                }
                return commentList.ToArray();
            }
        }

        #region IList<ChessComment> 成员

        public int IndexOf(ChessComment item)
        {
            return _comments.IndexOf(item);
        }

        public void Insert(int index, ChessComment item)
        {
            _comments.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _comments.RemoveAt(index);
        }

        public ChessComment this[int index]
        {
            get { return _comments[index]; }
            set { _comments[index] = value; }
        }

        #endregion

        #region ICollection<ChessComment> 成员

        public void Add(ChessComment item)
        {
            _comments.Add(item);
        }

        public void Clear()
        {
            _comments.Clear();
        }

        public bool Contains(ChessComment item)
        {
            return _comments.Contains(item);
        }

        public void CopyTo(ChessComment[] array, int arrayIndex)
        {
            _comments.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _comments.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ChessComment item)
        {
            return _comments.Remove(item);
        }

        #endregion

        #region IEnumerable<ChessComment> 成员

        public IEnumerator<ChessComment> GetEnumerator()
        {
            return _comments.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _comments.GetEnumerator();
        }

        #endregion
    }
}
