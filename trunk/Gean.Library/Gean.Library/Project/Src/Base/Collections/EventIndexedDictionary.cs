using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Gean.Resources;

namespace Gean
{
    /// <summary>
    /// һ�����¼����Ҽ�����������Dictionary��������0��ʼ��
    /// Gean: 2009-06-07 23:31:48
    /// </summary>
    /// <typeparam name="TKey">type for the key</typeparam>
    /// <typeparam name="TValue">type for the value</typeparam>
    public class EventIndexedDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        /// <summary>
        /// ��ָ���ļ���ֵ��ӵ��ֵ��С�
        /// </summary>
        /// <param name="key">Ҫ��ӵ�Ԫ�صļ���</param>
        /// <param name="value">Ҫ��ӵ�Ԫ�ص�ֵ�������������ͣ���ֵ����Ϊ null��</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="key"/> Ϊ null��</exception>
        /// <exception cref="T:System.ArgumentException">
        /// 	<see cref="T:System.Collections.Generic.Dictionary`2"/> ���Ѵ��ھ�����ͬ����Ԫ�ء�</exception>
        public new void Add(TKey key, TValue value)
        {
            OnBeforeAdd(new EventIndexedDictionaryEventArgs<TKey, TValue>(key, value));
            base.Add(key, value);
            OnAfterAdd(new EventIndexedDictionaryEventArgs<TKey, TValue>(key, value));
        }

        /// <summary>
        /// �� <see cref="T:System.Collections.Generic.Dictionary`2"/> ���Ƴ���ָ���ļ���ֵ��
        /// </summary>
        /// <param name="key">Ҫ�Ƴ���Ԫ�صļ���</param>
        /// <returns>
        /// ����ɹ��ҵ����Ƴ���Ԫ�أ���Ϊ true������Ϊ false�� ����� <see cref="T:System.Collections.Generic.Dictionary`2"/> ��û���ҵ� <paramref name="key"/>���˷����򷵻� false��
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="key"/> Ϊ null��</exception>
        public new bool Remove(TKey key)
        {
            TValue value = default(TValue);
            if (!base.TryGetValue(key, out value))
            {
                return false;
            }
            OnBeforeAdd(new EventIndexedDictionaryEventArgs<TKey, TValue>(key, value));
            bool flag = base.Remove(key);
            OnAfterAdd(new EventIndexedDictionaryEventArgs<TKey, TValue>(key, value));
            return flag;
        }

        /// <summary>
        /// �� <see cref="T:System.Collections.Generic.Dictionary`2"/> ���Ƴ����еļ���ֵ��
        /// </summary>
        public new void Clear()
        {
            OnBeforeClear(new EventIndexedDictionaryEventArgs(base.Count));
            base.Clear();
            OnAfterClear(new EventIndexedDictionaryEventArgs(base.Count));
        }

        /// <summary>
        /// Gets or sets the <see cref="TValue"/> with the specified key.
        /// </summary>
        /// <value></value>
        public new TValue this[TKey key]
        {
            get { return base[key]; }
            set
            {
                if (!base.ContainsKey(key))
                {
                    TValue oldvalue = base[key];
                    base[key] = value;
                    OnAfterValueChanged(new EventIndexedDictionaryValueChangedEventArgs<TKey, TValue>(key, value, oldvalue));
                }
                else
                {
                    OnBeforeAdd(new EventIndexedDictionaryEventArgs<TKey, TValue>(key, value));
                    base[key] = value;
                    OnAfterAdd(new EventIndexedDictionaryEventArgs<TKey, TValue>(key, value));
                }
            }
        }

