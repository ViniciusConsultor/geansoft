using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Gean
{
    /// <summary>
    /// 一个支持Event的List。
    /// 不可用，完成度50%
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventList<T> : List<T>
    {
        #region 一些新的方法
        /// <summary>
        /// 将对象添加到 <see cref="T:System.Collections.Generic.List`1"/> 的结尾处。
        /// </summary>
        /// <param name="item">要添加到 <see cref="T:System.Collections.Generic.List`1"/> 的末尾处的对象。对于引用类型，该值可以为 null。</param>
        public new void Add(T item)
        {
            OnBeforeAdd(new EventListEventArgs<T>(1, item));
            base.Add(item);
            OnAfterAdd(new EventListEventArgs<T>(1, item));
        }

        /// <summary>
        /// 将指定集合的元素添加到 <see cref="T:System.Collections.Generic.List`1"/> 的末尾。
        /// </summary>
        /// <param name="collection">一个集合，其元素应被添加到 <see cref="T:System.Collections.Generic.List`1"/> 的末尾。集合自身不能为 null，但它可以包含为 null 的元素（如果类型 <paramref name="T"/> 为引用类型）。</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="collection"/> 为 null。</exception>
        public new void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        /// <summary>
        /// 从 <see cref="T:System.Collections.Generic.List`1"/> 中移除所有元素。
        /// </summary>
        public new void Clear()
        {
            OnBeforeClear(new EventListClearEventArgs());
            base.Clear();
            OnAfterClear(new EventListClearEventArgs());
        }

        /// <summary>
        /// 从 <see cref="T:System.Collections.Generic.List`1"/> 中移除特定对象的第一个匹配项。
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Remove(T item)
        {
            OnBeforeRemove(new EventListEventArgs<T>(1, item));
            base.Remove(item);
            OnAfterRemove(new EventListEventArgs<T>(1, item));
        }

        public new T this[int index]
        {
            get { return base[index]; }
            set
            {
                T oldvalue = base[index];
                base[index] = value;
                OnValueChanged(new EventListValueChangedEventArgs<T>(index, oldvalue, value));
            }
        }
        #endregion

        public event ListBeforeDelegate<T> BeforeAdd;
        protected virtual void OnBeforeAdd(EventListEventArgs<T> e)
        {
            if (BeforeAdd != null)
            {
                BeforeAdd(this, e);
            }
        }
        
        public event ListBeforeDelegate<T> BeforeRemove;
        protected virtual void OnBeforeRemove(EventListEventArgs<T> e)
        {
            if (BeforeRemove != null)
            {
                BeforeRemove(this, e);
            }
        }
        
        public event ListBeforeClearDelegate BeforeClear;
        protected virtual void OnBeforeClear(EventListClearEventArgs e)
        {
            if (BeforeClear != null)
            {
                BeforeClear(this, e);
            }
        }

        public event ListAfterDelegate<T> AfterAdd;
        protected virtual void OnAfterAdd(EventListEventArgs<T> e)
        {
            if (AfterAdd != null)
            {
                AfterAdd(this, e);
            }
        }

        public event ListAfterDelegate<T> AfterRemove;
        protected virtual void OnAfterRemove(EventListEventArgs<T> e)
        {
            if (AfterRemove != null)
            {
                AfterRemove(this, e);
            }
        }

        public event ListAfterClearDelegate AfterClear;
        protected virtual void OnAfterClear(EventListClearEventArgs e)
        {
            if (AfterClear != null)
            {
                AfterClear(this, e);
            }
        }

        public event ListChangedDelegate<T> ValueChanged;
        protected virtual void OnValueChanged(EventListValueChangedEventArgs<T> e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }
    }

    public delegate void ListAfterDelegate<T>(object sender, EventListEventArgs<T> e);
    public delegate void ListBeforeDelegate<T>(object sender, EventListEventArgs<T> e);
    public delegate void ListChangedDelegate<T>(object sender, EventListValueChangedEventArgs<T> e);
    public delegate void ListAfterClearDelegate(object sender, EventListClearEventArgs e);
    public delegate void ListBeforeClearDelegate(object sender, EventListClearEventArgs e);

    public class EventListEventArgs<T> : EventArgs
    {
        public EventListEventArgs(int index, T value)
        {
            this.Index = index;
            this.Item = value;
        }
        public int Index { get; private set; }
        public T Item { get; private set; }
    }
    public class EventListValueChangedEventArgs<T> : EventArgs
    {
        public EventListValueChangedEventArgs(int index, T oldvalue, T newvalue)
        {
            this.Index = index;
            this.NewValue = newvalue;
            this.OldValue = oldvalue;
        }
        public int Index { get; private set; }
        public T OldValue { get; private set; }
        public T NewValue { get; private set; }
    }
    public class EventListClearEventArgs
    {
        public EventListClearEventArgs()
        {

        }
    }

    public class EventListException : ApplicationException
    {
        public EventListException()
        {

        }

        public EventListException(string message)
            : base(message)
        {

        }

        public EventListException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
    
    #region old code
    /*{
        List<T> _InnerList;
        /// <summary>
        /// 获取是否没有ItemChanged事件。
        /// （true则没有ItemChanged事件，针对Item的赋值转换成Remove和Insert的调用）
        /// </summary>
        public bool NoItemChangedEvent { get; private set; }

        #region 构造函数
        public EList()
        {
            _InnerList = new List<T>();
        }
        public EList(IEnumerable<T> collection)
        {
            _InnerList = new List<T>(collection);
        }
        public EList(int capacity)
        {
            _InnerList = new List<T>(capacity);
        }
        public EList(bool noItemChangedEvent)
        {
            this.NoItemChangedEvent = noItemChangedEvent;
            _InnerList = new List<T>();
        }
        #endregion

        #region IList<T> 成员

        public int IndexOf(T item)
        {
            Debug.Assert(item != null);

            return IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            Debug.Assert(index >= 0);
            Debug.Assert(item != null);

            try
            {
                _InnerList.Insert(index, item);
            }
            finally
            {
                OnInserted(new EventArgs<T>(item));
            }
        }

        public void RemoveAt(int index)
        {
            Debug.Assert(index >= 0);

            T item = this[index];

            _InnerList.RemoveAt(index);

            OnRemoved(new EventArgs<T>(item));
        }

        public T this[int index]
        {
            get
            {
                Debug.Assert(index >= 0);

                return _InnerList[index];
            }
            set
            {
                Debug.Assert(index >= 0);

                ///先取出旧值
                T oldValue = _InnerList[index];

                ///比较旧值和新值，不相等则继续
                if (!object.Equals(oldValue, value))
                {
                    if (NoItemChangedEvent)
                    {
                        RemoveAt(index);
                        Insert(index, value);
                    }
                    else
                    {
                        ///赋新值
                        _InnerList[index] = value;

                        ///触发事件
                        OnItemChanged(new ChangedEventArgs<T>(oldValue, value));
                    }
                }
            }
        }

        #endregion

        #region ICollection<T> 成员

        public void Add(T item)
        {
            Debug.Assert(item != null);

            ///调用本身的Insert方法，以共享Inserted事件
            this.Insert(this.Count, item);
        }

        public void Clear()
        {
            while (_InnerList.Count > 0)
            {
                RemoveAt(0);
            }
        }

        public void ClearWithout(T withoutItem)
        {
            int i = 0;
            while (_InnerList.Count > i)
            {
                T item = _InnerList[i];
                if (object.Equals(item, withoutItem))
                {
                    i++;
                    continue;
                }
                RemoveAt(i);
            }
        }

        public bool Contains(T item)
        {
            Debug.Assert(item != null);
            return _InnerList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _InnerList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _InnerList.Count; }
        }

        bool ICollection<T>.IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            Debug.Assert(item != null);
            try
            {
                return _InnerList.Remove(item);
            }
            finally
            {
                OnRemoved(new EventArgs<T>(item));
            }
        }

        #endregion

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
        {
            return _InnerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_InnerList).GetEnumerator();
        }

        #endregion

        #region 添加一些事件

        public event EventHandler<ChangedEventArgs<T>> ItemChanged;
        protected virtual void OnItemChanged(ChangedEventArgs<T> e)
        {
            if (ItemChanged != null)
            {
                ItemChanged(this, e);
            }
        }

        public event EventHandler<EventArgs<T>> Removed;
        protected virtual void OnRemoved(EventArgs<T> e)
        {
            if (Removed != null)
            {
                Removed(this, e);
            }
        }

        public event EventHandler<EventArgs<T>> Inserted;
        protected virtual void OnInserted(EventArgs<T> e)
        {
            if (Inserted != null)
            {
                Inserted(this, e);
            }
        }

        #endregion

        #region 添加List所拥有的一些方法

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T t in collection)
            {
                Add(t);
            }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<T> AsReadOnly()
        {
            return _InnerList.AsReadOnly();
        }

        public int BinarySearch(T item)
        {
            return _InnerList.BinarySearch(item);
        }
        public int BinarySearch(T item, IComparer<T> comparer)
        {
            return _InnerList.BinarySearch(item, comparer);
        }
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return _InnerList.BinarySearch(index, count, item, comparer);
        }

        public void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            _InnerList.CopyTo(index, array, arrayIndex, count);
        }
        public bool Exists(Predicate<T> match)
        {
            return _InnerList.Exists(match);
        }

        public T Find(Predicate<T> match)
        {
            return _InnerList.Find(match);
        }
        public List<T> FindAll(Predicate<T> match)
        {
            return _InnerList.FindAll(match);
        }
        public int FindIndex(Predicate<T> match)
        {
            return _InnerList.FindIndex(match);
        }
        public int FindIndex(int startIndex, Predicate<T> match)
        {
            return _InnerList.FindIndex(startIndex, match);
        }
        public int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return _InnerList.FindIndex(startIndex, count, match);
        }
        public T FindLast(Predicate<T> match)
        {
            return _InnerList.FindLast(match);
        }
        public int FindLastIndex(Predicate<T> match)
        {
            return _InnerList.FindLastIndex(match);
        }
        public int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return _InnerList.FindLastIndex(startIndex, match);
        }
        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return _InnerList.FindLastIndex(startIndex, count, match);
        }
        public void ForEach(Action<T> action)
        {
            _InnerList.ForEach(action);
        }

        public List<T> GetRange(int index, int count)
        {
            return _InnerList.GetRange(index, count);
        }
        public int IndexOf(T item, int index)
        {
            return _InnerList.IndexOf(item, index);
        }
        public int IndexOf(T item, int index, int count)
        {
            return _InnerList.IndexOf(item, index, count);
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            int insertIndex = index;
            foreach (T t in collection)
            {
                _InnerList.Insert(insertIndex++, t);
            }
        }
        public int LastIndexOf(T item)
        {
            return _InnerList.LastIndexOf(item);
        }
        public int LastIndexOf(T item, int index)
        {
            return _InnerList.LastIndexOf(item, index);
        }
        public int LastIndexOf(T item, int index, int count)
        {
            return _InnerList.LastIndexOf(item, index, count);
        }

        public int RemoveAll(Predicate<T> match)
        {
            int index = 0;
            int count = 0;
            while (index < this.Count)
            {
                if (match(this[index]))
                {
                    RemoveAt(index);
                    count++;
                }
                else
                {
                    index++;
                }
            }
            return count;
        }

        public void RemoveRange(int index, int count)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveAt(index);
            }
        }
        public void Reverse()
        {
            T[] tempArrs = this.ToArray();

            this.Clear();
            for (int i = Count - 1; i >= 0; i--)
            {
                this.Add(tempArrs[i]);
            }
        }
        //by zhucai：暂不提供 in 2008年3月2日
        //public void Reverse(int index, int count)
        //{
        //    if (index + count > this.Count)
        //    {
        //        throw new ArgumentOutOfRangeException();
        //    }
        //    List<T> temp = new List<T>(ToArray());

        //    int lastReverseIndex = count + index - 1;
        //    for (int i = index; i <= lastReverseIndex; i++)
        //    {
        //        temp[i] = _innerList[lastReverseIndex - (i - index)];
        //    }

        //    _innerList = temp;

        //    OnReversed(EventArgs.Empty);
        //}
        public void TrimExcess()
        {
            _InnerList.TrimExcess();
        }

        public bool TrueForAll(Predicate<T> match)
        {
            return _InnerList.TrueForAll(match);
        }
        public T[] ToArray()
        {
            T[] arr = new T[Count];
            _InnerList.CopyTo(arr);
            return arr;
        }

        #endregion
    }*/
    #endregion
}
