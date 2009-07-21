using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Wrapper.Chess
{
    public class ChessCommentCollection : IDictionary<int, ChessComment>
    {
        private Dictionary<int, ChessComment> _comments = new Dictionary<int, ChessComment>();

        public void Add(string comment)
        {
            ChessComment chesscomment = new ChessComment(string.Empty, comment);
            this.Add(chesscomment);
        }
        public void Add(ChessComment comment)
        {
            int i = this.Keys.Count + 1;
            while (this.ContainsKey(i))
                i++;
            _comments.Add(i, comment);
        }

        #region IDictionary<int,ChessComment> 成员

        public void Add(int key, ChessComment value)
        {
            _comments.Add(key, value);
        }

        public bool ContainsKey(int key)
        {
            return _comments.ContainsKey(key);
        }

        public ICollection<int> Keys
        {
            get { return _comments.Keys; }
        }

        public bool Remove(int key)
        {
            return _comments.Remove(key);
        }

        public bool TryGetValue(int key, out ChessComment value)
        {
            return _comments.TryGetValue(key, out value);
        }

        public ICollection<ChessComment> Values
        {
            get { return _comments.Values; }
        }

        public ChessComment this[int key]
        {
            get { return _comments[key]; }
            set { _comments[key] = value; }
        }

        #endregion

        #region ICollection<KeyValuePair<int,ChessComment>> 成员

        public void Add(KeyValuePair<int, ChessComment> item)
        {
            _comments.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _comments.Clear();
        }

        public bool Contains(KeyValuePair<int, ChessComment> item)
        {
            if (_comments.ContainsKey(item.Key) && _comments.ContainsValue(item.Value))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 拷贝指定的键值对数组，但未实现拷贝到指定的位置。
        /// 请使用该方法时注意。
        /// </summary>
        /// <param name="array">指定的键值对数组</param>
        /// <param name="arrayIndex">指定的位置</param>
        public void CopyTo(KeyValuePair<int, ChessComment>[] array, int arrayIndex)
        {
            foreach (var item in array)
                _comments.Add(item.Key, item.Value);
        }

        public int Count
        {
            get { return _comments.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<int, ChessComment> item)
        {
            return _comments.Remove(item.Key);
        }

        #endregion

        #region IEnumerable<KeyValuePair<int,ChessComment>> 成员

        public IEnumerator<KeyValuePair<int, ChessComment>> GetEnumerator()
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