        /// <summary>
        /// ʹ����������ȡ������<see cref="TValue"/>.
        /// </summary>
        /// <value></value>
        public TValue this[int index]
        {
            get 
            {
                if (index >= base.Count || index < 0)
                {
                    throw new IndexOutOfRangeException
                    (string.Format("Dictionary.Count:{0}, Index of Expect:{1}", base.Count, index));
                }
                int i = 0;
                foreach (KeyValuePair<TKey, TValue> pair in this)
                {
                    if (i == index)
                    {
                        return pair.Value;
                    }
                    i++;
                }
                return default(TValue);
            }
            set
            {
                if (index >= base.Count || index < 0)
                {
                    throw new IndexOutOfRangeException
                    (string.Format("Dictionary.Count:{0}, Index of Expect:{1}", base.Count, index));
                }
                int i = 0;
                foreach (KeyValuePair<TKey, TValue> pair in this)
                {
                    if (i == index)
                    {
                        TValue oldvalue = pair.Value;
                        base[pair.Key] = value;
                        OnAfterValueChanged(new EventIndexedDictionaryValueChangedEventArgs<TKey, TValue>(pair.Key, value, oldvalue));
                        break;
                    }
                    i++;
                }
            }
        }

        public event EventIndexedDictionaryBeforeDelegate<TKey, TValue> BeforeAdd;
        protected virtual void OnBeforeAdd(EventIndexedDictionaryEventArgs<TKey, TValue> e)
        {
            if (BeforeAdd != null)
            {
                BeforeAdd(this, e);
            }
        }

        public event EventIndexedDictionaryAfterDelegate<TKey, TValue> AfterAdd;
        protected virtual void OnAfterAdd(EventIndexedDictionaryEventArgs<TKey, TValue> e)
        {
            if (AfterAdd != null)
            {
                AfterAdd(this, e);
            }
        }

        public event EventIndexedDictionaryBeforeDelegate<TKey, TValue> BeforeRemove;
        protected virtual void OnBeforeRemove(EventIndexedDictionaryEventArgs<TKey, TValue> e)
        {
            if (BeforeRemove != null)
            {
                BeforeRemove(this, e);
            }
        }

        public event EventIndexedDictionaryAfterDelegate<TKey, TValue> AfterRemove;
        protected virtual void OnAfterRemove(EventIndexedDictionaryEventArgs<TKey, TValue> e)
        {
            if (AfterRemove != null)
            {
                AfterRemove(this, e);
            }
        }

        public event EventIndexedDictionaryValueChangedDelegate<TKey, TValue> AfterValueChanged;
        protected virtual void OnAfterValueChanged(EventIndexedDictionaryValueChangedEventArgs<TKey, TValue> e)
        {
            if (AfterValueChanged != null)
            {
                AfterValueChanged(this, e);
            }
        }

        public event EventIndexedDictionaryBeforeClearDelegate BeforeClear;
        protected virtual void OnBeforeClear(EventIndexedDictionaryEventArgs e)
        {
            if (BeforeClear != null)
            {
                BeforeClear(this, e);
            }
        }

