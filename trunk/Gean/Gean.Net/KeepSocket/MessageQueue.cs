using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Net.KeepSocket
{
    public class MessageQueue : IList<string>
    {
        private Queue<string> _InnerQueue = new Queue<string>();

        /// <summary>
        /// 移除并返回位于队列开始处的对象。
        /// </summary>
        public String Dequeue() { return ""; }
        /// <summary>
        /// 将对象添加到队列的结尾处。
        /// </summary>
        public void Enqueue(String item) { }

        public int IndexOf(string item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, string item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public string this[int index]
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

        public void Add(string item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(string[] array, int arrayIndex)
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

        public bool Remove(string item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
