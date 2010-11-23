using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Gean.Options.Interfaces;

namespace Gean.Options.Common
{
    /// <summary>
    /// ѡ���
    /// </summary>
    public class OptionMap : IDictionary<String, IOption>
    {
        private Dictionary<string, IOption> _InnerMap = new Dictionary<string, IOption>();

        public void Add(string key, IOption value)
        {
            _InnerMap.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return _InnerMap.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return _InnerMap.Keys; }
        }

        public bool Remove(string key)
        {
            return _InnerMap.Remove(key);
        }

        public bool TryGetValue(string key, out IOption value)
        {
            return _InnerMap.TryGetValue(key, out value);
        }

        public ICollection<IOption> Values
        {
            get { return _InnerMap.Values; }
        }

        public IOption this[string key]
        {
            get
            {
                return _InnerMap[key];
            }
            set
            {
                _InnerMap[key] = value;
            }
        }

        public void Add(KeyValuePair<string, IOption> item)
        {
            _InnerMap.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _InnerMap.Clear();
        }

        public bool Contains(KeyValuePair<string, IOption> item)
        {
            return _InnerMap.ContainsKey(item.Key);
        }

        /// <summary>
        /// ��ָ����������ʼ������ǰ���͵�Ԫ�ظ��Ƶ�һ��Ŀ��System.Array�С�
        /// </summary>
        /// <param name="array">���Ƶ�Ԫ�ص�Ŀ��λ�õ�һά System.Array.</param>
        /// <param name="arrayIndex">array �д��㿪ʼ���������Ӵ˴���ʼ���ơ�</param>
        public void CopyTo(KeyValuePair<string, IOption>[] targetArray, int arrayIndex)
        {
            KeyValuePair<string, IOption>[] sourceArray = new KeyValuePair<string, IOption>[_InnerMap.Count];
            int i = 0;
            foreach (var pair in _InnerMap)
            {
                sourceArray[i] = pair;
                i++;
            }
            Array.Copy(sourceArray, targetArray, arrayIndex);
        }

        public int Count
        {
            get { return _InnerMap.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, IOption> item)
        {
            return _InnerMap.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<string, IOption>> GetEnumerator()
        {
            return _InnerMap.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _InnerMap.GetEnumerator();
        }
    }
}