        public event EventIndexedDictionaryAfterClearDelegate AfterClear;
        protected virtual void OnAfterClear(EventIndexedDictionaryEventArgs e)
        {
            if (AfterClear != null)
            {
                AfterClear(this, e);
            }
        }

    }

    public delegate void EventIndexedDictionaryBeforeDelegate<TKey, TValue>(object sender, EventIndexedDictionaryEventArgs<TKey, TValue> e);
    public delegate void EventIndexedDictionaryAfterDelegate<TKey, TValue>(object sender, EventIndexedDictionaryEventArgs<TKey, TValue> e);
    public delegate void EventIndexedDictionaryValueChangedDelegate<TKey, TValue>(object sender, EventIndexedDictionaryValueChangedEventArgs<TKey, TValue> e);
    public delegate void EventIndexedDictionaryBeforeClearDelegate(object sender, EventIndexedDictionaryEventArgs e);
    public delegate void EventIndexedDictionaryAfterClearDelegate(object sender, EventIndexedDictionaryEventArgs e);

    public class EventIndexedDictionaryEventArgs<TKey, TValue> : EventArgs
    {
        public EventIndexedDictionaryEventArgs(TKey key, TValue value)
        {
            this.Value = value;
            this.Key = key;
        }

        public TValue Value { get; private set; }
        public TKey Key { get; private set; }
    }
    public class EventIndexedDictionaryValueChangedEventArgs<TKey, TValue> : EventArgs
    {
        public EventIndexedDictionaryValueChangedEventArgs(TKey key, TValue newValue, TValue oldValue)
        {
            this.NewValue = newValue;
            this.OldValue = oldValue;
            this.Key = key;
        }

        public TValue NewValue { get; private set; }
        public TValue OldValue { get; private set; }
        public TKey Key { get; private set; }
    }
    public class EventIndexedDictionaryEventArgs : EventArgs
    {
        public EventIndexedDictionaryEventArgs(int count)
        {
            this.Count = count;
        }

        public int Count { get; private set; }
    }

    public class EventIndexedDictionaryException : ApplicationException
    {
        public EventIndexedDictionaryException()
        {

        }

        public EventIndexedDictionaryException(string message)
            : base(message)
        {

        }

        public EventIndexedDictionaryException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }

}
/*
        protected List<TKey> _KeyList = new List<TKey>();
        protected bool _ReplaceDuplicateKeys = false; //can key be inserted more than once?
        //if so then it will if it contains the key on add

        protected bool _ThrowErrorOnInvalidRemove = false; //if true then throws an exception if the 
        //item you are trying to remove does not exist in the collection

        #region Contructors
        public EventIndexedDictionary()
        {
            ValidateKeyType();
        }

        public EventIndexedDictionary(bool bReplaceDuplicateKeys)
        {
            ValidateKeyType();
            _ReplaceDuplicateKeys = bReplaceDuplicateKeys;
        }

        public EventIndexedDictionary(bool bReplaceDuplicateKeys, bool bThrowErrorOnInvalidRemove)
            : this(bReplaceDuplicateKeys)
        {
            ValidateKeyType();
            _ThrowErrorOnInvalidRemove = bThrowErrorOnInvalidRemove;
        }

        /// <summary>
        /// Makes sure int is not used as dictionary key:
        /// </summary>
        private static void ValidateKeyType()
        {
            if (typeof(TKey) == typeof(int))
            {
                throw new EventIndexedDictionaryException("Key of type int is not supported.");
            }
        }


        #endregion

        public bool ReplaceDuplicateKeys
        {
            get { return _ReplaceDuplicateKeys; }
        }

        public bool ThrowErrorOnInvalidRemove
        {
            get { return _ThrowErrorOnInvalidRemove; }
        }
        //useful for changing the key in subclasses.
        protected virtual TKey TransformKey(TKey key)
        {
            return key;
        }

        public bool Contains(TKey key)
        {
            return base.ContainsKey(TransformKey(key));
        }

        /// <summary>
        /// ��ָ���ļ���ֵ��ӵ��ֵ��С�
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        public virtual new void Add(TKey key, TValue item)
        {
            AddAt(-1, key, item);
        }

        /// <summary>
        /// ��ָ���ļ���ֵ��ӵ��ֵ�ָ��������λ�á�
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        public virtual void AddAt(int index, TKey key, TValue item)
        {
            DictionaryBeforeEventArgs<TKey, TValue> e = new DictionaryBeforeEventArgs<TKey, TValue>(key, item);

            // Raise before events:
            bool bubble = true;
            if (BeforeAdd != null)
            {
                foreach (EventIndexedDictionaryBeforeDelegate<TKey, TValue> function in BeforeAdd.GetInvocationList())
                {
                    e.Bubble = true;
                    function.Invoke(this, e);
                    bubble = bubble && e.Bubble;
                }
            }
            if (!bubble) return;

            // Add item:
            // Use value returend by event:
            if (_ReplaceDuplicateKeys && ContainsKey(TransformKey(e.Key)))   //check if it contains and remove
                Remove(TransformKey(e.Key));

            base.Add(TransformKey(e.Key), e.Value);

            if (index != -1)
            {
                _KeyList.Insert(index, e.Key);
            }
            else
            {
                _KeyList.Add(e.Key);
            }

            // Raise after events:
            if (AfterAdd != null)
            {
                AfterAdd.Invoke(this, e);
            }
        }

        /// <summary>
        /// ���ֵ��ָ������λ���Ƴ���ָ���ļ���ֵ��
        /// </summary>
        /// <param name="index">The index.</param>
        public virtual void RemoveAt(int index)
        {
            if (_ThrowErrorOnInvalidRemove)
            {
                if (index < 0 || index >= _KeyList.Count)
                {
                    throw new EventIndexedDictionaryException("Cannot remove invalid Index");
                }
            }
            TKey key = _KeyList[index];
            Remove(TransformKey(key));
        }

        /// <summary>
        /// ���ֵ����Ƴ���ָ���ļ���ֵ��
        /// </summary>
        /// <param name="key">The key.</param>
        public new void Remove(TKey key)
        {
            bool bContains = ContainsKey(TransformKey(key));
            if (_ThrowErrorOnInvalidRemove && !bContains)
                throw new EventIndexedDictionaryException("Key does not exist within the Dictionary");
            else if (!bContains)
                return;

            // Raise before events:
            DictionaryBeforeEventArgs<TKey, TValue> e = new DictionaryBeforeEventArgs<TKey, TValue>
            (key, base[TransformKey(key)]);

            // Raise before events:
            bool bubble = true;
            if (BeforeRemove != null)
            {
                foreach (EventIndexedDictionaryBeforeDelegate<TKey, TValue> function in BeforeRemove.GetInvocationList())
                {
                    e.Bubble = true;
                    function.Invoke(this, e);
                    bubble = bubble && e.Bubble;
                }
            }
            if (!bubble) return;

            // Remove item:
            // Use value returend by event:
            _KeyList.Remove(e.Key);
            base.Remove(TransformKey(e.Key));

            // Raise after events:
            if (AfterRemove != null)
            {
                AfterRemove.Invoke(this, e);
            }
        }

        /// <summary>
        /// ȷ�� <see cref="T:System.Collections.Generic.Dictionary`2"/> �Ƿ����ָ���ļ���
        /// </summary>
        /// <param name="key">Ҫ�� <see cref="T:System.Collections.Generic.Dictionary`2"/> �ж�λ�ļ���</param>
        /// <returns>
        /// ��� <see cref="T:System.Collections.Generic.Dictionary`2"/> ��������ָ������Ԫ�أ���Ϊ true������Ϊ false��
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// 	<paramref name="key"/> Ϊ null��</exception>
        public new bool ContainsKey(TKey key)
        {
            return base.ContainsKey(TransformKey(key));
        }

        #region Indexers

        /// <summary>
        /// Gets or sets the <see cref="TValue"/> with the specified key.
        /// </summary>
        /// <value></value>
        public new TValue this[TKey key]
        {
            get
            {
                return base[TransformKey(key)];
            }
            set
            {
                if (!base.ContainsKey(TransformKey(key)))
                {
                    throw new EventIndexedDictionaryException("Values cannot be added directly. Use Add() or AddAt() method.");
                }
                base[TransformKey(key)] = value;
            }
        }

        public TValue this[int index]
        {
            get
            {
                return this[_KeyList[index]];
            }
            set
            {
                this[_KeyList[index]] = value;
            }
        }

        #endregion

        /// <summary>
        /// Clears all values and raises events.
        /// </summary>
        public new void Clear()
        {
            DictionaryBeforeClearEventArgs e = new DictionaryBeforeClearEventArgs();

            // Raise before events:
            bool bubble = true;
            if (BeforeClear != null)
            {
                foreach (EventIndexedDictionaryBeforeClearDelegate function in BeforeClear.GetInvocationList())
                {
                    e.Bubble = true;
                    function.Invoke(this, e);
                    bubble = bubble && e.Bubble;
                }
            }
            if (!bubble) return;

            // Clear items item:
            base.Clear();
            _KeyList.Clear();

            // Raise after events:
            if (AfterClear != null)
            {
                AfterClear.Invoke(this, new EventArgs());
            }
        }
*/