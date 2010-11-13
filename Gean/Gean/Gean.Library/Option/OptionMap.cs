using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Options
{
    /// <summary>
    /// 选项集合
    /// </summary>
    public class OptionMap : IDictionary<String, IOption>
    {

        public void Add(string key, IOption value)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public ICollection<string> Keys
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out IOption value)
        {
            throw new NotImplementedException();
        }

        public ICollection<IOption> Values
        {
            get { throw new NotImplementedException(); }
        }

        public IOption this[string key]
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

        public void Add(KeyValuePair<string, IOption> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, IOption> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, IOption>[] array, int arrayIndex)
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

        public bool Remove(KeyValuePair<string, IOption> item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, IOption>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}